// Copyright © Matt Hunt 2021

namespace GeoJSON.Text.Geometry;

/// <summary>
/// Base Interface for GeometryObject types.
/// </summary>
public interface IGeometryObject
{
    /// <summary>
    /// Gets the (mandatory) type of the GeoJSON Object.
    /// However, for GeoJSON Objects only the 'Point', 'MultiPoint', 'LineString', 'MultiLineString', 
    /// 'Polygon', 'MultiPolygon', or 'GeometryCollection' types are allowed.
    /// </summary>
    /// <remarks>
    /// See https://tools.ietf.org/html/rfc7946#section-3.1
    /// </remarks>
    /// <value>
    /// The type of the object.
    /// </value>
    GeoJSONObjectType Type { get; }
}