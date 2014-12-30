using Netcached;
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
    /// Sample services implementation
    /// </summary>
    public class SampleService : ISampleService
    {
        /// <summary>
        /// Gets the last DateTime when the server was driven up.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerUpDateTimeResponse GetServerUpDateTime(ServerUpDateTimeRequest request)
        {
            NetcachedServer netcached = new NetcachedServer();
            if (request.Override)
            {
                netcached.Delete(request.Key);
            }

            byte[] cachedDateTime = netcached.Get(request.Key);
            DateTime dateTime = DateTime.Now;

            if(cachedDateTime != null)
            {
                DateTime.TryParse(Encoding.ASCII.GetString(cachedDateTime), out dateTime);
            }

            return new ServerUpDateTimeResponse()
            {
                DateTime = dateTime.ToString(request.Format),
            };
        }
    }
}
