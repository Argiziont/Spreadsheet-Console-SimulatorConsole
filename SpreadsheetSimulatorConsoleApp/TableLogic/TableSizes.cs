namespace SpreadsheetSimulatorConsoleApp.TableLogic
{
    public class TableSizes
    {
        public TableSizes(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public TableSizes()
        {
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}