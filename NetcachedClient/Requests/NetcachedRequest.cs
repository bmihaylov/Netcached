﻿using System;
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
        /// The key for getting/setting/deleting the data.
        /// </summary>
        [DataMember]
        public string Key { get; set; }
    }
}