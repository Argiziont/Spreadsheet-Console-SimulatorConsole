﻿using System.Collections.Generic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.ContextLogic
{
    public class ExpressionContext:IExpressionContext
    {
        private readonly Dictionary<string, IExpression> _variables;
       
        public ExpressionContext()
        {
            _variables = new Dictionary<string, IExpression>();
        }

        public IExpression GetVariable(string expressionName)
        {
            return _variables[expressionName];
        }

        public void SetVariable(ExpressionVariable expressionVariable)
        {
            if (_variables.ContainsKey(expressionVariable.Name))
                _variables[expressionVariable.Name] = expressionVariable.Expression;
            else
                _variables.Add(expressionVariable.Name, expressionVariable.Expression);
        }

    }
}