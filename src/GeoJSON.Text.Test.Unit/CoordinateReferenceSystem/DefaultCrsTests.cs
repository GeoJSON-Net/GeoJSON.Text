using GeoJSON.Text.CoordinateReferenceSystem;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using NUnit.Framework;
using System.Text.Json;

namespace GeoJSON.Text.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class DefaultCrsTests : TestBase
    {
        [Test]
        public void Can_Serialize_Does_Not_Output_Crs_Property()
        {
            var collection = new FeatureCollection();

            var json = JsonSerializer.Serialize(collection);

            Assert.IsTrue(!json.Contains("\"crs\""));
        }

        [Test]
        public void Can_Deserialize_When_Json_Does_Not_Contain_Crs_Property()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";
             
            var point = JsonSerializer.Deserialize<Point>(json);

            Assert.IsNull(point.CRS);
        }

        [Test]
        public void Can_Deserialize_CRS_issue_89()
        {
            var json = "{\"coordinates\": [ 90.65464646, 53.2455662, 200.4567 ], \"type\": \"Point\", \"crs\": { \"type\": \"name\", \"properties\": { \"name\": \"urn:ogc:def:crs:OGC:1.3:CRS84\" }}}";

            var point = JsonSerializer.Deserialize<Point>(json);

            Assert.IsNotNull(point.CRS);
            Assert.AreEqual(CRSType.Name, point.CRS.Type);
        }

        [Test]
        public void Can_Serialize_CRS_issue_89()
        {
            var expected =
                "{\"type\":\"Point\",\"coordinates\":[34.57,12.35],\"crs\":{\"properties\":{\"name\":\"TEST NAME\"},\"type\":\"name\"}}";
            var point = new Point(new Position(12.35, 34.57)) { CRS = new NamedCRS("TEST NAME") };

            var json = JsonSerializer.Serialize(point);

            Assert.IsNotNull(json);
            Assert.AreEqual(expected, json);
        }

        [Test]
        public void Can_Serialize_DefaultCRS_issue_89()
        {
            var expected =
                "{\"type\":\"Point\",\"coordinates\":[34.57,12.35],\"crs\":{\"properties\":{\"name\":\"urn:ogc:def:crs:OGC::CRS84\"},\"type\":\"name\"}}";
            var point = new Point(new Position(12.35, 34.57)) { CRS = new NamedCRS("urn:ogc:def:crs:OGC::CRS84") };

            var json = JsonSerializer.Serialize(point);

            Assert.IsNotNull(json);
            Assert.AreEqual(expected, json);
        }
    }
}