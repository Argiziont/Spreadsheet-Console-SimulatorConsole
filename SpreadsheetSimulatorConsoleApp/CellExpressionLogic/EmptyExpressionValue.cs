using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic
{
    public class EmptyExpressionValue : IExpression, IVariable<string>
    {
        private readonly string _name;
        private readonly string _variable;

        public EmptyExpressionValue(ExpressionVariable expressionVariable)
        {
            _name = expressionVariable.Name;
        }

        public EmptyExpressionValue()
        {
            _variable = string.Empty;
        }

        public IExpression Interpret(ExpressionContext expressionContext)
        {
            IExpression resultingExpression =
                _name != null ? expressionContext.GetVariable(_name).Interpret(expressionContext) : this;

            return !(resultingExpression is EmptyExpressionValue)
                ? resultingExpression.Interpret(expressionContext)
                : resultingExpression;
        }

        public string GetValue(ExpressionContext expressionContext)
        {
            return _variable;
        }
    }
}