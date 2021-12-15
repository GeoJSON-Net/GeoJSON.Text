// Copyright © Matt Hunt 2021

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Text.Geometry;

namespace GeoJSON.Text.Converters
{
    /// <summary>
    ///     Converter to read and write an <see cref="IPosition" />, that is,
    ///     the coordinates of a <see cref="Point" />.
    /// </summary>
    public class PositionConverter : JsonConverter<Position>
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IPosition).IsAssignableFromType(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Text.Json.Utf8JsonReader" /> to read from.</param>
        /// <param name="typeToConvert">Type of the object.</param>
        /// <param name="options">Serializer options</param>
        /// <returns><see cref="Position"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            double[] coordinates;

            try
            {
                var jsonDoc = JsonDocument.ParseValue(ref reader).RootElement;
                coordinates = jsonDoc.Deserialize<double[]>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error parsing coordinates.", ex);
            }
            return coordinates?.ToPosition() ?? throw new Exception("Coordinates cannot be null.");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Text.Json.Utf8JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="options">Serializer options</param>
        public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
        {
            if (value is IPosition coordinates)
            {
                writer.WriteStartArray();

                writer.WriteNumberValue(coordinates.Longitude);
                writer.WriteNumberValue(coordinates.Latitude);

                if (coordinates.Altitude.HasValue)
                {
                    writer.WriteNumberValue(coordinates.Altitude.Value);
                }

                writer.WriteEndArray();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}