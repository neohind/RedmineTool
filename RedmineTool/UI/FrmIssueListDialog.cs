using RedmineTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedmineTool.UI
{
    public partial class FrmIssueListDialog : Form
    {
        List<RedmineIssueSimple> m_aryIssues = null;

        public RedmineIssueSimple SelectedIssue
        {
            get;
            private set;
        }

        public FrmIssueListDialog()
        {
            InitializeComponent();
            m_aryIssues = new List<RedmineIssueSimple>();
            listBox1.Font = new Font(ConfigManager.Current.Fonts.Families[0], 10, FontStyle.Regular);
            SelectedIssue = null;
        }

        public void SetupViewer(int nProjectId, string sStatus)
        {
            List<RedmineIssueSimple> aryAllIssues = RedmineConnector.Current.GetCurrentSimpleIssues(nProjectId, sStatus);

            m_aryIssues.AddRange(SortIssues(-1, aryAllIssues));

            listBox1.DataSource = m_aryIssues;
            
        }

        private List<RedmineIssueSimple> SortIssues(int nParentIssueId, List<RedmineIssueSimple> aryAllIssues)
        {
            List<RedmineIssueSimple> aryResults = new List<RedmineIssueSimple>();

            List<RedmineIssueSimple> aryCurLevelIssues = aryAllIssues.FindAll(m => m.ParentIssueId == nParentIssueId);
            aryCurLevelIssues.Sort((m, n) => DateTime.Compare(m.StartDate, n.StartDate));
            aryCurLevelIssues.Sort((m, n) => string.Compare(m.DisplayName, n.DisplayName));
            foreach (RedmineIssueSimple issue in aryCurLevelIssues)
            {
                aryResults.Add(issue);
                List<RedmineIssueSimple> aryChildIssues = SortIssues(issue.IssueId, aryAllIssues);
                aryResults.AddRange(aryChildIssues);
            }
            return aryResults;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSelect_Click(sender, EventArgs.Empty);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
            {
                RedmineIssueSimple selectedIssue = listBox1.Items[listBox1.SelectedIndex] as RedmineIssueSimple;
                SelectedIssue = selectedIssue;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}
