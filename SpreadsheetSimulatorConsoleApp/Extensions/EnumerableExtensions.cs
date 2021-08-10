using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpreadsheetSimulatorConsoleApp.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var enumerators = source.Select(x => x.GetEnumerator()).ToArray();
            try
            {
                while (enumerators.All(x => x.MoveNext())) yield return enumerators.Select(x => x.Current).ToArray();
            }
            finally
            {
                foreach (var enumerator in enumerators)
                    enumerator.Dispose();
            }
        }

        public static IEnumerable<T> Convert2DArrayTo1D<T>(this IEnumerable<T[]> array2D)
        {
            var lst = new List<T>();
            foreach (var a in array2D)
            {
                lst.AddRange(a);
            }
            return lst.ToArray();
        }
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
        public static IEnumerable<IEnumerable<T>> Partition<T>
            (this IEnumerable<T> source, int size)
        {
            T[] array = null;
            int count = 0;
            foreach (T item in source)
            {
                if (array == null)
                {
                    array = new T[size];
                }
                array[count] = item;
                count++;
                if (count == size)
                {
                    yield return new ReadOnlyCollection<T>(array);
                    array = null;
                    count = 0;
                }
            }
            if (array != null)
            {
                Array.Resize(ref array, count);
                yield return new ReadOnlyCollection<T>(array);
            }
        }
    }
}