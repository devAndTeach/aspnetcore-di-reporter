using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class DIController : Controller
    {
        public IActionResult SystemDetails()
        {
            return View();
        }
    }
}