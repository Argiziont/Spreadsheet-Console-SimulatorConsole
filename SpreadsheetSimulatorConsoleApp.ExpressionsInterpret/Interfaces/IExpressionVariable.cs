namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces
{
    public interface IExpressionVariable
    {
        public string Name { get; }
        public IExpression Expression { get; }
    }
}