using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FluentBootstrap.Tests.Web.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public virtual Microsoft.AspNetCore.Mvc.ActionResult Index()
        {
            return View();
        }
    }
}