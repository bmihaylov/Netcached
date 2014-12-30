using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Requests
{
    [DataContract]
    public class ServerUpDateTimeRequest : NetcachedRequest
    {
        /// <summary>
        /// The format in which the datetime will be returned.
        /// </summary>
        [DataMember]
        public string Format { get; set; }

        /// <summary>
        /// The pattern in which the key will be set-up. Override this method for every
        /// type of NetcachedRequest you are constructing. Keep it unique for different
        /// type of requests.
        /// </summary>
        /// <returns>A unique key for the request.</returns>
        public override string GetKey()
        {
            return "ServerUpDateTime";
        }
    }
}