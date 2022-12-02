// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System.Runtime.Serialization;

namespace GeoJSON.Text
{
    /// <summary>
    /// Defines the GeoJSON Objects types.
    /// </summary>
    public enum GeoJSONObjectType
    {
        /// <summary>
        /// Defines the Point type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.2
        /// </remarks>
        [EnumMember(Value = "Point")]
        Point,

        /// <summary>
        /// Defines the MultiPoint type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.3
        /// </remarks>
        [EnumMember(Value = "MultiPoint")]
        MultiPoint,

        /// <summary>
        /// Defines the LineString type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.4
        /// </remarks>
        [EnumMember(Value = "LineString")]
        LineString,

        /// <summary>
        /// Defines the MultiLineString type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.5
        /// </remarks>
        [EnumMember(Value = "MultiLineString")]
        MultiLineString,

        /// <summary>
        /// Defines the Polygon type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.6
        /// </remarks>
        [EnumMember(Value = "Polygon")]
        Polygon,

        /// <summary>
        /// Defines the MultiPolygon type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.7
        /// </remarks>
        [EnumMember(Value = "MultiPolygon")]
        MultiPolygon,

        /// <summary>
        /// Defines the GeometryCollection type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1.8
        /// </remarks>
        [EnumMember(Value = "GeometryCollection")]
        GeometryCollection,

        /// <summary>
        /// Defines the Feature type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.2
        /// </remarks>
        [EnumMember(Value = "Feature")]
        Feature,

        /// <summary>
        /// Defines the FeatureCollection type.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.3
        /// </remarks>
        [EnumMember(Value = "FeatureCollection")]
        FeatureCollection
    }
}
