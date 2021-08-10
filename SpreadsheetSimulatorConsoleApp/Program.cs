using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using SpreadsheetSimulatorConsoleApp.ExpressionWorkersLogic;
using SpreadsheetSimulatorConsoleApp.Extensions;
using SpreadsheetSimulatorConsoleApp.TableLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetSimulatorConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            string text = "4\t4";
            //Console.ReadLine();

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

            //StringBuilder tableBuilder = new StringBuilder();
            //for (int i = 0; i < tableSizes.Height; i++) tableBuilder.AppendLine(Console.ReadLine());

            text = "=A2+C2\t=B2\t=A1+7\t'Test\r\n" +
                   "=B2+D4\t28\t=58*2\t=A2\r\n" +
                   "=A2*2\t=C2\t=D2+D3\t=A3+A2\r\n" +
                   "17\t=B3\t=A1+7\t'Test";
            //tableBuilder.ToString();

            var tableSet = TableSplitter.GetTableDictionary(text, tableSizes);

            var tableDictionary =
                tableSet as Dictionary<string, string>[] ?? tableSet.ToArray(); // Enumerating dictionary to array

            CellBasedExpressionContext expressionContext = ContextFactory<CellBasedExpressionContext>.CreateContext(tableDictionary);

            ExpressionWorkerFactory expressionWorkerFactory = new ExpressionWorkerFactory(expressionContext, tableDictionary.Select(dictionary => dictionary.Select(pair => pair.Key).ToArray()).Convert2DArrayTo1D());
            expressionWorkerFactory.StartWork(expressionWorkerFactory.CreateWorkers());

            var interpretResults = CalculateOutput(tableDictionary, expressionContext);

            PrintOutput(interpretResults);
        }

        private static IEnumerable<Dictionary<string, string>> CalculateOutput(Dictionary<string, string>[] tableDictionary,
            IExpressionContext simpleExpressionContext)
        {
            if (tableDictionary == null) throw new ArgumentNullException(nameof(tableDictionary));
            if (simpleExpressionContext == null) throw new ArgumentNullException(nameof(simpleExpressionContext));

            var resultingTableDictionary = tableDictionary;

            for (int i = 0; i < resultingTableDictionary.Count(); i++)
            {
                int iterator = i;
                foreach (string cellName in resultingTableDictionary[i].Keys.ToList())
                {

                    IExpression contextCell = simpleExpressionContext.GetCellExpression(cellName);

                    resultingTableDictionary[iterator][cellName] = ExpressionValueResolver.Resolve(simpleExpressionContext,
                        contextCell);
                }
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