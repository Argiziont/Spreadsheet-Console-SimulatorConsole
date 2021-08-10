using System;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Exceptions;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class Cell
    {
        public CellState CellState { get; set; } = CellState.Waiting;

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
            try
            {
                _cellExpression = _cellExpression.Interpret(_context);

            }
            catch (CircularReferenceException e)
            {
                _cellExpression =new StringExpressionValue(e.Message) ;
            }
            catch (ArgumentException e)
            {
                _cellExpression = new StringExpressionValue(e.Message);
            }

          
            CellState = CellState.Done;
            return _cellExpression;
        }
        public IExpression GetExpression()
        {
            return _cellExpression;
        }
    }
}