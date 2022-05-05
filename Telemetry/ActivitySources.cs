extern alias CustRedis;

using System.Diagnostics;

namespace TestConnectedServicesInVS.Telemetry
{
    public static class ActivitySources
    {
        //...

        // Name it after the service name for your app.
        // It can come from a config file, constants file, etc.
        public static readonly ActivitySource TracingServiceActivitySource = new("TracingService");

        //...
    }
}
