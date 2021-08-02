using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using RedmineTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool
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

            m_aryIssueStatus = m_manager.GetObjects<IssueStatus>();

            return sResult;
        }

        internal int GetTrackerIndex(int nSelectedTrackerId)
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


        internal string GetUserLoginId(int assigneeId)
        {
            RedmineUser foundUser = m_aryAllUsers.Find(m => m.UserInfo.Id == assigneeId);
            if (foundUser != null)
                return foundUser.LoginName;
            return string.Empty;
        }

        internal List<RedmineIssue> GetCurrentIssues(int nProjectId, string sStatus)
        {
            List<RedmineIssue> aryResult = new List<RedmineIssue>();

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("status_id", sStatus);

            if(nProjectId > -1)
                parameters.Add("project_id", nProjectId.ToString());


            List<Issue> issues = m_manager.GetObjects<Issue>(parameters);

            if (issues != null)
            {
                foreach (Issue issue in issues)
                {
                    RedmineIssue curIssue = new RedmineIssue(issue);
                    aryResult.Add(curIssue);
                }
            }

            return aryResult;
        }

        internal List<RedmineIssueSimple> GetCurrentSimpleIssues(int nProjectId, string sStatus)
        {
            List<RedmineIssueSimple> aryResult = new List<RedmineIssueSimple>();

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("status_id", sStatus);

            if (nProjectId > -1)
                parameters.Add("project_id", nProjectId.ToString());


            List<Issue> issues = m_manager.GetObjects<Issue>(parameters);

            if (issues != null)
            {
                foreach (Issue issue in issues)
                {
                    RedmineIssueSimple curIssue = new RedmineIssueSimple(issue);
                    aryResult.Add(curIssue);
                }
            }

            return aryResult;
        }

        internal void CreateIssue(Issue issue)
        {
            m_manager.CreateObject<Issue>(issue);
        }
    }
}
