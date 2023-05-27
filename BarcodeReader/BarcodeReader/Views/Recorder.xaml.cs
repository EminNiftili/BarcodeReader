using BarcodeReader.Language;
using BarcodeReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Recorder : ContentPage
    {
        public bool IsKeepPageOpen = false;

        LanguageModel Language = LanguageModel.GenerateLanguage(App.Language);
        public Recorder()
        {
            InitializeComponent();
            ComponentGenerator();
            LanguageSetter();
        }
        public void LanguageSetter()
        {
            this.KeepOpenAnalyze.Text = Language.Recorder_KeepOpenPage;
            this.ManualInput.Placeholder = Language.Recorder_ManualInputName;
            this.ManualAdd.Text = Language.Recorder_ManualInputButton;
        }

        public void ComponentGenerator()
        {
            this.scanner.IsScanning = true;
            this.scanner.HeightRequest = Application.Current.MainPage.Width - 50;
            this.scanner.WidthRequest = Application.Current.MainPage.Width - 50;
            this.scanner.OnScanResult += ScannerOnScanResult;
            this.scanner.Options.DelayBetweenAnalyzingFrames = 50;
            this.scanner.Options.DelayBetweenContinuousScans = 1000;
            this.ManualAdd.Clicked += ManualAddClicked;
        }

        private void ManualAddClicked(object sender, EventArgs e)
        {
            if(this.ManualInput.Text == null || this.ManualInput.Text == string.Empty)
            {
                return;
            }

            AddGoods(this.ManualInput.Text);
            this.ManualInput.Text = null;
        }

        private void ScannerOnScanResult(ZXing.Result result)
        {
            AddGoods(result.Text);
        }

        private void AddGoods(string code)
        {
            DocumentListViewModel.Instance.AddBarcodeModel(code);

            if (!IsKeepPageOpen)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.NavigationPage.PopAsync();
                });
            }
        }

        private void SwitchToggled(object sender, ToggledEventArgs e)
        {
            IsKeepPageOpen = e.Value;
        }

        private void FlashButtonClicked(object sender, EventArgs e)
        {
            scanner.ToggleTorch();
        }


    }
}