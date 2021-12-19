
using BenchmarkDotNet.Running;
using GeoJSON.Text.Test.Benchmark;

namespace GeoJSON.Text.Test.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<BenchmarkDeserialize>();
            //var summary2 = BenchmarkRunner.Run<BenchmarkSerializeFeatureCollectionLinestring>();
            var summary2 = BenchmarkRunner.Run<BenchmarkSerializeFeatureLinestring>();
        }
    }
}


