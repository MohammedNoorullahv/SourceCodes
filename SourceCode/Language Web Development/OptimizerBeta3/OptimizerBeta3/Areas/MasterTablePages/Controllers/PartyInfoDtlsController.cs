using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class PartyInfoDtlsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public PartyInfoDtlsViewModel partyInfoDtlsVM { get; set; }

        public static int NFKParty;
        public static string SPICode, SPIName, SPIAddress, SPIGST;
        public PartyInfoDtlsController(ApplicationDbContext db)
        {
            _db = db;
            partyInfoDtlsVM = new PartyInfoDtlsViewModel()
            {
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                FKUnitType = _db.lookUpMasters,
                FKCountry = _db.lookUpMasters,
                PartyInfoDtls = new Models.MasterTables.PartyInfoDtls()
            };
        }
        public async Task<IActionResult> Index()
        {

            return View(await _db.partyInfos.ToListAsync());
        }

        public async Task<IActionResult> PartyDtlsIndex(int Id)
        {
            var PartyInfo = await _db.partyInfos.FindAsync(Id);

            TempData["PICompanyName"] = PartyInfo.CompanyName;
            TempData["PIId"] = PartyInfo.Id;
            TempData["PICode"] = PartyInfo.Code;
            TempData["PIAddress"] = PartyInfo.Address1;
            TempData["PIGST"] = PartyInfo.GSTNumber;

            NFKParty = PartyInfo.Id;
            SPICode = PartyInfo.Code;
            SPIName = PartyInfo.CompanyName;
            SPIAddress = PartyInfo.Address1;
            SPIGST = PartyInfo.GSTNumber;

            return View(await _db.partyInfoDtls.Where(p => p.FKPartyInfo == Id).ToListAsync());

        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            partyInfoDtlsVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            partyInfoDtlsVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            partyInfoDtlsVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            partyInfoDtlsVM.FKState = await _db.StateMasters.ToListAsync();
            partyInfoDtlsVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();
            partyInfoDtlsVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();

            TempData["PICompanyName"] = SPIName;
            TempData["PIId"] = NFKParty;
            TempData["PICode"] = SPICode;
            TempData["PIAddress"] = SPIAddress;
            TempData["PIGST"] = SPIGST;

            return View(partyInfoDtlsVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(partyInfoDtlsVM);
            }

            if (_db.partyInfoDtls.Where(x => x.ShortName == partyInfoDtlsVM.PartyInfoDtls.ShortName).ToList().Count > 0)
            {
                TempData["ErrorMessage"] = "Short Name Already Exists";
                return RedirectToAction(nameof(Create));
            }

            if (_db.partyInfoDtls.Where(x => x.CompanyName == partyInfoDtlsVM.PartyInfoDtls.CompanyName && x.Address1 == partyInfoDtlsVM.PartyInfoDtls.Address1).ToList().Count > 0)
            {
                TempData["ErrorMessage"] = "Company Name & Same Address Already Exists";
                return RedirectToAction(nameof(Create));
            }

            string codechar = partyInfoDtlsVM.PartyInfoDtls.CompanyName.Substring(0, 2).ToUpper();
            var maxcode = 0;

            if (_db.partyInfoDtls.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Count > 0)
            {
                maxcode = _db.partyInfoDtls.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Max();
            }

            partyInfoDtlsVM.PartyInfoDtls.Code = codechar + String.Format("{0:0000}", (maxcode + 1));


            _db.partyInfoDtls.Add(partyInfoDtlsVM.PartyInfoDtls);
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

            var partyInfoDtls = await _db.partyInfoDtls.SingleOrDefaultAsync(m => m.Id == id);

            if (partyInfoDtls == null)
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

            TempData["PICompanyName"] = SPIName;
            TempData["PIId"] = NFKParty;
            TempData["PICode"] = SPICode;
            TempData["PIAddress"] = SPIAddress;
            TempData["PIGST"] = SPIGST;

            partyInfoDtlsVM.PartyInfoDtls = await _db.partyInfoDtls.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterUnitType).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            partyInfoDtlsVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            partyInfoDtlsVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            partyInfoDtlsVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            partyInfoDtlsVM.FKState = await _db.StateMasters.ToListAsync();
            partyInfoDtlsVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();
            partyInfoDtlsVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            if (partyInfoDtlsVM.PartyInfoDtls == null)
            {
                return NotFound();
            }
            return View(partyInfoDtlsVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartyInfoDtlsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesPartyInfoDtlsExist = _db.partyInfoDtls.Include(s => s.CompanyName).Where(s => s.CompanyName == model.PartyInfoDtls.CompanyName && s.Address1 == model.PartyInfoDtls.Address1);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var PartyInfoDtlsfromDb = await _db.partyInfoDtls.FindAsync(id);

                PartyInfoDtlsfromDb.Code = model.PartyInfoDtls.Code;
                PartyInfoDtlsfromDb.CompanyName = model.PartyInfoDtls.CompanyName;
                PartyInfoDtlsfromDb.ShortName = model.PartyInfoDtls.ShortName;
                PartyInfoDtlsfromDb.Address1 = model.PartyInfoDtls.Address1;
                PartyInfoDtlsfromDb.Address2 = model.PartyInfoDtls.Address2;
                PartyInfoDtlsfromDb.FKArea = model.PartyInfoDtls.FKArea;
                PartyInfoDtlsfromDb.FKCity = model.PartyInfoDtls.FKCity;
                PartyInfoDtlsfromDb.FKPincode = model.PartyInfoDtls.FKPincode;
                PartyInfoDtlsfromDb.FKState = model.PartyInfoDtls.FKState;
                PartyInfoDtlsfromDb.FKCountry = model.PartyInfoDtls.FKCountry;
                PartyInfoDtlsfromDb.ContactPersonName = model.PartyInfoDtls.ContactPersonName;
                PartyInfoDtlsfromDb.ContactNo = model.PartyInfoDtls.ContactNo;
                PartyInfoDtlsfromDb.MailId = model.PartyInfoDtls.MailId;
                PartyInfoDtlsfromDb.PANNumber = model.PartyInfoDtls.PANNumber;
                PartyInfoDtlsfromDb.FKUnitType = model.PartyInfoDtls.FKUnitType;
                PartyInfoDtlsfromDb.ModifiedBy = model.PartyInfoDtls.ModifiedBy;
                PartyInfoDtlsfromDb.ModifiedDate = model.PartyInfoDtls.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction("PartyDtlsIndex", "PartyInfoDtls", new { Id = NFKParty });
                //}
            }
            PartyInfoDtlsViewModel modelVM = new PartyInfoDtlsViewModel()
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

            var PartyInfoDtl = await _db.partyInfoDtls.SingleOrDefaultAsync(m => m.Id == id);

            if (PartyInfoDtl == null)
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

            TempData["PICompanyName"] = SPIName;
            TempData["PIId"] = NFKParty;
            TempData["PICode"] = SPICode;
            TempData["PIAddress"] = SPIAddress;
            TempData["PIGST"] = SPIGST;

            partyInfoDtlsVM.PartyInfoDtls = await _db.partyInfoDtls.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterUnitType).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            partyInfoDtlsVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            partyInfoDtlsVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            partyInfoDtlsVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            partyInfoDtlsVM.FKState = await _db.StateMasters.ToListAsync();
            partyInfoDtlsVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();
            partyInfoDtlsVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            if (partyInfoDtlsVM.PartyInfoDtls == null)
            {
                return NotFound();
            }
            return View(partyInfoDtlsVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PartyInfo = await _db.partyInfoDtls.SingleOrDefaultAsync(m => m.Id == id);

            if (PartyInfo == null)
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

            TempData["PICompanyName"] = SPIName;
            TempData["PIId"] = NFKParty;
            TempData["PICode"] = SPICode;
            TempData["PIAddress"] = SPIAddress;
            TempData["PIGST"] = SPIGST;

            partyInfoDtlsVM.PartyInfoDtls = await _db.partyInfoDtls.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterUnitType).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            partyInfoDtlsVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            partyInfoDtlsVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            partyInfoDtlsVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            partyInfoDtlsVM.FKState = await _db.StateMasters.ToListAsync();
            partyInfoDtlsVM.FKUnitType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();
            partyInfoDtlsVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 5).ToListAsync();

            if (partyInfoDtlsVM.PartyInfoDtls == null)
            {
                return NotFound();
            }
            return View(partyInfoDtlsVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var UnitMaster = await _db.partyInfoDtls.FindAsync(id);

            if (UnitMaster == null)
            {
                return View();
            }
            _db.partyInfoDtls.Remove(UnitMaster);
            await _db.SaveChangesAsync();
            return RedirectToAction("PartyDtlsIndex", "PartyInfoDtls", new { Id = NFKParty });
        }

        [ActionName("GetDeliveryLocation")]
        public async Task<List<SelectListItem>> GetDeliveryLocation(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var locations = await (from location in _db.partyInfoDtls
                                   where location.FKPartyInfo == id
                                   orderby location.CompanyName
                                   select location).ToListAsync();
            foreach (var item in locations)
            {
                items.Add(new SelectListItem { Text = item.CompanyName, Value = item.Id.ToString() });
            }
            return items;
        }
    }
}
