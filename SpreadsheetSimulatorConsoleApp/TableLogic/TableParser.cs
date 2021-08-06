using System.Linq;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;

namespace SpreadsheetSimulatorConsoleApp.TableLogic
{
    public static class TableParser
    {

        //If nothing            => EmptyExpression
        //If number             => NumberExpression
        //If something with '   => StringExpression
        //If something with =   => Expression

        public static IExpression ParseCell(string cellContent)
        {
            if (cellContent==string.Empty)
            {
                return new EmptyExpressionValue();
            }
            if (int.TryParse(cellContent, out int value))
            {
                return new NumberExpressionValue(value);
            }
            if (cellContent.First()=='\'')
            {
                return new StringExpressionValue(cellContent[1..]);
            }
            return SolveEquation(cellContent[1..]);

        }

        //  ‘+’ | ‘-‘ | ‘*’ | ‘/’ 
        private static IExpression SolveEquation(string equation)
        {
            if (equation.Split('-').Length > 1)//We have subtract operation
            {
                string[] splittedEquation = equation.Split('-', 2);
                return new SubtractExpression(SolveEquation(splittedEquation.First()),
                    SolveEquation(splittedEquation.Last()));
            }
            if (equation.Split('+').Length > 1)//We have add operation
            {
                string[] splittedEquation = equation.Split('+', 2);
                return new AddExpression(SolveEquation(splittedEquation.First()),
                    SolveEquation(splittedEquation.Last()));
            }
            if (equation.Split('*').Length > 1)//We have multiply operation
            {
                string[] splittedEquation = equation.Split('*', 2);
                return new MultiplyExpression(SolveEquation(splittedEquation.First()),
                    SolveEquation(splittedEquation.Last()));
            }
            if (equation.Split('/').Length > 1)//We have divide operation
            {
                string[] splittedEquation = equation.Split('/', 2);
                return new DivideExpression(SolveEquation(splittedEquation.First()),
                    SolveEquation(splittedEquation.Last()));
            }
            

            //If it isn't operation, it's just a number
            return int.TryParse(equation, out int value) ? new NumberExpressionValue(value) : new NumberExpressionValue(new ExpressionVariable(equation));
        }
    }
}