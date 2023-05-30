using BarcodeReader.Language;
using BarcodeReader.Models;
using BarcodeReader.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentList : ContentPage
    {

        LanguageModel Language = LanguageModel.GenerateLanguage(App.Language);

        public DocumentListViewModel viewModel = new DocumentListViewModel();

        public DocumentList()
        {
            InitializeComponent();
            ComponentGenerator();
            LanguageSetter();
        }
        public void LanguageSetter()
        {

            this.DocumentAdded.Text = Language.DocumentList_AddGoods;
            this.tableAddedDateColumnName.Text = Language.DocumentList_TableAddedDateColumnName;
            this.tableBarcodeColumnName.Text = Language.DocumentList_TableBarcodeColumnName;
            this.tableCountColumnName.Text = Language.DocumentList_TableCountName;
            this.DocumentCancel.Text = Language.DocumentList_Cancel;
            this.DocumentSaved.Text = Language.General_SaveText;
        }

        public void ComponentGenerator()
        {
            this.BindingContext = viewModel;
            this.DocumentAdded.Clicked += DocumentAddedClicked;
            this.DocumentCancel.Clicked += DocumentCancelClicked;
            this.DocumentSaved.Clicked += DocumentSavedClicked;
            this.documentList.ItemSelected += DocumentListItemSelected;
        }

        private async void DocumentListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem is null)
            {

                documentList.SelectedItem = null;
                return;
            }
            var selectedItem = (BarcodeModel)e.SelectedItem;
            string result = await DisplayPromptAsync(selectedItem.Barcode,
                     Language.DocumentList_ChangeCountInPopUpMessage,
                     accept: Language.GeneralPopUpConfirm,
                     cancel: Language.GeneralPopUpCancel,
                     keyboard: Keyboard.Numeric,
                     initialValue: selectedItem.Count.ToString());

            if(result is null)
            {
                documentList.SelectedItem = null;
                return;
            }

            DocumentListViewModel.Instance.ChangeCount(selectedItem.Index, double.Parse(result));
            documentList.SelectedItem = null;
        }

        private async void DocumentSavedClicked(object sender, EventArgs e)
        {
            string result = null;
            if (viewModel.FileName == null)
            {
                result = await DisplayPromptAsync(Language.GeneralInformationPopUpHeader,
                     Language.DocumentList_FileSavePopUpMessage,
                     accept: Language.GeneralPopUpConfirm,
                     cancel: Language.GeneralPopUpCancel);
                if (result is null)
                {
                    return;
                }
            }
            else
            {
                result = viewModel.FileName;
            }

            if (result is null)
            {
                return;
            }
            var canAddedFile = DocumentListViewModel.Instance.SaveFile(result);
            if (!canAddedFile)
            {
                await DisplayAlert(Language.GeneralInformationPopUpHeader,
                    Language.DocumentList_FileNameInputError,
                    Language.DocumentList_GeneralPopUpOk);
            }
            else
            {
                App.Current.OpenMainPage();
            }
        }

        private async void DocumentCancelClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert(Language.GeneralInformationPopUpHeader,
                Language.DocumentList_CancelPopUpMessage,
                Language.GeneralPopUpConfirm, Language.GeneralPopUpCancel);
            if (answer)
            {
                App.Current.OpenMainPage();
            }
        }

        private void DocumentAddedClicked(object sender, EventArgs e)
        {
            App.Current.NavigationPage.PushAsync(new Recorder());
        }

    }
}