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
    public class ColorMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ColorMasterViewModel ColorMasterVM { get; set; }
        public ColorMasterController(ApplicationDbContext db)
        {
            _db = db;
            ColorMasterVM = new ColorMasterViewModel()
            {
                FKColour = _db.lookUpMasters,
                colorMaster = new Models.MasterTables.ColorMaster()
            };
        }
        public async Task<IActionResult> Index()
        {
            var colormaster = await _db.colorMasters.Include(s => s.LookUpMasterColour).ToListAsync();
            return View(colormaster);
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            ColorMasterVM.FKColour = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 34).ToListAsync();

            return View(ColorMasterVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ColorMasterVM);
            }

            _db.colorMasters.Add(ColorMasterVM.colorMaster);
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

            var ColorMaster = await _db.colorMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (ColorMaster == null)
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

            ColorMasterVM.colorMaster = await _db.colorMasters.Include(m => m.LookUpMasterColour).SingleOrDefaultAsync(m => m.Id == id);
            ColorMasterVM.FKColour = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 34).ToListAsync();

            if (ColorMasterVM.colorMaster == null)
            {
                return NotFound();
            }
            return View(ColorMasterVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ColorMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesColorMasterExist = _db.colorMasters.Include(s => s.ColourName).Where(s => s.ColourName == model.colorMaster.ColourName);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var ColourMasterfromDb = await _db.colorMasters.FindAsync(id);

                ColourMasterfromDb.FKColour = model.colorMaster.FKColour;
                ColourMasterfromDb.ColourName = model.colorMaster.ColourName;
                ColourMasterfromDb.Combination = model.colorMaster.Combination;
                ColourMasterfromDb.ModifiedBy = model.colorMaster.ModifiedBy;
                ColourMasterfromDb.ModifiedDate = model.colorMaster.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            ColorMasterViewModel modelVM = new ColorMasterViewModel()
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

            var ColorMaster = await _db.colorMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (ColorMaster == null)
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

            ColorMasterVM.colorMaster = await _db.colorMasters.Include(m => m.LookUpMasterColour).SingleOrDefaultAsync(m => m.Id == id);
            ColorMasterVM.FKColour = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 34).ToListAsync();

            if (ColorMasterVM.colorMaster == null)
            {
                return NotFound();
            }
            return View(ColorMasterVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ColorMaster = await _db.colorMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (ColorMaster == null)
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

            ColorMasterVM.colorMaster = await _db.colorMasters.Include(m => m.LookUpMasterColour).SingleOrDefaultAsync(m => m.Id == id);
            ColorMasterVM.FKColour = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 34).ToListAsync();

            if (ColorMasterVM.colorMaster == null)
            {
                return NotFound();
            }
            return View(ColorMasterVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var ColorMaster = await _db.colorMasters.FindAsync(id);

            if (ColorMaster == null)
            {
                return View();
            }
            _db.colorMasters.Remove(ColorMaster);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
