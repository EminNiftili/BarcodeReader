using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace BarcodeReader.Extensions
{
    public static class ListViewExtension
    {
        public static ITemplatedItemsList<Cell> GetAllViewCells(this ListView listView)
        {
            IEnumerable<PropertyInfo> pInfos = (listView as ItemsView<Cell>).GetType().GetRuntimeProperties();
            var templatedItems = pInfos.FirstOrDefault(info => info.Name == "TemplatedItems");
            if (templatedItems != null)
            {
                var cells = templatedItems.GetValue(listView);
                return (ITemplatedItemsList<Cell>)cells;
            }
            return null;
        }
    }
}
