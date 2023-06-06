using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BarcodeReader.Models
{
    public class BarcodeModel
    {
        public BarcodeModel()
        {
        }

        [XmlAttribute]
        public int Index { get; set; }

        [XmlAttribute]
        public string Barcode { get; set; }

        [XmlAttribute]
        public double Count { get; set; }

        [XmlAttribute]
        public DateTime AddedDate { get; set; }
    }
}
