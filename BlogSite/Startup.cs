using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogSite.Startup))]
namespace BlogSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
