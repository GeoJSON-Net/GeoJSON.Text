using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace GeoJSON.Text.Test.Benchmark.Deserialize
{
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class DeserializeFeatureCollectionLinestring
    {
        string fileContents = "";

        [Params(1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            fileContents = JsonEmbeddedFileReader.GetExpectedJson($"DeserializeFeatureCollectionLinestring_{N}");
        }

#pragma warning disable CS8603 // Possible null reference return.
        [Benchmark]
        public Net.Feature.FeatureCollection DeserializeNewtonsoft() => Newtonsoft.Json.JsonConvert.DeserializeObject<Net.Feature.FeatureCollection>(fileContents);


        [Benchmark]
        public Text.Feature.FeatureCollection DeserializeSystemTextJson() => System.Text.Json.JsonSerializer.Deserialize<Text.Feature.FeatureCollection>(fileContents);
#pragma warning restore CS8603 // Possible null reference return.
    }
}
