using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testDMS.Startup))]
namespace testDMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
