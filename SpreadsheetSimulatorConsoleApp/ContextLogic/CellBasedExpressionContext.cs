using System;
using System.Collections.Generic;
using System.Linq;
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

        public IExpression GetVariable(string expressionName)
        {
            var test = _cells.Select(t => t.Value.CellState);

            if (expressionName == null) throw new ArgumentNullException(nameof(expressionName));

            if (_cells.ContainsKey(expressionName))
            {
                switch (_cells[expressionName].CellState)
                {
                    case CellState.Waiting:
                        return _cells[expressionName].CalculateCell();
                    case CellState.Done:
                        return _cells[expressionName].GetExpression();
                }
            }

            throw new ArgumentException("#This cell contains non existing reference");
        }

        public void SetVariable(IExpressionVariable expressionVariable)
        {
            if (expressionVariable == null) throw new ArgumentNullException(nameof(expressionVariable));

            if (!_cells.ContainsKey(expressionVariable.Name))
                _cells[expressionVariable.Name] = new Cell(expressionVariable.Expression,this);

        }

        public void InterpretVariable(string expressionName)
        {
            if (expressionName == null) throw new ArgumentNullException(nameof(expressionName));

            if (_cells.ContainsKey(expressionName))
                _cells[expressionName].CalculateCell();
        }
    }
}