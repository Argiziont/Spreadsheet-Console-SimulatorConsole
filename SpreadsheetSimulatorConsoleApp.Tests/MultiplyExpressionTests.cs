using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class MultiplyExpressionTests
    {
        [Fact]
        public void InterpretMultiplyNumbersCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new NumberExpressionValue(5)));
            context.SetVariable(new ExpressionVariable("A2", new NumberExpressionValue(15)));

            MultiplyExpression expression = new MultiplyExpression(new NumberExpressionValue(new ExpressionVariable("A1")), new NumberExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            NumberExpressionValue castedResult= Assert.IsType<NumberExpressionValue>(result);
            Assert.Equal(75, castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretMultiplyStringsCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetVariable(new ExpressionVariable("A2", new StringExpressionValue("Test")));

            MultiplyExpression expression = new MultiplyExpression(new StringExpressionValue(new ExpressionVariable("A1")), new StringExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't multiply strings", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretMultiplyEmptyCellsCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new EmptyExpressionValue()));
            context.SetVariable(new ExpressionVariable("A2", new EmptyExpressionValue()));

            MultiplyExpression expression = new MultiplyExpression(new EmptyExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't multiply empty cells", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretMultiplyDifferentTypes()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetVariable(new ExpressionVariable("A2", new EmptyExpressionValue()));

            MultiplyExpression expression = new MultiplyExpression(new StringExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't multiply different types", castedResult.GetValue(context));
        }
    }
}