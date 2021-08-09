using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class Cell
    {
        public CellState CellState { get; private set; } = CellState.Waiting;

        private IExpression _cellExpression;


        private readonly IExpressionContext _context;

        public Cell(IExpression cellExpression, IExpressionContext context)
        {
            _cellExpression = cellExpression;
            _context = context;
        }

        public IExpression CalculateCell()
        {
            CellState = CellState.Processing;
            _cellExpression=_cellExpression.Interpret(_context);
            CellState = CellState.Done;
            return _cellExpression;
        }
        public IExpression GetExpression()
        {
            return _cellExpression;
        }
    }
}