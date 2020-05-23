using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UOBEVENT_Code_first_2.Startup))]
namespace UOBEVENT_Code_first_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
