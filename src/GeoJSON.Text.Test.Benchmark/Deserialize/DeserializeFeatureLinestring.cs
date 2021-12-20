using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace GeoJSON.Text.Test.Benchmark.Deserialize
{
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class DeserializeFeatureLinestring
    {
        string fileContents = "";

        [GlobalSetup]
        public void Setup()
        {
            fileContents = JsonEmbeddedFileReader.GetExpectedJson("DeserializeFeatureLinestring");
        }

#pragma warning disable CS8603 // Possible null reference return.
        [Benchmark]
        public Net.Feature.Feature<Net.Geometry.LineString> DeserializeNewtonsoft() => Newtonsoft.Json.JsonConvert.DeserializeObject<Net.Feature.Feature<Net.Geometry.LineString>>(fileContents);


        [Benchmark]
        public Text.Feature.Feature<Text.Geometry.LineString> DeserializeSystemTextJson() => System.Text.Json.JsonSerializer.Deserialize<Feature.Feature<Geometry.LineString>>(fileContents);
#pragma warning restore CS8603 // Possible null reference return.
    }
}
