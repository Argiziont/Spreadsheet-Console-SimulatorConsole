using System.Collections.Generic;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces
{
    public interface ITableReader
    {
        public IEnumerable<Dictionary<string,string>> ReadText();
    }
}