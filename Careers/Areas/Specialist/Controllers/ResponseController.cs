﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.SpecialistArea.Controllers
{
    public class ResponseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Respond(OrderResponse response)
        {
            return View();
        }
    }
}