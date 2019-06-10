using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Attempt2.Startup))]
namespace Attempt2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
