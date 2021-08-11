using SpreadsheetSimulatorConsoleApp.ContextLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class TableWriter
    {
        private readonly ITableWriter _tableReader;

        public TableWriter(ITableWriter tableReader)
        {
            _tableReader = tableReader;
        }

        public void WriteStingContent()
        {
            _tableReader.WriteText();
        }
    }
}