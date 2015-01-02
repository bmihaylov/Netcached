using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Requests
{
    /// <summary>
    /// GetRequest that is meant for memcaching.
    /// </summary>
    [DataContract]
    public class NetcachedGetRequest : NetcachedRequest
    {
    }
}