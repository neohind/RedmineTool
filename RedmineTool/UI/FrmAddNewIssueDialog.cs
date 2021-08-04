using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using RedmineTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedmineTool.UI
{
    public partial class FrmAddNewIssueDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        
        private RedmineProject m_selectedProject = null;

        public FrmAddNewIssueDialog()
        {
            InitializeComponent();

            txtDesciption.Font = new Font(ConfigManager.Current.Fonts.Families[0], 10, FontStyle.Regular);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmbAssignee.DataSource = RedmineConnector.Current.AllUsers;
            string sAssignee = ConfigManager.Current.DefaultNewIssue_Assignee;            
            if (string.IsNullOrEmpty(sAssignee) == false)
            {
                int nIndex = RedmineConnector.Current.GetUserIndex(sAssignee);
                cmbAssignee.SelectedIndex = nIndex;
            }


            List<string> arySelectedWatchers = new List<string>();
            arySelectedWatchers.AddRange(ConfigManager.Current.DefaultNewIssue_Watchers);
            foreach (RedmineUser user in RedmineConnector.Current.AllUsers)
            {
                bool bChecked = (arySelectedWatchers.Contains(user.LoginName));
                chkListWatchers.Items.Add(user, bChecked);
            }


            cmbProjects.DataSource = RedmineConnector.Current.AllProjects;
            int nSelectedProjectId = ConfigManager.Current.SelectedProject;
            if (nSelectedProjectId > -1)
            {
                m_selectedProject = RedmineConnector.Current.GetProjectById(nSelectedProjectId);
                if (m_selectedProject != null)
                {
                    cmbProjects.SelectedIndex = m_selectedProject.Index;
                    lblProjectPath.Text = m_selectedProject.ProjectPathByString;
                }
            }



            cmbTracker.DataSource = RedmineConnector.Current.AllTrackers;
            int nSelectedTracker = ConfigManager.Current.DefaultNewIssue_Tracker;
            if(nSelectedTracker > -1)
            {
                int nIndex = RedmineConnector.Current.GetTrackerIndex(nSelectedTracker);
                cmbTracker.SelectedIndex = nIndex;
            }

            string sAssinee = ConfigManager.Current.DefaultNewIssue_Assignee;
            if (string.IsNullOrEmpty(sAssinee))
            {
                sAssinee = ConfigManager.Current.CurrentLoginUser;
            }
            int nCurUserIndex = RedmineConnector.Current.GetUserIndex(sAssinee);
            if (nCurUserIndex > -1)
                cmbAssignee.SelectedIndex = nCurUserIndex;

            chkOpenWebPageOpen.Checked = ConfigManager.Current.DefaultNewIssue_IsOpenTracker;

            // 각 컨트롤에 Redmine 관련된 데이터를 처리하고, 동시에 이전에 선택된 내용들을
            // 선택한 뒤, 아래와 같은 이벤트들을 연결해야 한다. 그렇지 않으면, 
            // 이전에 선택된 값을 컨트롤 선택값으로 만드는 순간, 데이터를 저장하는 작업을 수행하게 된다.           
            this.cmbProjects.SelectedIndexChanged += new System.EventHandler(this.cmbProjects_SelectedIndexChanged);
            this.cmbAssignee.SelectedIndexChanged += new System.EventHandler(this.cmbAssignee_SelectedIndexChanged);
            this.cmbTracker.SelectedIndexChanged += new System.EventHandler(this.cmbTracker_SelectedIndexChanged);
            this.chkListWatchers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListWatchers_ItemCheck);
            this.chkOpenWebPageOpen.CheckedChanged += new System.EventHandler(this.chkOpenWebPageOpen_CheckedChanged);
        }

        private void cmbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblProjectPath.Text = "-";
            int nSelectedIndex = cmbProjects.SelectedIndex;
            if (nSelectedIndex == -1)
                return;

            m_selectedProject = cmbProjects.Items[nSelectedIndex] as RedmineProject;
            lblProjectPath.Text = m_selectedProject.ProjectPathByString;
            ConfigManager.Current.SelectedProject = m_selectedProject.ProjectId;
        }

        private void cmbAssignee_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = cmbAssignee.SelectedIndex;
            if (nSelectedIndex == -1)
                return;

            RedmineUser selectedAssignee = cmbAssignee.Items[nSelectedIndex] as RedmineUser;
            ConfigManager.Current.DefaultNewIssue_Assignee = selectedAssignee.LoginName;
        }

        private void cmbTracker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = cmbTracker.SelectedIndex;
            if (nSelectedIndex == -1)
                return;

            RedmineTracker redmineTracker = cmbTracker.Items[nSelectedIndex] as RedmineTracker;
            ConfigManager.Current.DefaultNewIssue_Tracker = redmineTracker.TrackerId;
        }

        private void chkOpenWebPageOpen_CheckedChanged(object sender, EventArgs e)
        {
            ConfigManager.Current.DefaultNewIssue_IsOpenTracker = chkOpenWebPageOpen.Checked;
        }
        private void chkListWatchers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            RedmineUser curUser = null;
            List<string> arySelectedWatchers = new List<string>();
            foreach(var item in chkListWatchers.CheckedItems)
            {
                curUser = item as RedmineUser;
                if(curUser != null)
                {
                    arySelectedWatchers.Add(curUser.LoginName);
                }
            }

            if (e.Index > -1)
            {
                curUser = chkListWatchers.Items[e.Index] as RedmineUser;
                if (e.NewValue == CheckState.Checked)
                    arySelectedWatchers.Add(curUser.LoginName);
                else
                    arySelectedWatchers.Remove(curUser.LoginName);
            }
            ConfigManager.Current.DefaultNewIssue_Watchers = arySelectedWatchers.ToArray();
        }

        /// <summary>
        /// 프로젝트를 직접 찾을 때 사용하는 Browser 버튼에 대한 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            int nProjectIndex = cmbProjects.SelectedIndex;

            if (nProjectIndex == -1)
                return;

            RedmineProject curProject = cmbProjects.Items[nProjectIndex] as RedmineProject;
            FrmIssueListDialog dlg = new FrmIssueListDialog();
            dlg.SetupViewer(curProject.ProjectId, "open");

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                this.txtUpperIssueId.TextChanged -= new System.EventHandler(this.txtUpperIssueId_TextChanged);
                RedmineIssueSimple selectedIssue = dlg.SelectedIssue;
                lblUpperIssueName.Text = selectedIssue.DisplayName;
                txtUpperIssueId.Text = selectedIssue.IssueId.ToString();
                this.txtUpperIssueId.TextChanged += new System.EventHandler(this.txtUpperIssueId_TextChanged);
            }
        }

        

        private void txtUpperIssueId_TextChanged(object sender, EventArgs e)
        {
            lblUpperIssueName.Text = string.Empty;
            if (txtUpperIssueId.Tag != null)
            {
                List<RedmineIssueSimple> aryAllIssues = txtUpperIssueId.Tag as List<RedmineIssueSimple>;
                try
                {
                    int nSelectedIssuesId = Convert.ToInt32(txtUpperIssueId.Text);
                    RedmineIssueSimple foundIssue = aryAllIssues.Find(m => m.IssueId == nSelectedIssuesId);
                    if(foundIssue != null)
                        lblUpperIssueName.Text = foundIssue.DisplayName;                    
                }
                catch(FormatException)
                {
                    log.Error("Value is invalid! - " + txtUpperIssueId.Text);
                }
                catch(Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        private void txtUpperIssueId_Enter(object sender, EventArgs e)
        {
            if (m_selectedProject != null)
            {
                List<RedmineIssueSimple> aryAllIssues
                    = RedmineConnector.Current.GetCurrentSimpleIssues(m_selectedProject.ProjectId, "*");
                txtUpperIssueId.Tag = aryAllIssues;
                var source = new AutoCompleteStringCollection();
                foreach (RedmineIssueSimple issue in aryAllIssues)
                {
                    source.Add(issue.IssueId.ToString());
                }
                txtUpperIssueId.AutoCompleteCustomSource = source;
            }
            else
            {
                txtUpperIssueId.AutoCompleteCustomSource = null;
                txtUpperIssueId.Tag = null;
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string sSubject = txtSubject.Text;
            int nProjectId = m_selectedProject.ProjectId;
            int nEstimatedHours = 0;

            Issue newIssue = new Issue();
            newIssue.Subject = sSubject;
            newIssue.Project = IdentifiableName.Create<IdentifiableName>(nProjectId);
            newIssue.Description = txtDesciption.Text;

            if (string.IsNullOrEmpty(txtEstimatedDate.Text) == false)
            {
                try
                {
                    nEstimatedHours = Convert.ToInt32(txtEstimatedDate.Text) * 8;
                    nEstimatedHours += Convert.ToInt32(txtEstimatedHour.Text);
                    newIssue.EstimatedHours = nEstimatedHours;
                }
                catch(Exception ex)
                {
                    txtEstimatedDate.Focus();
                    log.Error("Upper Issue id error!");
                    log.Error(ex);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtUpperIssueId.Text) == false)
            {
                if (string.IsNullOrEmpty(lblUpperIssueName.Text))
                {
                    txtUpperIssueId.Focus();
                    MessageBox.Show("Invalided Upper Issue Id!!!");
                    return;
                }

                try
                {
                    int nUpperIssueId = Convert.ToInt32(txtUpperIssueId.Text);
                    newIssue.ParentIssue = IdentifiableName.Create<IdentifiableName>(nUpperIssueId);
                }
                catch(Exception ex)
                {
                    txtUpperIssueId.Focus();
                    log.Error("Upper Issue id error!");
                    log.Error(ex);
                    return;
                }
            }

            if (chkListWatchers.CheckedItems.Count > 0)
            {
                List<Watcher> aryWatcher = new List<Watcher>();
                foreach (var item in chkListWatchers.CheckedItems)
                {
                    RedmineUser curUser = item as RedmineUser;
                    Watcher watcher = IdentifiableName.Create<Watcher>(curUser.UserInfo.Id);
                    aryWatcher.Add(watcher);
                }
                newIssue.Watchers = aryWatcher;
            }

            if (cmbTracker.SelectedIndex > -1)
            {
                RedmineTracker selectedTracker = cmbTracker.Items[cmbTracker.SelectedIndex] as RedmineTracker;
                newIssue.Tracker = IdentifiableName.Create<Tracker>(selectedTracker.TrackerId);
            }

            Issue createdIssue = RedmineConnector.Current.CreateIssue(newIssue);
            

            if(chkOpenWebPageOpen.Checked)
            {
                UriBuilder builder = new UriBuilder(ConfigManager.Current.RedmineUrl);
                builder.Path = "issues/" + createdIssue.Id.ToString();
                string sUrl = builder.ToString();

                ProcessStartInfo info = new ProcessStartInfo(sUrl);
                info.UseShellExecute = true;
                Process.Start(info).WaitForExit(5000);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}
