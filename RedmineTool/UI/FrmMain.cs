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
    public partial class FrmMain : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private BackgroundWorker m_workerRebuildDueDate = null;

        public FrmMain()
        {
            InitializeComponent();
            m_workerRebuildDueDate = new BackgroundWorker();
            m_workerRebuildDueDate.DoWork += workerRebuildDueDate_DoWork;
            m_workerRebuildDueDate.RunWorkerCompleted += workerRebuildDueDate_RunWorkerCompleted;
        }

        

        

        private void btnNewIssue_Click(object sender, EventArgs e)
        {
            FrmAddNewIssueDialog frmNewIssue = new FrmAddNewIssueDialog();
            frmNewIssue.ShowDialog();
        }

        private void btnRebuildDueDate_Click(object sender, EventArgs e)
        {
            if (m_workerRebuildDueDate.IsBusy)
                return;

            m_workerRebuildDueDate.RunWorkerAsync();
        }

        private void workerRebuildDueDate_DoWork(object sender, DoWorkEventArgs e)
        {
            RedmineConnector.Current.RebuildDueDate();
        }

        private void workerRebuildDueDate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("종료 예정 처리 완료");
        }
    }
}
