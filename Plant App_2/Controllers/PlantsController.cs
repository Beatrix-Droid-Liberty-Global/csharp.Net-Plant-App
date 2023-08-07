using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Plant_App_2.Data;
using Plant_App_2.Models;

namespace Plant_App_2.Controllers
{
    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plants/
        public async Task<IActionResult> Index()
        {
              return _context.Plant != null ? 
                          View(await _context.Plant.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Plant'  is null.");
        }

        // GET: Plant/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Plant/ShowSearchResults 
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase) //captures the input from the form
        {
            return View("Index", await _context.Plant.Where(j=>j.latinName.Contains(SearchPhrase)).ToListAsync());
    //return View();
        }

        public async Task<IActionResult> IdentifyPlant()
        {
            return View();
        }





        //POST PLANT TO API


       // public async Task<JObject> identifyPlant()
        //{
            //string API_Key;
          //  string api_endpoint = $"https://my-api.plantnet.org/v2/identify/all?api-key={API_KEY}";



        //}

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plant == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .FirstOrDefaultAsync(m => m.id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,latinName")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plant);
        }

        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plant == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,latinName")] Plant plant)
        {
            if (id != plant.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plant);
        }

        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plant == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .FirstOrDefaultAsync(m => m.id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Plant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Plant'  is null.");
            }
            var plant = await _context.Plant.FindAsync(id);
            if (plant != null)
            {
                _context.Plant.Remove(plant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
          return (_context.Plant?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
