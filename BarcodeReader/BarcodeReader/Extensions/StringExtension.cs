using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Extensions
{
    public static class StringExtension
    {
        public static string RemoveDestinationInFilename(this string filename)
        {
            var seperated = filename.Split('.');
            var fileName = "";
            for (int i = 0; i < seperated.Length - 1; i++)
            {
                fileName += seperated[i];
            }
            return fileName;
        }
    }
}
