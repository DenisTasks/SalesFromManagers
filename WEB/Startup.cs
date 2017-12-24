using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth.Messages;


[assembly: OwinStartup(typeof(IdentityApp.Startup))]
namespace IdentityApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureSignalR(app);
        }
    }
}
