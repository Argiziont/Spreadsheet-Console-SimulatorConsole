using System;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues
{
    public class StringExpressionValue : IExpression, IVariable<string>
    {
        private readonly string _name;
        private readonly string _value;

        public StringExpressionValue(IExpressionVariable expressionVariable)
        {
            if (expressionVariable == null) throw new ArgumentNullException(nameof(expressionVariable));

            _name = expressionVariable.Name;
        }

        public StringExpressionValue(string value)
        {
            _value = value;
        }

        public IExpression Interpret(IExpressionContext expressionContext)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));
            IExpression resultingExpression =
                _name != null ? expressionContext.GetVariable(_name).Interpret(expressionContext) : this;

            return !(resultingExpression is StringExpressionValue)
                ? resultingExpression.Interpret(expressionContext)
                : resultingExpression;
        }

        public string GetValue(IExpressionContext expressionContext)
        {
            return _value;
        }
    }
}