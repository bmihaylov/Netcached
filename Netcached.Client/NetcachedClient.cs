﻿using Netcached.Client.Netcached.Server.NetcachedServer.Adapter;
using Netcached.Client.Serializing;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Netcached.Client
{
    public class NetcachedClient
    {
        private static readonly NetcachedServerClient[] netcachedServerServiceClients =
            GetNetcachedServerServiceClients();

        /// <summary>
        /// Gets data from cache.
        /// </summary>
        /// <typeparam name="T">The type of data to get.</typeparam>
        /// <param name="key">The key to get from.</param>
        /// <returns>The data</returns>
        public T Get<T>(string key)
        {
            if (netcachedServerServiceClients.Length == 0)
            {
                return default(T);
            }

            NetcachedServerClient netcachedServerServiceClient = GetNetcachedServerClient(key);
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
            if (data == null || netcachedServerServiceClients.Length == 0)
            {
                return false;
            }

            NetcachedServerClient netcachedServerServiceClient = GetNetcachedServerClient(key);
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
            if (netcachedServerServiceClients.Length == 0)
            {
                return false;
            }

            NetcachedServerClient netcachedServerServiceClient = GetNetcachedServerClient(key);
            bool isSuccessful = netcachedServerServiceClient.Delete(key);
            return isSuccessful;
        }

        private static NetcachedServerClient GetNetcachedServerClient(string key)
        {
            int serviceClientIndex = key.GetHashCode() % netcachedServerServiceClients.Length;
            return netcachedServerServiceClients[serviceClientIndex];
        }
        private static NetcachedServerClient[] GetNetcachedServerServiceClients()
        {
            var section = (ConfigurationManager.GetSection("NetcachedClient/Servers") as Hashtable)
                .Cast<DictionaryEntry>()
                .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            string addressFormat = "http://{0}:{1}/NetcachedServer.svc";
            return section.Keys.Select(key => new NetcachedServerClient(
                new BasicHttpBinding(),
                new EndpointAddress(string.Format(addressFormat, key, section[key])))).ToArray();
        }
    }
}