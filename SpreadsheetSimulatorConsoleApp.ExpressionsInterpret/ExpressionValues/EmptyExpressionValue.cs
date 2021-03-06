using System;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues
{
    public class EmptyExpressionValue : IExpression, IVariable<string>
    {
        private readonly string _name;
        private readonly string _value;

        public EmptyExpressionValue(IExpressionVariable expressionVariable)
        {
            _name = expressionVariable.Name;
        }

        public EmptyExpressionValue()
        {
            _value = string.Empty;
        }

        public IExpression Interpret(IExpressionContext expressionContext)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));

            IExpression resultingExpression =
                _name != null ? expressionContext.GetCellExpression(_name).Interpret(expressionContext) : this;

            return !(resultingExpression is EmptyExpressionValue)
                ? resultingExpression.Interpret(expressionContext)
                : resultingExpression;
        }

        public string GetValue(IExpressionContext expressionContext)
        {
            return _value;
        }
    }
}