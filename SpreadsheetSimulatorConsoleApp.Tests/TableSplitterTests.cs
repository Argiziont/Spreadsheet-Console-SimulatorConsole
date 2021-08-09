using System.Collections.Generic;
using System.Linq;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.EquationExpressions;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.ExpressionValues;
using SpreadsheetSimulatorConsoleApp.CellExpressionLogic.Interfaces;
using SpreadsheetSimulatorConsoleApp.ContextLogic;
using SpreadsheetSimulatorConsoleApp.TableLogic;
using Xunit;

namespace SpreadsheetSimulatorConsoleApp.Tests
{
    public class TableSplitterTests
    {
        [Fact]
        public void GetTableSizesReturnsCorrectSizes()
        {
            //Arrange
            const string tableSizes = "5\t10";

            //Act
            TableSizes result = TableSplitter.GetTableSizes(tableSizes);

            //Assert
            Assert.Equal(5, result.Height);
            Assert.Equal(10, result.Width);
        }

        [Fact]
        public void GetTableDictionaryReturnsParsedDictionary()
        {
            //Arrange
            const string tableSizes = "2\t2";
            TableSizes actualSizes = TableSplitter.GetTableSizes(tableSizes);
            const string tableContext = "10\t=A1\r\n" +
                                        "20\t=A2";
            //Act
            var result = TableSplitter.GetTableDictionary(tableContext, actualSizes);
            var enumerableResult = result as Dictionary<string, string>[] ?? result.ToArray();

            //Assert
            Assert.Equal("10", enumerableResult[0]["A1"]);
            Assert.Equal("20", enumerableResult[0]["A2"]);
            Assert.Equal("=A1", enumerableResult[1]["B1"]);
            Assert.Equal("=A2", enumerableResult[1]["B2"]);
        }
    }
}