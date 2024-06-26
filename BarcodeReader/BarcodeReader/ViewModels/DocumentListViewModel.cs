﻿using BarcodeReader.Configration;
using BarcodeReader.Extensions;
using BarcodeReader.Models;
using BarcodeReader.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BarcodeReader.ViewModels
{
    public class DocumentListViewModel : BaseViewModel
    {
        public int indexerFlag = 0;

        public event EventHandler BarcodeListChanged;

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public void SetFileName(string filename, bool withExtensions = false)
        {
            if (withExtensions)
            {
                _fileName = filename.RemoveDestinationInFilename();
            }
            else
            {
                _fileName = filename;
            }
        }

        public static DocumentListViewModel Instance { get; private set; }

        public DocumentListViewModel()   
        {
            Instance = this;

            var scanner = DependencyService.Get<IScanner>();

            scanner.Enable();
            scanner.OnScanDataCollected += Scanner_OnScanDataCollected;

            var config = new ZebraScannerConfig();
            config.IsUPCE0 = false;
            config.IsUPCE1 = false;

            scanner.SetConfig(config);
        }

        private void Scanner_OnScanDataCollected(object sender, StatusEventArgs e)
        {
            this.AddBarcodeModel(e.Data);
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
                    Index = ++indexerFlag,
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

            BarcodeListChanged?.Invoke(barcode, new EventArgs());
        }

        public void ChangeCount(string barcode, double count)
        {

            if(count < 0)
            {
                return;
            }
            else if(count == 0)
            {
                _barcodeModels = new ObservableCollection<BarcodeModel>(_barcodeModels.Where(x => x.Barcode != barcode).ToList());
            }
            else
            {
                var existBarcode = _barcodeModels.FirstOrDefault(x => x.Barcode == barcode);

                if (existBarcode != null)
                {
                    existBarcode.Count = count;
                }
            }
            
            _barcodeModels = new ObservableCollection<BarcodeModel>(_barcodeModels.ToList());
            PropertyChange(nameof(BarcodeModels));
            DependencyService.Get<IAudioService>().PlayAudioFile("scannerSound.mp4");

            BarcodeListChanged?.Invoke(barcode, new EventArgs());
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
