using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using RedmineTool.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool.Common
{
    /// <summary>
    /// https://github.com/zapadi/redmine-net-api
    /// 
    /// </summary>
    public class RedmineConnector
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private List<RedmineUser> m_aryAllUsers = null;
        private List<RedmineProject> m_aryAllProjects = null;
        private List<RedmineTracker> m_aryAllTrackers = null;
        private List<IssueStatus> m_aryIssueStatus = null;
        private List<RedmineIssue> m_aryAllIssues = null;        

        private RedmineManager m_manager = null;        

        public static RedmineConnector Current
        {
            get;
            private set;
        }

        public List<RedmineUser> AllUsers
        {
            get { return m_aryAllUsers; }
        }

        public List<RedmineProject> AllProjects
        {
            get { return m_aryAllProjects; }
        }

        public List<RedmineTracker> AllTrackers
        {
            get { return m_aryAllTrackers; }
        }

        public List<IssueStatus> AllStatus
        {
            get { return m_aryIssueStatus; }
        }

        public List<RedmineIssue> AllIssues 
        { 
            get { return m_aryAllIssues; } 
        }

        static RedmineConnector()
        {
            if (Current == null)
                Current = new RedmineConnector();
        }

        private RedmineConnector()
        {
            m_aryAllProjects = new List<RedmineProject>();
            m_aryAllTrackers = new List<RedmineTracker>();
            m_aryAllUsers = new List<RedmineUser>();
            m_aryIssueStatus = new List<IssueStatus>();
            m_aryAllIssues = new List<RedmineIssue>();
        }

        

        public string Init(string sUrl, string sApiKey)
        {
            string sResult = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(sUrl))
                    throw new System.Net.WebException("EmptyURL");
                sUrl = sUrl.Trim();

                if (string.IsNullOrEmpty(sApiKey))
                    throw new System.Net.WebException("EmptyAPIKey");
                
                sApiKey = sApiKey.Trim();

                ConfigManager.Current.RedmineUrl = sUrl;
                ConfigManager.Current.ApiKey = sApiKey;

                
                m_manager = new RedmineManager(sUrl, sApiKey);
                User user = m_manager.GetCurrentUser();
                ConfigManager.Current.CurrentLoginUser = user.Login;
                ConfigManager.Current.CurrentLoginUserId = user.Id;
            }
            catch (System.Net.WebException ex)
            {
                log.Error(ex);
                sResult = ex.Message;
                if (ex.InnerException != null)
                    sResult = ex.InnerException.Message;
                return sResult;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                sResult = ex.Message;
                if (ex.InnerException != null)
                    sResult = ex.InnerException.Message;
                return sResult;
            }

            m_aryAllUsers.Clear();
            List<User> aryUsers = m_manager.GetObjects<User>();

            foreach (User user in aryUsers)
            {
                RedmineUser redmineUser = new RedmineUser(user);
                m_aryAllUsers.Add(redmineUser);
            }
            m_aryAllUsers.Sort((m, n) => string.Compare(m.DisplayName, n.DisplayName));
            
            List<Project> aryProjects = m_manager.GetObjects<Project>();
            foreach (Project project in aryProjects)
            {
                RedmineProject redmineProject = new RedmineProject(project, aryProjects);
                redmineProject.Index = m_aryAllProjects.Count;
                m_aryAllProjects.Add(redmineProject);
            }
                        
            List<Tracker> aryTrackers = m_manager.GetObjects<Tracker>();
            foreach (Tracker tracker in aryTrackers)
            {
                RedmineTracker redmineTracker = new RedmineTracker(tracker);
                m_aryAllTrackers.Add(redmineTracker);
            }

            string sAssinee = ConfigManager.Current.DefaultNewIssue_Assignee;
            if (string.IsNullOrEmpty(sAssinee))
            {
                User curUser = m_manager.GetCurrentUser();
                if (curUser != null)
                    sAssinee = curUser.Login;
            }
            try
            {
                m_aryIssueStatus = m_manager.GetObjects<IssueStatus>();
            }catch(Exception ex)
            {
                log.Error(ex);
            }

            RefreshIssues();
            

            return sResult;
        }

        public int GetTrackerIndex(int nSelectedTrackerId)
        {
            int nIndex = m_aryAllTrackers.FindIndex(m => m.TrackerId == nSelectedTrackerId);
            return (nIndex == -1 && m_aryAllTrackers.Count > 0) ? 0 : nIndex;
        }

        public int GetUserIndex(string sAssinee)
        {
            int nIndex = m_aryAllUsers.FindIndex(m => string.Equals(m.LoginName, sAssinee));
            return (nIndex == -1 && m_aryAllUsers.Count > 0) ? 0 : nIndex;
        }

        public RedmineProject GetProjectById(int nSelectedProjectId)
        {
            RedmineProject project = m_aryAllProjects.Find(m => m.ProjectId == nSelectedProjectId);
            return project;
        }

        public string GetUserLoginId(int assigneeId)
        {
            RedmineUser foundUser = m_aryAllUsers.Find(m => m.UserInfo.Id == assigneeId);
            if (foundUser != null)
                return foundUser.LoginName;
            return string.Empty;
        }

        public List<RedmineIssue> RefreshIssues()
        {
            m_aryAllIssues.Clear();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("status_id", "*");

            
            try
            {
                List<Issue> issues = m_manager.GetObjects<Issue>(parameters);

                if (issues != null)
                {
                    foreach (Issue issue in issues)
                    {
                        RedmineIssue curIssue = new RedmineIssue(issue);
                        m_aryAllIssues.Add(curIssue);
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
            return m_aryAllIssues;
        }

        public List<RedmineIssueSimple> GetCurrentSimpleIssues(int nProjectId, string sStatus)
        {
            List<RedmineIssueSimple> aryResult = new List<RedmineIssueSimple>();

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("status_id", sStatus);

            if (nProjectId > -1)
                parameters.Add("project_id", nProjectId.ToString());

            try
            {
                List<Issue> issues = m_manager.GetObjects<Issue>(parameters);

                if (issues != null)
                {
                    foreach (Issue issue in issues)
                    {
                        RedmineIssueSimple curIssue = new RedmineIssueSimple(issue);
                        aryResult.Add(curIssue);
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }

            return aryResult;
        }

        public Issue CreateIssue(Issue issue)
        {
            Issue newIssue = m_manager.CreateObject<Issue>(issue);
            return newIssue;
        }

        public void RebuildDueDate()
        {
            // utf8=%E2%9C%93&set_filter=1&sort=id%3Adesc&f%5B%5D=status_id&op%5Bstatus_id%5D=o&f%5B%5D=author_id
            // &op%5Bauthor_id%5D=%3D&v%5Bauthor_id%5D%5B%5D=5&f%5B%5D=estimated_hours
            // &op%5Bestimated_hours%5D=*&f%5B%5D=&c%5B%5D=tracker&c%5B%5D=status&c%5B%5D=priority
            // &c%5B%5D=subject&c%5B%5D=start_date&c%5B%5D=due_date&c%5B%5D=assigned_to&group_by=&t%5B%5D=

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("author_id", Convert.ToString(ConfigManager.Current.CurrentLoginUserId));
            parameters.Add("estimated_hours", "*");
            parameters.Add("start_date", "*");

            List<Issue> aryAllIssues = m_manager.GetObjects<Issue>(parameters);

            if (aryAllIssues != null)
            {
                foreach (Issue issue in aryAllIssues)
                {
                    log.Debug($"issue : {issue.Subject}, {issue.EstimatedHours}");
                    if (issue.StartDate != null)
                    {
                        DateTime dtStart = Convert.ToDateTime(issue.StartDate);
                        double fEstimatedHour = Convert.ToDouble(issue.EstimatedHours);
                        int nDays = Convert.ToInt32(fEstimatedHour / 8) - 1;

                        DateTime dtEnd = dtStart;
                        for (int i = 0; i < nDays; i++)
                        {
                            dtEnd = dtEnd.AddDays(1);
                            if (dtEnd.DayOfWeek == DayOfWeek.Saturday
                                || dtEnd.DayOfWeek == DayOfWeek.Sunday)
                                i--;
                        }

                        if (Convert.ToDateTime(issue.DueDate) != dtEnd)
                        {
                            issue.DueDate = dtEnd;

                            try
                            {
                                m_manager.UpdateObject<Issue>(Convert.ToString(issue.Id), issue);
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}
