﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Assistent()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Log()
        {
            return View();
        }
    }
}