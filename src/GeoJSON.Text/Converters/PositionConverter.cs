// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Text.Geometry;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Text.Converters
{
    /// <summary>
    ///     Converter to read and write an <see cref="IPosition" />, that is,
    ///     the coordinates of a <see cref="Point" />.
    /// </summary>
    public class PositionConverter : JsonConverter<IPosition>
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
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Text.Json.Utf8JsonReader" /> to read from.</param>
        /// <param name="type">Type of the object.</param>
        /// <param name="options">Serializer options.</param>
        /// <returns>
        ///     The object value.
        /// </returns>
        public override IPosition Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            try
            { 
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new ArgumentException("Expected start of array");
                }

                double lng, lat;
                double? alt;

                // Read longitude
                if (!reader.Read())
                {
                    throw new ArgumentException("Expected number, but got end of data");
                }
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    throw new ArgumentException("Expected 2 or 3 coordinates but got 0");
                }
                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new ArgumentException("Expected number but got other type");
                }
                lng = reader.GetDouble();

                // Read latitude
                if (!reader.Read())
                {
                    throw new ArgumentException("Expected number, but got end of data");
                }
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    throw new ArgumentException("Expected 2 or 3 coordinates but got 1");
                }
                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new ArgumentException("Expected number but got other type");
                }
                lat = reader.GetDouble();

                // Read altitude, or return if end of array is found
                if (!reader.Read())
                {
                    throw new ArgumentException("Unexpected end of data");
                }
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return new Position(lat, lng);
                }
                else if (reader.TokenType == JsonTokenType.Null)
                {
                    alt = null;
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    alt = reader.GetDouble();
                }
                else
                {
                    throw new ArgumentException("Expected number but got other type");
                }

                // Check what comes next. Expects end of array.
                if (!reader.Read())
                {
                    throw new ArgumentException("Expected end of array, but got end of data");
                }
                if (reader.TokenType != JsonTokenType.EndArray)
                {
                    throw new ArgumentException("Expected 2 or 3 coordinates but got >= 4");
                }

                return new Position(lat, lng, alt);
            }
            catch (Exception e)
            {
                throw new JsonException("Error parsing coordinates", e);
            }
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(
            Utf8JsonWriter writer,
            IPosition coordinates,
            JsonSerializerOptions options)
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
    }
}