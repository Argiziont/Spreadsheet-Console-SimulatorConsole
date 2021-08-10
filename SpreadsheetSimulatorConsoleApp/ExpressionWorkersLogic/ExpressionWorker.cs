using System;
using System.Collections.Generic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ExpressionWorkersLogic
{
    public class ExpressionWorker
    {
        private readonly IExpressionContext _expressionContext;
        private readonly Queue<string> _cellsQueue;

        public ExpressionWorker(IExpressionContext expressionContext, Queue<string> cellsQueue)
        {
            _expressionContext = expressionContext ?? throw new ArgumentNullException(nameof(expressionContext));
            _cellsQueue = cellsQueue ?? throw new ArgumentNullException(nameof(cellsQueue));
        }

        public void DoWork()
        {
            while (_cellsQueue.Count>0)
            {
                string cellName = _cellsQueue.Dequeue();
                CellState cellState = _expressionContext.GetCellState(cellName);
                switch (cellState)
                {
                    case CellState.Done:
                        continue;
                    case CellState.Processing:
                        _cellsQueue.Enqueue(cellName);
                        break;
                    case CellState.Waiting:

                        _expressionContext.InterpretCell(cellName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}