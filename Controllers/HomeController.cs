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
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private static Logger Log = LogManager.GetCurrentClassLogger();

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
            try
            {
                var movies = from m in _context.Movies
                         orderby m.Title ascending
                         select m;

                return View(await movies.ToListAsync());
            }
            catch (Exception e)
            {
                Log.LogException(LogLevel.Error, "There was an error getting Movies ", e.InnerException);
                return NotFound();
            }
            
        }
    }
}
