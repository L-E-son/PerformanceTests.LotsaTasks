using BenchmarkDotNet.Running;
using PerformanceTests.LotsaTasks;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Benchmarks>();
    }
}