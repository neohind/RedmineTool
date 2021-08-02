using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedmineTool.UI
{
    public partial class FrmSetupApiKeyDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public FrmSetupApiKeyDialog()
        {
            InitializeComponent();

            txtApiKey.Text = ConfigManager.Current.ApiKey;
            txtUrl.Text = ConfigManager.Current.RedmineUrl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sApiKey = txtApiKey.Text;
            string sUrl = txtUrl.Text;
            string sInitResult = RedmineConnector.Current.Init(sUrl, sApiKey);

            if (string.IsNullOrEmpty(sInitResult) == false)
            {
                MessageBox.Show(sInitResult);
                return;
            }

            ConfigManager.Current.RedmineUrl = txtUrl.Text;
            ConfigManager.Current.ApiKey = txtApiKey.Text;

            this.DialogResult = DialogResult.OK;
            Close();

        }

    }
}
