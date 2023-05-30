using Android.App;
using BarcodeReader.Services;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(BarcodeReader.Droid.Services.Application))]
namespace BarcodeReader.Droid.Services
{
    public class Application : IApplication
    {
        public void Close()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
        public void Kill()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}