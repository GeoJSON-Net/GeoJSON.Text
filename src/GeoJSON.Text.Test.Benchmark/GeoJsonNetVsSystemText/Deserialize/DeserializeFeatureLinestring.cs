using BenchmarkDotNet.Attributes;
using System;

namespace GeoJSON.Text.Test.Benchmark.Deserialize
{
    [Config(typeof(TestConfig))]
    [MemoryDiagnoser]
    public class DeserializeFeatureLinestring
    {
        string fileContents = "";

        [GlobalSetup]
        public void Setup()
        {
            fileContents = JsonEmbeddedFileReader.GetExpectedJson("FeatureLinestring");
        }

        [Benchmark]
        public Net.Feature.Feature<Net.Geometry.LineString> DeserializeNewtonsoft() => Newtonsoft.Json.JsonConvert.DeserializeObject<Net.Feature.Feature<Net.Geometry.LineString>>(fileContents) 
            ?? throw new NullReferenceException("Deserialization should not return a null value.");


        [Benchmark]
        public Text.Feature.Feature<Text.Geometry.LineString> DeserializeSystemTextJson() => System.Text.Json.JsonSerializer.Deserialize<Feature.Feature<Geometry.LineString>>(fileContents)
            ?? throw new NullReferenceException("Deserialization should not return a null value.");
    }
}
