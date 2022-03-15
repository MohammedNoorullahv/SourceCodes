using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages
{
    [Area("MasterTablePages")]
    public class SizeMasterforLeatherGoodsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public SizeMasterforLeatherGoodsViewModel SizeMasterforLeatherGoodsVM { get; set; }

        public SizeMasterforLeatherGoodsController(ApplicationDbContext db)
        {
            _db = db;
            SizeMasterforLeatherGoodsVM = new SizeMasterforLeatherGoodsViewModel()
            {
                FKMeasurement = _db.lookUpMasters,
                SizeMasterforLeatherGoods = new Models.MasterTables.SizeMasterforLeatherGoods()
            };
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.SizeMasterforLeatherGoods.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            SizeMasterforLeatherGoodsVM.FKMeasurement = await _db.lookUpMasters.OrderByDescending(s  => s.SetAsDefault).ThenBy(s => s.Description).Where(s => s.FKLookUpCategory == 62 && s.IsActive == true).ToListAsync();

            return View(SizeMasterforLeatherGoodsVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(SizeMasterforLeatherGoodsVM);
            }

            SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods.Measurement = _db.lookUpMasters.Where(x => x.Id == SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods.FKMeasurement).FirstOrDefault().Description;

            _db.SizeMasterforLeatherGoods.Add(SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Create));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sm4lg = await _db.SizeMasterforLeatherGoods.SingleOrDefaultAsync(m => m.Id == id);

            if (sm4lg == null)
            {
                return NotFound();
            }
            
            SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods = await _db.SizeMasterforLeatherGoods.SingleOrDefaultAsync(m => m.Id == id);
            SizeMasterforLeatherGoodsVM.FKMeasurement = await _db.lookUpMasters.OrderByDescending(s => s.SetAsDefault).ThenBy(s => s.Description).Where(s => s.FKLookUpCategory == 62 && s.IsActive == true).ToListAsync();
            

            if (SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods == null)
            {
                return NotFound();
            }
            return View(SizeMasterforLeatherGoodsVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SizeMasterforLeatherGoodsViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                //var doesBankExist = _db.banks.Include(s => s.BranchName).Where(s => s.BranchName == model.bank.BranchName);

                var sm4lgfromDb = await _db.SizeMasterforLeatherGoods.FindAsync(id);
            //var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            sm4lgfromDb.Code = model.SizeMasterforLeatherGoods.Code;
                sm4lgfromDb.Description = model.SizeMasterforLeatherGoods.Description;
                sm4lgfromDb.ShortDescription = model.SizeMasterforLeatherGoods.ShortDescription;
                sm4lgfromDb.FKMeasurement = model.SizeMasterforLeatherGoods.FKMeasurement;
                sm4lgfromDb.ModifiedBy = model.SizeMasterforLeatherGoods.ModifiedBy;
                sm4lgfromDb.ModifiedDate = model.SizeMasterforLeatherGoods.ModifiedDate;
            sm4lgfromDb.Measurement = _db.lookUpMasters.Where(x => x.Id == model.SizeMasterforLeatherGoods.FKMeasurement).FirstOrDefault().Description;

            await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            //}
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sm4lg = await _db.SizeMasterforLeatherGoods.SingleOrDefaultAsync(m => m.Id == id);

            if (sm4lg == null)
            {
                return NotFound();
            }

            SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods = await _db.SizeMasterforLeatherGoods.SingleOrDefaultAsync(m => m.Id == id);
            SizeMasterforLeatherGoodsVM.FKMeasurement = await _db.lookUpMasters.OrderByDescending(s => s.SetAsDefault).ThenBy(s => s.Description).Where(s => s.FKLookUpCategory == 62 && s.IsActive == true).ToListAsync();

            if (SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods == null)
            {
                return NotFound();
            }
            return View(SizeMasterforLeatherGoodsVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sm4lg = await _db.SizeMasterforLeatherGoods.SingleOrDefaultAsync(m => m.Id == id);

            if (sm4lg == null)
            {
                return NotFound();
            }

            SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods = await _db.SizeMasterforLeatherGoods.SingleOrDefaultAsync(m => m.Id == id);
            SizeMasterforLeatherGoodsVM.FKMeasurement = await _db.lookUpMasters.OrderByDescending(s => s.SetAsDefault).ThenBy(s => s.Description).Where(s => s.FKLookUpCategory == 62 && s.IsActive == true).ToListAsync();

            if (SizeMasterforLeatherGoodsVM.SizeMasterforLeatherGoods == null)
            {
                return NotFound();
            }
            return View(SizeMasterforLeatherGoodsVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var sm4lg = await _db.SizeMasterforLeatherGoods.FindAsync(id);

            if (sm4lg == null)
            {
                return View();
            }
            _db.SizeMasterforLeatherGoods.Remove(sm4lg);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
