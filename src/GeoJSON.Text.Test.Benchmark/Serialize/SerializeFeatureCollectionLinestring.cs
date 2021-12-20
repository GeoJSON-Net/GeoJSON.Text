using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;

namespace GeoJSON.Text.Test.Benchmark.Serialize
{
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class SerializeFeatureCollectionLinestring
    {
        // GeoJson.NET
        private Net.Feature.FeatureCollection featureCollectionGeoJsonNET = new Net.Feature.FeatureCollection();

        // GeoJson.Text
        private Text.Feature.FeatureCollection featureCollectionGeoJsonTEXT = new Text.Feature.FeatureCollection();

        [Params(1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var coordinates1 = new double[] { 10, 50 };
            var coordinates2 = new double[] { 10, 50 };
            var coordinates3 = new double[] { 10, 50 };
            var coordinates4 = new double[] { 10, 50 };
            var line = new List<IEnumerable<double>> { coordinates1, coordinates2, coordinates3, coordinates4 };
            for (int i = 0; i< N; i++)
            {
                var linestringNET = new Net.Geometry.LineString(line);
                GeoJSON.Net.Feature.Feature featureNET = new Net.Feature.Feature(linestringNET);
                featureCollectionGeoJsonNET.Features.Add(featureNET);

                var linestringTEXT = new Text.Geometry.LineString(line);
                Text.Feature.Feature featureTEXT = new Text.Feature.Feature(linestringTEXT);
                featureCollectionGeoJsonTEXT.Features.Add(featureTEXT);
            }
        }

        [Benchmark]
        public string SerializeNewtonsoft() => Newtonsoft.Json.JsonConvert.SerializeObject(featureCollectionGeoJsonNET);

        [Benchmark]
        public string SerializeSystemTextJson() => System.Text.Json.JsonSerializer.Serialize(featureCollectionGeoJsonTEXT);
    }
}
