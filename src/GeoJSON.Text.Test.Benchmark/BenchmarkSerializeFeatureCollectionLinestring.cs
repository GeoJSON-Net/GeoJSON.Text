using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;

using System.Collections.Generic;
using System.Security.Cryptography;

namespace GeoJSON.Text.Test.Benchmark
{
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RPlotExporter]
    public class BenchmarkSerializeFeatureCollectionLinestring
    {
        // GeoJson.NET
        private Net.Feature.FeatureCollection featureCollectionGeoJsonNET;

        // GeoJson.Text
        private Text.Feature.FeatureCollection featureCollectionGeoJsonTEXT;

        [Params(1000, 10000, 100000)]
        public int N;

        public BenchmarkSerializeFeatureCollectionLinestring()
        {
            var featuresNET = new List<Net.Feature.Feature>();
            var featuresTEXT = new List<Text.Feature.Feature>();

            for (int i = 0; i< N; i++)
            {
                var lineCoordinates = new List<List<double>>
                {
                    new List<double>
                    {
                        -0.26092529296875,
                        51.470691106434884
                    },
                };

                var linestringNET = new Net.Geometry.LineString(lineCoordinates);
                var linestringTEXT = new Text.Geometry.LineString(lineCoordinates);

                var featureNET = new Net.Feature.Feature(linestringNET);
                var featureTEXT = new Text.Feature.Feature(linestringTEXT);

                featuresNET.Add(featureNET);
                featuresTEXT.Add(featureTEXT);
            }

            featureCollectionGeoJsonNET = new Net.Feature.FeatureCollection(featuresNET);
            featureCollectionGeoJsonTEXT = new Text.Feature.FeatureCollection(featuresTEXT);
        }

        [Benchmark]
        public void SerializeNewtonsoft()
        {
            Newtonsoft.Json.JsonConvert.SerializeObject(featureCollectionGeoJsonNET);
        }

        [Benchmark]
        public void SerializeSystemTextJson()
        {
            System.Text.Json.JsonSerializer.Serialize(featureCollectionGeoJsonTEXT);
        }
    }
}
