using System;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class ExpressionValueResolverTests
    {
        [Fact]
        public void InterpretNumberWorksCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new NumberExpressionValue(5)));
            
            //Act
            string result = ExpressionValueResolver.Resolve(context, context.GetVariable("A1"));

            //Assert
            Assert.Equal("5", result);
        }

        [Fact]
        public void InterpretStringWorksCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new StringExpressionValue("5")));

            //Act
            string result = ExpressionValueResolver.Resolve(context, context.GetVariable("A1"));

            //Assert
            Assert.Equal("5", result);
        }

        [Fact]
        public void InterpretEmptyWorksCorrectly()
        {
            //Arrange
            ExpressionContext context = new ExpressionContext();
            context.SetVariable(new ExpressionVariable("A1", new EmptyExpressionValue()));

            //Act
            string result = ExpressionValueResolver.Resolve(context, context.GetVariable("A1"));

            //Assert
            Assert.Equal(string.Empty, result);
        }
    }
}