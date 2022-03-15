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
    public class SizeMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public SizeMasterViewModel sizeMasterVM { get; set; }
        public SizeAssortmentViewModel sizeAssortmentVM { get; set; }

        public static int NFKSizeMaster;
        public SizeMasterController(ApplicationDbContext db)
        {
            _db = db;
            sizeMasterVM = new SizeMasterViewModel()
            {
                FKSizeCategory = _db.lookUpMasters,
                FKSizeFor = _db.lookUpMasters,
                SizeMaster = new Models.MasterTables.SizeMaster()
            };
            sizeAssortmentVM = new SizeAssortmentViewModel()
            {
                //FKSizeMaster = _db.sizeMasters,
                SizeAssortment = new Models.MasterTables.SizeAssortment()
            };
        }
        public async Task<IActionResult> Index()
        {
            var sizemaster = await _db.sizeMasters.Include(s => s.LookUpSizeCategory).Include(s => s.LookUpSizeFor).ToListAsync();
            //return View(await _db.sizeMasters.ToListAsync());
            return View(sizemaster);
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            sizeMasterVM.FKSizeCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 22).ToListAsync();
            sizeMasterVM.FKSizeFor = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 23).ToListAsync();

            return View(sizeMasterVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(sizeMasterVM);
            }

            _db.sizeMasters.Add(sizeMasterVM.SizeMaster);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SizeMaster = await _db.sizeMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (SizeMaster == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            sizeMasterVM.SizeMaster = await _db.sizeMasters.Include(m => m.LookUpSizeCategory).Include(m => m.LookUpSizeFor).SingleOrDefaultAsync(m => m.Id == id);
            sizeMasterVM.FKSizeCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 22).ToListAsync();
            sizeMasterVM.FKSizeFor = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 23).ToListAsync();

            if (sizeMasterVM.SizeMaster == null)
            {
                return NotFound();
            }
            return View(sizeMasterVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SizeMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSizeMasterExist = _db.sizeMasters.Include(s => s.Description).Where(s => s.Description == model.SizeMaster.Description);

                //if (doesSizeMasterExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var CompanyInfofromDb = await _db.sizeMasters.FindAsync(id);

                CompanyInfofromDb.FKSizeCategory = model.SizeMaster.FKSizeCategory;
                CompanyInfofromDb.FKSizeFor = model.SizeMaster.FKSizeFor;
                CompanyInfofromDb.Code = model.SizeMaster.Code;
                CompanyInfofromDb.Description = model.SizeMaster.Description;
                CompanyInfofromDb.IsHalfSize = model.SizeMaster.IsHalfSize;
                CompanyInfofromDb.Size01 = model.SizeMaster.Size01;
                CompanyInfofromDb.Size02 = model.SizeMaster.Size02;
                CompanyInfofromDb.Size03 = model.SizeMaster.Size03;
                CompanyInfofromDb.Size04 = model.SizeMaster.Size04;
                CompanyInfofromDb.Size05 = model.SizeMaster.Size05;
                CompanyInfofromDb.Size06 = model.SizeMaster.Size06;
                CompanyInfofromDb.Size07 = model.SizeMaster.Size07;
                CompanyInfofromDb.Size08 = model.SizeMaster.Size08;
                CompanyInfofromDb.Size09 = model.SizeMaster.Size09;
                CompanyInfofromDb.Size10 = model.SizeMaster.Size10;
                CompanyInfofromDb.Size11 = model.SizeMaster.Size11;
                CompanyInfofromDb.Size12 = model.SizeMaster.Size12;
                CompanyInfofromDb.Size13 = model.SizeMaster.Size13;
                CompanyInfofromDb.Size14 = model.SizeMaster.Size14;
                CompanyInfofromDb.Size15 = model.SizeMaster.Size15;
                CompanyInfofromDb.Size16 = model.SizeMaster.Size16;
                CompanyInfofromDb.Size17 = model.SizeMaster.Size17;
                CompanyInfofromDb.Size18 = model.SizeMaster.Size18;
                CompanyInfofromDb.ModifiedBy = model.SizeMaster.ModifiedBy;
                CompanyInfofromDb.ModifiedDate = model.SizeMaster.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            SizeMasterViewModel modelVM = new SizeMasterViewModel()
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

            var SizeMaster = await _db.sizeMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (SizeMaster == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            sizeMasterVM.SizeMaster = await _db.sizeMasters.Include(m => m.LookUpSizeCategory).Include(m => m.LookUpSizeFor).SingleOrDefaultAsync(m => m.Id == id);
            sizeMasterVM.FKSizeCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 22).ToListAsync();
            sizeMasterVM.FKSizeFor = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 23).ToListAsync();

            if (sizeMasterVM.SizeMaster == null)
            {
                return NotFound();
            }
            return View(sizeMasterVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SizeMaster = await _db.sizeMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (SizeMaster == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            sizeMasterVM.SizeMaster = await _db.sizeMasters.Include(m => m.LookUpSizeCategory).Include(m => m.LookUpSizeFor).SingleOrDefaultAsync(m => m.Id == id);
            sizeMasterVM.FKSizeCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 22).ToListAsync();
            sizeMasterVM.FKSizeFor = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 23).ToListAsync();

            if (sizeMasterVM.SizeMaster == null)
            {
                return NotFound();
            }
            return View(sizeMasterVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var SizeMaster = await _db.sizeMasters.FindAsync(id);

            if (SizeMaster == null)
            {
                return View();
            }
            _db.sizeMasters.Remove(SizeMaster);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SizeAssortmentIndex(int Id)
        {
            var sizeMaster = await _db.sizeMasters.FindAsync(Id);
            NFKSizeMaster = sizeMaster.Id;
            TempData["SizeMaster"] = sizeMaster;

            return View(await _db.sizeAssortments.Where(x => x.FKSizeMaster == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> SizeAssortmentCreate()
        {
            var sizeMaster = await _db.sizeMasters.FindAsync(NFKSizeMaster);
            TempData["SizeMaster"] = sizeMaster;
            
            return View(sizeAssortmentVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SizeAssortmentCreate(SizeAssortmentViewModel model)
        {
            if (model.SizeAssortment != null)
            {
                _db.sizeAssortments.Add(model.SizeAssortment);
                await _db.SaveChangesAsync();

                return RedirectToAction("SizeAssortmentCreate", "SizeMaster", new { Id = NFKSizeMaster });
            }
            return View(sizeAssortmentVM);
        }

        //GET - EDIT
        public async Task<IActionResult> SizeAssortmentEdit(int? Id)
        {
            var sizeMaster = await _db.sizeMasters.FindAsync(NFKSizeMaster);
            TempData["SizeMaster"] = sizeMaster;

            if (Id == null)
            {
                return NotFound();
            }

            var sizeAssortment = await _db.sizeAssortments.SingleOrDefaultAsync(m => m.Id == Id);

            if (sizeAssortment == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            sizeAssortmentVM.SizeAssortment = await _db.sizeAssortments.SingleOrDefaultAsync(m => m.Id == Id);

            if (sizeAssortmentVM.SizeAssortment == null)
            {
                return NotFound();
            }
            return View(sizeAssortmentVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SizeAssortmentEdit(int id, SizeAssortmentViewModel model)
        {
            if (model.SizeAssortment != null)
            {
                //var doesSizeMasterExist = _db.sizeMasters.Include(s => s.Description).Where(s => s.Description == model.SizeMaster.Description);

                //if (doesSizeMasterExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var SizeAssortmentfromDb = await _db.sizeAssortments.FindAsync(id);


                SizeAssortmentfromDb.Code = model.SizeAssortment.Code;
                SizeAssortmentfromDb.Description = model.SizeAssortment.Description;
                SizeAssortmentfromDb.Quantity01 = model.SizeAssortment.Quantity01;
                SizeAssortmentfromDb.Quantity02 = model.SizeAssortment.Quantity02;
                SizeAssortmentfromDb.Quantity03 = model.SizeAssortment.Quantity03;
                SizeAssortmentfromDb.Quantity04 = model.SizeAssortment.Quantity04;
                SizeAssortmentfromDb.Quantity05 = model.SizeAssortment.Quantity05;
                SizeAssortmentfromDb.Quantity06 = model.SizeAssortment.Quantity06;
                SizeAssortmentfromDb.Quantity07 = model.SizeAssortment.Quantity07;
                SizeAssortmentfromDb.Quantity08 = model.SizeAssortment.Quantity08;
                SizeAssortmentfromDb.Quantity09 = model.SizeAssortment.Quantity09;
                SizeAssortmentfromDb.Quantity10 = model.SizeAssortment.Quantity10;
                SizeAssortmentfromDb.Quantity11 = model.SizeAssortment.Quantity11;
                SizeAssortmentfromDb.Quantity12 = model.SizeAssortment.Quantity12;
                SizeAssortmentfromDb.Quantity13 = model.SizeAssortment.Quantity13;
                SizeAssortmentfromDb.Quantity14 = model.SizeAssortment.Quantity14;
                SizeAssortmentfromDb.Quantity15 = model.SizeAssortment.Quantity15;
                SizeAssortmentfromDb.Quantity16 = model.SizeAssortment.Quantity16;
                SizeAssortmentfromDb.Quantity17 = model.SizeAssortment.Quantity17;
                SizeAssortmentfromDb.Quantity18 = model.SizeAssortment.Quantity18;
                SizeAssortmentfromDb.TotalQuantity = model.SizeAssortment.TotalQuantity;
                SizeAssortmentfromDb.ModifiedBy = model.SizeAssortment.ModifiedBy;
                SizeAssortmentfromDb.ModifiedDate = model.SizeAssortment.ModifiedDate;

                await _db.SaveChangesAsync();

                return RedirectToAction("SizeAssortmentIndex", "SizeMaster", new { Id = NFKSizeMaster });
                //}
            }
            SizeMasterViewModel modelVM = new SizeMasterViewModel()
            {
                //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
                //LookUpMasters = model.LookUpMasters,
                ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET - DETAIL
        public async Task<IActionResult> SizeAssortmentDetail(int? Id)
        {
            var sizeMaster = await _db.sizeMasters.FindAsync(NFKSizeMaster);
            TempData["SizeMaster"] = sizeMaster;

            if (Id == null)
            {
                return NotFound();
            }

            var sizeAssortment = await _db.sizeAssortments.SingleOrDefaultAsync(m => m.Id == Id);

            if (sizeAssortment == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            sizeAssortmentVM.SizeAssortment = await _db.sizeAssortments.SingleOrDefaultAsync(m => m.Id == Id);

            if (sizeAssortmentVM.SizeAssortment == null)
            {
                return NotFound();
            }
            return View(sizeAssortmentVM);
        }

        //GET - DELETE
        public async Task<IActionResult> SizeAssortmentDelete(int? Id)
        {
            var sizeMaster = await _db.sizeMasters.FindAsync(NFKSizeMaster);
            TempData["SizeMaster"] = sizeMaster;

            if (Id == null)
            {
                return NotFound();
            }

            var sizeAssortment = await _db.sizeAssortments.SingleOrDefaultAsync(m => m.Id == Id);

            if (sizeAssortment == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            sizeAssortmentVM.SizeAssortment = await _db.sizeAssortments.SingleOrDefaultAsync(m => m.Id == Id);

            if (sizeAssortmentVM.SizeAssortment == null)
            {
                return NotFound();
            }
            return View(sizeAssortmentVM);
        }

        //POST - Delete
        [HttpPost, ActionName("SizeAssortmentDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SizeAssortmentDeleteConfirmed(int? id)
        {
            var sizeAssortment = await _db.sizeAssortments.FindAsync(id);

            if (sizeAssortment == null)
            {
                return View();
            }
            _db.sizeAssortments.Remove(sizeAssortment);
            await _db.SaveChangesAsync();

            return RedirectToAction("SizeAssortmentIndex", "SizeMaster", new { Id = NFKSizeMaster });
        }
    }
}
