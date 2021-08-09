using System;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.EquationExpressions
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

        public IExpression Interpret(IExpressionContext expressionContext)
        {
            if (expressionContext == null) throw new ArgumentNullException(nameof(expressionContext));

            if (_leftExpression.Interpret(expressionContext) is NumberExpressionValue leftExpression &&
                _rightExpression.Interpret(expressionContext) is NumberExpressionValue rightExpression)
                return new NumberExpressionValue(leftExpression.GetValue(expressionContext) /
                                                 rightExpression.GetValue(expressionContext));

            if (_leftExpression.Interpret(expressionContext) is StringExpressionValue &&
                _rightExpression.Interpret(expressionContext) is StringExpressionValue)
                return new StringExpressionValue("#Couldn't divide strings");

            if (_leftExpression.Interpret(expressionContext) is EmptyExpressionValue &&
                _rightExpression.Interpret(expressionContext) is EmptyExpressionValue)
                return new StringExpressionValue("#Couldn't divide empty cells");

            return new StringExpressionValue("#Couldn't divide different types");
        }
    }
}