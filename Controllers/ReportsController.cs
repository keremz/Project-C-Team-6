﻿using System;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using AuthSystem.Areas.Identity.Data;

using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.TagHelpers;


namespace AuthSystem.Controllers
{
    public class ReportsController : Controller
    {
        //private readonly IHtmlLocalizer<ProductsController> _localizer;
        private readonly AuthDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ReportsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AuthDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }




        public async Task<IActionResult> Index()
        {
            var reports = from p in _context.Reports
                           select p;

            return View(await reports.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        public async Task<IActionResult> Ban(string id, string bannedreason)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            user.Banned = true;
            user.BannedReason = bannedreason;
            await _userManager.UpdateAsync(user); 
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction(nameof(Index));
        }

        
        //[HttpGet("{repid}")]
        public async Task<IActionResult> Create(string repid)
        {

            ViewData["ReportedId"] = repid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportedUserId,Reporter,Subject,Explanation")] Report report)
        {

            _context.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ReportFinished));

        }

        public IActionResult ReportFinished()
        {
            return View();
        }
    }
}