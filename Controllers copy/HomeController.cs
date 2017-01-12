﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Movies.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Forbidden() 
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AdminOnly() 
        {
            return View();
        }

        public async Task <IActionResult> Movies()
        {
            var movies = from m in _context.Movies
                         orderby m.Title
                         select m;   

            return View(await movies.ToListAsync());
        }
    }
}