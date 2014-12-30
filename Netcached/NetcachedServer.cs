using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using C5;

namespace Netcached
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NetcachedServer : INetcachedServer
    {
        int a = 0;
        struct OrderEntry : IComparable<OrderEntry>
        {
            public OrderEntry(string key, DateTime lastAccess, int priority)
            {
                this.key = key;
                this.lastAccess = lastAccess;
                this.priority = priority;
            }

            int IComparable<OrderEntry>.CompareTo(OrderEntry other)
            {
                if (priority == other.priority)
                {
                    TimeSpan diff = lastAccess - other.lastAccess;
                    return TimeSpan.Compare(diff, TimeSpan.Zero);
                }
                return priority - other.priority;
            }

            public string key;
            public DateTime lastAccess;
            public int priority;
        }

        private IntervalHeap<OrderEntry> order = new IntervalHeap<OrderEntry>();
        private Dictionary<string, byte[]> keyValueStore = new Dictionary<string,byte[]>();

        /// <summary>
        /// Set the value for the key with data, replacing it if the key is already present.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="priority">All keys with priority X are ordered before all keys with priority X+1.
        /// That is: if a key1 has priority 0 and key2 has priority 1 -> key1 will be evicted first, should
        /// the need arise (due to lack of memory).
        /// Keys with the same priority are ordered according to the time stamp of their last use.</param>
        /// <returns>Wheter the operation was successful</returns>
        public bool Set(string key, byte[] data, int priority = 0)
        {
            return false;
        }

        /// <summary>
        /// Delete the entry with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Wheter the operation was successful</returns</returns>
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
            return new byte[]{(byte)++a};
        }
    }
}
