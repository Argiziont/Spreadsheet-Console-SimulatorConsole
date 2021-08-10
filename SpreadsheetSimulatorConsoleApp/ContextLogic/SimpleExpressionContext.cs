using System;
using System.Collections.Generic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class SimpleExpressionContext : IExpressionContext
    {
        private readonly Dictionary<string, IExpression> _cells;

        public SimpleExpressionContext()
        {
            _cells = new Dictionary<string, IExpression>();
        }

        public IExpression GetCellExpression(string cellName)
        {
            if (cellName == null) throw new ArgumentNullException(nameof(cellName));

            if (_cells.ContainsKey(cellName))
                return _cells[cellName];

            throw new ArgumentException("#This cell contains non existing reference");
        }

        public void SetExpression(IExpressionVariable expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            if (_cells.ContainsKey(expression.Name))
                _cells[expression.Name] = expression.Expression;
            else
                _cells.Add(expression.Name, expression.Expression);
        }

        public void InterpretCell(string cellName)
        {
            if (cellName == null) throw new ArgumentNullException(nameof(cellName));

            if (_cells.ContainsKey(cellName))
                _cells[cellName].Interpret(this);
        }

        public CellState GetCellState(string cellName)
        {
            if (cellName == null) throw new ArgumentNullException(nameof(cellName));

            if (_cells.ContainsKey(cellName))
                return CellState.Processing;
            throw new ArgumentException("Dictionary doesn't contain this key");
        }
    }
}