using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class MultiplyExpressionTests
    {
        [Fact]
        public void InterpretMultiplyNumbersCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new NumberExpressionValue(5)));
            context.SetExpression(new ExpressionVariable("A2", new NumberExpressionValue(15)));

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
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetExpression(new ExpressionVariable("A2", new StringExpressionValue("Test")));

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
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new EmptyExpressionValue()));
            context.SetExpression(new ExpressionVariable("A2", new EmptyExpressionValue()));

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
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new StringExpressionValue("Test")));
            context.SetExpression(new ExpressionVariable("A2", new EmptyExpressionValue()));

            MultiplyExpression expression = new MultiplyExpression(new StringExpressionValue(new ExpressionVariable("A1")), new EmptyExpressionValue(new ExpressionVariable("A2")));

            //Act
            IExpression result = expression.Interpret(context);

            //Assert
            StringExpressionValue castedResult = Assert.IsType<StringExpressionValue>(result);
            Assert.Equal("#Couldn't multiply different types", castedResult.GetValue(context));
        }
    }
}