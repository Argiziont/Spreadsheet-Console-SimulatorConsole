using System;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic
{
    internal class StringExpressionValue : IExpression, IVariable<string>
    {
        private readonly string _name;
        private readonly string _variable;

        public StringExpressionValue(ExpressionVariable expressionVariable)
        {
            if (expressionVariable == null) throw new ArgumentNullException(nameof(expressionVariable));

            _name = expressionVariable.Name;
        }

        public StringExpressionValue(string variable)
        {
            _variable = variable;
        }

        public IExpression Interpret(ExpressionContext expressionContext)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));
            IExpression resultingExpression =
                _name != null ? expressionContext.GetVariable(_name).Interpret(expressionContext) : this;

            return !(resultingExpression is StringExpressionValue)
                ? resultingExpression.Interpret(expressionContext)
                : resultingExpression;
        }

        public string GetValue(ExpressionContext expressionContext)
        {
            return _variable;
        }
    }
}