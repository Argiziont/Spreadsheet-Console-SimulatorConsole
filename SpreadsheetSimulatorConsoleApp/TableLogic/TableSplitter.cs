using System;
using System.Collections.Generic;
using System.Linq;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;

namespace SpreadsheetSimulatorConsoleApp.TableLogic
{
    public static class TableSplitter
    {
        private const char RowSplitter = '\n';
        private const char ColumnSplitter = '\t';
        private const int AsciiACharIndex = 65;

        public static TableSizes GetTableSizes(string inputText)
        {
            //
            var tableTextArray = inputText.Split(RowSplitter).ToList();

            //Evaluating size of the table
            var tableSize = tableTextArray.First().Split(ColumnSplitter).ToList();

            int tableWidth = Convert.ToInt32(tableSize.First());
            int tableHeight = Convert.ToInt32(tableSize.Last());

            return new TableSizes(tableWidth, tableHeight);
        }

        public static IEnumerable<Dictionary<string, string>> GetTableDictionary(string inputText, TableSizes tableSizes)
        {
            var dictionaries = new Dictionary<string, string>[tableSizes.Height];

            var tableTextArray = inputText.Split(RowSplitter).ToList();

            //Evaluating cells
            tableTextArray.Remove(tableTextArray.First());//Removing line with sizes of table

            string[][] tableCells=tableTextArray.Select(row => row.Split(ColumnSplitter).ToArray()).ToArray();

            for (int i = 0; i < tableSizes.Height; i++)
            {
                dictionaries[i] = new Dictionary<string, string>();
                for (int j = 1; j <= tableSizes.Width; j++)
                {
                    dictionaries[i].Add($"{Convert.ToChar(AsciiACharIndex + i)}{j}", tableCells[j - 1][i]);
                }
            }

            return dictionaries;
        }
    }
}