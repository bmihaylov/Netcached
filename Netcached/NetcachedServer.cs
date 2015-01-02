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
        private long usedSpace = 0;
        private long allowedSpace = 128 * 1000 * 1000;
        private IntervalHeap<Entry> priorityQueue = new IntervalHeap<Entry>();
        private Dictionary<string, IPriorityQueueHandle<Entry>> keyHandleStore =
            new Dictionary<string, IPriorityQueueHandle<Entry>>();

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
            Entry newEntry = new Entry(key, DateTime.Now.Ticks, data, priority);
            IPriorityQueueHandle<Entry> oldHandle = null;
            bool enoughSpace = true;

            if (keyHandleStore.TryGetValue(key, out oldHandle))
            {
                long oldSize = priorityQueue[oldHandle].Size; 
                long sizeDifference = newEntry.Size - oldSize;
                if (usedSpace - sizeDifference > allowedSpace)
                {
                    enoughSpace = FreeSpace(sizeDifference);
                }
                priorityQueue.Delete(oldHandle);
                usedSpace -= oldSize;
            }
            else if (usedSpace + newEntry.Size > allowedSpace)
            {
                enoughSpace = FreeSpace(newEntry.Size);
            }


            if (enoughSpace)
            {
                IPriorityQueueHandle<Entry> handle = null;
                priorityQueue.Add(ref handle, newEntry);
                usedSpace += newEntry.Size;
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
            IPriorityQueueHandle<Entry> handle = null;
            if (!keyHandleStore.TryGetValue(key, out handle))
            {
                return false;
            }

            keyHandleStore.Remove(key);
            priorityQueue.Delete(handle);
            return true;
        }

        /// <summary>
        /// Returns the value for the specified key if present, and null if the operation failed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Value for the specified key if present, and null if the operation failed</returns>
        public byte[] Get(string key)
        {
            IPriorityQueueHandle<Entry> handle;
            if (!keyHandleStore.TryGetValue(key, out handle))
            {
                return null;
            }

            Entry entry = priorityQueue.Delete(handle);
            entry.LastAccess = DateTime.Now.Ticks;
            IPriorityQueueHandle<Entry> newHandle = null;
            priorityQueue.Add(ref newHandle, entry);
            keyHandleStore[key] = newHandle;

            return entry.data;
        }

        struct Entry : IComparable<Entry>
        {
            public string Key;
            /// <summary>
            // this is DateTime.Now.Ticks for the last access to the key
            /// </summary>
            public long LastAccess;
            public byte[] data;
            /// <summary>
            /// this is only an approximation of the memory in bytes taken due to this particular Entry
            /// </summary>
            public long Size;
            int priority;

            public Entry(string key, long lastAccess, byte[] data, int priority)
            {
                this.Key = key;
                this.LastAccess = lastAccess;
                this.priority = priority;
                this.data = data;
                // 16 is the overehead of an empty string
                // first constant is due to the overhead of the data structures and the stored PriorityQueueHandles
                this.Size = 160 + (16 + key.Length * sizeof(char) + sizeof(long) + sizeof(int) + data.Length + sizeof(long));
            }

            int IComparable<Entry>.CompareTo(Entry other)
            {
                if (priority == other.priority)
                {
                    if (LastAccess == other.LastAccess)
                    {
                        return 0;
                    }
                    return LastAccess - other.LastAccess > 0 ? 1 : -1;
                }
                return priority - other.priority;
            }
        }
    }
}