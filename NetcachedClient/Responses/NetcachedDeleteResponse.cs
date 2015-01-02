using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Responses
{
    /// <summary>
    /// DeleteResponse from deleting a key in the memcache.
    /// </summary>
    [DataContract]
    public class NetcachedDeleteResponse
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }
    }
}