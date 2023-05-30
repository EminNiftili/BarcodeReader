using BarcodeReader.Language;
using BarcodeReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FtpSettings : ContentPage
    {
        LanguageModel Language = LanguageModel.GenerateLanguage(App.Language);
        public FtpSettings()
        {
            InitializeComponent();
            ComponentGenerator();
            LanguageSetter();
        }

        public void ComponentGenerator()
        {
            this.saveButton.Clicked += SaveButtonClicked;

            var ftpCredentials = App.Current.GetFtpCredentials();
            if (ftpCredentials != null)
            {
                this.address.Text = ftpCredentials.Url;
                this.port.Text = ftpCredentials.Port;
                this.username.Text = ftpCredentials.Username;
                this.password.Text = ftpCredentials.Password;
                this.path.Text = ftpCredentials.FilePath;
            }
        }

        public void LanguageSetter()
        {
            this.address.Placeholder = Language.FtpSettings_UrlInput;
            this.port.Placeholder = Language.FtpSettings_PortInput;
            this.username.Placeholder = Language.FtpSettings_UsernameInput;
            this.password.Placeholder = Language.FtpSettings_PasswordInput;
            this.path.Placeholder = Language.FtpSettings_PathInput;
            this.saveButton.Text = Language.General_SaveText;
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            FtpCredentialsModel ftpCredentials = new FtpCredentialsModel
            {
                Url = this.address.Text,
                Port = this.port.Text,
                Username = this.username.Text,
                Password = this.password.Text,
                FilePath = this.path.Text,
            };

            App.Current.SaveFtpCredentials(ftpCredentials);
            await App.Current.NavigationPage.PopAsync();

            //if (CheckFtpConnection(ftpCredentials))
            //{
            //    await DisplayAlert(Language.GeneralInformationPopUpHeader,
            //        Language.General_SuccesMessage,
            //        Language.DocumentList_GeneralPopUpOk);

                
            //}
            //else
            //{
            //    await DisplayAlert(Language.GeneralInformationPopUpHeader,
            //        Language.General_FtpConnectionError,
            //        Language.DocumentList_GeneralPopUpOk);
            //}
        }
        public static bool CheckFtpConnection(FtpCredentialsModel ftpCredentials)
        {

            try
            {
                var ftpUrl = "ftp://" + ftpCredentials.Url ?? "" + ":" + ftpCredentials?.Port ?? "" + ftpCredentials?.FilePath ?? "";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(ftpCredentials.Username ?? "", ftpCredentials.Password ?? "");
                request.Timeout = 10000;
                var response = request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool Upload(FtpCredentialsModel ftpCredentials, string fileName, byte[] content)
        {
            try
            {

                var ftpUrl = "ftp://" + ftpCredentials.Url ?? "" + ":" + ftpCredentials.Port ?? "" + ftpCredentials.FilePath ?? "";

                String uploadUrl = String.Format("{0}/{1}", ftpUrl, fileName);
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
                req.Proxy = null;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(ftpCredentials.Username, ftpCredentials.Password);
                req.UseBinary = true;
                req.UsePassive = true;
                req.ContentLength = content.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(content, 0, content.Length);
                stream.Close();
                FtpWebResponse res = (FtpWebResponse)req.GetResponse();
                return true;

            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}