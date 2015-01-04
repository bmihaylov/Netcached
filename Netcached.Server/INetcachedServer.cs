using System;
using System.ServiceModel;

namespace Netcached.Server
{
    [ServiceContract]
    public interface INetcachedServer
    {
        [OperationContract]
        bool Set(string key, byte[] data, Int32 priority = 0);

        [OperationContract]
        bool Delete(string key);

        [OperationContract]
        byte[] Get(string key);
    }
}
