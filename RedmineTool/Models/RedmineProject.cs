using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineTool.Models
{
    public class RedmineProject
    {
        private Project m_curProjectInfo = null;
        public Project ProjectInfo
        {
            get
            {
                return m_curProjectInfo;
            }
        }

        public string DisplayName
        {
            get;
            private set;
        }

        public int ProjectId
        {
            get
            {
                return m_curProjectInfo.Id;
            }
        }

        public List<int> ProjectPathById
        {
            get;
            private set;
        }

        public string ProjectPathByString
        {
            get;
            private set;
        }

        public int Index 
        { 
            get; 
            internal set; 
        }

        public RedmineProject(Project redmineProject, List<Project> aryAllProjects)
        {
            Stack<int> stackProjectIds = new Stack<int>();
            ProjectPathById = new List<int>();


            m_curProjectInfo = redmineProject;


            string sProjectPath = string.Empty;
            Project foundParentProject 
                = aryAllProjects.Find(m => m_curProjectInfo.Parent != null && m.Id == m_curProjectInfo.Parent.Id);

            while(foundParentProject != null)
            {
                stackProjectIds.Push(foundParentProject.Id);
                if (foundParentProject != null)
                {
                    if (string.IsNullOrEmpty(sProjectPath))
                        sProjectPath = foundParentProject.Name + " / ";
                    else
                        sProjectPath = foundParentProject.Name + " / " + sProjectPath ;
                } 
                foundParentProject 
                    = aryAllProjects.Find(m => foundParentProject.Parent != null && m.Id == foundParentProject.Parent.Id);
            }

            while(stackProjectIds.Count > 0)
            {
                ProjectPathById.Add(stackProjectIds.Pop());
            }


            
            DisplayName = redmineProject.Name;
            ProjectPathByString = sProjectPath;
        }




        public override string ToString()
        {
            return DisplayName;
        }
    }
}
