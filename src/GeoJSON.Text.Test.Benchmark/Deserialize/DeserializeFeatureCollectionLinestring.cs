using BenchmarkDotNet.Attributes;
using System;

namespace GeoJSON.Text.Test.Benchmark.Deserialize
{
    [Config(typeof(TestConfig))]
    [MemoryDiagnoser]
    public class DeserializeFeatureCollectionLinestring
    {
        string fileContents = "";

        [Params(1000, 10000/*, 100000*/)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            fileContents = JsonEmbeddedFileReader.GetExpectedJson($"DeserializeFeatureCollectionLinestring_{N}");
        }

        [Benchmark]
        public Net.Feature.FeatureCollection DeserializeFeatureCollectionNewtonsoft() => Newtonsoft.Json.JsonConvert.DeserializeObject<Net.Feature.FeatureCollection>(fileContents ?? "") 
            ?? throw new NullReferenceException("Deserialization should not return a null value.");


        [Benchmark]
        public Text.Feature.FeatureCollection DeserializeFeatureCollectionSystemTextJson() => System.Text.Json.JsonSerializer.Deserialize<Text.Feature.FeatureCollection>(fileContents)
            ?? throw new NullReferenceException("Deserialization should not return a null value.");
    }
}
