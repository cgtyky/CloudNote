using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudNoteV1.Startup))]
namespace CloudNoteV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
