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

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnNewIssue_Click(object sender, EventArgs e)
        {
            FrmAddNewIssueDialog frmNewIssue = new FrmAddNewIssueDialog();
            frmNewIssue.ShowDialog();
        }
    }
}
