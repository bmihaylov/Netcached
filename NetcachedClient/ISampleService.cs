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
    public interface ISampleService
    {
        /// <summary>
        /// Gets the last DateTime when the server was driven up.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServerUpDateTimeResponse GetServerUpDateTime(ServerUpDateTimeRequest request);

        // TODO: Add your service operations here
    }
}
