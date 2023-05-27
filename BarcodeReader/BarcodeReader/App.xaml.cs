using BarcodeReader.Language;
using BarcodeReader.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReader
{
    public partial class App : Application
    {
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
            OpenMainPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
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
        public void OpenMainPage()
        {
            NavigationPage = new NavigationPage();
            MainPage = _navigationPage;
            NavigationPage.PushAsync(new StartScreen());
        }
    }
}
