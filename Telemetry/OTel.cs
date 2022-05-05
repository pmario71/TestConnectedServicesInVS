extern alias CustRedis;

using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using CustRedis.StackExchange.Redis;

namespace TestConnectedServicesInVS.Telemetry
{
    internal class OTel
    {
        public static void SetupOpenTelemetry(WebApplicationBuilder builder)
        {
            // Define some important constants and the activity source
            var serviceName = "TestConnectedServicesInVS";
            var serviceVersion = "1.0.0";

            // Configure important OpenTelemetry settings, the console exporter, and automatic instrumentation
            builder.Services.AddOpenTelemetryTracing(b =>
            {
                b
                .AddSource(serviceName)
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                .AddJaegerExporter(o =>
                {
                    o.AgentHost = "localhost";
                    o.AgentPort = 6831; // use port number here
                })
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddRedisInstrumentation()
                .AddSource(ActivitySources.TracingServiceActivitySource.Name);
            });
        }

    }
}
