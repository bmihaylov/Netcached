using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Responses
{
    /// <summary>
    /// SetResponse from the memcache.
    /// </summary>
    [DataContract]
    public class NetcachedSetResponse
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }
    }
}