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
    public class MoveController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Post(int objectId, int destinationId)
        {
            // Hardcoded to move the first player
            var player = Program.Game.GetAllObjects().OfType<Player>().FirstOrDefault();
            Program.Game.PostMessage(new MoveObjectMessage {DestinationId = destinationId, ObjectId = player.Id});
            logger.Debug("\tPlayer: {0} moved to Location: {1}", player.Id, destinationId);
        }
    }
}
