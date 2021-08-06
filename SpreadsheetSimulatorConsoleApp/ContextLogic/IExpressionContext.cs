using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public interface IExpressionContext
    {
        public IExpression GetVariable(string expressionName);
        public void SetVariable(ExpressionVariable expressionVariable);
    }
}