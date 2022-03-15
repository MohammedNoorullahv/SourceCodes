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
    public class LookUpCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LookUpCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.lookUpCategories.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LookUpCategory lookUpCategory)
        {
            if (ModelState.IsValid)
            {
                _db.lookUpCategories.Add(lookUpCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(lookUpCategory);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var LookUPHdr = await _db.lookUpCategories.FindAsync(id);
            if (LookUPHdr == null)
            {
                return NotFound();
            }
            return View(LookUPHdr);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LookUpCategory lookUpCategory)
        {
            if (ModelState.IsValid)
            {
                var LookUPHdr = await _db.lookUpCategories.FindAsync(lookUpCategory.Id);
                LookUPHdr.SlNo = lookUpCategory.SlNo;
                LookUPHdr.Description = lookUpCategory.Description;
                LookUPHdr.ShortCode = lookUpCategory.ShortCode;
                LookUPHdr.ModifiedBy = lookUpCategory.ModifiedBy;
                LookUPHdr.ModifiedDate = DateTime.Now;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(lookUpCategory);
        }

        //GET - Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var LookUPHdr = await _db.lookUpCategories.FindAsync(id);
            if (LookUPHdr == null)
            {
                return NotFound();
            }
            return View(LookUPHdr);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var LookUPHdr = await _db.lookUpCategories.FindAsync(id);
            if (LookUPHdr == null)
            {
                return NotFound();
            }
            return View(LookUPHdr);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var category = await _db.lookUpCategories.FindAsync(id);

            if (category == null)
            {
                return View();
            }
            _db.lookUpCategories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsDescriptionExists(string description)
        {
            if (_db.lookUpCategories.Any(x => x.Description == description))
            {
                return Json($"Description {description} is already exist.");
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsSlNoExists(int slNo)
        {
            if (_db.lookUpCategories.Any(x => x.SlNo == slNo))
            {
                return Json($"Serial No. {slNo} is already exist.");
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsShortCodeExists(string shortCode)
        {
            if (_db.lookUpCategories.Any(x => x.ShortCode == shortCode))
            {
                return Json($"Short Code {shortCode} is already exist.");
            }
            return Json(true);
        }
    }
}
