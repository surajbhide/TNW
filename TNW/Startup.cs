using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TNW.Startup))]
namespace TNW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
