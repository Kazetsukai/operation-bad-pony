﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BadPony.WebInterface.Controllers
{
    public class HereController : Controller
    {
        //
        // GET: /Here/
        public ActionResult Index(int id = 0)
        {
            dynamic loc = JsonConvert.DeserializeObject(new WebClient().DownloadString("http://localhost:9090/api/Location/" + id));

            ViewBag.Location = loc;

            return View();
        }
	}
}