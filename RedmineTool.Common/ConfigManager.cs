using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace RedmineTool
{
    public class ConfigManager : IDisposable
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private static byte[] g_aryAdditionalEntropy = { 9, 8, 7, 6, 5 };

        private PrivateFontCollection m_fontCollection = null;
        private RegistryKey m_regkeyForApp = null;


        static ConfigManager()
        {
            if (Current == null)
                Current = new ConfigManager();
        }

        public static ConfigManager Current
        {
            get;
            private set;
        }
        
        public string CurrentLoginUser
        {
            get;
            set;
        }

        public int CurrentLoginUserId
        {
            get;
            set;
        }

        public string RedmineUrl
        {
            get
            {
                string sUrl = m_regkeyForApp.GetValue("Url") as string;
                return sUrl;
            }
            set
            {
                m_regkeyForApp.SetValue("Url", value);
            }
        }
        
        public string ApiKey
        {
            get
            {
                string sApiKey = m_regkeyForApp.GetValue("ApiKey") as string;
                if (string.IsNullOrEmpty(sApiKey))
                    return string.Empty;
                byte [] aryApiKey = Convert.FromBase64String(sApiKey);
                byte [] aryDecodedApiKey 
                    = ProtectedData.Unprotect(aryApiKey, g_aryAdditionalEntropy, DataProtectionScope.CurrentUser);
                return Encoding.ASCII.GetString(aryDecodedApiKey);
            }
            set
            {
                byte[] aryApiKey = Encoding.ASCII.GetBytes(value);
                byte[] aryEncodedApiKey
                    = ProtectedData.Protect(aryApiKey, g_aryAdditionalEntropy, DataProtectionScope.CurrentUser);
                string sEncodedApiKey = Convert.ToBase64String(aryEncodedApiKey);
                m_regkeyForApp.SetValue("ApiKey", sEncodedApiKey);
            }
        }


        public string DefaultNewIssue_Assignee
        {
            get
            {
                return GetDefaultValue("DefaultNewIssue", "Assignee");
            }
            set
            {
                SetDefaultValue("DefaultNewIssue", "Assignee", value);
            }
        }

        public string[] DefaultNewIssue_Watchers
        {
            get
            {
                List<string> aryWatchers = new List<string>();
                string sWatchers = GetDefaultValue("DefaultNewIssue", "Watchers");
                if (string.IsNullOrEmpty(sWatchers) == false)
                {
                    string[] aryTokens = sWatchers.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    aryWatchers.AddRange(aryTokens);
                }
                return aryWatchers.ToArray();
            }
            set {
                StringBuilder sbAllWatchers = new StringBuilder();
                foreach(string sWatcher in value)
                {
                    sbAllWatchers.Append(sWatcher);
                    sbAllWatchers.Append(",");
                }
                SetDefaultValue("DefaultNewIssue", "Watchers", sbAllWatchers.ToString());
            }
        }

        public int SelectedProject 
        { 
            get
            {
                var objValue = m_regkeyForApp.GetValue("SelectedProject");
                int nResult = 0;
                try
                {
                    nResult = Convert.ToInt32(objValue);
                }
                catch(Exception ex)
                {
                    SelectedProject = 0;
                    log.Error(ex);
                    return 0;
                }
                return nResult;
            }
            set
            {
                m_regkeyForApp.SetValue("SelectedProject", value);
            }
        }

        public int DefaultNewIssue_Tracker {
            get
            {
                string sValue = GetDefaultValue("DefaultNewIssue", "SelectedTracker");
                int nResult = -1;
                try
                {
                    nResult = Convert.ToInt32(sValue);
                }
                catch (Exception ex)
                {
                    DefaultNewIssue_Tracker = -1;
                    log.Error(ex);
                }
                if (string.IsNullOrEmpty(sValue))
                    return -1;

                return nResult;
            }
            set
            {
                SetDefaultValue("DefaultNewIssue", "SelectedTracker", value.ToString());
            }
        }

        public bool DefaultNewIssue_IsOpenTracker {
            get
            {
                string sValue = GetDefaultValue("DefaultNewIssue", "IsOpenBrowser");
                bool bResult = false;
                try
                {
                    bResult = Convert.ToBoolean(sValue);
                }
                catch (Exception ex)
                {
                    DefaultNewIssue_Tracker = -1;
                    log.Error(ex);
                }
                if (string.IsNullOrEmpty(sValue))
                    return false;

                return bResult;
            }
            set
            {
                SetDefaultValue("DefaultNewIssue", "IsOpenBrowser", value.ToString());
            }
        }


        public PrivateFontCollection Fonts
        {
            get
            {
                return m_fontCollection;
            }
        }

        

        private IntPtr m_font = IntPtr.Zero;

        public ConfigManager()
        {
            m_fontCollection = new PrivateFontCollection();

            if (System.IO.File.Exists("D2Coding.ttc") == false)
            {
                using (System.IO.FileStream writer = System.IO.File.Create("D2Coding.ttc"))
                {
                    writer.Write(Common.Properties.Resources.D2Coding, 0, Common.Properties.Resources.D2Coding.Length);
                }
            }

            RegistryKey key  = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            m_regkeyForApp = key.OpenSubKey(@"SOFTWARE\RedmineTool", true);
            if(m_regkeyForApp == null)
            {
                m_regkeyForApp = key.CreateSubKey(@"SOFTWARE\RedmineTool");
            }
            m_fontCollection.AddFontFile("D2Coding.ttc");
        }

        ~ConfigManager()
        {
            if (m_regkeyForApp != null)
                m_regkeyForApp.Dispose();

            m_fontCollection.Dispose();
        }

        private string GetDefaultValue(string sSubKeyName, string sName)
        {
            try
            {
                using (RegistryKey key = m_regkeyForApp.OpenSubKey(sSubKeyName))
                {
                    if(key != null)
                        return key.GetValue(sName) as string;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return string.Empty;
        }

        private string SetDefaultValue(string sSubKeyName, string sName, string sValue)
        {
            try
            {
                using (RegistryKey key = m_regkeyForApp.CreateSubKey(sSubKeyName)) 
                {
                    key.SetValue(sName, sValue);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return string.Empty;
        }

        public void Dispose()
        {
            m_fontCollection.Dispose();
        }

       
    }
}
