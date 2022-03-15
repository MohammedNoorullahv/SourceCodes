using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class LookUpMasterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LookUpMasterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            var lookUpMaster = await _db.lookUpMasters.OrderBy(c => c.LookUpCategory.Description).Include(s => s.LookUpCategory).ToListAsync();
            return View(lookUpMaster);

        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderByDescending(x =>x.LastEntryCategory).ThenBy(c => c.Description).ToListAsync(),
                LookUpMasters = new Models.MasterTables.LookUpMaster(),
                LookUpMstList = await _db.lookUpMasters.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            };
            return View(model);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LookUpCategoryAndLookUpMstViewModel model)
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



                    var lastentry = _db.lookUpCategories.Where(s => s.LastEntryCategory == true);
                    if (lastentry.Count() > 0)
                    {
                        var lastentrycategory = await _db.lookUpCategories.Where(x => x.LastEntryCategory == true).FirstOrDefaultAsync();
                        if (lastentrycategory.Id != model.LookUpMasters.FKLookUpCategory)
                        {

                            var lkctlastentry = await _db.lookUpCategories.Where(s => s.LastEntryCategory == true).ToListAsync();
                            foreach (var item in lkctlastentry)
                            {
                                item.LastEntryCategory = false;
                                await _db.SaveChangesAsync();
                            }

                            var lkct = await _db.lookUpCategories.Where(x => x.Id == model.LookUpMasters.FKLookUpCategory).FirstOrDefaultAsync();
                            lkct.LastEntryCategory = true;
                            await _db.SaveChangesAsync();
                        }

                        
                    }




                    return RedirectToAction(nameof(Create));
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

        [ActionName("GetLookUpMaster")]
        public async Task<List<SelectListItem>> GetLookUpMaster(int id)
        {
            //List<LookUpMaster> lookUpMasters = new List<LookUpMaster>();
            List<SelectListItem> items = new List<SelectListItem>();
            var lookUpMasters = await (from lookUpMaster in _db.lookUpMasters
                                       where lookUpMaster.FKLookUpCategory == id
                                       orderby lookUpMaster.Description
                                       select lookUpMaster).ToListAsync();
            foreach (var item in lookUpMasters)
            {
                items.Add(new SelectListItem { Text = item.Description, Value = item.Id.ToString() });
            }
            return items;
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookUpMaster = await _db.lookUpMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (lookUpMaster == null)
            {
                return NotFound();
            }

            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderBy(c => c.Description).ToListAsync(),
                LookUpMasters = lookUpMaster
                //LookUpMstList = await _db.lookUpMasters.OrderBy(p => p.FKLookUpCategory).Select(p => p.FKLookUpCategory).Distinct().ToListAsync()
            };

            return View(model);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LookUpCategoryAndLookUpMstViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var doesLookUpMasterExists = _db.lookUpMasters.Include(s => s.FKLookUpCategory).Where(s => s.Description == model.LookUpMasters.Description && s.LookUpCategory.Id == model.LookUpMasters.FKLookUpCategory);

                //if (doesLookUpMasterExists.Count() > 0)
                //{
                //    //Error
                //    //StatusMessage = "Error : Sub Category exists under " + doesSubCategoryExists.First().Category.Name + " category. Please use another name.";
                //}
                //else
                {
                    if (model.LookUpMasters.SetAsDefault == true)
                    {
                        var defaultId = _db.lookUpMasters.Where(s => s.FKLookUpCategory == model.LookUpMasters.FKLookUpCategory && s.SetAsDefault == true);
                        if (defaultId.Count() > 0)
                        {
                            var lookUpMasterFromDb1 = await _db.lookUpMasters.FindAsync(defaultId.First().Id);

                            lookUpMasterFromDb1.SetAsDefault = false;
                            await _db.SaveChangesAsync();

                        }
                    }

                    //var lookUpMasterFromDb = await _db.lookUpMasters.FindAsync(model.LookUpMasters.Id);
                    var lookUpMasterFromDb = await _db.lookUpMasters.FindAsync(model.Id);
                    lookUpMasterFromDb.FKLookUpCategory = model.LookUpMasters.FKLookUpCategory;
                    lookUpMasterFromDb.Description = model.LookUpMasters.Description;
                    lookUpMasterFromDb.ShortCode = model.LookUpMasters.ShortCode;
                    lookUpMasterFromDb.SetAsDefault = model.LookUpMasters.SetAsDefault;
                    lookUpMasterFromDb.ModifiedBy = model.LookUpMasters.ModifiedBy;
                    lookUpMasterFromDb.ModifiedDate = DateTime.Now;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            LookUpCategoryAndLookUpMstViewModel modelVM = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.ToListAsync(),
                LookUpMasters = model.LookUpMasters,
                //SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            //modelVM.SubCategory.Id = id;
            return View(modelVM);
        }

        //GET Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookUpMaster = await _db.lookUpMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (lookUpMaster == null)
            {
                return NotFound();
            }

            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderBy(c => c.Description).ToListAsync(),
                LookUpMasters = lookUpMaster
                //LookUpMstList = await _db.lookUpMasters.OrderBy(p => p.FKLookUpCategory).Select(p => p.FKLookUpCategory).Distinct().ToListAsync()
            };

            return View(model);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookUpMaster = await _db.lookUpMasters.SingleOrDefaultAsync(m => m.Id == id);

            if (lookUpMaster == null)
            {
                return NotFound();
            }

            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderBy(c => c.Description).ToListAsync(),
                LookUpMasters = lookUpMaster
                //LookUpMstList = await _db.lookUpMasters.OrderBy(p => p.FKLookUpCategory).Select(p => p.FKLookUpCategory).Distinct().ToListAsync()
            };

            return View(model);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lookUpMaster = await _db.lookUpMasters.SingleOrDefaultAsync(m => m.Id == id);
            _db.lookUpMasters.Remove(lookUpMaster);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
