using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces
{
    public interface IExpression
    {
        IExpression Interpret(IExpressionContext expressionContext);
    }
}