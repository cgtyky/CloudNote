using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(deneme_git.Startup))]
namespace deneme_git
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
