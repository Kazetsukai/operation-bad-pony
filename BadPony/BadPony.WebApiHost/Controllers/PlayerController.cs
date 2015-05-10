using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using BadPony.Core;
using NLog;

namespace BadPony.WebApiHost.Controllers
{
    [Authorize]
    public class PlayerController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Player Get()
        {
            string id = Utility.UserInfo.GetCurrentUserId(Request);
            if (id != null)
            {
                Player player = Program.Game.GetPlayerByUsername(id);
                if (player == null)
                {
                    logger.Debug("\tWAPI\tSent player {0}", id);
                    Program.Game.PostMessage(new CreateNewPlayerMessage() { UserName = id, Name = Utility.UserInfo.GetCurrentUserName(Request) });
                    player = Program.Game.GetPlayerByUsername(id);
                }

                logger.Debug("\tWAPI\tRetrieved player {0}", id);
                return player;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /*public Player Get(string id)
        {
            logger.Debug("\tWAPI\tRetrieved player {0}", id);
            return Program.Game.GetPlayerByUsername(id);
        }

        public void Post(string id)
        {
            if (id != null)
            {
                logger.Debug("\tWAPI\tSent player {0}", id);
                Program.Game.PostMessage(new CreateNewPlayerMessage() { UserName = id, Name = id });
            }
        }*/
    }
}
