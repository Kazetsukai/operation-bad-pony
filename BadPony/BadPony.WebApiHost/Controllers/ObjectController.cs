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
    [Authorize]
    public class ObjectController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GameObject Get(int id)
        {
            logger.Debug("\tWAPI\tRequested object: {0}", id);
            return Program.Game.GetObject(id);
        }
    }
}
