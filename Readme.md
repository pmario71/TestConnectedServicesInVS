# Readme

## Instrumenting StackExchange Redis

* works now for `directRedisCon` endpoint
* tracing is not working for `IDistributedCache`, this would require to use reflection to get at the `ConnectionMultiplexer`

## Using Aliases with nuget

`OTel` class uses nuget package alias to make sure that correct `StackExchange.Redis` instance is pulled.

[See docs for details:](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#packagereference-aliases)
