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
        /// <summary>
        /// Set the value for the key with data, replacing it if the key is already present.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="priority">All keys with priority X are ordered before all keys with priority X+1.
        /// That is: if a key1 has priority 0 and key2 has priority 1 -> key1 will be evicted first, should
        /// the need arise (due to lack of memory).
        /// Keys with the same priority are ordered according to the time stamp of their last use.</param>
        /// <returns></returns>
        public bool Set(string key, byte[] data, int priority = 0)
        {
            return false;
        }

        /// <summary>
        /// Delete the entry the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            return false;
        }

        /// <summary>
        /// Returns the value for the specified key if present, and null if the operation failed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Get(string key)
        {
            return null;
        }
    }
}
