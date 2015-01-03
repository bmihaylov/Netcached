using Netcached.Client.Netcached.Server.NetcachedServer.Adapter;
using Netcached.Client.Serializing;

namespace Netcached.Client
{
    public class NetcachedClient
    {
        private static readonly NetcachedServerClient netcachedServerServiceClient
            = new NetcachedServerClient();

        /// <summary>
        /// Gets data from cache.
        /// </summary>
        /// <typeparam name="T">The type of data to get.</typeparam>
        /// <param name="key">The key to get from.</param>
        /// <returns>The data</returns>
        public T Get<T>(string key)
        {
            byte[] cacheData = netcachedServerServiceClient.Get(key);
            if (cacheData == null)
            {
                return default(T);
            }

            return DataSerializer.Deserialize<T>(cacheData);
        }

        /// <summary>
        /// Stores data in the cache
        /// </summary>
        /// <typeparam name="T">The type of data to store.</typeparam>
        /// <param name="key">The key to use for storing.</param>
        /// <param name="data">The data to store.</param>
        /// <returns>Whether the operation was successful</returns>
        public bool Set<T>(string key, T data)
        {
            if (data == null)
            {
                return false;
            }

            byte[] serializedData = DataSerializer.Serialize<T>(data);
            bool isSuccessful = netcachedServerServiceClient.Set(key, serializedData);
            return isSuccessful;
        }

        /// <summary>
        /// Deletes data from the cache.
        /// </summary>
        /// <param name="request">The request to pass.</param>
        /// <returns>Whether the operation was successful</returns>
        public bool Delete(string key)
        {
            bool isSuccessful = netcachedServerServiceClient.Delete(key);
            return isSuccessful;
        }
    }
}
