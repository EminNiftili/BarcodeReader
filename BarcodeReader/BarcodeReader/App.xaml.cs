using BarcodeReader.Language;
using BarcodeReader.Models;
using BarcodeReader.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReader
{
    public partial class App : Application
    {
        /*
         * version 1.0.1 = E123456hQAZWSX   Date : 31 may 2023   04:00
         * App created
         * 
         * version 1.0.2 = E123456hQAZWSX   Date : 1 June 2023   23:50
         * Scanner Reader Added
         * 
         * version 1.1.1 = QAZWSXEH123456   Date : 2 June 2023   1:30
         * Some View Component Changed. Optimized Codes. Functionality Changed
         */
        public const string ApplicationAccessPassword = "QAZWSXEH123456";
        public const string ApplicationVersion = "1.1.1";

        public const string DocumentDefaultName = "Invoice";

        public const string AppPublicFileDirectory = "storage/emulated/0/BarcodeReader/";

        private NavigationPage _navigationPage;

        public static string InternalStoragePath;
        public static App Current { get; private set; }
        public static Languages Language;
        public NavigationPage NavigationPage
        {
            get
            {
                return _navigationPage;
            }
            set
            {
                _navigationPage = value;
                MainPage = _navigationPage;
            }
        }
        public App()
        {
            InitializeComponent();
            InternalStoragePath = Xamarin.Essentials.FileSystem.AppDataDirectory;
            Current = this;
        }


        protected override void OnStart()
        {
            LanguageSettings();
            AuthApp();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        public void SecurityPass()
        {
            Preferences.Set("securityAuthPassKey", true);
        }

        public void LanguageSettings()
        {
            string langCodeAsObject =  Preferences.Get("lang", null);
            if (langCodeAsObject == null)
            {
                Language = Languages.Azerbaijan;
            }
            else
            {
                int langCode = int.Parse(langCodeAsObject);
                Language = (Languages)langCode;
            }
        }

        public void ChangeLanguage(Languages languages)
        {
            if (Preferences.ContainsKey("lang"))
            {
                Preferences.Remove("lang");
            }
            Preferences.Set("lang", ((int)languages).ToString());
            Language = languages;
        }

        public void SaveFtpCredentials(FtpCredentialsModel ftpCredentials)
        {
            if (ftpCredentials is null 
                //||
                //ftpCredentials.Url is null ||
                //ftpCredentials.Username is null ||
                //ftpCredentials.Password is null ||
                //ftpCredentials.Port is null ||
                //ftpCredentials.FilePath is null
                )
            {
                return;
            }

            var data = JsonConvert.SerializeObject(ftpCredentials);

            if (Preferences.ContainsKey("ftp"))
            {
                Preferences.Remove("ftp");
            }
            Preferences.Set("ftp", data);

        }

        public FtpCredentialsModel GetFtpCredentials()
        {
            try
            {
                string ftpCodeAsObject = Preferences.Get("ftp", null);
                if (ftpCodeAsObject != null)
                {
                    var ftpData = JsonConvert.DeserializeObject<FtpCredentialsModel>(ftpCodeAsObject);
                    return ftpData;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void OpenMainPage()
        {
            NavigationPage = new NavigationPage();
            MainPage = _navigationPage;
            NavigationPage.PushAsync(new StartScreen());
        }

        public void AuthApp()
        {
            bool securityPass = Preferences.Get("securityAuthPassKey", false);
            if (securityPass)
            {
                OpenMainPage();
            }
            else
            {
                MainPage = new AppSecurity();
            }
        }
    }
}
