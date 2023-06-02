using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReader.Extensions
{
    public static class DateTimeExtension
    {
        public static string GetUniqueFormat(this DateTime dateTime)
        {
            var result = string.Empty;
            
            result += dateTime.Day.ToString(); 
            result += "-";
            result += dateTime.Month.ToString();
            result += "-";
            result += dateTime.Year.ToString();
            result += "-";
            result += dateTime.Hour.ToString();
            result += "-";
            result += dateTime.Minute.ToString();
            result += "-";
            result += dateTime.Second.ToString();
            result += "-";
            result += dateTime.Millisecond.ToString();

            return result;
        }
    }
}
