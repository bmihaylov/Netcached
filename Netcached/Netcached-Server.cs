using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Netcached
{
    public class Netcached_Server : INetcached_Server
    {
        public bool Set(string key, byte[] data, bool replace = true)
        {
            return false;
        }

        public bool Get(string key)
        {
            return false;
        }
    }
}
