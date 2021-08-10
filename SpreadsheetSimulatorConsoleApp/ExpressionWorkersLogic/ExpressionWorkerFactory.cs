using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpreadsheetSimulatorConsoleApp.ExpressionsInterpret.Interfaces;
using SpreadsheetSimulatorConsoleApp.Extensions;

namespace SpreadsheetSimulatorConsoleApp.ExpressionWorkersLogic
{
    public class ExpressionWorkerFactory
    {
        private readonly IExpressionContext _context;
        private readonly IEnumerable<IEnumerable<string>> _cells;
        private readonly int _workersNumber;
        public ExpressionWorkerFactory(IExpressionContext context, IEnumerable<string> cells)
        {
            _context = context;
            int threads = Process.GetCurrentProcess().Threads.Count;
            string[] enumerableCells = cells as string[] ?? cells.ToArray();
            _workersNumber = threads > enumerableCells.Count() / 2 ? threads/2 : 2;

            _cells = enumerableCells.Partition(_workersNumber);

        }

        public IEnumerable<ExpressionWorker> CreateWorkers()
        {
            var workerArray = new ExpressionWorker[_cells.Count()];
            for (int index = 0; index < workerArray.Length; index++)
            {
                var cells = new Queue<string>(_cells.ToArray()[index]);
                workerArray[index] = new ExpressionWorker(_context, cells);
            }

            return workerArray;
        }

        public void StartWork(IEnumerable<ExpressionWorker> expressionWorkers)
        {
            var tasks = expressionWorkers.Select(expressionWorker => Task.Run((expressionWorker.DoWork))).ToArray();

            Task.WaitAll(tasks);
        }
    }
}