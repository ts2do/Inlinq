using System;
using System.Diagnostics;
using System.Linq;

namespace Inlinq.Benchmark
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            const int Iterations = 100;

            while (true)
            {

                Random r = new Random(0);
                var list = Enumerable.Range(0, 100000).OrderBy(x => r.Next()).ToList();

                Stopwatch s = new Stopwatch();
                double a, b;

                var e1 = list
                    //.SelectMany(x => new[] { x, x * 2, x * 3, x * 4 })
                    .Select(x => x + 1)
                    .Where(x => (x & 1) == 0)
                    .SkipWhile(x => x < 100)
                    .Select(x => x - 1)
                    .OrderBy(x => (uint)x >> 4)
                    .ThenByDescending(x => x & 6)
                    .ThenBy(x => x & 1);
                s.Restart();
                for (int i = 0; i < Iterations; ++i)
                    e1.Last();
                a = s.Elapsed.TotalMilliseconds;

                var e2 = list.AsInlinq()
                    //.SelectMany(x => new[] { x, x * 2, x * 3, x * 4 })
                    .Select(x => x + 1)
                    .Where(x => (x & 1) == 0)
                    .SkipWhile(x => x < 100)
                    .Select(x => x - 1)
                    .OrderBy(x => (uint)x >> 4, new Cmp.UInt32Comparer())
                    .ThenByDescending(x => x & 6, new Cmp.Int32Comparer())
                    .ThenBy(x => x & 1, new Cmp.Int32Comparer());
                s.Restart();
                for (int i = 0; i < Iterations; ++i)
                    e2.Last();
                b = s.Elapsed.TotalMilliseconds;

                //var list1 = e1.ToList();
                //var list2 = e2.ToList();
                //e1.SequenceEqual(e2);

                Console.WriteLine("{0}\t{1}", a, b);
                //Console.ReadLine();
            }
        }
    }
}
