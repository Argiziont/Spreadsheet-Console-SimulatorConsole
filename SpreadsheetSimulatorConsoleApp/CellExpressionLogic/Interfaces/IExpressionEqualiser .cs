namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces
{
    public interface IExpressionSolver
    {
        IExpression Solve(IExpression leftExpression, IExpression rightExpression);
    }
}