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
    public partial class AppSecurity : ContentPage
    {
        public AppSecurity()
        {
            InitializeComponent();
            versionLabel.Text = App.ApplicationVersion;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(entryLabel.Text == App.ApplicationAccessPassword)
            {
                App.Current.SecurityPass();
                App.Current.OpenMainPage();
            }
            else
            {
                await DisplayAlert("Error",
                    "Invalid Password",
                    "OK");
            }
        }
    }
}