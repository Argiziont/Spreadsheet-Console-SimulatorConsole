using System.Collections.Generic;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class TableReader
    {
        private readonly ITableReader _tableReader;

        public TableReader(ITableReader tableReader)
        {
            _tableReader = tableReader;
        }

        public IEnumerable<Dictionary<string, string>> GetStingContent()
        {
            return _tableReader.ReadText();
        }
    }
}