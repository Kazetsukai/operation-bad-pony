using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BadPony.WebInterface.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BadPony.WebInterface.Controllers
{
    public class HereController : Controller
    {
        private static readonly string UserIdPrefix = "userId-";
        //
        // GET: /Here/
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var key = UserIdPrefix + userName;
            var userGameId = Session[key];

            dynamic playerObject;

            if (userGameId == null)
            {
                playerObject =
                    JsonConvert.DeserializeObject(
                        new WebClient().DownloadString("http://localhost:9090/api/Player/" + User.Identity.Name));


                /////////// TEMPORARILY HARDCODING TO KAZETSUKAI LOL /////////////////
                if (playerObject == null)
                    playerObject = JsonConvert.DeserializeObject(
                        new WebClient().DownloadString("http://localhost:9090/api/Player/Kazetsukai"));
                //////////////////////////////////////////////////////////////////////


                // TODO: Currently redirecting if the player doesn't have an object, need to change to creating an object later.
                if (playerObject == null)
                    return new RedirectResult("/");


                // Remember the player's object ID for later.
                Session[key] = playerObject.Id;
            }
            else
            {
                playerObject =
                    JsonConvert.DeserializeObject(
                        new WebClient().DownloadString("http://localhost:9090/api/Object/" + userGameId));
            }

            var locationId = playerObject.ContainerId;

            dynamic location =
                JsonConvert.DeserializeObject(new WebClient().DownloadString("http://localhost:9090/api/Location/" + locationId));

            ViewBag.Location = location;

            return View();
        }
	}
}