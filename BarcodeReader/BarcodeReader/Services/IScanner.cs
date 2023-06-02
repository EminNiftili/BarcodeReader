using BarcodeReader.Configration;
using BarcodeReader.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Services
{
    public interface IScanner
    {
        event EventHandler<StatusEventArgs> OnScanDataCollected;
        event EventHandler<string> OnStatusChanged;

        void Read();

        void Enable();

        void Disable();

        void SetConfig(IScannerConfig scannerConfig);
    }
}
