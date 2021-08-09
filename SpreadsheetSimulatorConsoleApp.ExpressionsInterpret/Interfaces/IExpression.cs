namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces
{
    public interface IExpression
    {
        IExpression Interpret(IExpressionContext expressionContext);
    }
}