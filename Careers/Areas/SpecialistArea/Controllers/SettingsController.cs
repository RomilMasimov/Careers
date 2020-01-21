﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Areas.SpecialistArea.ViewModels;
using Careers.Models;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.SpecialistArea.Controllers
{
    [Area("SpecialistArea")]
    [Authorize(Roles = "admin,specialist")]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISpecialistService _specialistService;

        public SettingsController(UserManager<AppUser> userManager, ISpecialistService specialistService)
        {
            _userManager = userManager;
            _specialistService = specialistService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            return View(specialist);
        }

        [HttpPost]
        public IActionResult TakeOrders(bool flag)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReceiveMessages(bool flag)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReceiveNotifications(bool flag)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditContacts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);

            var model = new ContactsViewModel()
            {
                Name = specialist.Name,
                Surname = specialist.Surname,
                Fathername = specialist.Fathername,
                PhoneNumber = specialist.AppUser.PhoneNumber,
                Email = specialist.AppUser.Email
            };

            setImageUrl(specialist);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContacts(ContactsViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            if (ModelState.IsValid)
            {
                specialist.Name = model.Name;
                specialist.Surname = model.Surname;
                specialist.Fathername = model.Fathername;
                specialist.AppUser.PhoneNumber = model.PhoneNumber;
                specialist.AppUser.Email = model.Email;
                var result = await _specialistService.UpdateAsync(specialist);
                if (result != null)
                {
                    TempData["Status"] = "Contacts has been edited";
                    return RedirectToAction("Index");
                }
            }
            setImageUrl(specialist);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAdditionally()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);

            var model = new EditAdditionallyViewModel()
            {
                Birthday = specialist.DateOfBirth
            };

            setImageUrl(specialist);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdditionally(EditAdditionallyViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            if (ModelState.IsValid)
            {
                specialist.DateOfBirth = model.Birthday;
                var result = await _specialistService.UpdateAsync(specialist);
                if (result != null)
                {
                    TempData["Status"] = "Additionally information has been edited";
                    return RedirectToAction("Index");
                }
            }
            setImageUrl(specialist);
            return View(model);
        }

        private string setImageUrl(Specialist specialist)
        {
            string path = specialist.ImageUrl;
            if (string.IsNullOrWhiteSpace(specialist.ImageUrl))
                path = "N/A";
            ViewData["ImageUrl"] = path;
            return path;
        }
    }
}