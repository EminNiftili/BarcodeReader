using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Models
{
    public class StatusEventArgs : EventArgs
    {
        private string barcodeData;

        public StatusEventArgs(string barcode)
        {
            barcodeData = barcode;
        }

        public string Data { get { return barcodeData; } }

    }
}
