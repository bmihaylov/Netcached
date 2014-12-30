using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Requests
{
    /// <summary>
    /// Request that is meant for memcaching.
    /// </summary>
    [DataContract]
    public class NetcachedRequest
    {
        /// <summary>
        /// Constructs a NetcachedRequest.
        /// </summary>
        public NetcachedRequest()
        {
            this.Key = this.GetKey();
        }

        /// <summary>
        /// Indicates wether the data should be overriden.
        /// </summary>
        [DataMember]
        public bool Override { get; set; }

        /// <summary>
        /// The key for getting/setting the data.
        /// </summary>
        [DataMember]
        public string Key { get; set; }

        /// <summary>
        /// The pattern in which the key will be set-up. Override this method for every
        /// type of NetcachedRequest you are constructing. Keep it unique for different
        /// type of requests.
        /// </summary>
        /// <returns></returns>
        public virtual string GetKey()
        {
            return null;
        }
    }
}