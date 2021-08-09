﻿using System;
using System.Collections.Generic;
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

        public IExpression GetVariable(string expressionName)
        {
            if (expressionName == null) throw new ArgumentNullException(nameof(expressionName));

            if (_cells.ContainsKey(expressionName))
                return _cells[expressionName];

            throw new ArgumentException("#This cell contains non existing reference");
        }

        public void SetVariable(IExpressionVariable expressionVariable)
        {
            if (expressionVariable == null) throw new ArgumentNullException(nameof(expressionVariable));

            if (_cells.ContainsKey(expressionVariable.Name))
                _cells[expressionVariable.Name] = expressionVariable.Expression;
            else
                _cells.Add(expressionVariable.Name, expressionVariable.Expression);
        }
    }
}