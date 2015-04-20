using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace BadPony.WebInterface.Controllers
{
    // Oh wow this is so terrible I have a duplicate code.
    // Ian you are right we should scrap this interface layer and do everything client side with Angular or something.
    // I can write shitty comments here because this file's lifespan should be no longer than a week.
    public class MoveController : ApiController
    {
        public void Get(int id)
        {
            dynamic playerObject =
                JsonConvert.DeserializeObject(
                    new WebClient().DownloadString("http://localhost:9090/api/Player/" + User.Identity.Name));

            int playerid = playerObject.Id;

            new WebClient().UploadString("http://localhost:9090/api/Move/" + playerid + "/" + id, "");
        }
    }
}