using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeoJSON.Text.Tests
{
    public abstract class TestBase
    {
        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var assembly = Assembly.GetExecutingAssembly();
            var type = GetType().FullName;
            using (Stream stream = assembly.GetManifestResourceStream($"{type}_{name}.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }

            throw new ArgumentException("File with name could not be found");
        }
    }
}