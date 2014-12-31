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
        struct Entry : IComparable<Entry>
        {
            long lastAccess; // this is DateTime.Now.Ticks for the last access to the key
            int priority;

            //TODO change order of arguments
            public Entry(string key, long lastAccess, int priority, byte[] data)
            {
                this.lastAccess = lastAccess;
                this.priority = priority;
                this.Key = key;
                this.Data = data;
                // 16 is the overehead of an empty string
                // first constant is due to the overhead of the data structures and the stored PriorityQueueHandles
                this.Size = 160 +  (16 + key.Length * sizeof(char) + sizeof(long) + sizeof(int) + data.Length + sizeof(long));
            }

            public string Key;
            public byte[] Data;

            /// <summary>
            /// This is only an approximation of the memory in bytes taken due to this particular Entry.
            /// </summary>
            public long Size;

            public int CompareTo(Entry other)
            {
                if (priority == other.priority)
                {
                    if (lastAccess == other.lastAccess)
                    {
                        return 0;
                    }

                    return lastAccess - other.lastAccess > 0 ? 1 : -1;
                }

                return priority - other.priority;
            }
        }

        private IntervalHeap<Entry> priorityQueue = new IntervalHeap<Entry>();
        private Dictionary<string, IPriorityQueueHandle<Entry>> keyHandleStore =
            new Dictionary<string, IPriorityQueueHandle<Entry>>();
        long usedSpace = 0;
        long allowedSpace = 128 * 1000 * 1000;

        /// <summary>
        /// Set the value for the key with data, replacing it if the key is already present.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="priority">All keys with priority X are ordered before all keys with priority X+1.
        /// That is: if a key1 has priority 0 and key2 has priority 1 -> key1 will be evicted first, should
        /// the need arise (due to lack of memory).
        /// Keys with the same priority are ordered according to the time stamp of their last use.</param>
        /// <returns>Whether the operation was successful</returns>
        public bool Set(string key, byte[] data, int priority = 0)
        {
            Entry newEntry = new Entry(key, DateTime.Now.Ticks, priority, data);
            IPriorityQueueHandle<Entry> oldHandle = null;
            bool enoughSpace = true;

            if (keyHandleStore.TryGetValue(key, out oldHandle))
            {
                long sizeDifference = newEntry.Size - priorityQueue[oldHandle].Size;
                if (usedSpace - sizeDifference > allowedSpace)
                {
                    enoughSpace = FreeSpace(sizeDifference);
                }
            }
            else if (usedSpace + newEntry.Size > allowedSpace)
            {
                enoughSpace = FreeSpace(newEntry.Size);
            }


            if (enoughSpace)
            {
                IPriorityQueueHandle<Entry> handle = null;
                priorityQueue.Add(ref handle, new Entry(key, DateTime.Now.Ticks, priority, data));
                keyHandleStore[key] = handle;
                return true;
            }

            return false;
        }

        private bool FreeSpace(long desiredSpace)
        {
            if (desiredSpace > allowedSpace)
            {
                return false;
            }

            while (allowedSpace - usedSpace < desiredSpace)
            {
                Entry entry = priorityQueue.DeleteMin();
                usedSpace -= entry.Size;
                keyHandleStore.Remove(entry.Key);
            }
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
        /// <returns>Value for the specified key if present, and null if the operation failed</returns>
        public byte[] Get(string key)
        {
            return null;
        }
    }
}
