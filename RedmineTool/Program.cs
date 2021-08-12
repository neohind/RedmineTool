using RedmineTool.Common;
using RedmineTool.Common.Dac;
using RedmineTool.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedmineTool
{
    static class Program
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach(string sWatcher in ConfigManager.Current.DefaultNewIssue_Watchers)
            {
                log.Debug(sWatcher);
            }

            string sDatabaseAddress = ConfigManager.Current.DatabaseUrl;
            string sDatabaseUserName = ConfigManager.Current.DatabaseUserName;
            string sDatabaseUserPassword = ConfigManager.Current.DatabaseUserPassword;
            string sDatabaseName = ConfigManager.Current.DatabaseName;
            string sDatabasePortNo = ConfigManager.Current.DatabasePort;

            ConfigManager.Current.CanConnectDb = DbAgent.CanConnect(
                sDatabaseAddress, sDatabaseUserName, sDatabaseUserPassword, sDatabasePortNo, sDatabaseName);



            string sUrl = ConfigManager.Current.RedmineUrl;
            string sApiKey = ConfigManager.Current.ApiKey;
            if (string.IsNullOrEmpty(RedmineConnector.Current.Init(sUrl, sApiKey)) == false)
            {
                FrmSetupApiKeyDialog frmSetup = new FrmSetupApiKeyDialog();
                if (frmSetup.ShowDialog() != DialogResult.OK)
                    return;
            }
            Application.Run(new FrmMain());


            ConfigManager.Current.Dispose();
        }
    }
}
