using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Netcached.Client.Serializing
{
    public class DataSerializer
    {
        public static byte[] Serialize<T>(T data)
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

        public static T Deserialize<T>(byte[] data)
        {
            T deserialized = default(T);

            using (MemoryStream ms = new MemoryStream(data))
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