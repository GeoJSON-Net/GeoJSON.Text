
using BenchmarkDotNet.Running;
using GeoJSON.Text.Test.Benchmark;

namespace GeoJSON.Text.Test.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Serialize
            var summary1 = BenchmarkRunner.Run<BenchmarkSerializeFeatureCollectionLinestring>();
            var summary2 = BenchmarkRunner.Run<BenchmarkSerializeFeatureLinestring>();
        }
    }
}


