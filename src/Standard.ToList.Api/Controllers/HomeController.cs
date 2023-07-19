﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Standard.ToList.Api.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Welcome");
        }
    }
}

