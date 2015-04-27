using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;
using NLog;

namespace BadPony.WebApiHost.Controllers
{
    public class JobController : ApiController
    {
        public Job Get(int id)
        {
            return (Job)Program.Game.GetObject(id);
        }

        public bool Post(DoJobMessage message)
        {            
            return Program.Game.PostMessage(message);            
        }
    }
}
