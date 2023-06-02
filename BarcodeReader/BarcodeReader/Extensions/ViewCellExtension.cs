using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BarcodeReader.Extensions
{
    public static class ViewCellExtension
    {
        public static string GetBarcodeForDocumentView(this ViewCell cell)
        {
            var grid = (Grid)cell.View;
            var label = (Label)grid.Children[0];
            return label.Text;
        }

        public static void ChangeBackGroundColorForDocumentView(this ViewCell cell, Color color)
        {
            var grid = (Grid)cell.View;
            var label1 = (Label)grid.Children[0];
            var label2 = (Label)grid.Children[1];
            var label3 = (Label)grid.Children[2];
            label1.BackgroundColor = color;
            label2.BackgroundColor = color;
            label3.BackgroundColor = color;
        }
    }
}
