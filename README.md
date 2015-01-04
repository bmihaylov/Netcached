Netcached
=========

Overview
--------
Netcached consists of two parts:
* An in memory key-value store server
* А client class with methods that encapsulate the needed [de]serialization and calls to the server  
  

Installation
------------
* To install the server - add an application to an IIS site and set the physical path of the application to the Netcached.Server folder. Also an https binding is required for the server
* To use the client - simply add reference the Netcached.Client.dll file in your project after building the Netcached project in Visual Studio  
  
  

Configuration
-------------
* The server supports the configuration of the maximum amount of memory it should use at a time This is done through the allowedMegabytes setting in Web.config
* The client supports configuration of multiple servers, to be used for caching. Examples are given in the NetcachedClient's app.config file  
  
  
Use
---
After setting up the server[s] and referencing the client library all that is needed is to import Netcached.Client and use the public methods like so:
```
NetcachedClient client = new NetcachedClient();
client.Set("some_key", value);
int n = client.Get<int>("another_key");
client.Delete("yet_another_key");
```  
Note that the type parameter for Get is neeed for deserialization to work for arbitrary types.  
Look at Netcached.Example to see how Netcached can be integrated in a project.
