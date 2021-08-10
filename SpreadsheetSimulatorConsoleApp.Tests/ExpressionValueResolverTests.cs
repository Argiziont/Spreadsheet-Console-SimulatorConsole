using System;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.ExpressionValues;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class ExpressionValueResolverTests
    {
        [Fact]
        public void InterpretNumberWorksCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new NumberExpressionValue(5)));
            
            //Act
            string result = ExpressionValueResolver.Resolve(context, context.GetCellExpression("A1"));

            //Assert
            Assert.Equal("5", result);
        }

        [Fact]
        public void InterpretStringWorksCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new StringExpressionValue("5")));

            //Act
            string result = ExpressionValueResolver.Resolve(context, context.GetCellExpression("A1"));

            //Assert
            Assert.Equal("5", result);
        }

        [Fact]
        public void InterpretEmptyWorksCorrectly()
        {
            //Arrange
            SimpleExpressionContext context = new SimpleExpressionContext();
            context.SetExpression(new ExpressionVariable("A1", new EmptyExpressionValue()));

            //Act
            string result = ExpressionValueResolver.Resolve(context, context.GetCellExpression("A1"));

            //Assert
            Assert.Equal(string.Empty, result);
        }
    }
}