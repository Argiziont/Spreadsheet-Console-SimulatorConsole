using System;
using System.Collections.Generic;
using System.IO;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.Extensions;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic.Writers
{
    public class FileWriter:ITableWriter
    {
        private readonly IEnumerable<Dictionary<string, string>> _outputContent;
        private readonly string _path;

        public FileWriter(IEnumerable<Dictionary<string, string>> outputContent, string path)
        {
            _outputContent = outputContent;
            _path = path;
        }

        public void WriteText()
        {
            string fileOutput=_outputContent.ConvertDictionaryToString();
            string actualPath = _path + ".output.txt";

            File.WriteAllText(actualPath, fileOutput);
;        }
    }
}