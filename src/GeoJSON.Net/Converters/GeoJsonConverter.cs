// Copyright © Matt Hunt 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;

namespace GeoJSON.Text.Converters;

/// <summary>
/// Converts <see cref="IGeoJSONObject"/> types to and from JSON.
/// </summary>
public class GeoJsonConverter : JsonConverter<object>
{
    /// <summary>
    /// Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
    public override bool CanConvert(Type objectType)
    {
        return typeof(IGeoJSONObject).IsAssignableFromType(objectType);
    }


    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The <see cref="T:System.Text.Json.Utf8JsonReader" /> to read from.</param>
    /// <param name="typeToConvert">Type of the object.</param>
    /// <param name="options">The existing value of object being read.</param>		/// 
    /// <returns>The object value.</returns>
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartObject:
                var value = JsonDocument.ParseValue(ref reader).RootElement;
                return ReadGeoJson(value);
            case JsonTokenType.StartArray:
                var values = JsonElement.ParseValue(ref reader);
                var geometries = new List<IGeoJSONObject>(values.GetArrayLength());
                geometries.AddRange(values.EnumerateArray().Select(ReadGeoJson));
                return geometries;
        }

        throw new Exception($"Expected null, object or array token but received {reader.TokenType}.");
    }

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Text.Json.Utf8JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">Serializer options</param>
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, typeof(IGeoJSONObject), options);
    }


    /// <summary>
    /// Reads the geo json.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    /// <exception cref="Newtonsoft.Json.JsonReaderException">
    /// json must contain a "type" property
    /// or
    /// type must be a valid geojson object type
    /// </exception>
    /// <exception cref="System.NotSupportedException">
    /// Unknown geoJsonType {geoJsonType}
    /// </exception>
    private static IGeoJSONObject ReadGeoJson(JsonElement value)
    {
        JsonElement token;

        if (!value.TryGetProperty("type", out token))
        {
            throw new Exception("Json must contain a \"type\" property.");
        }

        GeoJSONObjectType geoJsonType;

        if (!Enum.TryParse(token.Deserialize<string>(), true, out geoJsonType))
        {
            throw new Exception("Type must be a valid geojson object type.");
        }

        switch (geoJsonType)
        {
            case GeoJSONObjectType.Point:
                return value.Deserialize<Point>();
            case GeoJSONObjectType.MultiPoint:
                return value.Deserialize<MultiPoint>();
            case GeoJSONObjectType.LineString:
                return value.Deserialize<LineString>();
            case GeoJSONObjectType.MultiLineString:
                return value.Deserialize<MultiLineString>();
            case GeoJSONObjectType.Polygon:
                return value.Deserialize<Polygon>();
            case GeoJSONObjectType.MultiPolygon:
                return value.Deserialize<MultiPolygon>();
            case GeoJSONObjectType.GeometryCollection:
                return value.Deserialize<GeometryCollection>();
            case GeoJSONObjectType.Feature:
                return value.Deserialize<Feature.Feature>();
            case GeoJSONObjectType.FeatureCollection:
                return value.Deserialize<FeatureCollection>();
            default:
                throw new NotSupportedException($"Unknown geoJsonType {geoJsonType}");
        }
    }
}