using BarcodeReader.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BarcodeReader.Workers
{
    public class ExcelWorker
    {

        public string GenerateExcel(DataTable dataTable)
        {
            XLWorkbook workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            // Başlık satırını ekleyin
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
            }

            // Veri satırlarını ekleyin
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cell(row + 2, col + 1).Value = dataTable.Rows[row][col].ToString();
                }
            }

            // Excel dosyasını kaydedin
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "data.xlsx");
            workbook.SaveAs(filePath);
            return filePath;
        }
    }
}
