using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.Extensions;
using SpreadsheetSimulatorConsoleApp.TableLogic;

// ReSharper disable CommentTypo

namespace SpreadsheetSimulatorConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {

            string text =
                "3\t4\n" +
                "12\t=C2\t3\t'Sample\n" +
                "=A1+B1*C1/5\t=A2*B1\t=B3-C3\t'Spread\n" +
                "'Test\t=4-3\t5\t'Test";

            /*
             *3             4
             *12            =C2          3      'Sample
             *=A1+B1*C1/5   =A2*B1       =B3-C3 'Spread
             *'Test         =4-3         5       
             */

            //

            TableSizes tableSizes = TableSplitter.GetTableSizes(text);

            var tableSet = TableSplitter.GetTableDictionary(text, tableSizes);

            var tableDictionary = tableSet as Dictionary<string, string>[] ?? tableSet.ToArray();// Enumerating dictionary to array

            ExpressionContext expressionContext = ContextFactory<ExpressionContext>.CreateContext(tableDictionary);
            
            StringBuilder tableBuilder = new StringBuilder();

            foreach (var column in tableDictionary.Transpose())
            {
                foreach ((string cellName, string _) in column)
                {
                    IExpression interpretedCell = expressionContext.GetVariable(cellName).Interpret(expressionContext);//Interpreting all cells and outputing to console

                    tableBuilder.Append(ExpressionValueResolver.Resolve(expressionContext, interpretedCell) + "\t");

                }
                tableBuilder.AppendLine();
            }
            Console.WriteLine(tableBuilder.ToString());
        }
       
    }

}
