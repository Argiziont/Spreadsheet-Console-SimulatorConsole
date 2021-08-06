using System;
using System.Collections.Generic;
using System.Linq;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;

namespace SpreadsheetSimulatorConsoleApp.TableLogic
{
    public static class TableSplitter
    {
        private const string RowSplitter = "\r\n";
        private const char ColumnSplitter = '\t';
        private const int AsciiACharIndex = 65;

        public static TableSizes GetTableSizes(string inputText)
        {
            //

            var tableTextArray = inputText.Split(RowSplitter).ToList();
            //Evaluating size of the table
            var tableSize = tableTextArray.First().Split(ColumnSplitter).ToList();

            int tableWidth = Convert.ToInt32(tableSize.Last());
            int tableHeight = Convert.ToInt32(tableSize.First());

            return new TableSizes(tableWidth, tableHeight);
        }

        public static IEnumerable<Dictionary<string, string>> GetTableDictionary(string inputText, TableSizes tableSizes)
        {
            var dictionaries = new Dictionary<string, string>[tableSizes.Width];

            var tableTextArray = inputText.Split(RowSplitter).ToList();
            
            string[][] tableCells=tableTextArray.Select(row => row.Split(ColumnSplitter).ToArray()).ToArray();

            for (int i = 0; i < tableSizes.Width; i++)
            {
                dictionaries[i] = new Dictionary<string, string>();
                for (int j = 1; j <= tableSizes.Height; j++)
                {
                    dictionaries[i].Add($"{Convert.ToChar(AsciiACharIndex + i)}{j}", tableCells[j - 1][i]);
                }
            }

            return dictionaries;
        }
    }
}