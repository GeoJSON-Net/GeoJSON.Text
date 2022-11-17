// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Text.Geometry;
using System;
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
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        ///     The object value.
        /// </returns>
        public override IPosition Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            double[] coordinates;

            try
            {
                coordinates = JsonSerializer.Deserialize<double[]>(ref reader, options);
            }
            catch (Exception e)
            {
                throw new JsonException("Error parsing coordinates", e);
            }
            return coordinates?.ToPosition() ?? throw new JsonException("Coordinates cannot be null");
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