using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoProj.Startup))]
namespace DemoProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
