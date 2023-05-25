using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLTS.Startup))]
namespace QLTS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
