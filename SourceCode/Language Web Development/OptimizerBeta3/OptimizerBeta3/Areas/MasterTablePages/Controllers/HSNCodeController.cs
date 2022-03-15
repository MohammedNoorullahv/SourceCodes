using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class HSNCodeController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public HSNCodeViewModel HSNCodeVM { get; set; }
        public HSNCodeController(ApplicationDbContext db)
        {
            _db = db;
            HSNCodeVM = new HSNCodeViewModel()
            {
                FKPercentageType = _db.lookUpMasters,
                HSNCodeMaster = new Models.MasterTables.HSNCodeMaster()
            };
        }
        public async Task<IActionResult> Index()
        {

            var hsnCode = await _db.HSNCodeMasters.OrderBy(c => c.Category).ToListAsync();
            return View(hsnCode);

        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            HSNCodeVM.FKPercentageType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 55).ToListAsync();
            return View(HSNCodeVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HSNCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesHSNCodeExist = _db.HSNCodeMasters.Where(s => s.Category == model.HSNCodeMaster.Category && s.FKPercentageType == model.HSNCodeMaster.FKPercentageType);

                if (doesHSNCodeExist.Count() > 0)
                {
                    ////ERROR.
                    //StatusMessage = "Error : Look Up Master Exists under " + doesLookUpMstExist.First().LookUpCategory.Description + " Category. Please use anothe name ";
                }
                else
                {
                    
                    var lookUpMaster = _db.lookUpMasters.Where(s => s.Id == model.HSNCodeMaster.FKPercentageType);
                    model.HSNCodeMaster.PercentageType= lookUpMaster.Where(x => x.Id == model.HSNCodeMaster.FKPercentageType).FirstOrDefault().Description;

                    _db.HSNCodeMasters.Add(model.HSNCodeMaster);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }
            }
            return View(HSNCodeVM);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HSNCode = await _db.HSNCodeMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (HSNCode == null)
            {
                return NotFound();
            }

            HSNCodeVM.HSNCodeMaster = await _db.HSNCodeMasters.SingleOrDefaultAsync(m => m.Id == id);
            HSNCodeVM.FKPercentageType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 55).ToListAsync();

            if (HSNCodeVM.HSNCodeMaster == null)
            {
                return NotFound();
            }
            return View(HSNCodeVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HSNCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var doesHSNCodeExist = _db.HSNCodeMasters.Where(s => s.BranchName == model.HSNCode.BranchName);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var HSNCodefromDb = await _db.HSNCodeMasters.FindAsync(id);

                HSNCodefromDb.HSNCode = model.HSNCodeMaster.HSNCode;
                HSNCodefromDb.Category = model.HSNCodeMaster.Category;
                HSNCodefromDb.FKPercentageType = model.HSNCodeMaster.FKPercentageType;
                HSNCodefromDb.GSTPercentage = model.HSNCodeMaster.GSTPercentage;
                HSNCodefromDb.ModifiedBy = model.HSNCodeMaster.ModifiedBy;
                HSNCodefromDb.ModifiedDate = model.HSNCodeMaster.ModifiedDate;

                var lookUpMaster = _db.lookUpMasters.Where(s => s.Id == model.HSNCodeMaster.FKPercentageType);
                HSNCodefromDb.PercentageType = lookUpMaster.Where(x => x.Id == model.HSNCodeMaster.FKPercentageType).FirstOrDefault().Description;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            HSNCodeViewModel modelVM = new HSNCodeViewModel()
            {
                //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
                //LookUpMasters = model.LookUpMasters,
                ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HSNCode = await _db.HSNCodeMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (HSNCode == null)
            {
                return NotFound();
            }

            HSNCodeVM.HSNCodeMaster = await _db.HSNCodeMasters.SingleOrDefaultAsync(m => m.Id == id);
            HSNCodeVM.FKPercentageType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 55).ToListAsync();

            if (HSNCodeVM.HSNCodeMaster == null)
            {
                return NotFound();
            }
            return View(HSNCodeVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HSNCode = await _db.HSNCodeMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (HSNCode == null)
            {
                return NotFound();
            }

            HSNCodeVM.HSNCodeMaster = await _db.HSNCodeMasters.SingleOrDefaultAsync(m => m.Id == id);
            HSNCodeVM.FKPercentageType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 55).ToListAsync();

            if (HSNCodeVM.HSNCodeMaster == null)
            {
                return NotFound();
            }
            return View(HSNCodeVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, HSNCodeViewModel model)
        {
            var HSNCode = await _db.HSNCodeMasters.FindAsync(id);

            if (HSNCode == null)
            {
                return View();
            }
            _db.HSNCodeMasters.Remove(HSNCode);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
