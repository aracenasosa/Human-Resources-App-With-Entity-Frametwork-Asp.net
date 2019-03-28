using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Human_Resources.Startup))]
namespace Human_Resources
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
