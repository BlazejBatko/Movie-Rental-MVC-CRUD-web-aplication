using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.Controllers
{


    public class FilmsController : Controller
    {
        private readonly FilmContext _context;

        public FilmsController(FilmContext context)
        {
            _context = context;
        }

        
        // GET: Films
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }
            
        [HttpGet]

            

        /*public async Task<IActionResult> Index (string FilmSearch)
        {
            ViewData["GetFilmDetails"] = FilmSearch;

            var Filmquery = from x in _context.Film select x;

            if (!String.IsNullOrEmpty(FilmSearch))
            {
                Filmquery = Filmquery.Where(x => x.Name.Contains(FilmSearch) || x.Genre.Contains(FilmSearch) ||
                  x.Director.Contains(FilmSearch) || x.Available.Contains(FilmSearch));
            }
            
            return View(await Filmquery.AsNoTracking().ToListAsync());

        }*/

        public async Task<IActionResult> Index (string sortOrder, string FilmSearch)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
            ViewData["DirectorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "director_desc" : "";
            ViewData["AvailabilitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "director_desc" : "";
            ViewData["CurrentFilter"] = FilmSearch;
            
            var films = from s in _context.Film
                           select s;
            if (!String.IsNullOrEmpty(FilmSearch))
            {
                films = films.Where(x => x.Name.Contains(FilmSearch) || x.Genre.Contains(FilmSearch) ||
                  x.Director.Contains(FilmSearch) || x.Available.Contains(FilmSearch));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    films = films.OrderBy(s => s.Name);
                   
                    break;

                case "genre_desc":
                    films = films.OrderBy(s => s.Genre);

                    break;

                case "director_desc":
                    films = films.OrderBy(s => s.Director);

                    break;

                
            }

            return View(await films.AsNoTracking().ToListAsync()); /*brakiem śledzenia danych przez kontekst*/
        }



        [HttpGet]
        


        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.FilmID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
        [Authorize]
        // GET: Films/Create
        public IActionResult Create(int id=0) //domyslne id = 0
        {
            return View(new Film());
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmID,Name,Genre,Director,Available")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Created: " + film.Name + " successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmID,Name,Genre,Director,Available")] Film film)
        {
            if (id != film.FilmID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Edited: " + film.Name + " successfully";
                return RedirectToAction(nameof(Index));

            }
            return View(film);
        }

        // GET: Films/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.FilmID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
            
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Deleted: " + film.Name + " successfully";
            return RedirectToAction(nameof(Index));
            
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.FilmID == id);
        }
    }
}
