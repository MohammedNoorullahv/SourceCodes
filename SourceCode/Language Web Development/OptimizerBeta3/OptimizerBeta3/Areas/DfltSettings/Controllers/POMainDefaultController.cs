using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.GeneralTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.DfltSettings.Controllers
{
    [Area("DfltSettings")]
    public class POMainDefaultController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public POMainDefaultViewModel POMainDefaultVM { get; set; }

        public POMainDefaultController(ApplicationDbContext db)
        {
            _db = db;
            POMainDefaultVM = new POMainDefaultViewModel()
            {
                FKModeofTransport = _db.lookUpMasters,
                FKDeliveryTo = _db.lookUpMasters,
                FKDestination = _db.lookUpMasters,
                FKOrderStatus = _db.lookUpMasters,
                POMainDefault = new Models.GeneralTables.POMainDefault()
            };
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.POMainDefaults.OrderByDescending(s => s.Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            POMainDefaultVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            POMainDefaultVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            POMainDefaultVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            POMainDefaultVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            return View(POMainDefaultVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(POMainDefaultViewModel poMainDefaultVM)
        {
            if (!ModelState.IsValid)
            {
                return View(poMainDefaultVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            poMainDefaultVM.POMainDefault.OrderStatus = lookUpMaster.Where(x => x.Id == poMainDefaultVM.POMainDefault.FKOrderStatus).FirstOrDefault().Description;
            poMainDefaultVM.POMainDefault.ModeofTransport = lookUpMaster.Where(x => x.Id == poMainDefaultVM.POMainDefault.FKModeofTransport).FirstOrDefault().Description;
            poMainDefaultVM.POMainDefault.DeliveryTo = lookUpMaster.Where(x => x.Id == poMainDefaultVM.POMainDefault.FKDeliveryTo).FirstOrDefault().Description;
            poMainDefaultVM.POMainDefault.Destination = lookUpMaster.Where(x => x.Id == poMainDefaultVM.POMainDefault.FKDestination).FirstOrDefault().Description;

            _db.POMainDefaults.Add(poMainDefaultVM.POMainDefault);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
