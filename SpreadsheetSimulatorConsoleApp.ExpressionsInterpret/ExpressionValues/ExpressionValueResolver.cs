using System;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues
{
    public static class ExpressionValueResolver
    {
        public static string Resolve(IExpressionContext expressionContext, IExpression expression)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            if (expression.Interpret(expressionContext) is NumberExpressionValue numberValue)
                return numberValue.GetValue(expressionContext).ToString();
            if (expression.Interpret(expressionContext) is StringExpressionValue stringValue)
                return stringValue.GetValue(expressionContext);
            if (expression.Interpret(expressionContext) is EmptyExpressionValue) 
                return string.Empty;

            return "#Couldn't solve cell!";
        }
    }
}