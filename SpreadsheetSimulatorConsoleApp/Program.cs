using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.Exceptions;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using SpreadsheetSimulatorConsoleApp.Extensions;
using SpreadsheetSimulatorConsoleApp.TableLogic;

namespace SpreadsheetSimulatorConsoleApp
{

    /*
     *  =A2+B1  =B2
     *  =B2+B1  =7
     */
    internal static class Program
    {
        private static void Main()
        {
            string text = Console.ReadLine();
            
            TableSizes tableSizes;

            try
            {
                tableSizes = TableSplitter.GetTableSizes(text);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("#Wrong size arguments");
                return;
            }

            StringBuilder tableBuilder = new StringBuilder();
            for (int i = 0; i < tableSizes.Height; i++) tableBuilder.AppendLine(Console.ReadLine());

            text = tableBuilder.ToString();

            var tableSet = TableSplitter.GetTableDictionary(text, tableSizes);

            var tableDictionary =
                tableSet as Dictionary<string, string>[] ?? tableSet.ToArray(); // Enumerating dictionary to array

            SimpleExpressionContext simpleExpressionContext = ContextFactory<SimpleExpressionContext>.CreateContext(tableDictionary);


            var interpretResults=CalculateOutput(tableDictionary, simpleExpressionContext);
            PrintOutput(interpretResults);
        }

        private static IEnumerable<Dictionary<string, string>> CalculateOutput(Dictionary<string, string>[] tableDictionary,
            SimpleExpressionContext simpleExpressionContext)
        {
            if (tableDictionary == null) throw new ArgumentNullException(nameof(tableDictionary));
            if (simpleExpressionContext == null) throw new ArgumentNullException(nameof(simpleExpressionContext));

            var resultingTableDictionary = tableDictionary;

            for (int i = 0; i < resultingTableDictionary.Count(); i++)
            {
                int iterator = i;
                Parallel.ForEach(resultingTableDictionary[i].Keys.ToList(), cellName =>
                {
                    IExpression contextCell = simpleExpressionContext.GetVariable(cellName);
                    try
                    {
                        resultingTableDictionary[iterator][cellName] = ExpressionValueResolver.Resolve(simpleExpressionContext,
                            contextCell.Interpret(simpleExpressionContext));
                    }
                    catch (CircularReferenceException e)
                    {
                        resultingTableDictionary[iterator][cellName] = e.Message;
                    }
                    catch (ArgumentException e)
                    {
                        resultingTableDictionary[iterator][cellName] = e.Message;
                    }

                   
                });
            }

            return resultingTableDictionary;
        }
        private static void PrintOutput(IEnumerable<Dictionary<string, string>> tableDictionary)
        {
            if (tableDictionary == null) throw new ArgumentNullException(nameof(tableDictionary));


            StringBuilder tableBuilder = new StringBuilder();

            tableBuilder.AppendLine("\n-------Results-------\n");
            foreach (var column in tableDictionary.Transpose())
            {
                foreach (var cell in column)
                {
                    tableBuilder.Append(cell.Value + "\t");
                }

                tableBuilder.AppendLine();
            }

            Console.WriteLine(tableBuilder.ToString());
        }
    }
}