using GeoJSON.Text.Geometry;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GeoJSON.Text.Tests.Geometry
{
    [TestFixture]
    public class LineStringTests : TestBase
    {
        [Test]
        public void Is_Closed()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var lineString = new LineString(coordinates);

            Assert.IsTrue(lineString.IsClosed());
        }

        [Test]
        public void Is_Not_Closed()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211592, 4.889259338378955)
            };

            var lineString = new LineString(coordinates);

            Assert.IsFalse(lineString.IsClosed());
        }


        [Test]
        public void Can_Serialize()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var lineString = new LineString(coordinates);

            var actualJson = JsonSerializer.Serialize(lineString);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var expectedLineString = new LineString(coordinates);

            var json = GetExpectedJson();
            var actualLineString = JsonSerializer.Deserialize<LineString>(json);

            Assert.AreEqual(expectedLineString, actualLineString);

            Assert.AreEqual(4, actualLineString.Coordinates.Count);
            Assert.AreEqual(expectedLineString.Coordinates[0].Latitude, actualLineString.Coordinates[0].Latitude);
            Assert.AreEqual(expectedLineString.Coordinates[0].Longitude, actualLineString.Coordinates[0].Longitude);
        }

        [Test]
        public void Can_Deserialize_Strings()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var expectedLineString = new LineString(coordinates);

            var json = GetExpectedJson();
            var options = new JsonSerializerOptions { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString };
            var actualLineString = JsonSerializer.Deserialize<LineString>(json, options);

            Assert.AreEqual(expectedLineString, actualLineString);

            Assert.AreEqual(4, actualLineString.Coordinates.Count);
            Assert.AreEqual(expectedLineString.Coordinates[0].Latitude, actualLineString.Coordinates[0].Latitude);
            Assert.AreEqual(expectedLineString.Coordinates[0].Longitude, actualLineString.Coordinates[0].Longitude);
        }

        [Test]
        public void Can_Deserialize_With_Altitude()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906, 10.0),
                new Position(52.3711451105601, 4.895267486572266, 10.5),
                new Position(52.36931095278263, 4.892091751098633, null),
                new Position(52.370725881211314, 4.889259338378906, 10.2)
            };

            var expectedLineString = new LineString(coordinates);

            var json = GetExpectedJson();
            var actualLineString = JsonSerializer.Deserialize<LineString>(json);

            Assert.AreEqual(expectedLineString, actualLineString);

            Assert.AreEqual(4, actualLineString.Coordinates.Count);
            Assert.AreEqual(expectedLineString.Coordinates[0].Latitude, actualLineString.Coordinates[0].Latitude);
            Assert.AreEqual(expectedLineString.Coordinates[0].Longitude, actualLineString.Coordinates[0].Longitude);
            Assert.AreEqual(expectedLineString.Coordinates[0].Altitude, actualLineString.Coordinates[0].Altitude);
            Assert.AreEqual(expectedLineString.Coordinates[2].Altitude, actualLineString.Coordinates[2].Altitude);
        }

        [Test]
        public void Can_Deserialize_String_Literals()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906, double.NegativeInfinity),
                new Position(52.3711451105601, 4.895267486572266, double.PositiveInfinity),
                new Position(52.36931095278263, 4.892091751098633, double.NaN),
                new Position(52.370725881211314, 4.889259338378906, double.NegativeInfinity)
            };

            var expectedLineString = new LineString(coordinates);

            var json = GetExpectedJson();
            var options = new JsonSerializerOptions { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals };
            var actualLineString = JsonSerializer.Deserialize<LineString>(json, options);

            bool b = expectedLineString.Coordinates[0].Equals(actualLineString.Coordinates[0]);
            Assert.AreEqual(expectedLineString, actualLineString);

            Assert.AreEqual(4, actualLineString.Coordinates.Count);
            Assert.AreEqual(expectedLineString.Coordinates[0].Latitude, actualLineString.Coordinates[0].Latitude);
            Assert.AreEqual(expectedLineString.Coordinates[0].Longitude, actualLineString.Coordinates[0].Longitude);
            Assert.AreEqual(expectedLineString.Coordinates[0].Altitude, actualLineString.Coordinates[0].Altitude);
            Assert.AreEqual(expectedLineString.Coordinates[1].Altitude, actualLineString.Coordinates[1].Altitude);
            Assert.AreEqual(expectedLineString.Coordinates[2].Altitude, actualLineString.Coordinates[2].Altitude);
        }

        [Test]
        public void Constructor_No_Coordinates_Throws_Exception()
        {
            var coordinates = new List<IPosition>();
            Assert.Throws<ArgumentOutOfRangeException>(() => new LineString(coordinates));
        }

        [Test]
        public void Constructor_Null_Coordinates_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new LineString((IEnumerable<IPosition>)null));
        }

        private LineString GetLineString(double offset = 0.0)
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314 + offset, 4.889259338378906 + offset),
                new Position(52.3711451105601 + offset, 4.895267486572266 + offset),
                new Position(52.36931095278263 + offset, 4.892091751098633 + offset),
                new Position(52.370725881211314 + offset, 4.889259338378906 + offset)
            };
            var lineString = new LineString(coordinates);
            return lineString;
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var rnd = new System.Random();
            var offset = rnd.NextDouble() * 60;
            if (rnd.NextDouble() < 0.5)
            {
                offset *= -1;
            }

            var left = GetLineString(offset);
            var right = GetLineString(offset);

            Assert.AreEqual(left, right);
            Assert.AreEqual(right, left);

            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(left.Equals(left));
            Assert.IsTrue(right.Equals(left));
            Assert.IsTrue(right.Equals(right));

            Assert.IsTrue(left == right);
            Assert.IsTrue(right == left);

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}