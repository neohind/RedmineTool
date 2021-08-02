using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool.Models
{
    public class RedmineIssueSimple : RedmineIssue
    {
        public RedmineIssueSimple(Issue redmineIssue) : base(redmineIssue)
        {
        }

        public override string ToString()
        {
            string sResult = string.Empty;
            if(string.IsNullOrEmpty(AssignedName))
                sResult = string.Format("[{0,6}] {1} ", IssueId, DisplayName);
            else
                sResult = string.Format("[{0,6}] {1} <{2}>", IssueId, DisplayName, AssignedName);
            return sResult;
        }
    }
}
