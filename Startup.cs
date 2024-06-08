using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwimmingLessonManagementSystem.Startup))]
namespace SwimmingLessonManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
