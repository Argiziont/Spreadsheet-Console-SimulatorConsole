using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;

namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces
{
    public interface IExpressionContext
    {
        public IExpression GetCellExpression(string cellName);
        public void SetExpression(IExpressionVariable expression);
        public void InterpretCell(string cellName);
        public CellState GetCellState(string cellName);
    }
}