
using BenchmarkDotNet.Running;
using GeoJSON.Text.Test.Benchmark.Deserialize;
using GeoJSON.Text.Test.Benchmark.Serialize;

namespace GeoJSON.Text.Test.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[] {
                typeof(SerializeFeatureCollectionLinestring),
                typeof(SerializeFeatureLinestring),
                typeof(DeserializeFeatureLinestring),
                typeof(DeserializeFeatureCollectionLinestring),
                typeof(SerializeAndDeserialize),
            });
            switcher.Run(args);
        }
    }
}


