using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool.Models
{
    public class RedmineIssue
    {
        protected Issue m_curIssueInfo = null;
        protected List<int> m_aryChildIssuesId = null;

        public Issue IssueInfo
        {
            get
            {
                return m_curIssueInfo;
            }
        }

        public string DisplayName
        {
            get
            {
                if (IsClosed)
                    return m_curIssueInfo.Subject + "*";
                return m_curIssueInfo.Subject;
            }
        }

        public int IssueId
        {
            get
            {
                return m_curIssueInfo.Id;
            }
        }

        public string Description
        {
            get
            {
                return m_curIssueInfo.Description;
            }
        }

        public int AssigneeId
        {
            get
            {
                return m_curIssueInfo.AssignedTo.Id;
            }
        }

        public string AssignedName
        {
            get
            {
                string sName = (m_curIssueInfo.AssignedTo != null)
                    ? m_curIssueInfo.AssignedTo.Name : string.Empty;
                return sName;
            }
        }

        public string AssignedLoginId
        {
            get
            {
                string sLoginId = RedmineConnector.Current.GetUserLoginId(AssigneeId);
                return sLoginId;
            }
        }

        public string StatusName
        {
            get
            {
                return m_curIssueInfo.Status.Name;
            }
        }

        public int StatusId
        {
            get
            {
                return m_curIssueInfo.Status.Id;
            }
        }

        public double EstimatedHours
        {
            get
            {
                double estimatedHour = 0;
                if (m_curIssueInfo.EstimatedHours != null)
                    estimatedHour = Convert.ToDouble(m_curIssueInfo.EstimatedHours);
                return estimatedHour;
            }
        }

        public int EstimatedHoursToDate
        {
            get
            {
                return (int)(EstimatedHours / 24);
            }
        }

        public int EstimatedHoursToHour
        {
            get
            {
                return (int)(EstimatedHours % 24);
            }
        }

        public List<IssueAllowedStatus> AllowdStatuses
        {
            get;
        }

        public double DoneRatio
        {
            get
            {
                double result = 0;
                if (m_curIssueInfo.DoneRatio != null)
                    result = Convert.ToDouble(m_curIssueInfo.DoneRatio);
                return result;
            }
        }

        public DateTime StartDate
        {
            get
            {
                DateTime result = DateTime.MinValue;
                if (m_curIssueInfo.StartDate != null)
                    result = Convert.ToDateTime(m_curIssueInfo.StartDate);
                return result;
            }
        }

        public DateTime? DueDate
        {
            get
            {
                DateTime result = DateTime.MinValue;
                if (m_curIssueInfo.DueDate != null)
                    result = Convert.ToDateTime(m_curIssueInfo.DueDate);
                return result;
            }
        }

        public DateTime LastedUpdated
        {
            get
            {
                DateTime result = DateTime.MinValue;
                if (m_curIssueInfo.UpdatedOn == null)
                    result = Convert.ToDateTime(m_curIssueInfo.CreatedOn);
                else
                    result = Convert.ToDateTime(m_curIssueInfo.UpdatedOn);
                return result;
            }
        }

        //
        // Summary:
        //     Gets or sets the parent issue id. Only when a new issue is created this property
        //     shall be used.
        //
        // Value:
        //     The parent issue id.
        public int ParentIssueId 
        { 
            get
            {
                if (m_curIssueInfo.ParentIssue == null)
                    return -1;
                return m_curIssueInfo.ParentIssue.Id;
            }
        }

        public bool IsClosed
        {
            get;
            private set;
        }

        //
        // Summary:
        //     Gets or sets the issue children.
        //
        // Value:
        //     The issue children. NOTE: Only Id, tracker and subject are filled.
        public List<int> ChildrenId 
        { 
            get;
            set; 
        }


        public RedmineIssue(Issue redmineIssue)
        {
            m_curIssueInfo = redmineIssue;
            AllowdStatuses = new List<IssueAllowedStatus>();
            if(m_curIssueInfo.AllowedStatuses != null)
                AllowdStatuses.AddRange(m_curIssueInfo.AllowedStatuses);

            IssueStatus foundStatus = RedmineConnector.Current.AllStatus.Find(m => m.Id == this.StatusId);
            if (foundStatus != null)
                IsClosed = foundStatus.IsClosed;


            m_aryChildIssuesId = new List<int>();
            if(m_curIssueInfo.Children != null)
            {
                foreach(IssueChild childIssue in m_curIssueInfo.Children)
                {
                    m_aryChildIssuesId.Add(childIssue.Id);
                }
            }

        }

        public override string ToString()
        {
            return DisplayName;
        }



        //
        // Summary:
        //     Gets or sets a value indicating whether [private notes].
        //
        // Value:
        //     true if [private notes]; otherwise, false.
        public bool PrivateNotes { get; set; }
        //
        // Summary:
        //     Gets or sets the custom fields.
        //
        // Value:
        //     The custom fields.
        public IList<IssueCustomField> CustomFields { get; set; }
        //
        // Summary:
        //     Gets or sets the created on.
        //
        // Value:
        //     The created on.
        public DateTime? CreatedOn { get; set; }
        //
        // Summary:
        //     Gets or sets the updated on.
        //
        // Value:
        //     The updated on.
        public DateTime? UpdatedOn { get; }
        //
        // Summary:
        //     Gets or sets the closed on.
        //
        // Value:
        //     The closed on.
        public DateTime? ClosedOn { get; }
        //
        // Summary:
        //     Gets or sets the notes.
        public string Notes { get; set; }
        //
        // Summary:
        //     Gets or sets the ID of the user to assign the issue to (currently no mechanism
        //     to assign by name).
        //
        // Value:
        //     The assigned to.
        public IdentifiableName AssignedTo { get; set; }
        
        //
        // Summary:
        //     Gets or sets the fixed version.
        //
        // Value:
        //     The fixed version.
        public IdentifiableName FixedVersion { get; set; }
        //
        // Summary:
        //     indicate whether the issue is private or not
        //
        // Value:
        //     true if this issue is private; otherwise, false.
        public bool IsPrivate { get; set; }
        //
        // Summary:
        //     Returns the sum of spent hours of the task and all the sub tasks.
        //
        // Remarks:
        //     Availability starting with redmine version 3.3
        public float? TotalSpentHours { get; set; }
        //
        // Summary:
        //     Returns the sum of estimated hours of task and all the sub tasks.
        //
        // Remarks:
        //     Availability starting with redmine version 3.3
        public float? TotalEstimatedHours { get; set; }
        //
        // Summary:
        //     Gets or sets the journals.
        //
        // Value:
        //     The journals.
        public IList<Journal> Journals { get; set; }
        //
        // Summary:
        //     Gets or sets the change sets.
        //
        // Value:
        //     The change sets.
        public IList<ChangeSet> ChangeSets { get; set; }
        //
        // Summary:
        //     Gets or sets the attachments.
        //
        // Value:
        //     The attachments.
        public IList<Attachment> Attachments { get; set; }
        //
        // Summary:
        //     Gets or sets the issue relations.
        //
        // Value:
        //     The issue relations.
        public IList<IssueRelation> Relations { get; set; }
       
        //
        // Summary:
        //     Gets or sets the attachments.
        //
        // Value:
        //     The attachment.
        public IList<Upload> Uploads { get; set; }
        //
        // Summary:
        //     Gets or sets the hours spent on the issue.
        //
        // Value:
        //     The hours spent on the issue.
        public float? SpentHours { get; set; }
        
     

        //
        // Summary:
        //     Gets or sets the project.
        //
        // Value:
        //     The project.
        public IdentifiableName Project { get; set; }

        public IList<Watcher> Watchers { get; set; }

        //
        // Summary:
        //     Gets or sets the status.Possible values: open, closed, * to get open and closed
        //     issues, status id
        //
        // Value:
        //     The status.
        public IdentifiableName Status { get; set; }

        //
        // Summary:
        //     Gets or sets the priority.
        //
        // Value:
        //     The priority.
        public IdentifiableName Priority { get; set; }

        //
        // Summary:
        //     Gets or sets the author.
        //
        // Value:
        //     The author.
        public IdentifiableName Author { get; set; }

        //
        // Summary:
        //     Gets or sets the tracker.
        //
        // Value:
        //     The tracker.
        public IdentifiableName Tracker { get; set; }

        
        //
        // Summary:
        //     Gets or sets the category.
        //
        // Value:
        //     The category.
        public IdentifiableName Category { get; set; }
    }
}
