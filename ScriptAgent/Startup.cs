using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptAgent
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            

            //app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
