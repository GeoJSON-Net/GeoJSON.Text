﻿// Copyright © Matt Hunt 2021

namespace GeoJSON.Text.CoordinateReferenceSystem;

/// <summary>
/// Base Interface for CRSBase Object types.
/// </summary>
public interface ICRSObject
{
    /// <summary>
    /// Gets the CRS type.
    /// </summary>
    CRSType Type { get; }
}
