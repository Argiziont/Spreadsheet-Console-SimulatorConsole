using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions
{
    public class SubtractExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public SubtractExpression(IExpression left, IExpression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public IExpression Interpret(ExpressionContext expressionContext)
        {
            if (_leftExpression.Interpret(expressionContext) is NumberExpressionValue leftNumberExpression && _rightExpression.Interpret(expressionContext) is NumberExpressionValue rightNumberExpression)
            {
                return new NumberExpressionValue(leftNumberExpression.GetValue(expressionContext) - rightNumberExpression.GetValue(expressionContext));
            }

            if (_leftExpression.Interpret(expressionContext) is StringExpressionValue && _rightExpression.Interpret(expressionContext) is StringExpressionValue)
            {
               return new StringExpressionValue("#Couldn't subtract strings");
            }

            return new StringExpressionValue("#Couldn't subtract different types");
        }
    }
}