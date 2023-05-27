using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Models
{
    public class BarcodeModel
    {
        private static int? _index = null;
        public BarcodeModel()
        {
            if(_index is null)
            {
                _index = 1;
            }
            else
            {
                _index++;
            }
            Index = _index.Value;
        }
        public int Index { get; private set; }
        public string Barcode { get; set; }
        public double Count { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
