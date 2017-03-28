using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using NLog;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private static Logger Log = LogManager.GetCurrentClassLogger();

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult CreateMovie()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Forbidden() 
        {
            return View();
        }
        public IActionResult AdminOnly() 
        {
            return View();
        }

        public async Task <IActionResult> Movies()
        {
            return View();
        }
    }
}
