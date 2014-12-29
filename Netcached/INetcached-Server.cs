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
        bool Set(string key, byte[] data, bool replace = true);

        [OperationContract]
        bool Get(string key);
    }
}
