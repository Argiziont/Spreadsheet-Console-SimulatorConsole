using SpreadsheetSimulatorConsoleApp.ContextLogic;

namespace SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces
{
    public interface IVariable<out T>
    {
        T GetValue(IExpressionContext expressionContext);
    }
}