using TestConnectedServicesInVS.Telemetry;

namespace TestConnectedServicesInVS.Workload
{
    static class WorkloadProducer
    {
        public static async Task ChildActivity(bool error)
        {
            using var childActivity = ActivitySources.TracingServiceActivitySource.StartActivity("ChildActivy");

            try
            {
                if (error)
                {
                    throw new Exception("simulated error");
                }

                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                childActivity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, ex.Message);
            }
        }
    }
}
