using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetSimulatorConsoleApp.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertDictionaryToString(this IEnumerable<Dictionary<string, string>> dictionaries)
        {
            if (dictionaries == null) throw new ArgumentNullException(nameof(dictionaries));

            StringBuilder tableBuilder = new StringBuilder();

            tableBuilder.AppendLine("\n-------Results-------\n");
            foreach (var column in dictionaries.Transpose())
            {
                foreach (var cell in column)
                {
                    tableBuilder.Append(cell.Value + "\t");
                }

                tableBuilder.AppendLine();
            }

            return tableBuilder.ToString();
        }
    }
}