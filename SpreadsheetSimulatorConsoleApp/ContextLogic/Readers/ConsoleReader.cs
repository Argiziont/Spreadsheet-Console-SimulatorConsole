using System;
using System.Collections.Generic;
using System.Text;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.TableLogic;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic.Readers
{
    public class ConsoleReader:ITableReader
    {
        public IEnumerable<Dictionary<string, string>> ReadText()
        {
            string text = Console.ReadLine();

            TableSizes tableSizes;

            try
            {
                tableSizes = TableSplitter.GetTableSizes(text);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("#Wrong size arguments");
                throw;
            }

            StringBuilder tableBuilder = new StringBuilder();
            for (int i = 0; i < tableSizes.Height; i++) tableBuilder.AppendLine(Console.ReadLine());

            return TableSplitter.GetTableDictionary(tableBuilder.ToString(), tableSizes);
        }
    }
}