using System;

namespace SpreadsheetSimulatorConsoleApp.Exceptions
{
    public class CircularReferenceException:Exception
    {
        public CircularReferenceException()
        {
            
        }

        public CircularReferenceException(string message):base(message)
        {
            
        }
    }
}