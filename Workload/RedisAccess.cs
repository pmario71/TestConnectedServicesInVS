extern alias CustRedis;
using CustRedis.StackExchange.Redis;

namespace TestConnectedServicesInVS.Workload
{
    internal static class RedisAccess
    {
        public static WebApplication? MapRoute(this WebApplication? app)
        {
            app.MapGet("/directredis", async (IConnectionMultiplexer multiplexer) =>
            {
                var db = multiplexer.GetDatabase();
                var result = await db.StringIncrementAsync("directRedisCon");
                return result;
            });
            return app;
        }

        public static WebApplicationBuilder AddRedisMultiplexer(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(builder.Configuration["redisConnection"]));
            return builder;
        }
    }
}
