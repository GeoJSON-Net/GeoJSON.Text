
using BenchmarkDotNet.Running;
using GeoJSON.Text.Test.Benchmark.Deserialize;
using GeoJSON.Text.Test.Benchmark.Serialize;

namespace GeoJSON.Text.Test.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Serialize
            var summary1 = BenchmarkRunner.Run<SerializeFeatureCollectionLinestring>();
            var summary2 = BenchmarkRunner.Run<SerializeFeatureLinestring>();

            // Deserialize
            var summary3 = BenchmarkRunner.Run<DeserializeFeatureLinestring>();
            var summary4 = BenchmarkRunner.Run<DeserializeFeatureCollectionLinestring>();
        }
    }
}


