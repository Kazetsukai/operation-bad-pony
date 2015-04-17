using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;

namespace BadPony.WebApiHost.Controllers
{
    public class ObjectController : ApiController
    {
        public GameObject Get(int id)
        {
            return Program.Game.GetObject(id);
        }
    }
}
