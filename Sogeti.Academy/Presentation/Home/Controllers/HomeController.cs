﻿using Microsoft.AspNet.Mvc;

namespace Sogeti.Academy.Presentation.Home.Controllers
{
    [Route("home")]
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
