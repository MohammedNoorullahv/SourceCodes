using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;

namespace OptimizerBeta3.Areas.Complaints.Controllers
{
    [Area("Complaints")]
    public class ComplaintLookUpMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ComplaintLookUpMasterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var lookUpMaster = await _db.ComplaintLookUpMasters.OrderBy(c => c.FKCategory).ToListAsync();
            return View(lookUpMaster);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComplaintLookUpMaster model)
        {
            if (ModelState.IsValid)
            {
                if (model.FKCategory < 1 || model.FKCategory > 6)
                {
                    TempData["ErrorMessage"] = "Invalid FKCategory";
                    return RedirectToAction(nameof(Create));
                }
                _db.ComplaintLookUpMasters.Add(model);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(model);
        }


    }
}
