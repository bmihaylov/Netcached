using NetcachedClient.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Responses
{
    /// <summary>
    /// GetResponse from the memcache.
    /// </summary>
    [DataContract]
    public class NetcachedGetResponse
    {
        /// <summary>
        /// A byte array of the cached data. Could be null if the data is not in the cache.
        /// </summary>
        [DataMember]
        public byte[] CacheData { get; set; }

        /// <summary>
        /// Gets the deserialized data from the cache's response
        /// </summary>
        /// <typeparam name="T">The data's type.</typeparam>
        /// <returns>The data.</returns>
        public T GetData<T>()
        {
            if (this.CacheData == null)
            {
                return default(T);
            }

            return DataSerializer.Deserialize<T>(this.CacheData);
        }
    }
}