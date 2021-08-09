using System;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Exceptions;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues
{
    public class NumberExpressionValue : IExpression, IVariable<int>
    {
        private readonly string _name;
        private readonly int _value;

        public NumberExpressionValue(IExpressionVariable expressionVariable)
        {
            if (expressionVariable == null) throw new ArgumentNullException(nameof(expressionVariable));

            _name = expressionVariable.Name;
        }

        public NumberExpressionValue(int value)
        {
            _value = value;
        }

        public IExpression Interpret(IExpressionContext expressionContext)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));

            if (_name != null && this == expressionContext.GetVariable(_name))
                throw new CircularReferenceException("#Cell contains circular reference");

            IExpression resultingExpression =
                _name != null ? expressionContext.GetVariable(_name).Interpret(expressionContext) : this;

            return !(resultingExpression is NumberExpressionValue)
                ? resultingExpression.Interpret(expressionContext)
                : resultingExpression;
        }

        public int GetValue(IExpressionContext expressionContext)
        {
            return _value;
        }
    }
}