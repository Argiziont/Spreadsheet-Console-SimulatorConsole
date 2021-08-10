using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class SubtractExpressionTests
    {
        [Fact]
        public void InterpretSubtractNumbersCorrectly()
        {
            //Arrange
               SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new NumberExpressionValue(5)));
            context.SetExpression(new ExpressionVariable("A2", new NumberExpressionValue(15)));

            SubtractExpression expression = new SubtractExpression(new NumberExpressionValue(new ExpressionVariable("A1")), new NumberExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            NumberExpressionValue castedResult= Assert.IsType<NumberExpressionValue>(result);
            Assert.Equal(-10, castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretSubtractStringsCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetExpression(new ExpressionVariable("A2", new StringExpressionValue("Test")));

            SubtractExpression expression = new SubtractExpression(new StringExpressionValue(new ExpressionVariable("A1")), new StringExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't subtract strings", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretSubtractEmptyCellsCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new EmptyExpressionValue()));
            context.SetExpression(new ExpressionVariable("A2", new EmptyExpressionValue()));

            SubtractExpression expression = new SubtractExpression(new EmptyExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't subtract empty cells", castedResult.GetValue(context));
        }

        [Fact]
        public void InterpretSubtractDifferentTypes()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetExpression(new ExpressionVariable("A2", new EmptyExpressionValue()));

            SubtractExpression expression = new SubtractExpression(new StringExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't subtract different types", castedResult.GetValue(context));
        }
    }
}