using System;
using System.IO;
using System.Reflection;

namespace GeoJSON.Text.Test.Benchmark.Deserialize
{
    internal static class JsonEmbeddedFileReader
    {
        public static string GetExpectedJson(string fileName)
        {
            var myType = typeof(JsonEmbeddedFileReader);
            var currentNamespace = myType.Namespace;

            var assembly = Assembly.GetCallingAssembly();

            var fileStreamName = $"{currentNamespace}.{fileName}.json";

            using (Stream stream = assembly.GetManifestResourceStream(fileStreamName) 
                ?? throw new NullReferenceException("Manifest stream should not be null. Incorrect filename specified."))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }

            throw new ArgumentException("File with name could not be found");
        }
    }
}
