using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.Exceptions;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic
{
    public class NumberExpressionValue : IExpression, IVariable<int>
    {
        private readonly string _name;
        private readonly int _variable;
        public NumberExpressionValue(ExpressionVariable expressionVariable)
        {
            _name = expressionVariable.Name;
        }
        public NumberExpressionValue(int variable)
        {
            _variable = variable;
        }
        public IExpression Interpret(ExpressionContext expressionContext)
        {
            if (_name!=null && this == expressionContext.GetVariable(_name))
            {
                throw new CircularReferenceException("#Cell contains circular reference");
            }

            IExpression resultingExpression = _name != null ? expressionContext.GetVariable(_name).Interpret(expressionContext) : this;

            return !(resultingExpression is NumberExpressionValue) ? resultingExpression.Interpret(expressionContext) : resultingExpression;
        }

        public int GetValue(ExpressionContext expressionContext)
        {
            return  _variable;
        }
    }
}