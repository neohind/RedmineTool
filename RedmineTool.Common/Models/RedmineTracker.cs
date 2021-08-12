using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool.Common.Models
{
    public class RedmineTracker
    {
        private Tracker m_curTracker = null;


        public Tracker TrackerInfo
        {
            get
            {
                return m_curTracker;
            }
        }

        public string DisplayName
        {
            get;
            private set;
        }

        public int TrackerId
        {
            get
            {
                return m_curTracker.Id;
            }
        }

        public RedmineTracker(Tracker redmineTracker)
        {
            m_curTracker = redmineTracker;

            DisplayName = redmineTracker.Name;
            
        }




        public override string ToString()
        {
            return DisplayName;
        }
    }
}
