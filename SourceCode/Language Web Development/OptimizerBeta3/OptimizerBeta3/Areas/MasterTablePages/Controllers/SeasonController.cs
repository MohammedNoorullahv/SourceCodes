using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class SeasonController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SeasonController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.seasons.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Season season)
        {
            if (ModelState.IsValid)
            {
                _db.seasons.Add(season);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(season);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Season = await _db.seasons.FindAsync(id);
            if (Season == null)
            {
                return NotFound();
            }
            return View(Season);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Season season)
        {
            if (ModelState.IsValid)
            {
                var Season = await _db.seasons.FindAsync(season.Id);
                Season.Code = season.Code;
                Season.Description = season.Description;
                Season.StartDate = season.StartDate;
                Season.EndDate = season.EndDate;
                Season.ModifiedBy = season.ModifiedBy;
                Season.ModifiedDate = DateTime.Now;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(season);
        }

        //GET - Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Season = await _db.seasons.FindAsync(id);
            if (Season == null)
            {
                return NotFound();
            }
            return View(Season);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Season = await _db.seasons.FindAsync(id);
            if (Season == null)
            {
                return NotFound();
            }
            return View(Season);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var season = await _db.seasons.FindAsync(id);

            if (season == null)
            {
                return View();
            }
            _db.seasons.Remove(season);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
