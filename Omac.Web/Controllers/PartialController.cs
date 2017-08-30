using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Omack.Web.Controllers
{
    public class PartialController : Controller
    {
        public IActionResult LoginComponent() { return PartialView(); }
    }
}