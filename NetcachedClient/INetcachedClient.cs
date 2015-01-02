using NetcachedClient.Requests;
using NetcachedClient.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NetcachedClient
{
    /// <summary>
    /// Services interface.
    /// </summary>
    [ServiceContract]
    public interface INetcachedClient
    {
        /// <summary>
        /// Gets data from the memcache.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        NetcachedGetResponse Get(NetcachedGetRequest request);

        /// <summary>
        /// Sets data in the memcache.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        NetcachedSetResponse Set(NetcachedSetRequest request);

        /// <summary>
        /// Deletes data from the memcache.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        NetcachedDeleteResponse Delete(NetcachedDeleteRequest request);



        // TODO: Add your service operations here
    }
}
