extern alias CustRedis;
using CustRedis.StackExchange.Redis;

namespace TestConnectedServicesInVS
{
    internal class RedisAccess
    {
        public static async Task<long> Increment()
        {
            using var connection = ConnectionMultiplexer.Connect("localhost:6379");
            var db = connection.GetDatabase();
            var result = await db.StringIncrementAsync("directRedisCon");
            
            return result;
        }
    }
}
