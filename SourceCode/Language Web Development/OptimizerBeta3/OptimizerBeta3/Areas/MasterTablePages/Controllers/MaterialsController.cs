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
    public class MaterialsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public MaterialsViewModel materialsVM { get; set; }
        public MaterialDetailViewModel materialDtlsVM { get; set; }

        public static int NFKMaterialId;
        public static string SCategory, SType, SSubType, SCode, SDescription;
        public MaterialsController(ApplicationDbContext db)
        {
            _db = db;
            materialsVM = new MaterialsViewModel()
            {
                FKCategory = _db.lookUpMasters,
                FKType = _db.lookUpMasters,
                FKSubType = _db.lookUpMasters,
                FKBrand = _db.lookUpMasters,
                FKSource = _db.lookUpMasters,
                FKUom = _db.lookUpMasters,
                FKColour = _db.lookUpMasters,
                FKHSNCode = _db.HSNCodeMasters,
                materials = new Models.MasterTables.Materials()
            };
            materialDtlsVM = new MaterialDetailViewModel()
            {
                FKParty = _db.partyInfos,
                FKCurrency = _db.lookUpMasters,
                MaterialDetails = new Models.MasterTables.MaterialDetails()
            };
        }
        public async Task<IActionResult> Index()
        {
            var materialmaster = await _db.materials.Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterType).Include(m => m.LookUpMasterSubType)
                .Include(m => m.LookUpMasterBrand).Include(m => m.LookUpMasterSource).ToListAsync();
            return View(materialmaster);

            //return View(await _db.materials.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            materialsVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            materialsVM.FKType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 25).ToListAsync();
            materialsVM.FKSubType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 26).ToListAsync();
            materialsVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 27).ToListAsync();
            materialsVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 28).ToListAsync();
            materialsVM.FKUom = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 29).ToListAsync();
            materialsVM.FKColour = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 34).ToListAsync();
            materialsVM.FKHSNCode = await _db.HSNCodeMasters.ToListAsync();

            return View(materialsVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(materialsVM);
            }

            //var lookUpMaster = await _db.lookUpMasters.FindAsync(materialsVM.materials.FKCategory);
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            
            string sCategory = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKCategory).FirstOrDefault().Description;
            string sType = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKType).FirstOrDefault().Description;
            string sSubType = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKSubType).FirstOrDefault().Description;
            string sBrand = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKBrand).FirstOrDefault().Description;
            //lookUpMaster = await _db.lookUpMasters.FindAsync(materialsVM.materials.FKSource);
            string sSource = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKSource).FirstOrDefault().Description;

            string codechar = (sCategory.Substring(0, 2) + sType.Substring(0, 2) + sSubType.Substring(0, 2) + sBrand.Substring(0, 2) + sSource.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.materials.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(11, 4))).ToList().Count > 0)
            {
                maxcode = _db.materials.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(11, 4))).ToList().Max();
            }

            materialsVM.materials.Code = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            var HSNCode = await _db.HSNCodeMasters.ToListAsync();
            materialsVM.materials.HSNCode = HSNCode.Where(x => x.Id == materialsVM.materials.FKHSNCode).FirstOrDefault().HSNCode;

            _db.materials.Add(materialsVM.materials);
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

            var materials = await _db.materials.SingleOrDefaultAsync(m => m.Id == id);

            if (materials == null)
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

            materialsVM.materials = await _db.materials.Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterType).
                Include(m => m.LookUpMasterSubType).Include(m => m.LookUpMasterSubType).Include(m => m.LookUpMasterBrand).
                Include(m => m.LookUpMasterSource).Include(m => m.LookUpMasterUOM).Include(m => m.LookUpMasterColour).SingleOrDefaultAsync(m => m.Id == id);
            materialsVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            materialsVM.FKType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 25).ToListAsync();
            materialsVM.FKSubType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 26).ToListAsync();
            materialsVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 27).ToListAsync();
            materialsVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 28).ToListAsync();
            materialsVM.FKUom = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 29).ToListAsync();
            materialsVM.FKColour = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 34).ToListAsync();

            if (materialsVM.materials == null)
            {
                return NotFound();
            }
            return View(materialsVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaterialsViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var doesMaterialsExist = _db.materials.Include(s => s.Description).Where(s => s.Description == model.materials.Description);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var MaterialsfromDb = await _db.materials.FindAsync(id);

                MaterialsfromDb.FKCategory = model.materials.FKCategory;
                MaterialsfromDb.FKType = model.materials.FKType;
                MaterialsfromDb.FKSubType = model.materials.FKSubType;
                MaterialsfromDb.FKBrand = model.materials.FKBrand;
                MaterialsfromDb.FKSource = model.materials.FKSource;
                MaterialsfromDb.Code = model.materials.Code;
                MaterialsfromDb.Description = model.materials.Description;
                MaterialsfromDb.ShortDescription = model.materials.ShortDescription;
                MaterialsfromDb.PrintDescription = model.materials.PrintDescription;
                MaterialsfromDb.FKUom = model.materials.FKUom;
                MaterialsfromDb.FKColour = model.materials.FKColour;
                MaterialsfromDb.IsExpirable = model.materials.IsExpirable;
                MaterialsfromDb.ModifiedBy = model.materials.ModifiedBy;
                MaterialsfromDb.ModifiedDate = model.materials.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            //}
            //CompanyInfoViewModel modelVM = new CompanyInfoViewModel()
            //{
            //    //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    //LookUpMasters = model.LookUpMasters,
            //    ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
            //    //StatusMessage = StatusMessage
            //};
            //return View(modelVM);
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _db.materials.SingleOrDefaultAsync(m => m.Id == id);

            if (materials == null)
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

            materialsVM.materials = await _db.materials.Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterType).
                Include(m => m.LookUpMasterSubType).Include(m => m.LookUpMasterSubType).Include(m => m.LookUpMasterBrand).
                Include(m => m.LookUpMasterSource).Include(m => m.LookUpMasterUOM).Include(m => m.LookUpMasterColour).SingleOrDefaultAsync(m => m.Id == id);
            materialsVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            materialsVM.FKType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 25).ToListAsync();
            materialsVM.FKSubType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 26).ToListAsync();
            materialsVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 27).ToListAsync();
            materialsVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 28).ToListAsync();
            materialsVM.FKUom = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 29).ToListAsync();
            materialsVM.FKColour = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 34).ToListAsync();

            if (materialsVM.materials == null)
            {
                return NotFound();
            }
            return View(materialsVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _db.materials.SingleOrDefaultAsync(m => m.Id == id);

            if (materials == null)
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

            materialsVM.materials = await _db.materials.Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterType).
                Include(m => m.LookUpMasterSubType).Include(m => m.LookUpMasterSubType).Include(m => m.LookUpMasterBrand).
                Include(m => m.LookUpMasterSource).Include(m => m.LookUpMasterUOM).Include(m => m.LookUpMasterColour).SingleOrDefaultAsync(m => m.Id == id);
            materialsVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            materialsVM.FKType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 25).ToListAsync();
            materialsVM.FKSubType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 26).ToListAsync();
            materialsVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 27).ToListAsync();
            materialsVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 28).ToListAsync();
            materialsVM.FKUom = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 29).ToListAsync();
            materialsVM.FKColour = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 34).ToListAsync();

            if (materialsVM.materials == null)
            {
                return NotFound();
            }
            return View(materialsVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var materials = await _db.materials.FindAsync(id);

            if (materials == null)
            {
                return View();
            }
            _db.materials.Remove(materials);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MaterialDetailIndex(int Id)
        {
            var Materials = await _db.materials.FindAsync(Id);

            TempData["Category"] = Materials.FKCategory;
            TempData["Type"] = Materials.FKSubType;
            TempData["SubType"] = Materials.FKSubType;
            TempData["Code"] = Materials.Code;
            TempData["Description"] = Materials.Description;
            TempData["Id"] = Materials.Id;

            NFKMaterialId = Materials.Id;
            SCategory = Materials.FKCategory.ToString();
            SType = Materials.FKType.ToString();
            SSubType = Materials.FKSubType.ToString();
            SCode = Materials.Code;
            SDescription = Materials.Description;

            return View(await _db.materialDetails.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> MaterialDetailCreate()
        {
            materialDtlsVM.FKParty = await _db.partyInfos.ToListAsync();
            materialDtlsVM.FKCurrency = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 30).ToListAsync();

            TempData["Category"] = SCategory;
            TempData["Type"] = SType;
            TempData["SubType"] = SSubType;
            TempData["Code"] = SCode;
            TempData["Description"] = SDescription;
            TempData["Id"] = NFKMaterialId;

            return View(materialDtlsVM);
        }

        //POST - CREATE
        //[HttpPost, ActionName("MaterialDetailCreate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MaterialDetailCreate(MaterialDetailViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(materialDtlsVM);
            //}

            //_db.materialDetails.Add(materialDtlsVM.MaterialDetails);
            //await _db.SaveChangesAsync();

            ////return RedirectToAction(nameof(Create));
            //return RedirectToAction("MaterialDetailCreate", "Materials", new { Id = NFKMaterialId });

            if (ModelState.IsValid)
            {
                _db.materialDetails.Add(model.MaterialDetails);
                await _db.SaveChangesAsync();
                return RedirectToAction("MaterialDetailCreate", "Materials", new { Id = NFKMaterialId });
            }
            return View(materialDtlsVM);
        }

        //GET - Edit
        public async Task<IActionResult> MaterialDetailEdit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var materialsDtls = await _db.materialDetails.SingleOrDefaultAsync(m => m.Id == Id);

            if (materialsDtls == null)
            {
                return NotFound();
            }

            materialDtlsVM.MaterialDetails = await _db.materialDetails.SingleOrDefaultAsync(m => m.Id == Id);
            materialDtlsVM.FKParty = await _db.partyInfos.ToListAsync();
            materialDtlsVM.FKCurrency = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 30).ToListAsync();
            
            if (materialDtlsVM.MaterialDetails == null)
            {
                return NotFound();
            }
            
            TempData["Category"] = SCategory;
            TempData["Type"] = SType;
            TempData["SubType"] = SSubType;
            TempData["Code"] = SCode;
            TempData["Description"] = SDescription;
            TempData["Id"] = NFKMaterialId;

            return View(materialDtlsVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MaterialDetailEdit(int id, MaterialDetailViewModel model)
        {
            
            //var doesMaterialDtlssExist = _db.materials.Include(s => s.Description).Where(s => s.Description == model.materials.Description);

            //if (doesLookUpMstExist.Count() > 0 )
            //{
            //    //ERROR.    
            //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
            //}
            //else
            //{
            var MaterialDtlssfromDb = await _db.materialDetails.FindAsync(id);

            MaterialDtlssfromDb.FKParty = model.MaterialDetails.FKParty;
            MaterialDtlssfromDb.FKCurrency = model.MaterialDetails.FKCurrency;
            MaterialDtlssfromDb.Rate = model.MaterialDetails.Rate;
            MaterialDtlssfromDb.MinimumOrdQty = model.MaterialDetails.MinimumOrdQty;
            MaterialDtlssfromDb.MinimumTransitDays = model.MaterialDetails.MinimumTransitDays;
            MaterialDtlssfromDb.IsPrimeSupplier = model.MaterialDetails.IsPrimeSupplier;
            MaterialDtlssfromDb.ApplicableFrom = model.MaterialDetails.ApplicableFrom;
            MaterialDtlssfromDb.ApplicableTo = model.MaterialDetails.ApplicableTo;
            MaterialDtlssfromDb.ModifiedBy = model.MaterialDetails.ModifiedBy;
            MaterialDtlssfromDb.ModifiedDate = model.MaterialDetails.ModifiedDate;

            await _db.SaveChangesAsync();
            return RedirectToAction("MaterialDetailIndex", "Materials", new { Id = NFKMaterialId }); ;
            
        }

        //GET - Detail
        public async Task<IActionResult> MaterialDetailDetail(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var materialsDtls = await _db.materialDetails.SingleOrDefaultAsync(m => m.Id == Id);

            if (materialsDtls == null)
            {
                return NotFound();
            }

            materialDtlsVM.MaterialDetails = await _db.materialDetails.SingleOrDefaultAsync(m => m.Id == Id);
            materialDtlsVM.FKParty = await _db.partyInfos.ToListAsync();
            materialDtlsVM.FKCurrency = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 30).ToListAsync();

            if (materialDtlsVM.MaterialDetails == null)
            {
                return NotFound();
            }

            TempData["Category"] = SCategory;
            TempData["Type"] = SType;
            TempData["SubType"] = SSubType;
            TempData["Code"] = SCode;
            TempData["Description"] = SDescription;
            TempData["Id"] = NFKMaterialId;

            return View(materialDtlsVM);
        }

        //GET - Delete
        public async Task<IActionResult> MaterialDetailDelete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var materialsDtls = await _db.materialDetails.SingleOrDefaultAsync(m => m.Id == Id);

            if (materialsDtls == null)
            {
                return NotFound();
            }

            materialDtlsVM.MaterialDetails = await _db.materialDetails.SingleOrDefaultAsync(m => m.Id == Id);
            materialDtlsVM.FKParty = await _db.partyInfos.ToListAsync();
            materialDtlsVM.FKCurrency = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 30).ToListAsync();

            if (materialDtlsVM.MaterialDetails == null)
            {
                return NotFound();
            }

            TempData["Category"] = SCategory;
            TempData["Type"] = SType;
            TempData["SubType"] = SSubType;
            TempData["Code"] = SCode;
            TempData["Description"] = SDescription;
            TempData["Id"] = NFKMaterialId;

            return View(materialDtlsVM);
        }

        //POST - Delete
        [HttpPost, ActionName("MaterialDetailDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MaterialDetailDeleteConfirmed(int? id)
        {
            var materialDtls = await _db.materialDetails.FindAsync(id);

            if (materialDtls == null)
            {
                return View();
            }
            _db.materialDetails.Remove(materialDtls);
            await _db.SaveChangesAsync();
            return RedirectToAction("MaterialDetailIndex", "Materials", new { Id = NFKMaterialId });
        }


        //GET - CREATE
        public async Task<IActionResult> LookUpMasterCreate()
        {
            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderBy(c => c.Description).ToListAsync(),
                LookUpMasters = new Models.MasterTables.LookUpMaster()
                //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            };
            return View(model);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LookUpMasterCreate(LookUpCategoryAndLookUpMstViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesLookUpMstExist = _db.lookUpMasters.Include(s => s.LookUpCategory).Where(s => s.Description == model.LookUpMasters.Description && s.LookUpCategory.Id == model.LookUpMasters.FKLookUpCategory);

                if (doesLookUpMstExist.Count() > 0)
                {
                    ////ERROR.
                    //StatusMessage = "Error : Look Up Master Exists under " + doesLookUpMstExist.First().LookUpCategory.Description + " Category. Please use anothe name ";
                }
                else
                {
                    if (model.LookUpMasters.SetAsDefault == true)
                    {
                        var defaultId = _db.lookUpMasters.Where(s => s.FKLookUpCategory == model.LookUpMasters.FKLookUpCategory && s.SetAsDefault == true);
                        if (defaultId.Count() > 0)
                        {
                            var lookUpMasterFromDb = await _db.lookUpMasters.FindAsync(defaultId.First().Id);

                            lookUpMasterFromDb.SetAsDefault = false;
                            await _db.SaveChangesAsync();

                        }
                    }
                    _db.lookUpMasters.Add(model.LookUpMasters);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(LookUpMasterCreate));
                }
            }
            LookUpCategoryAndLookUpMstViewModel modelVM = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.ToListAsync(),
                LookUpMasters = new Models.MasterTables.LookUpMaster()
                //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }
    }
}
