using Netcached;
using NetcachedClient.Requests;
using NetcachedClient.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NetcachedClient
{
    /// <summary>
    /// Sample services implementation
    /// </summary>
    public class NetcachedClient : INetcachedClient
    {
        //Example of how to use the service:
        //http://www.fascinatedwithsoftware.com/blog/post/2011/06/03/Generic-Data-Contracts-in-WCF.aspx
        private INetcachedServer netcached = new NetcachedServer();

        /// <summary>
        /// Gets data from cache.
        /// </summary>
        /// <typeparam name="T">The type of data to get.</typeparam>
        /// <param name="request">The request to pass.</param>
        /// <returns></returns>
        public NetcachedGetResponse Get(NetcachedGetRequest request)
        {

            byte[] cacheData = netcached.Get(request.Key);
            return new NetcachedGetResponse()
            {
                CacheData = cacheData,
            };
        }

        /// <summary>
        /// Stores data in the cache
        /// </summary>
        /// <typeparam name="T">The type of data to set.</typeparam>
        /// <param name="request">The request to pass.</param>
        /// <returns></returns>
        public NetcachedSetResponse Set(NetcachedSetRequest originalRequest)
        {
            NetcachedSetRequest request = NetcachedSetRequest.Create<DateTime>(originalRequest.Key, DateTime.Now);
            byte[] serializedData = null;
            if (request.SerializedData != null)
            {
                serializedData = request.SerializedData;
            }

            bool isSuccessful = netcached.Set(request.Key, serializedData);
            return new NetcachedSetResponse()
            {
                IsSuccessful = isSuccessful,
            };
        }

        /// <summary>
        /// Deletes data from the cache.
        /// </summary>
        /// <param name="request">The request to pass.</param>
        /// <returns></returns>
        public NetcachedDeleteResponse Delete(NetcachedDeleteRequest request)
        {
            bool isSuccessful = netcached.Delete(request.Key);
            return new NetcachedDeleteResponse()
            {
                IsSuccessful = isSuccessful,
            };
        }
    }
}
