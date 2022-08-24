using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceTests.LotsaTasks
{
    [MemoryDiagnoser]
    [MarkdownExporter]
    public class Benchmarks
    {
        [Params(2, 20, 200, 2_000, 20_000)]
        public ulong N { get; set; }

#pragma warning disable CS8618
        private IEnumerable<Task>[] RunData;
#pragma warning restore CS8618

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.RunData = new[]
            {
                // One per test
                TaskCreator.Create(N),
                TaskCreator.Create(N),
                TaskCreator.Create(N),
            };
        }

        [Benchmark]
        public async Task TaskWhenAnyDrain()
        {
            var testData = RunData[0];

            await testData.DrainTasks();
        }

        [Benchmark]
        public async Task TaskWhenAll()
        {
            var testData = RunData[1];

            await Task.WhenAll(testData);
        }

        [Benchmark]
        public async Task TaskCompletionSource()
        {
            var testData = RunData[2];

            var container = new TaskContainer(testData);

            await container.CompleteTask();
        }
    }
}
