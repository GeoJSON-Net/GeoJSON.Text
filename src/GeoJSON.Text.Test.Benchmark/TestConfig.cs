using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GeoJSON.Text.Test.Benchmark
{
    public class TestConfig : ManualConfig
    {
        public TestConfig()
        {
            AddJob(Job.Default.WithRuntime(CoreRuntime.Core31)
                .WithLaunchCount(1)     // benchmark process will be launched only once
                .WithWarmupCount(3)     // 3 warmup iteration
                .WithIterationCount(3),     // 3 target iteration,
             Job.Default.WithRuntime(CoreRuntime.Core50)
                .WithLaunchCount(1)     // benchmark process will be launched only once
                .WithWarmupCount(3)     // 3 warmup iteration
                .WithIterationCount(3),     // 3 target iteration,
            Job.Default.WithRuntime(CoreRuntime.Core60)
                .WithLaunchCount(1)     // benchmark process will be launched only once
                .WithWarmupCount(3)     // 3 warmup iteration
                .WithIterationCount(3));     // 3 target iteration);

            AddColumn(RankColumn.Roman);
            AddExporter(CsvMeasurementsExporter.Default,
                RPlotExporter.Default,
                JsonExporter.Full,
                JsonExporter.FullCompressed);
            AddDiagnoser(MemoryDiagnoser.Default);

            WithOrderer(new FastestToSlowestOrderer());
        }

        private class FastestToSlowestOrderer : IOrderer
        {
            public IEnumerable<BenchmarkCase> GetExecutionOrder(ImmutableArray<BenchmarkCase> benchmarksCase) =>
                from benchmark in benchmarksCase
                orderby benchmark.Parameters["X"] descending,
                    benchmark.Descriptor.WorkloadMethodDisplayInfo
                select benchmark;

            public string GetHighlightGroupKey(BenchmarkCase benchmarkCase) => "";

            public string GetLogicalGroupKey(ImmutableArray<BenchmarkCase> allBenchmarksCases, BenchmarkCase benchmarkCase) =>
                benchmarkCase.Job.DisplayInfo + "_" + benchmarkCase.Parameters.DisplayInfo;

            public IEnumerable<IGrouping<string, BenchmarkCase>> GetLogicalGroupOrder(IEnumerable<IGrouping<string, BenchmarkCase>> logicalGroups) =>
                logicalGroups.OrderBy(it => it.Key);

            public IEnumerable<BenchmarkCase> GetSummaryOrder(ImmutableArray<BenchmarkCase> benchmarksCases, Summary summary)
            {
                var benchmarkResult = from benchmark in benchmarksCases
                                      orderby summary[benchmark].ResultStatistics.Mean
                                      select benchmark;

                return benchmarkResult;
            }

            public bool SeparateLogicalGroups => true;
        }
    }
}