using StackExchange.Redis;

namespace VillaLuxeMvcNet.Helpers
{
    public class HelperCacheRedis
    {
        private static Lazy<ConnectionMultiplexer> CreateConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string connectionString = "cache-villas.gzec9r.ng.0001.use1.cache.amazonaws.com:6379";
            return ConnectionMultiplexer.Connect(connectionString);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return CreateConnection.Value;
            }
        }
    }
}
