using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class DivideExpressionTests
    {
        [Fact]
        public void InterpretDivideNumbersCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new NumberExpressionValue(15)));
            context.SetVariable(new ExpressionVariable("A2", new NumberExpressionValue(5)));

            DivideExpression expression = new DivideExpression(new NumberExpressionValue(new ExpressionVariable("A1")), new NumberExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            NumberExpressionValue castedResult= Assert.IsType<NumberExpressionValue>(result);
            Assert.Equal(3, castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretDivideStringsCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetVariable(new ExpressionVariable("A2", new StringExpressionValue("Test")));

            DivideExpression expression = new DivideExpression(new StringExpressionValue(new ExpressionVariable("A1")), new StringExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't divide strings", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretDivideEmptyCellsCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new EmptyExpressionValue()));
            context.SetVariable(new ExpressionVariable("A2", new EmptyExpressionValue()));

            DivideExpression expression = new DivideExpression(new EmptyExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't divide empty cells", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretDivideDifferentTypes()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetVariable(new ExpressionVariable("A2", new EmptyExpressionValue()));

            DivideExpression expression = new DivideExpression(new StringExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't divide different types", castedResult.GetValue(context));
        }
    }
}