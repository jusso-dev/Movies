using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Microsoft.AspNetCore.Authorization;
using Movies.Models;
using NLog;

namespace Movies.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.ApplicationUser> _userManager;
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public static string userAdminEmail = "admin@admin.com";

        public MovieController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;    
            _userManager = userManager;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            if(email == userAdminEmail) 
            {
                return View(await _context.Movies.ToListAsync());
            }

            else return Redirect("/Home/Forbidden");
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.id == id);
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            if (movie == null)
            {
                return NotFound();
            }

            if(email == userAdminEmail) 
            {
                return View(movie);
            }

            else return Redirect("/Home/Forbidden");
        }

        // GET: Movie/Create
        public async Task <IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            if(email == userAdminEmail) 
            {
                return View();
            }

            else return Redirect("Home/Forbidden");
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Genre,MainActor,ReleaseDate,Title,Worth,imageUrl")] Movie movie)
        {
            if (ModelState.IsValid || !ModelState.IsValid)
            {
                try
                {
                    _context.Add(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Log.LogException(LogLevel.Error, "There was an error adding Movie ", e.InnerException);
                }
                
                
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.id == id);
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            if (movie == null)
            {
                return NotFound();
            }

            if(email == userAdminEmail) 
            {
                return View(movie);
            }
            
            else return Redirect("Home/Forbidden");
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Genre,MainActor,ReleaseDate,Title,Worth,imageUrl")] Movie movie)
        {
            if (id != movie.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!MovieExists(movie.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Log.LogException(LogLevel.Error, "Movie already exists! ", e.InnerException);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.id == id);
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            if (movie == null)
            {
                return NotFound();
            }

            if(email == userAdminEmail) 
            {
                return View(movie);
            }

            else return Redirect("/Home/Forbidden");
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var movie = await _context.Movies.SingleOrDefaultAsync(m => m.id == id);
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.LogException(LogLevel.Error, "There was an error removing Movie ", e.InnerException);
                return Redirect("/Home/Error");
            }
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.id == id);
        }
    }
}
