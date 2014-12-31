using Netcached;
using NetcachedClient.Requests;
using NetcachedClient.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NetcachedClient
{
    /// <summary>
    /// Sample services implementation
    /// </summary>
    public class SampleService : ISampleService
    {
        /// <summary>
        /// Gets the last DateTime when the server was driven up.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerUpDateTimeResponse GetServerUpDateTime(ServerUpDateTimeRequest request)
        {
            NetcachedServer netcached = new NetcachedServer();
            if (request.Override)
            {
                netcached.Delete(request.Key);
            }

            byte[] cachedDateTime = netcached.Get(request.Key);
            DateTime dateTime = DateTime.Now;

            if (cachedDateTime != null)
            {
                dateTime = this.Deserialize<DateTime>(cachedDateTime);
            }
            else
            {
                netcached.Set(request.Key, this.Serialize<DateTime>(dateTime));
            }

            return new ServerUpDateTimeResponse()
            {
                DateTime = dateTime,
            };
        }

        private byte[] Serialize<T>(T data)
        {
            byte[] serialized = null;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, data);
                    serialized = ms.ToArray();
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
            }

            return serialized;
        }

        private T Deserialize<T>(byte[] data)
        {
            T deserialized = default(T);

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    object deserializedObject = formatter.Deserialize(ms);

                    if (deserializedObject != null && deserializedObject is T)
                    {
                        deserialized = (T)deserializedObject;
                    }
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
            }

            return deserialized;
        }
    }
}
