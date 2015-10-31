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
            string authId = Utility.UserInfo.GetCurrentUserId(Request);
            if (authId != null)
            {
                Player player = Program.Game.GetPlayerByUsername(authId);
                if (player == null)
                {
                    logger.Debug("\tWAPI\tSent player {0}", authId);
                    Program.Game.PostMessage(new CreateNewPlayerMessage() { UserName = authId, Name = Utility.UserInfo.GetCurrentUserName(Request) });
                    player = Program.Game.GetPlayerByUsername(authId);                    
                }
                player.LastActionTime = Program.UpTime;
                logger.Debug("\tWAPI\tRetrieved player {0}", authId);
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
