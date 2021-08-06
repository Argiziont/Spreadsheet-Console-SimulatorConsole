using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic
{
    public static class ExpressionValueResolver
    {
        public static string Resolve(ExpressionContext expressionContext, IExpression expression)
        {
            if (expression.Interpret(expressionContext) is NumberExpressionValue numberValue)
                return numberValue.GetValue(expressionContext).ToString();

            if (expression.Interpret(expressionContext) is StringExpressionValue stringValue)
                return stringValue.GetValue(expressionContext);
            if (expression.Interpret(expressionContext) is EmptyExpressionValue) return string.Empty;

            return "#Couldn't solve cell !";
        }
    }
}