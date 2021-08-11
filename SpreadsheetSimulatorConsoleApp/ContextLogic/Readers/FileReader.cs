using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.TableLogic;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic.Readers
{
    public class FileReader:ITableReader
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public IEnumerable<Dictionary<string, string>> ReadText()
        {
            if (!File.Exists(_path)) throw new FileNotFoundException(nameof(_path));

            string fileContent = File.ReadAllText(_path);
            string sizeContent = fileContent.Split("\r\n").First();
            int sizeLineLength = sizeContent.Length + "\r\n".Length;
            string tableContent = fileContent.Substring(sizeLineLength, fileContent.Length - sizeLineLength);
            TableSizes tableSizes;

            try
            {
                tableSizes = TableSplitter.GetTableSizes(sizeContent);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("#Wrong size arguments");
                throw;
            }
            return TableSplitter.GetTableDictionary(tableContent, tableSizes);

        }
    }
}