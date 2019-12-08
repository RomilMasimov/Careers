﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.Specialist.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadPortrait()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult UploadPortrait(IFormFile Image)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Works()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadWork()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadWork(IFormFile Image)
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadPassport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadPassport(IFormFile Image)
        {
            return View();
        }

        public IActionResult Educations()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEducation(Education education)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditAbout(string text)
        {
            return View();
        }

        public IActionResult WhereCanGo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWhereCanGo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWhereCanGo(MeetingPoint point)
        {
            return View();
        }

        public IActionResult WhereCanMeet()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWhereCanMeet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWhereCanMeet(MeetingPoint point)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditCity(City city)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditServices()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditServices(object services)  // Add a ViewModel
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}