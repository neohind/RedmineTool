using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool.Common.Models
{
    public class RedmineUser 
    {
        private User m_curUserInfo = null;
        public User UserInfo
        {
            get
            {
                return m_curUserInfo;
            }
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string LoginName
        {
            get
            {
                return m_curUserInfo.Login;
            }
        }

        public RedmineUser(User redmineUser)
        {
            m_curUserInfo = redmineUser;

            DisplayName = string.Format("{1}{0}({2})", redmineUser.FirstName, redmineUser.LastName, redmineUser.Login);
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
