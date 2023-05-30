using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BarcodeReader.Models
{
    public class BarcodeModel
    {
        private static int? _index = null;
        public BarcodeModel()
        {
            if (_index is null)
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

        [XmlAttribute]
        public string Barcode { get; set; }

        [XmlAttribute]
        public double Count { get; set; }

        [XmlAttribute]
        public DateTime AddedDate { get; set; }
    }
}
