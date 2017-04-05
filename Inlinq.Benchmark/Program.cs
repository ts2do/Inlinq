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

                Random r = new Random();
                var list = Enumerable.Range(0, 100000).OrderBy(x => r.Next()).ToList();

                Stopwatch s = new Stopwatch();
                double a, b;
                {
                    var e = list
                        //.SelectMany(x => new[] { x, x * 2, x * 3, x * 4 })
                        .Select(x => x + 1)
                        .Where(x => (x & 1) == 0)
                        .SkipWhile(x => x < 100)
                        .Select(x => x - 1)
                        .OrderBy(x => x);
                    s.Restart();
                    for (int i = 0; i < Iterations; ++i)
                        e.Last();
                    a = s.Elapsed.TotalMilliseconds;
                }

                {
                    var e = list.AsInlinq()
                        //.SelectMany(x => new[] { x, x * 2, x * 3, x * 4 })
                        .Select(x => x + 1)
                        .Where(x => (x & 1) == 0)
                        .SkipWhile(x => x < 100)
                        .Select(x => x - 1)
                        .OrderBy(x => x, new Cmp.Int32Comparer());
                    s.Restart();
                    for (int i = 0; i < Iterations; ++i)
                        e.Last();
                    b = s.Elapsed.TotalMilliseconds;
                }
                Console.WriteLine("{0}\t{1}", a, b);
                //Console.ReadLine();
            }
        }
    }
}
