using System;
using System.Collections.Generic;
using System.Linq;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class CellBasedExpressionContext:IExpressionContext
    {
        private readonly Dictionary<string, Cell> _cells;

        public CellBasedExpressionContext()
        {
            _cells = new Dictionary<string, Cell>();
        }

        public IExpression GetCellExpression(string cellName)
        {
            var test = _cells.Select(t => t.Value.CellState.ToString()).ToList();

            if (cellName == null) throw new ArgumentNullException(nameof(cellName));

            if (_cells.ContainsKey(cellName))
            {
                switch (_cells[cellName].CellState)
                {
                    case CellState.Waiting:
                        return _cells[cellName].CalculateCell();
                    case CellState.Done:
                        return _cells[cellName].GetExpression();
                    case CellState.Processing:
                        return _cells[cellName].GetExpression();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new ArgumentException("#This cell contains non existing reference");
        }

        public void SetExpression(IExpressionVariable expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            if (!_cells.ContainsKey(expression.Name))
                _cells[expression.Name] = new Cell(expression.Expression,this);

        }

        public void InterpretCell(string cellName)
        {
            if (cellName == null) throw new ArgumentNullException(nameof(cellName));

            if (!_cells.ContainsKey(cellName)) return;
            if (_cells[cellName].CellState == CellState.Waiting)
            {
                _cells[cellName].CalculateCell();
            }
               

        }

        public CellState GetCellState(string cellName)
        {
            if (cellName == null) throw new ArgumentNullException(nameof(cellName));

            if (_cells.ContainsKey(cellName))
                return _cells[cellName].CellState;

            throw new ArgumentException("Dictionary doesn't contain this key");
        }
    }
}