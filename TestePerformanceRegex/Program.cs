using BenchmarkDotNet.Running;

namespace TestePerformanceRegex
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TesteRegex>();
        }
    }
}