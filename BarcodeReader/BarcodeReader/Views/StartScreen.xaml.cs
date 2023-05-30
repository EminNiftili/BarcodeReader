using BarcodeReader.Language;
using BarcodeReader.Models;
using BarcodeReader.Services;
using BarcodeReader.ViewModels;
using BarcodeReader.Workers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarcodeReader.Extensions;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartScreen : ContentPage
    {
        LanguageModel Language = LanguageModel.GenerateLanguage(App.Language);

        StartScreenViewModel viewModel = new StartScreenViewModel();
        public StartScreen()
		{
			InitializeComponent();
			ComponentGenerator();
			LanguageSetter();
        }

        public void ComponentGenerator()
        {
			this.BindingContext = viewModel;
			viewModel.LoadList();
			this.FileAdded.Clicked += FileAddedClicked;
            this.LanguageSettings.Clicked += LanguageSettingsClicked;
            this.FTPSettings.Clicked += FtpSettingsClicked;
            this.exitApp.Clicked += ExitAppClicked;
        }

        public void LanguageSetter()
		{

            this.FileAdded.Text = Language.StartScreen_AddFile;
            this.FTPSettings.Text = Language.StartScreen_FtpSettings;
            this.LanguageSettings.Text = Language.StartScreen_LanguageSettings;
            this.tableFileNameColumnName.Text = Language.StartScreen_TableFilenameColumn;
            this.tableOperationColumnName.Text = Language.StartScreen_TableOperationColumn;
            this.exitApp.Text = Language.StartScreen_ExitApp;
        }

        private void ExitAppClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IApplication>().Kill();
        }

        private void FileAddedClicked(object sender, EventArgs e)
        {
            DocumentList documentList = new DocumentList();
            App.Current.NavigationPage = new NavigationPage(documentList);
        }

        private async void FtpSettingsClicked(object sender, EventArgs e)
        {
            FtpSettings view = new FtpSettings();
            await App.Current.NavigationPage.PushAsync(view);
        }

        private async void LanguageSettingsClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet(Language.StartScreen_LanguageSettings, Language.DocumentList_GeneralPopUpOk, null, LanguageModel.GetLangueages());
            if(action == null || action == Language.DocumentList_GeneralPopUpOk)
            {
                return;
            }
            var language = LanguageModel.StringToLanguage(action);
            App.Current.ChangeLanguage(language);
            App.Current.OpenMainPage();
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {
            var fileName = ((ImageButton)sender).ClassId;
            var json = File.ReadAllText(Path.Combine(App.InternalStoragePath, fileName));
            var data = JsonConvert.DeserializeObject<List<BarcodeModel>>(json);

            DocumentList documentList = new DocumentList();

            documentList.viewModel.BarcodeModels = new ObservableCollection<BarcodeModel>(data);
            documentList.viewModel.FileName = fileName;

            App.Current.NavigationPage = new NavigationPage(documentList);
        }

        private async void DeleteButtonClicked(object sender, EventArgs e)
        {
            var fileName = ((ImageButton)sender).ClassId;
            bool answer = await DisplayAlert(Language.GeneralInformationPopUpHeader,
                Language.StartScreen_DeleteFileInPopUp,
                Language.GeneralPopUpConfirm, Language.GeneralPopUpCancel);
            if (answer)
            {
                File.Delete(Path.Combine(App.InternalStoragePath, fileName));
                App.Current.OpenMainPage();
            }
        }

        private async void FtpButtonClicked(object sender, EventArgs e)
        {
            var fileName = ((ImageButton)sender).ClassId;

            var ftpCredentials = App.Current.GetFtpCredentials();
            if (FtpSettings.CheckFtpConnection(ftpCredentials))
            {
                byte[] contentFile = null;

                var url = Path.Combine(App.InternalStoragePath, fileName);
                var json = File.ReadAllText(Path.Combine(App.InternalStoragePath, fileName));
                var data = JsonConvert.DeserializeObject<List<BarcodeModel>>(json);
                var senderFileName = fileName;


                string action = await DisplayActionSheet("File Type", Language.DocumentList_GeneralPopUpOk, null, new string[] { "JSON", "XML", "EXCEL" });
                if (action == null || action == Language.DocumentList_GeneralPopUpOk)
                {
                    return;
                }

                switch (action)
                {
                    case "JSON":
                        {
                            contentFile = File.ReadAllBytes(url);
                            break;
                        }
                    case "XML":
                        {
                            var writer = new StringWriter();
                            var serializer = new XmlSerializer(typeof(List<BarcodeModel>));
                            serializer.Serialize(writer, data);
                            string xml = writer.ToString();

                            var seperated = fileName.Split('.');
                            var orginName = string.Empty;
                            for (int i = 0; i < seperated.Length - 1; i++)
                            {
                                orginName += seperated[i];
                            }

                            var path = Path.Combine(App.InternalStoragePath, "dataExportedAsXml" + ".xml");
                            if (!File.Exists(path))
                            {
                                File.Create(path).Close();
                            }
                            File.WriteAllText(path, xml);
                            senderFileName = orginName + ".xml";
                            contentFile = File.ReadAllBytes(path);
                            File.Delete(path);
                            break;
                        }
                    case "EXCEL":
                        {
                            var seperated = fileName.Split('.');
                            var orginName = string.Empty;
                            for (int i = 0; i < seperated.Length - 1; i++)
                            {
                                orginName += seperated[i];
                            }

                            var dataTable = data.ToDataTable();

                            ExcelWorker excel = new ExcelWorker();
                            var path = excel.GenerateExcel(dataTable);

                            senderFileName = orginName + ".xlsx";
                            contentFile = File.ReadAllBytes(path);
                            File.Delete(path);
                            break;
                        }
                    default:
                        {
                            await DisplayAlert(Language.GeneralInformationPopUpHeader,
                                Language.General_FtpConnectionError,
                                Language.DocumentList_GeneralPopUpOk);
                            break;
                        }
                }

                if (FtpSettings.Upload(ftpCredentials, senderFileName, contentFile))
                {
                    await DisplayAlert(Language.GeneralInformationPopUpHeader,
                       Language.General_SuccesMessage,
                       Language.DocumentList_GeneralPopUpOk);
                }
                else
                {
                    await DisplayAlert(Language.GeneralInformationPopUpHeader,
                        Language.General_FtpConnectionError,
                        Language.DocumentList_GeneralPopUpOk);
                }
            }
            else
            {
                await DisplayAlert(Language.GeneralInformationPopUpHeader,
                    Language.General_FtpConnectionError,
                    Language.DocumentList_GeneralPopUpOk);
            }
        }
    }
}