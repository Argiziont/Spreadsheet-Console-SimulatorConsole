using System;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class ExpressionContextTests
    {
        [Fact]
        public void SetVariableCreatesNewVariable()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();

            //Act
            context.SetExpression(new ExpressionVariable("A1", new EmptyExpressionValue()));

            //Assert
            EmptyExpressionValue castedResults = Assert.IsType<EmptyExpressionValue>(context.GetCellExpression("A1"));
            Assert.Equal(string.Empty, castedResults.GetValue(context));
        }

        [Fact]
        public void SetVariableReplacesOldVariable()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new EmptyExpressionValue()));

            //Act
            context.SetExpression(new ExpressionVariable("A1", new NumberExpressionValue(5)));

            //Assert
            NumberExpressionValue castedResults = Assert.IsType<NumberExpressionValue>(context.GetCellExpression("A1"));
            Assert.Equal(5, castedResults.GetValue(context));
        }
    }
}