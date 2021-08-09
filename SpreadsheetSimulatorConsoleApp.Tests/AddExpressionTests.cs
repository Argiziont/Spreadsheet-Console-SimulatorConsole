using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class AddExpressionTests
    {
        [Fact]
        public void InterpretAddNumbersCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new NumberExpressionValue(5)));
            context.SetVariable(new ExpressionVariable("A2", new NumberExpressionValue(15)));

            AddExpression expression = new AddExpression(new NumberExpressionValue(new ExpressionVariable("A1")), new NumberExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            NumberExpressionValue castedResult= Assert.IsType<NumberExpressionValue>(result);
            Assert.Equal(20, castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretAddStringsCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetVariable(new ExpressionVariable("A2", new StringExpressionValue("Test")));

            AddExpression expression = new AddExpression(new StringExpressionValue(new ExpressionVariable("A1")), new StringExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't add strings", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretAddEmptyCellsCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new EmptyExpressionValue()));
            context.SetVariable(new ExpressionVariable("A2", new EmptyExpressionValue()));

            AddExpression expression = new AddExpression(new EmptyExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't add empty cells", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretAddDifferentTypes()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetVariable(new ExpressionVariable("A2", new EmptyExpressionValue()));

            AddExpression expression = new AddExpression(new StringExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't add different types", castedResult.GetValue(context));
        }
    }
}