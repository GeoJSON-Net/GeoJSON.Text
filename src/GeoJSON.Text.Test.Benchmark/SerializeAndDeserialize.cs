using BenchmarkDotNet.Attributes;
using GeoJSON.Text.Test.Benchmark.Deserialize;
using System;
using System.Collections.Generic;

namespace GeoJSON.Text.Test.Benchmark
{
    [Config(typeof(TestConfig))]
    public class SerializeAndDeserialize
    {
        string fileContents = "";

        // GeoJson.NET
        private readonly Net.Feature.FeatureCollection featureCollectionGeoJsonNET = new Net.Feature.FeatureCollection();

        // GeoJson.Text
        private readonly Text.Feature.FeatureCollection featureCollectionGeoJsonTEXT = new Text.Feature.FeatureCollection();

        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            fileContents = JsonEmbeddedFileReader.GetExpectedJson($"FeatureCollectionLinestring_{N}");

            var coordinates1 = new double[] { 10, 50 };
            var coordinates2 = new double[] { 10, 50 };
            var coordinates3 = new double[] { 10, 50 };
            var coordinates4 = new double[] { 10, 50 };
            var line = new List<IEnumerable<double>> { coordinates1, coordinates2, coordinates3, coordinates4 };
            for (int i = 0; i < N; i++)
            {
                var linestringNET = new Net.Geometry.LineString(line);
                Net.Feature.Feature featureNET = new Net.Feature.Feature(linestringNET);
                featureCollectionGeoJsonNET.Features.Add(featureNET);

                var linestringTEXT = new Text.Geometry.LineString(line);
                Text.Feature.Feature featureTEXT = new Text.Feature.Feature(linestringTEXT);
                featureCollectionGeoJsonTEXT.Features.Add(featureTEXT);
            }
        }

        [Benchmark]
        public Text.Feature.FeatureCollection DeserializeFeatureCollection() => System.Text.Json.JsonSerializer.Deserialize<Text.Feature.FeatureCollection>(fileContents)
            ?? throw new NullReferenceException("Deserialization should not return a null value.");

        [Benchmark]
        public string SerializeFeatureCollection() => System.Text.Json.JsonSerializer.Serialize(fileContents);
    }
}
