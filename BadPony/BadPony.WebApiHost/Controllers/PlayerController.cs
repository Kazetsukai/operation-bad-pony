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

        public void Post(string id)
        {
            if (id != null)
            {
                Program.Game.PostMessage(new CreateNewPlayerMessage() { UserName = id, Name = id });
            }
        }
    }
}
