using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class AreaLookUpMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AreaLookUpMasterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.AreaLookUpMasters.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            var LookUpMasters = new Models.MasterTables.AreaLookUpMaster();
            //var LookUpMstList = await _db.AreaLookUpMasters.OrderBy(p => p.Category).ThenBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync();
            ViewBag.LookUpMstList = await _db.AreaLookUpMasters.OrderBy(p => p.Category).ThenBy(p => p.Description).ToListAsync();
            ViewBag.LookUpMstList1 = await _db.AreaLookUpMasters.OrderBy(p => p.Category).ThenBy(p => p.Description).ToListAsync();
            return View(LookUpMasters);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AreaLookUpMaster areaLookUpMaster)
        {
            if (ModelState.IsValid)
            {
                string sCategory = areaLookUpMaster.Category;
                int nFKAreaLookUpMaster = areaLookUpMaster.FKAreaLookUpMaster;

                string sParentCategory = "";
                if (sCategory == "CO") { sParentCategory = ""; }
                else if (sCategory == "ST") { sParentCategory = "CO"; }
                else if (sCategory == "CI") { sParentCategory = "ST"; }
                else if (sCategory == "AR") { sParentCategory = "CI"; }
                else if (sCategory == "PI") { sParentCategory = "AR"; }
                else { sParentCategory = ""; }

                if (sCategory != "CO" && nFKAreaLookUpMaster > 0)
                {
                    if (_db.AreaLookUpMasters.Where(x => x.Category == sParentCategory && x.Id == nFKAreaLookUpMaster).ToList().Count == 0)
                    {
                        TempData["ErrorMessage"] = "Invalid Look Up Master Code";
                        return RedirectToAction(nameof(Create));
                    }
                }

                _db.AreaLookUpMasters.Add(areaLookUpMaster);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(areaLookUpMaster);
        }

        [ActionName("GetLookUpMaster")]
        public async Task<List<SelectListItem>> GetLookUpMaster(string category)
        {
            string sCategory = "";
            if (category == "CO")
            {
                sCategory = category;
            } else if (category == "ST")
            {
                sCategory = "CO";
            } else if (category == "CI")
            {
                sCategory = "ST";
            } else if (category == "AR")
            {
                sCategory = "CI";
            } else if (category == "PI")
            {
                sCategory = "AR";
            } else
            {
                sCategory = "";
            }

            //List<LookUpMaster> lookUpMasters = new List<LookUpMaster>();
            List<SelectListItem> items = new List<SelectListItem>();
            var lookUpMasters = await (from lookUpMaster in _db.AreaLookUpMasters
                                       where lookUpMaster.Category == sCategory
                                       orderby lookUpMaster.Description
                                       select lookUpMaster).ToListAsync();
            foreach (var item in lookUpMasters)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString() + " : " + item.Description, Value = item.Id.ToString() });
            }
            
            ViewBag.LookUpMstList = await _db.AreaLookUpMasters.Where(p => p.Category == sCategory).OrderBy(p => p.Category).ThenBy(p => p.Description).ToListAsync();
            return items;
        }

        [ActionName("GetEnteredLookUpMaster")]
        public async Task<List<SelectListItem>> GetEnteredLookUpMaster(string category)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var lookUpMasters = await (from lookUpMaster in _db.AreaLookUpMasters
                                       where lookUpMaster.Category == category
                                       orderby lookUpMaster.Description
                                       select lookUpMaster).ToListAsync();
            foreach (var item in lookUpMasters)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString() + " : " + item.Description, Value = item.Id.ToString() });
            }

            ViewBag.LookUpMstList1 = await _db.AreaLookUpMasters.Where(p => p.Category == category).OrderBy(p => p.Category).ThenBy(p => p.Description).ToListAsync();
            return items;
        }
    }
}
