using System;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions
{
    public class DivideExpression : IExpression
    {
        private readonly IExpression _leftExpression;
        private readonly IExpression _rightExpression;

        public DivideExpression(IExpression left, IExpression right)
        {
            _leftExpression = left ?? throw new ArgumentNullException(nameof(left));
            _rightExpression = right ?? throw new ArgumentNullException(nameof(right));
        }

        public IExpression Interpret(ExpressionContext expressionContext)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));

            if (_leftExpression.Interpret(expressionContext) is NumberExpressionValue leftExpression &&
                _rightExpression.Interpret(expressionContext) is NumberExpressionValue rightExpression)
                return new NumberExpressionValue(leftExpression.GetValue(expressionContext) /
                                                 rightExpression.GetValue(expressionContext));


            return new StringExpressionValue("#Couldn't subtract different types");
        }
    }
}