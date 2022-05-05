# Readme

## Instrumenting StackExchange Redis

* works now for `directRedisCon` endpoint
* tracing is not working for `IDistributedCache`, this would require to use reflection to get at the `ConnectionMultiplexer`

## Using Aliases with nuget

`OTel` class uses nuget package alias to make sure that correct `StackExchange.Redis` instance is pulled.

[See docs for details:](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#packagereference-aliases)

## References

* [Asp.net Core minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
* [StackExchange.Redis Docs](https://stackexchange.github.io/StackExchange.Redis/)
* [OpenTelemetry Docs](https://opentelemetry.io/docs/instrumentation/net/getting-started/)
* [OpenTelemetry .net on Github](https://github.com/open-telemetry/opentelemetry-dotnet)