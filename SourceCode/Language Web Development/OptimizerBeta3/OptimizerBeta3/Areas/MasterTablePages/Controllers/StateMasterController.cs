using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class StateMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StateMasterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.StateMasters.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MdlStateMaster mdlStateMaster)
        {
            if (ModelState.IsValid)
            {
                _db.StateMasters.Add(mdlStateMaster);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(mdlStateMaster);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var StateMaster = await _db.StateMasters.FindAsync(id);
            if (StateMaster == null)
            {
                return NotFound();
            }
            return View(StateMaster);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MdlStateMaster mdlStateMaster)
        {
            if (ModelState.IsValid)
            {
                var StateMaster = await _db.StateMasters.FindAsync(mdlStateMaster.Id);
                StateMaster.StateCode = mdlStateMaster.StateCode;
                StateMaster.StateName = mdlStateMaster.StateName;
                StateMaster.ShortName = mdlStateMaster.ShortName;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(mdlStateMaster);
        }
    }
}
