﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GithubTarget.Controllers {
    public class HomeController : Controller {
        //
        // GET: /Home/
        public ActionResult Index() {
            ViewBag.Message = "Hello github";
            return View();
        }
    }
}