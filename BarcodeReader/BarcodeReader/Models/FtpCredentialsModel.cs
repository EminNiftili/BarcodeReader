using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Models
{
    public class FtpCredentialsModel
    {
        public string Url { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FilePath { get; set; }
    }
}
