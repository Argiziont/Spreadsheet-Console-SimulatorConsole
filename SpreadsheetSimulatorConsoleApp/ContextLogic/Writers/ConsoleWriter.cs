using System;
using System.Collections.Generic;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.Extensions;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic.Writers
{
    public class ConsoleWriter:ITableWriter
    {
        private readonly IEnumerable<Dictionary<string, string>> _outputContent;

        public ConsoleWriter(IEnumerable<Dictionary<string,string>> outputContent)
        {
            _outputContent = outputContent;
        }

        public void WriteText()
        {
            Console.WriteLine(_outputContent.ConvertDictionaryToString());
        }
    }
}