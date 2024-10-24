using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLoopComparisonBenchmark
{
    [MemoryDiagnoser]
    public class BenchMarks
    {
        private List<int> randomList;

        public BenchMarks()
        {
            int listSize = 1000;
            randomList = GenerateRandomList(listSize);
        }


        [Benchmark]
        public void ForLoop()
        {
            for (int i = 0; i < randomList.Count; i++)
            {
                var temp = randomList[i] * 2;
            }
        }

        [Benchmark]
        public void ForEachLoop()
        {
            foreach (var number in randomList)
            {
                var temp = number * 2;
            }
        }

        [Benchmark]
        public void LambdaExpression()
        {
            randomList.ForEach(item =>
            {
                var temp = item * 2;
            });
        }

        [Benchmark]
        public void WhileLoop()
        {
            int i = 0;
            while (i < randomList.Count)
            {
                var temp = randomList[i] * 2;
                i++;
            }
        }

        [Benchmark]
        public void ParallelForEachLoop()
        {
            Parallel.ForEach(randomList, item =>
            {
                var temp = item * 2;
            });
        }

        [Benchmark]
        public void IterateWithYield()
        {
            foreach (var item in GetItems())
            {
                var temp = item * 2;
            }
        }

        [Benchmark]
        public void SpanForEach()
        {
            foreach (var item in CollectionsMarshal.AsSpan(randomList))
            {
                var temp = item * 2;
            }
        }

        private IEnumerable<int> GetItems()
        {
            foreach (var item in randomList)
            {
                yield return item;
            }
        }

        private List<int> GenerateRandomList(int size)
        {
            Random random = new Random();
            List<int> randomList = new List<int>();
            for (int i = 0; i < size; i++)
            {
                randomList.Add(random.Next(1, 101));
            }

            return randomList;
        }

    }
}
