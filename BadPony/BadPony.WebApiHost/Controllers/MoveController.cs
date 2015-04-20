using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;

namespace BadPony.WebApiHost.Controllers
{
    public class MoveController : ApiController
    {
        public void Post(int objectId, int destinationId)
        {
            // Hardcoded to move the first player
            var player = Program.Game.GetAllObjects().OfType<Player>().FirstOrDefault();
            Program.Game.PostMessage(new MoveObjectMessage {DestinationId = destinationId, ObjectId = player.Id});
        }
    }
}
