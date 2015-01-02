using NetcachedClient.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Requests
{
    /// <summary>
    /// SetRequest that is meant for memcaching.
    /// </summary>
    [DataContract]
    public class NetcachedSetRequest : NetcachedRequest
    {
        /// <summary>
        /// The serialized data that will be cached.
        /// </summary>
        [DataMember]
        public byte[] SerializedData { get; set; }

        /// <summary>
        /// Used to create a NetcachedSetRequest with serialized data.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="data">The data that will be cached.</param>
        /// <returns></returns>
        public static NetcachedSetRequest Create<T>(string key, T data)
        {
            return new NetcachedSetRequest()
            {
                Key = key,
                SerializedData = DataSerializer.Serialize<T>(data),
            };
        }
    }
}