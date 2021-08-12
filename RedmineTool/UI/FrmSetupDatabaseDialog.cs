using RedmineTool.Common.Dac;
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
    public partial class FrmSetupDatabaseDialog : Form
    {
        public FrmSetupDatabaseDialog()
        {
            InitializeComponent();

            string sDatabaseAddress = ConfigManager.Current.DatabaseUrl;
            string sDatabaseUserName = ConfigManager.Current.DatabaseUserName;
            string sDatabaseUserPassword = ConfigManager.Current.DatabaseUserPassword;
            string sDatabaseName = ConfigManager.Current.DatabaseName;
            string sDatabasePortNo = ConfigManager.Current.DatabasePort;

            if (string.IsNullOrEmpty(sDatabaseName))
                sDatabaseName = "redmine";
            if (string.IsNullOrEmpty(sDatabasePortNo))
                sDatabasePortNo = "3306";

            txtDbAddress.Text = sDatabaseAddress;
            txtDbName.Text = sDatabaseName;
            txtDbPortNo.Text = sDatabasePortNo;
            txtDbUserName.Text = sDatabaseUserName;
            txtDbUserPassword.Text = sDatabaseUserPassword;



        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sDatabaseAddress = txtDbAddress.Text;
            string sDatabaseUserName = txtDbUserName.Text;
            string sDatabaseUserPassword = txtDbUserPassword.Text;
            string sDatabaseName = txtDbName.Text;
            string sDatabasePortNo = txtDbPortNo.Text;

            if (!string.IsNullOrEmpty(sDatabaseAddress))
                sDatabaseAddress = sDatabaseAddress.Trim();

            if (!string.IsNullOrEmpty(sDatabaseUserName))
                sDatabaseUserName = sDatabaseUserName.Trim();

            if (!string.IsNullOrEmpty(sDatabaseUserPassword))
                sDatabaseUserPassword = sDatabaseUserPassword.Trim();

            if (!string.IsNullOrEmpty(sDatabaseName))
                sDatabaseName = sDatabaseName.Trim();

            if (!string.IsNullOrEmpty(sDatabasePortNo))
                sDatabasePortNo = sDatabasePortNo.Trim();


            if (!DbAgent.CanConnect(
                sDatabaseAddress, sDatabaseUserName, sDatabaseUserPassword, sDatabasePortNo, sDatabaseName))
            {
                MessageBox.Show("데이터베이스 연결이 되지 않습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            ConfigManager.Current.SetDatabaseInfo(
                sDatabaseAddress, sDatabaseUserName, sDatabaseUserPassword, sDatabasePortNo, sDatabaseName);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
