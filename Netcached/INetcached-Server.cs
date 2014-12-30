using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Netcached
{
    [ServiceContract]
    public interface INetcached_Server
    {
        [OperationContract]
        bool Set(string key, byte[] data, Int32 priority = 0);

        [OperationContract]
        bool Delete(string key);

        [OperationContract]
        bool Get(string key);
    }
}
