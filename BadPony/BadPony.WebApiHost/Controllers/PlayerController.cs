using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;

namespace BadPony.WebApiHost.Controllers
{
    public class PlayerController : ApiController
    {
        public Player Get(string id)
        {
            return Program.Game.GetPlayerByUsername(id);
        }

        public void Post(dynamic player)
        {
            if (player != null)
            {
                Program.Game.PostMessage(new CreateNewPlayerMessage() { UserName = player.UserName, Name = player.Name});
            }
        }
    }
}
