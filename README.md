# Elmah.Contrib.WebApi.Demystifier

[![NuGet version (DIContextAutoLoader)](https://img.shields.io/nuget/v/DIContextAutoLoader.svg?style=flat-square)](https://www.nuget.org/packages/DIContextAutoLoader/)

<!--
ElmahExceptionLogger for WebAPI using [Ben.Demystifier](https://github.com/benaadams/Ben.Demystifier) for high performance understanding for stack traces

For more information about Ben.Demystifier: https://github.com/benaadams/Ben.Demystifier

# Package Manager

```
    Install-Package Elmah.Contrib.WebApi.Demystifier
```

# Usage
```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.Services.Add(typeof(IExceptionLogger), new ElmahDemystifierExceptionLogger());
 
        ...
    }
}
```

# Acknowledgements
Just a copy of [elmah-contrib-webapi](https://github.com/rdingwall/elmah-contrib-webapi)'s [ElmahExceptionLogger](https://github.com/rdingwall/elmah-contrib-webapi/blob/master/src/Elmah.Contrib.WebApi/ElmahExceptionLogger.cs) using [Ben.Demystifier](https://github.com/benaadams/Ben.Demystifier)

# License

As a derivative work of elmah-contrib-webapi, this library is available under the same [MS-PL license](http://www.opensource.org/licenses/ms-pl).
-->
