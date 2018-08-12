using Owin;

namespace Tracker
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<TrackerConnection>("/tracker");
        }
    }
}