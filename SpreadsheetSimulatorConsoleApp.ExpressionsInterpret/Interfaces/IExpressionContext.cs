namespace SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces
{
    public interface IExpressionContext
    {
        public IExpression GetVariable(string expressionName);
        public void SetVariable(IExpressionVariable expressionVariable);
        public void InterpretVariable(string expressionName);
    }
}