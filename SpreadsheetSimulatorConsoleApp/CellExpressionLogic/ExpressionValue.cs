using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic
{
    public class ExpressionValue : IExpression, IVariable<IExpression>
    {
        private readonly string _name;
        private readonly IExpression _variable;

        public ExpressionValue(ExpressionVariable expressionVariable)
        {
            _name = expressionVariable.Name;
        }

        public ExpressionValue(IExpression variable)
        {
            _variable = variable;
        }

        public IExpression Interpret(ExpressionContext expressionContext)
        {
            return ((ExpressionValue) expressionContext.GetVariable(_name)).GetValue(expressionContext)
                .Interpret(expressionContext); //Interpreting internal expression  
        }

        public IExpression GetValue(ExpressionContext expressionContext)
        {
            return _variable;
        }
    }
}