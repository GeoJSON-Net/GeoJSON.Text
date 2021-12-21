[![Build and Test](https://github.com/GeoJSON-Net/GeoJSON.Text/actions/workflows/ci-build.yml/badge.svg?branch=main)](https://github.com/GeoJSON-Net/GeoJSON.Text/actions/workflows/ci-build.yml) [![codecov](https://codecov.io/gh/GeoJSON-Net/GeoJSON.Text/branch/main/graph/badge.svg?token=SE9XY1T8XO)](https://codecov.io/gh/GeoJSON-Net/GeoJSON.Text)

# GeoJSON.Text
GeoJSON.Text is a .NET library for the [RFC 7946 The GeoJSON Format](https://tools.ietf.org/html/rfc7946) and it uses and provides [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=net-6.0) converters for serialization and deserialization of GeoJSON data.

## Installation & Usage

[GeoJSON.Text NuGet package](https://www.nuget.org/packages/GeoJSON.Text/):

`Install-Package GeoJSON.Text`

### Serialization

```csharp
Position position = new Position(51.899523, -2.124156);
Point point = new Point(position);

string json = JsonSerializer.Serialize(point);
```

### Deserialization

```csharp
string json = "{\"coordinates\":[-2.124156,51.899523],\"type\":\"Point\"}";

Point point = JsonSerializer.Deserialize<Point>(json);
```

See the [Tests](https://github.com/GeoJSON-Net/GeoJSON.Text/tree/master/src/GeoJSON.Text.Test.Unit) for more examples.

## Special considerations for Newtonsoft.Json

GeoJSON.Text is made to support System.Text.Json, and does not support serializing or deserializing GeoJSON models using Newtonsoft.Json.

If using Newtonsoft.Json support is needed, please use [GeoJSON.Net](https://github.com/GeoJSON-Net/GeoJSON.Net).

## Contributing
Highly welcome! Just fork away and send a pull request. We try and review most pull requests within a couple of days.

## Thanks
This library would be NOTHING without its [contributors](https://github.com/GeoJSON-Net/GeoJSON.Text/graphs/contributors) - thanks so much!!

## Sponsors

 We have the awesome .NET tools from [JetBrains](http://www.jetbrains.com/).

[![Resharper](http://www.filehelpers.net/images/tools_resharper.gif)](http://www.jetbrains.com/resharper/)

## Contributors

This project exists thanks to all the people who contribute. 
<a href="https://github.com/GeoJSON-Net/GeoJSON.Text/graphs/contributors"><img src="https://opencollective.com/geojson-net/contributors.svg?width=890&button=false" /></a>


## Backers

Thank you to all our backers! 🙏 [[Become a backer](https://opencollective.com/geojson-net#backer)]

<a href="https://opencollective.com/geojson-net#backers" target="_blank"><img src="https://opencollective.com/geojson-net/backers.svg?width=890"></a>


## Sponsors

Support this project by becoming a sponsor. Your logo will show up here with a link to your website. [[Become a sponsor](https://opencollective.com/geojson-net#sponsor)]

<a href="https://opencollective.com/geojson-net/sponsor/0/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/0/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/1/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/1/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/2/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/2/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/3/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/3/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/4/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/4/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/5/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/5/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/6/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/6/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/7/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/7/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/8/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/8/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/9/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/9/avatar.svg"></a>
