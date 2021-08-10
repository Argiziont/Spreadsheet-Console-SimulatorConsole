using System;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Exceptions
{
    public class CellInProgressException : Exception
    {
        public CellInProgressException()
        {
        }

        public CellInProgressException(string message) : base(message)
        {
        }
    }
}