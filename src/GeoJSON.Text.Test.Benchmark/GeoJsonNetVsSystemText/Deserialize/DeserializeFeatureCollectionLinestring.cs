﻿using BenchmarkDotNet.Attributes;
using System;

namespace GeoJSON.Text.Test.Benchmark.Deserialize
{
    [Config(typeof(TestConfig))]
    public class DeserializeFeatureCollectionLinestring
    {
        string fileContents = "";

        [Params(100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            fileContents = JsonEmbeddedFileReader.GetExpectedJson($"FeatureCollectionLinestring_{N}");
        }

        [Benchmark]
        public Net.Feature.FeatureCollection DeserializeNewtonsoft() => Newtonsoft.Json.JsonConvert.DeserializeObject<Net.Feature.FeatureCollection>(fileContents ?? "") 
            ?? throw new NullReferenceException("Deserialization should not return a null value.");


        [Benchmark]
        public Text.Feature.FeatureCollection DeserializeSystemTextJson() => System.Text.Json.JsonSerializer.Deserialize<Text.Feature.FeatureCollection>(fileContents)
            ?? throw new NullReferenceException("Deserialization should not return a null value.");
    }
}
