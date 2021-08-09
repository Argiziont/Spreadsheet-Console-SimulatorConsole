using System;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.ExpressionValues
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
                _name != null ? expressionContext.GetVariable(_name).Interpret(expressionContext) : this;

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