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
    public class AreaMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public AreaMasterViewModel AreaMasterVM { get; set; }

        public AreaMasterController(ApplicationDbContext db)
        {
            _db = db;
            AreaMasterVM = new AreaMasterViewModel()
            {
                FKCountry = _db.AreaLookUpMasters,
                FKState = _db.AreaLookUpMasters,
                FKCity = _db.AreaLookUpMasters,
                FKArea = _db.AreaLookUpMasters,
                FKPincode = _db.AreaLookUpMasters,
                AreaMaster = new Models.MasterTables.AreaMaster()
            };
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.AreaMasters.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            AreaMasterVM.FKCountry = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "CO" && s.IsActive == true).ToListAsync();
            //AreaMasterVM.FKState = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "ST" && s.IsActive == true).ToListAsync();
            //AreaMasterVM.FKCity = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "CI" && s.IsActive == true).ToListAsync();
            //AreaMasterVM.FKArea = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "AR" && s.IsActive == true).ToListAsync();
            //AreaMasterVM.FKPincode = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "PI" && s.IsActive == true).ToListAsync();

            return View(AreaMasterVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string sInsertCategory)
        {
            if (!ModelState.IsValid)
            {
                AreaMasterVM.FKCountry = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "CO" && s.IsActive == true).ToListAsync();

                return View(AreaMasterVM);
            }

            string sCode = AreaMasterVM.AreaMaster.FKCountry.ToString() + ":" +
                AreaMasterVM.AreaMaster.FKState.ToString() + ":" +
                AreaMasterVM.AreaMaster.FKCity.ToString() + ":" +
                AreaMasterVM.AreaMaster.FKArea.ToString() + ":" +
                AreaMasterVM.AreaMaster.FKPincode.ToString();

            var lookUpMaster = await _db.AreaLookUpMasters.ToListAsync();

            AreaMasterVM.AreaMaster.Country = lookUpMaster.Where(x => x.Id == AreaMasterVM.AreaMaster.FKCountry).FirstOrDefault().Description;
            AreaMasterVM.AreaMaster.State = lookUpMaster.Where(x => x.Id == AreaMasterVM.AreaMaster.FKState).FirstOrDefault().Description;
            AreaMasterVM.AreaMaster.City = lookUpMaster.Where(x => x.Id == AreaMasterVM.AreaMaster.FKCity).FirstOrDefault().Description;
            AreaMasterVM.AreaMaster.Area = lookUpMaster.Where(x => x.Id == AreaMasterVM.AreaMaster.FKArea).FirstOrDefault().Description;
            AreaMasterVM.AreaMaster.Pincode = lookUpMaster.Where(x => x.Id == AreaMasterVM.AreaMaster.FKPincode).FirstOrDefault().Description;

            string sAreaMaster = AreaMasterVM.AreaMaster.Country + "->" +
                AreaMasterVM.AreaMaster.State + "->" +
                AreaMasterVM.AreaMaster.City + "->" +
                AreaMasterVM.AreaMaster.Area + "->" +
                AreaMasterVM.AreaMaster.Pincode;
            if (_db.AreaMasters.Where(x => x.Code == sCode).ToList().Count > 0)
            {
                TempData["ErrorMessage"] = "This Area Master Already Exists" + sAreaMaster;
                return RedirectToAction(nameof(Create));
            }

            AreaMasterVM.AreaMaster.Code = sCode;

            _db.AreaMasters.Add(AreaMasterVM.AreaMaster);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Create));
        }

        [ActionName("GetStatesList")]
        public async Task<List<SelectListItem>> GetStatesList(int id, string sCategory)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var states = await (from state in _db.AreaLookUpMasters
                                   where state.FKAreaLookUpMaster == id && state.Category == sCategory
                                   orderby state.Description
                                   select state).ToListAsync();
            foreach (var item in states)
            {
                items.Add(new SelectListItem { Text = item.Description, Value = item.Id.ToString() });
            }
            return items;
        }

        [ActionName("GetCityList")]
        public async Task<List<SelectListItem>> GetCityList(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var states = await (from state in _db.AreaLookUpMasters
                                where state.FKAreaLookUpMaster == id
                                orderby state.Description
                                select state).ToListAsync();
            foreach (var item in states)
            {
                items.Add(new SelectListItem { Text = item.Description, Value = item.Id.ToString() });
            }
            return items;
        }


        //POST - InsertAreaLookUpMaster
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertAreaLookUpMaster(string Category, string Code, string Description, string SetDefault, string FKLookUpHdr)
        {
            //string sCategory = Request.Form["Category"];
            //string sCode = Request.Form["Code"];
            //string sDescription = Request.Form["Description"];
            //bool bSetAsDefault = Convert.ToBoolean(Request.Form["SetDefault"]);
            //int nFKLookUpHdr = Convert.ToInt32(Request.Form["FKLookUpHdr"]);
            
            if (Description == "" || Description == null)
            {
                AreaMasterVM.FKCountry = await _db.AreaLookUpMasters.OrderByDescending(c => c.Description).Where(s => s.Category == "CO" && s.IsActive == true).ToListAsync();

                TempData["ErrorMessage"] = "Empty Descriptions cannot be Saved";
                return RedirectToAction(nameof(Create));
            }

            var alm = new AreaLookUpMaster();
            alm.Category = Category;
            alm.FKAreaLookUpMaster = Convert.ToInt32(FKLookUpHdr);
            alm.Description = Description;
            alm.Code = Code;
            alm.IsActive = true;
            alm.CreatedBy = 0;
            alm.CreatedDate = DateTime.Now;
            alm.SetAsDefault = Convert.ToBoolean(SetDefault);

            _db.AreaLookUpMasters.Add(alm);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Create));
        }
    }
}
