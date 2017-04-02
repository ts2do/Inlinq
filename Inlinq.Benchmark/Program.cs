using System;
using System.Diagnostics;
using System.Linq;

namespace Inlinq.Benchmark
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            const int Iterations = 10000;

            var list = Inlinqer.Range(0, 2500).ToList();

            while (true)
            {

                Stopwatch s = new Stopwatch();
                double a, b, c;
                {
                    var e = list
                        //.SelectMany(x => new[] { x, x * 2, x * 3, x * 4 });
                        //.Select(x => x + 1)
                        //.Where(x => (x & 1) == 0)
                        //.SkipWhile(x => x < 100)
                        //.Select(x => x - 1)
                        .OrderBy(x => x);
                    s.Restart();
                    for (int i = 0; i < Iterations; ++i)
                        e.Last();
                    a = s.Elapsed.TotalMilliseconds;
                }

                {
                    var e = list.AsInlinq()
                        //.SelectMany(x => new[] { x, x * 2, x * 3, x * 4 }.AsInlinq());
                        //.Select(x => x + 1)
                        //.Where(x => (x & 1) == 0)
                        //.SkipWhile(x => x < 100)
                        //.Select(x => x - 1)
                        .OrderBy(x => x);
                    s.Restart();
                    for (int i = 0; i < Iterations; ++i)
                        e.Last();
                    b = s.Elapsed.TotalMilliseconds;
                }

                {
                    var e = list.AsInlinq()
                        //.SelectMany(new SelectMany());
                        //.Select(new Plus(1))
                        //.Where(new IsEven())
                        //.SkipWhile(new IsLessThan(100))
                        //.Select(new Plus(-1))
                        .OrderBy(new IdentityFunctor<int>());
                    s.Restart();
                    for (int i = 0; i < Iterations; ++i)
                        e.Last();
                    c = s.Elapsed.TotalMilliseconds;
                }
                Console.WriteLine("{0}\t{1}\t{2}", a, b, c);
                Console.ReadLine();
            }
        }

        private struct SelectMany : ITFunctor<SelectMany, int, int[]>
        {
            public int[] Invoke(int x) => new[] { x, x * 2, x * 3, x * 4 };
            public SelectMany Unwrap() => this;
        }

        private struct Plus : ITFunctor<Plus, int, int>
        {
            private int i;
            public Plus(int i) => this.i = i;
            public int Invoke(int arg) => arg + i;
            public Plus Unwrap() => this;
        }

        private struct IsEven : IPredicate<int>
        {
            public bool Invoke(int arg) => (arg & 1) == 0;
        }

        private struct IsLessThan : IPredicate<int>
        {
            private int i;
            public IsLessThan(int i) => this.i = i;
            public bool Invoke(int arg) => arg < i;
        }
    }
}
