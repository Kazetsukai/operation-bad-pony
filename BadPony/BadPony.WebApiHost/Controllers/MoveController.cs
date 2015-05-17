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
    public class MoveController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Post(MoveObjectMessage message)
        {
            Program.Game.PostMessage(message);
            logger.Debug("\tWAPI\tObject: {0} moved to Location: {1}", message.ObjectId, message.DestinationId);
        }
    }
}
