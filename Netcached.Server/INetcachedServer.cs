using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
