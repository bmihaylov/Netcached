using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NetcachedClient.Responses
{
    [DataContract]
    public class ServerUpDateTimeResponse
    {
        /// <summary>
        /// The last DateTime when the server was driven up.
        /// </summary>
        [DataMember]
        public string DateTime { get; set; }
    }
}