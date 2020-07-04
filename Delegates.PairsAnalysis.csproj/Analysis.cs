using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.PairsAnalysis
{
    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data)
        {
            if (data.Length < 2)
                throw new ArgumentException();
            return data.Pairs().Select(e => (e.Item2 - e.Item1).TotalSeconds).MaxIndex();
        }

        public static int MaxIndex<T>(this IEnumerable<T> rowData) where T : IComparable, new()
        {
            var maxIndex = 0;
            var currentIndex = 1;
            var isFirstElement = true;
            T exElement = new T();

            foreach (var val in rowData)
            {
                
                if (isFirstElement)
                {
                    exElement = val;
                    isFirstElement = false;
                    continue;
                }
                if (val.CompareTo(exElement) > 0)
                {
                    maxIndex = currentIndex;
                    exElement = val;
                }
                
                currentIndex++;
            }
            if (isFirstElement)
                throw new ArgumentException();
            return maxIndex;
        }
        public static double FindAverageRelativeDifference(params double[] data)
        {
            if (data.Length < 2)
                throw new ArgumentException();
            return data.Pairs().Select(e => (e.Item2 - e.Item1) / e.Item1).Sum() / (data.Length - 1);
            //return data
            //    .Pairs()
            //    .Aggregate(new Tuple<double, double>(0, 1), (cur, next) => new Tuple<double, double>((next.Item2 - next.Item1) / next.Item1 + cur.Item1, 0))//((cur.Item2 - cur.Item1) / cur.Item1 + next.Item1, 0))
            //    .Item1 / (data.Length - 1); //new Tuple<double, double>(1, 1),
        }

        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> dates) where T : new()
        {
            //T exValue = dates.First();
            var isFirstElement = true;
            var exValue = default(T);
            foreach (T val in dates)
            {
                if (isFirstElement)
                {
                    exValue = val;
                    isFirstElement = false;
                    continue;
                }
                yield return new Tuple<T, T>(exValue, val);
                exValue = val;
            }
        }
    }
}
