using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Services
{
    public interface IApplication
    {
        void Close();
        void Kill();
    }
}
