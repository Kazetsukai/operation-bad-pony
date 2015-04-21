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
    public class PlayerController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Player Get(string id)
        {
            logger.Debug("\tRetrieved player {0}", id);
            return Program.Game.GetPlayerByUsername(id);
        }

        public void Post(string id)
        {
            if (id != null)
            {
                logger.Debug("\tSent player {0}", id);
                Program.Game.PostMessage(new CreateNewPlayerMessage() { UserName = id, Name = id, Type = GameObjectType.Player });
            }
        }
    }
}
