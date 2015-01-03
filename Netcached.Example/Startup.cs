using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Netcached.Example.Startup))]
namespace Netcached.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
