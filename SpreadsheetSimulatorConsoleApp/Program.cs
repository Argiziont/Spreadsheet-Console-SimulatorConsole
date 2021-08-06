using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.Exceptions;
using SpreadsheetSimulatorConsoleApp.Extensions;
using SpreadsheetSimulatorConsoleApp.TableLogic;

// ReSharper disable CommentTypo

namespace SpreadsheetSimulatorConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            string text = Console.ReadLine();

            TableSizes tableSizes = TableSplitter.GetTableSizes(text);
            StringBuilder tableBuilder = new StringBuilder();
            for (int i = 0; i < tableSizes.Height; i++)
            {
                tableBuilder.AppendLine(Console.ReadLine());
            }

            text = tableBuilder.ToString();

            /*
             *3             4
             *12            =C2          3          'Sample
             *=A1+B1*C1/5   =A2*B1       =B3-C3     'Spread
             *'Test         =4-3         5          'Sheet       
             */



            var tableSet = TableSplitter.GetTableDictionary(text, tableSizes);

            var tableDictionary = tableSet as Dictionary<string, string>[] ?? tableSet.ToArray();// Enumerating dictionary to array

            ExpressionContext expressionContext = ContextFactory<ExpressionContext>.CreateContext(tableDictionary);
            

            PrintOutput(tableDictionary, expressionContext);
        }

        private static void PrintOutput(IEnumerable<Dictionary<string, string>> tableDictionary, ExpressionContext expressionContext)
        {
            StringBuilder tableBuilder = new StringBuilder();

            tableBuilder.AppendLine("\n-------Results-------\n");
            foreach (var column in tableDictionary.Transpose())
            {
                foreach ((string cellName, string _) in column)
                {
                    IExpression contextCell = expressionContext.GetVariable(cellName);

                    try
                    {
                        IExpression
                            interpretedCell =
                                contextCell.Interpret(expressionContext); //Interpreting all cells and outputing to console
                        tableBuilder.Append(ExpressionValueResolver.Resolve(expressionContext, interpretedCell) + "\t");
                    }
                    catch (CircularReferenceException e)
                    {
                        tableBuilder.Append(e.Message + "\t");
                    }
                }

                tableBuilder.AppendLine();
            }

            Console.WriteLine(tableBuilder.ToString());
        }
    }

}
