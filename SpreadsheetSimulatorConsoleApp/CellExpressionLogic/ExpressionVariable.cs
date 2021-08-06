using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic
{
    public class ExpressionVariable
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

        public string Name { get; set; }
        public IExpression Expression { get; set; }
    }
}