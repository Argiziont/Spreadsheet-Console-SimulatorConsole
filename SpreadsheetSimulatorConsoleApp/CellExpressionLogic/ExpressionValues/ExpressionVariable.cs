using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.ExpressionValues
{
    public class ExpressionVariable: IExpressionVariable
    {
        public ExpressionVariable(string name, IExpression expression)
        {
            Name = name;
            Expression = expression;
        }

        public ExpressionVariable(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public IExpression Expression { get; }
    }
}