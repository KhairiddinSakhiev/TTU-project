﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{

    [Area("Admin")]
   // [Authorize]
    public class BaseController : Controller
    {

        public  IActionResult Index()
        {
            return View();
        }
        
    }
}
