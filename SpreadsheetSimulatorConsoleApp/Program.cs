using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using SpreadsheetSimulatorConsoleApp.ExpressionWorkersLogic;
using SpreadsheetSimulatorConsoleApp.Extensions;
using SpreadsheetSimulatorConsoleApp.TableLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Readers;
using SpreadsheetSimulatorConsoleApp.ContextLogic.Writers;

namespace SpreadsheetSimulatorConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {

            TableReader tableReader;
            if (!args.Any())
                Console.WriteLine("Specify way to read/write table: \n" +
                                  "\t-console       :for console input\n" +
                                  "\t-file [path]   :for processing table from file");

            if (args.Contains("-console"))
                tableReader = new TableReader(new ConsoleReader());
            else if (args.Contains("-file"))
                tableReader = new TableReader(new FileReader(args.Last()));
            else
                return;

            var tableSet = tableReader.GetStingContent();

            var tableDictionary =
                tableSet as Dictionary<string, string>[] ?? tableSet.ToArray(); // Enumerating dictionary to array

            CellBasedExpressionContext expressionContext = ContextFactory<CellBasedExpressionContext>.CreateContext(tableDictionary);

            ExpressionWorkerFactory expressionWorkerFactory = new ExpressionWorkerFactory(expressionContext, tableDictionary.Select(dictionary => dictionary.Select(pair => pair.Key).ToArray()).Convert2DArrayTo1D());
            expressionWorkerFactory.StartWork(expressionWorkerFactory.CreateWorkers());

            var interpretResults = CalculateOutput(tableDictionary, expressionContext);

            TableWriter  tableWriter;
            if (args.Contains("-console"))
                tableWriter = new TableWriter(new ConsoleWriter(interpretResults));
            else if (args.Contains("-file"))
                tableWriter = new TableWriter(new FileWriter(interpretResults, args.Last()));
            else
                return;

            tableWriter.WriteStingContent();
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
    }
}