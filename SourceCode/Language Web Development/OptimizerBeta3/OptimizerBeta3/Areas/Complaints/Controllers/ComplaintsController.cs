using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.GeneralTables;

namespace OptimizerBeta3.Areas.Complaints.Controllers
{
    [Area("Complaints")]
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ComplaintsViewModel ComplaintsVM { get; set; }

        public ComplaintsController(ApplicationDbContext db)
        {
            _db = db;
            ComplaintsVM = new ComplaintsViewModel()
            {
                FKMenuCategory = _db.ComplaintLookUpMasters,
                FKMenuName = _db.ComplaintLookUpMasters,
                FKLocation = _db.ComplaintLookUpMasters,
                FKAdminName = _db.ComplaintLookUpMasters,
                FKUserName = _db.ComplaintLookUpMasters,
                FKStatus = _db.ComplaintLookUpMasters,
                Complaint = new Models.GeneralTables.Complaint()
            };
        }
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;


            return View(await _db.Complaints.OrderByDescending(s => s.Id).Where(x => x.ComplaintDt >= effectStartDate && x.ComplaintDt <= effectEndDate).Include(s => s.LookUpMasterMenuCategory)
                .Include(s => s.LookUpMasterMenuName).Include(s => s.LookUpMasterLocation).Include(s => s.LookUpMasterAdminName)
                .Include(s => s.LookUpMasterUserName).Include(s => s.LookUpMasterStatus)
                .ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "Complaints", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            ComplaintsVM.FKMenuCategory = await _db.ComplaintLookUpMasters.OrderBy(c => c.Description).Where(c => c.FKCategory == 1 && c.IsActive == true).ToListAsync();
            ComplaintsVM.FKMenuName = await _db.ComplaintLookUpMasters.OrderBy(c => c.Description).Where(c => c.FKCategory == 2 && c.IsActive == true).ToListAsync();
            ComplaintsVM.FKLocation = await _db.ComplaintLookUpMasters.OrderBy(c => c.Description).Where(c => c.FKCategory == 3 && c.IsActive == true).ToListAsync();
            ComplaintsVM.FKAdminName = await _db.ComplaintLookUpMasters.OrderBy(c => c.Description).Where(c => c.FKCategory == 4 && c.IsActive == true).ToListAsync();
            ComplaintsVM.FKUserName = await _db.ComplaintLookUpMasters.OrderBy(c => c.Description).Where(c => c.FKCategory == 6 && c.IsActive == true).ToListAsync();
            ComplaintsVM.FKStatus = await _db.ComplaintLookUpMasters.OrderBy(c => c.Description).Where(c => c.FKCategory == 5 && c.IsActive == true).ToListAsync();
             return View(ComplaintsVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ComplaintsVM);
            }

            _db.Complaints.Add(ComplaintsVM.Complaint);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
    }
}
