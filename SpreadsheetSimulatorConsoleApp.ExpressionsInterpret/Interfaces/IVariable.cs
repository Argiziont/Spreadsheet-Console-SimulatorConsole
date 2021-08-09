namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces
{
    public interface IVariable<out T>
    {
        T GetValue(IExpressionContext expressionContext);
    }
}