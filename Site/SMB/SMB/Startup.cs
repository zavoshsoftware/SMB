using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMB.Startup))]
namespace SMB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
