using BarcodeReader.Models;
using BarcodeReader.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BarcodeReader.ViewModels
{
    public class DocumentListViewModel : BaseViewModel
    {
        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                var seperated = value.Split('.');
                var fileName = "";
                for (int i = 0; i < seperated.Length - 1; i++ )
                {
                    fileName += seperated[i];
                }
                _fileName = fileName;
            }
        }

        public static DocumentListViewModel Instance { get; private set; }

        public DocumentListViewModel()
        {
            Instance = this;
        }

        private ObservableCollection<BarcodeModel> _barcodeModels = new ObservableCollection<BarcodeModel>();

        public ObservableCollection<BarcodeModel> BarcodeModels
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
        public void AddBarcodeModel(string barcode)
        {
            var existBarcode = _barcodeModels.FirstOrDefault(x => x.Barcode == barcode);

            if(existBarcode is null)
            {
                _barcodeModels.Add(new BarcodeModel
                {
                    Barcode = barcode,
                    Count = 1,
                    AddedDate = DateTime.Now
                });
            }
            else
            {
                existBarcode.Count++;
            }
            _barcodeModels = new ObservableCollection<BarcodeModel>(_barcodeModels.ToList()); 
            PropertyChange(nameof(BarcodeModels));
            DependencyService.Get<IAudioService>().PlayAudioFile("scannerSound.mp4");
        }

        public void ChangeCount(int index, double count)
        {

            if(count < 0)
            {
                return;
            }
            else if(count == 0)
            {
                _barcodeModels = new ObservableCollection<BarcodeModel>(_barcodeModels.Where(x => x.Index != index).ToList());
            }
            else
            {
                var existBarcode = _barcodeModels.FirstOrDefault(x => x.Index == index);

                if (existBarcode != null)
                {
                    existBarcode.Count = count;
                }
            }
            
            _barcodeModels = new ObservableCollection<BarcodeModel>(_barcodeModels.ToList());
            PropertyChange(nameof(BarcodeModels));
            DependencyService.Get<IAudioService>().PlayAudioFile("scannerSound.mp4");
        }

        public bool SaveFile(string result)
        {
            if(FileName != null)
            {
                result = FileName;
            }
            try
            {
                var path = Path.Combine(App.InternalStoragePath, result + ".json");
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                var data = JsonConvert.SerializeObject(_barcodeModels);
                File.WriteAllText(path, data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
