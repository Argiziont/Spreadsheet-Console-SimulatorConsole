using System;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Exceptions
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException()
        {
        }

        public CircularReferenceException(string message) : base(message)
        {
        }
    }
}