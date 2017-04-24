using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webclient.Startup))]
namespace webclient
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
