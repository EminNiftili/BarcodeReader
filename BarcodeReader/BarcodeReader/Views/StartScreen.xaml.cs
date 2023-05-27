using BarcodeReader.Language;
using BarcodeReader.Models;
using BarcodeReader.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        public void LanguageSetter()
		{

            this.FileAdded.Text = Language.StartScreen_AddFile;
            this.FtpSettings.Text = Language.StartScreen_FtpSettings;
            this.LanguageSettings.Text = Language.StartScreen_LanguageSettings;
            this.tableFileNameColumnName.Text = Language.StartScreen_TableFilenameColumn;
            this.tableOperationColumnName.Text = Language.StartScreen_TableOperationColumn;
        }

        private void FileAddedClicked(object sender, EventArgs e)
        {
			DocumentList documentList = new DocumentList();
			App.Current.NavigationPage = new NavigationPage(documentList);
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

        private void FtpButtonClicked(object sender, EventArgs e)
        {

        }
    }
}