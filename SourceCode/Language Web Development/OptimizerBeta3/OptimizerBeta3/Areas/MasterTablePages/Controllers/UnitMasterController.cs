using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class UnitMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public UnitMasterViewModel unitMasterVM { get; set; }
        public LocationViewModel locationVM { get; set; }

        public static int NFKCompany, NFKUnitMaster;
        public static string SCICode, SCIName, SCIAddress, SCIGST;
        public static string SUMCode, SUMName, SUMAddress;

        public UnitMasterController(ApplicationDbContext db)
        {
            _db = db;
            unitMasterVM = new UnitMasterViewModel()
            {
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                FKUnitType = _db.lookUpMasters,
                UnitMaster = new Models.MasterTables.UnitMaster()

            };
            locationVM = new LocationViewModel()
            {
                FKLocationType = _db.lookUpMasters,
                Locations = new Models.MasterTables.Locations()
            };
        }
        public async Task<IActionResult> Index()
        {

            return View(await _db.companyInfos.ToListAsync());
        }

        public async Task<IActionResult> UnitIndex(int Id)
        {
            var companyInfo = await _db.companyInfos.FindAsync(Id);

            NFKCompany = companyInfo.Id;
            SCICode = companyInfo.Code;
            SCIName = companyInfo.CompanyName;
            SCIAddress = companyInfo.Address1;
            SCIGST = companyInfo.GSTNumber;

            TempData["CICompanyName"] = companyInfo.CompanyName;
            TempData["CIId"] = NFKCompany;
            TempData["CICode"] = companyInfo.Code;
            TempData["CIAddress"] = companyInfo.Address1;
            TempData["CIGST"] = companyInfo.GSTNumber;

            TempData["CompanyInfo"] = companyInfo;

            var result = (CompanyInfo)TempData["CompanyInfo"];




            return View(await _db.unitMasters.Where(x => x.FKCompanyInfo == Id).ToListAsync());

        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            unitMasterVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            unitMasterVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            unitMasterVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            unitMasterVM.FKState = await _db.StateMasters.ToListAsync();
            unitMasterVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            //var CompanyInfo = await _db.companyInfos.FindAsync(NFKCompany);

            //TempData["CICompanyName"] = CompanyInfo.CompanyName;
            //TempData["CIId"] = CompanyInfo.Id;
            //TempData["CICode"] = CompanyInfo.Code;
            //TempData["CIAddress"] = CompanyInfo.Address1;
            //TempData["CIGST"] = CompanyInfo.GSTNumber;
            var companyInfo = await _db.companyInfos.FindAsync(NFKCompany);
            TempData["CompanyInfo"] = companyInfo;

            TempData["CICompanyName"] = SCIName;
            TempData["CIId"] = NFKCompany;
            TempData["CICode"] = SCICode;
            TempData["CIAddress"] = SCIAddress;
            TempData["CIGST"] = SCIGST;

            return View(unitMasterVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(unitMasterVM);
            }

            string codechar = unitMasterVM.UnitMaster.CompanyName.Substring(0, 2).ToUpper();
            var maxcode = 0;

            if (_db.unitMasters.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Count > 0)
            {
                maxcode = _db.unitMasters.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Max();
            }

            unitMasterVM.UnitMaster.Code = codechar + String.Format("{0:0000}", (maxcode + 1));

            _db.unitMasters.Add(unitMasterVM.UnitMaster);
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

            var UnitMaster = await _db.unitMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (UnitMaster == null)
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

            TempData["CICompanyName"] = SCIName;
            TempData["CIId"] = NFKCompany;
            TempData["CICode"] = SCICode;
            TempData["CIAddress"] = SCIAddress;
            TempData["CIGST"] = SCIGST;

            unitMasterVM.UnitMaster = await _db.unitMasters.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterUnitType).SingleOrDefaultAsync(m => m.Id == id);
            unitMasterVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            unitMasterVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            unitMasterVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            unitMasterVM.FKState = await _db.StateMasters.ToListAsync();
            unitMasterVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            if (unitMasterVM.UnitMaster == null)
            {
                return NotFound();
            }
            return View(unitMasterVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UnitMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesUnitMasterExist = _db.unitMasters.Include(s => s.CompanyName).Where(s => s.CompanyName == model.UnitMaster.CompanyName && s.Address1 == model.UnitMaster.Address1);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var UnitMasterfromDb = await _db.unitMasters.FindAsync(id);

                UnitMasterfromDb.Code = model.UnitMaster.Code;
                UnitMasterfromDb.CompanyName = model.UnitMaster.CompanyName;
                UnitMasterfromDb.ShortName = model.UnitMaster.ShortName;
                UnitMasterfromDb.Address1 = model.UnitMaster.Address1;
                UnitMasterfromDb.Address2 = model.UnitMaster.Address2;
                UnitMasterfromDb.FKArea = model.UnitMaster.FKArea;
                UnitMasterfromDb.FKCity = model.UnitMaster.FKCity;
                UnitMasterfromDb.FKPincode = model.UnitMaster.FKPincode;
                UnitMasterfromDb.FKState = model.UnitMaster.FKState;
                UnitMasterfromDb.ContactPersonName = model.UnitMaster.ContactPersonName;
                UnitMasterfromDb.ContactNo = model.UnitMaster.ContactNo;
                UnitMasterfromDb.MailId = model.UnitMaster.MailId;
                UnitMasterfromDb.PANNumber = model.UnitMaster.PANNumber;
                UnitMasterfromDb.GSTNumber = model.UnitMaster.GSTNumber;
                UnitMasterfromDb.FKUnitType = model.UnitMaster.FKUnitType;
                UnitMasterfromDb.ModifiedBy = model.UnitMaster.ModifiedBy;
                UnitMasterfromDb.ModifiedDate = model.UnitMaster.ModifiedDate;

                await _db.SaveChangesAsync();
                //return RedirectToAction(nameof(UnitIndex));
                return RedirectToAction("UnitIndex", "UnitMaster", new { Id = NFKCompany });
                //}
            }
            UnitMasterViewModel modelVM = new UnitMasterViewModel()
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

            var UnitMaster = await _db.unitMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (UnitMaster == null)
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

            TempData["CICompanyName"] = SCIName;
            TempData["CIId"] = NFKCompany;
            TempData["CICode"] = SCICode;
            TempData["CIAddress"] = SCIAddress;
            TempData["CIGST"] = SCIGST;

            unitMasterVM.UnitMaster = await _db.unitMasters.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterUnitType).SingleOrDefaultAsync(m => m.Id == id);
            unitMasterVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            unitMasterVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            unitMasterVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            unitMasterVM.FKState = await _db.StateMasters.ToListAsync();
            unitMasterVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            if (unitMasterVM.UnitMaster == null)
            {
                return NotFound();
            }
            return View(unitMasterVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UnitMaster = await _db.unitMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (UnitMaster == null)
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

            TempData["CICompanyName"] = SCIName;
            TempData["CIId"] = NFKCompany;
            TempData["CICode"] = SCICode;
            TempData["CIAddress"] = SCIAddress;
            TempData["CIGST"] = SCIGST;

            unitMasterVM.UnitMaster = await _db.unitMasters.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterUnitType).SingleOrDefaultAsync(m => m.Id == id);
            unitMasterVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            unitMasterVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            unitMasterVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            unitMasterVM.FKState = await _db.StateMasters.ToListAsync();
            unitMasterVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            if (unitMasterVM.UnitMaster == null)
            {
                return NotFound();
            }
            return View(unitMasterVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var UnitMaster = await _db.unitMasters.FindAsync(id);

            if (UnitMaster == null)
            {
                return View();
            }
            _db.unitMasters.Remove(UnitMaster);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(UnitIndex));
            return RedirectToAction("UnitIndex", "UnitMaster", new { Id = NFKCompany });
        }

        public async Task<IActionResult> LocationIndex(int Id)
        {
            var unitInfo = await _db.unitMasters.FindAsync(Id);

            TempData["UMCompanyName"] = unitInfo.CompanyName;
            TempData["UMId"] = unitInfo.Id;
            TempData["UMCode"] = unitInfo.Code;
            TempData["UMAddress"] = unitInfo.Address1;

            NFKUnitMaster = unitInfo.Id;
            SUMCode = unitInfo.Code;
            SUMName = unitInfo.CompanyName;
            SUMAddress = unitInfo.Address1;


            TempData["UnitInfo"] = unitInfo;
            var result = (UnitMaster)TempData["UnitInfo"];

            return View(await _db.locations.Where(x => x.FKUnitDtls == Id).ToListAsync());

        }

        //GET - CREATE
        public async Task<IActionResult> LocationCreate()
        {
            var unitInfo = await _db.unitMasters.FindAsync(NFKUnitMaster);
            TempData["UnitInfo"] = unitInfo;
            //var result = (UnitMaster)TempData["UnitInfo"];

            locationVM.FKLocationType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 40).ToListAsync();

            return View(locationVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocationCreate(LocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(locationVM);
            }

            _db.locations.Add(model.Locations);
            await _db.SaveChangesAsync();

            return RedirectToAction("LocationCreate", "UnitMaster", new { Id = NFKUnitMaster });
        }

        //GET - EDIT
        public async Task<IActionResult> LocationEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _db.locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }


            var unitInfo = await _db.unitMasters.FindAsync(NFKUnitMaster);
            TempData["UnitInfo"] = unitInfo;
            var result = (UnitMaster)TempData["UnitInfo"];

            locationVM.Locations = await _db.locations.SingleOrDefaultAsync(m => m.Id == id);
            locationVM.FKLocationType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 40).ToListAsync();
            
            if (locationVM.Locations == null)
            {
                return NotFound();
            }
            return View(locationVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocationEdit(int id, LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesLocationExist = _db.locations.Include(s => s.LocationName).Where(s => s.LocationName == model.Locations.LocationName && s.FKUnitDtls == model.UnitMaster.Id);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var LocationfromDb = await _db.locations.FindAsync(id);

                LocationfromDb.FKUnitDtls = model.Locations.FKUnitDtls;
                LocationfromDb.FKLocationType = model.Locations.FKLocationType;
                LocationfromDb.Code = model.Locations.Code;
                LocationfromDb.LocationName = model.Locations.LocationName;
                LocationfromDb.ModifiedBy = model.Locations.ModifiedBy;
                LocationfromDb.ModifiedDate = model.Locations.ModifiedDate;

                await _db.SaveChangesAsync();
                //return RedirectToAction(nameof(UnitIndex));
                return RedirectToAction("LocationIndex", "UnitMaster", new { Id = NFKUnitMaster });
                //}
            }
            UnitMasterViewModel modelVM = new UnitMasterViewModel()
            {
                //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
                //LookUpMasters = model.LookUpMasters,
                ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET - EDIT
        public async Task<IActionResult> LocationDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _db.locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }


            var unitInfo = await _db.unitMasters.FindAsync(NFKUnitMaster);
            TempData["UnitInfo"] = unitInfo;
            var result = (UnitMaster)TempData["UnitInfo"];

            locationVM.Locations = await _db.locations.SingleOrDefaultAsync(m => m.Id == id);
            locationVM.FKLocationType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 40).ToListAsync();

            if (locationVM.Locations == null)
            {
                return NotFound();
            }
            return View(locationVM);
        }

        //GET - DELETE
        public async Task<IActionResult> LocationDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _db.locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }


            var unitInfo = await _db.unitMasters.FindAsync(NFKUnitMaster);
            TempData["UnitInfo"] = unitInfo;
            var result = (UnitMaster)TempData["UnitInfo"];

            locationVM.Locations = await _db.locations.SingleOrDefaultAsync(m => m.Id == id);
            locationVM.FKLocationType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 40).ToListAsync();

            if (locationVM.Locations == null)
            {
                return NotFound();
            }
            return View(locationVM);
        }

        //POST - Delete
        [HttpPost, ActionName("LocationDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocationDeleteConfirmed(int? id)
        {
            var location = await _db.locations.FindAsync(id);

            if (location == null)
            {
                return View();
            }
            _db.locations.Remove(location);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(UnitIndex));
            return RedirectToAction("LocationIndex", "UnitMaster", new { Id = NFKUnitMaster });
        }

        [ActionName("GetUnitsLocation")]
        public async Task<List<SelectListItem>> GetUnitsLocation(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var locations = await (from location in _db.locations
                                       where location.FKUnitDtls == id
                                       orderby location.LocationName
                                       select location).ToListAsync();
            foreach (var item in locations)
            {
                items.Add(new SelectListItem { Text = item.LocationName, Value = item.Id.ToString() });
            }
            return items;
        }

    }
}
