using System;
using System.Collections.Generic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.TableLogic;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public static class ContextFactory<TIContext> where TIContext : IExpressionContext, new()
    {
        public static TIContext CreateContext(IEnumerable<Dictionary<string, string>> tableDictionary)
        {
            if (tableDictionary == null) throw new ArgumentNullException(nameof(tableDictionary));

            TIContext expressionContext = new TIContext();

            FillContext(expressionContext, tableDictionary);

            return expressionContext;
        }

        private static void FillContext(IExpressionContext expressionContext,
            IEnumerable<Dictionary<string, string>> tableDictionary)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));
            if (tableDictionary == null) throw new ArgumentNullException(nameof(tableDictionary));

            foreach (var column in tableDictionary)
            foreach ((string cellName, string cellValue) in column)
                expressionContext.SetVariable(new ExpressionVariable(cellName, TableParser.ParseCell(cellValue)));
        }
    }
}