using System;
using SpreadsheetSimulatorConsoleApp.TableLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.TableLogic
{
    public class ConsoleWriter : IWritable
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}