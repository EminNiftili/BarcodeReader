using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace BarcodeReader.ViewModels
{
    public class StartScreenViewModel : BaseViewModel
    {
        public static StartScreenViewModel Instance { get; private set; }
        public StartScreenViewModel()
        {
            Instance = this;
        }
        private ObservableCollection<string> _barcodeModels = new ObservableCollection<string>();

        public ObservableCollection<string> BarcodeModels
        {
            get
            {
                return _barcodeModels;
            }
            set
            {
                _barcodeModels = value;
                PropertyChange(nameof(BarcodeModels));
            }
        }

        public void LoadList()
        {
            var files = Directory.GetFiles(App.InternalStoragePath).ToList();
            files = files.Select(x => x.Split('/').Last()).ToList();
            BarcodeModels = new ObservableCollection<string>(files.ToList());
        }
    }
}
