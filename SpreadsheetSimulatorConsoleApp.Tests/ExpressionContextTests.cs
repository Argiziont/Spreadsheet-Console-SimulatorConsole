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
            context.SetVariable(new ExpressionVariable("A1", new EmptyExpressionValue()));

            //Assert
            EmptyExpressionValue castedResults = Assert.IsType<EmptyExpressionValue>(context.GetVariable("A1"));
            Assert.Equal(string.Empty, castedResults.GetValue(context));
        }

        [Fact]
        public void SetVariableReplacesOldVariable()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new EmptyExpressionValue()));

            //Act
            context.SetVariable(new ExpressionVariable("A1", new NumberExpressionValue(5)));

            //Assert
            NumberExpressionValue castedResults = Assert.IsType<NumberExpressionValue>(context.GetVariable("A1"));
            Assert.Equal(5, castedResults.GetValue(context));
        }
    }
}