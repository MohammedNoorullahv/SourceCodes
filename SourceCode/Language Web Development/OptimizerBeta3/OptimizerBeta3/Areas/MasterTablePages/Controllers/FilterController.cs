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
    public class FilterController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public FilterViewModel FilterVM { get; set; }

        public FilterController(ApplicationDbContext db)
        {
            _db = db;
            FilterVM = new FilterViewModel()
            {
                FKLookUpCategoryId = _db.lookUpCategories,
                FKLookUpMasterId = _db.lookUpMasters,
                Filter = new Models.MasterTables.Filter()
            };
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Filters.OrderBy(x => x.ControllerName).ThenBy(x => x.ActionMethod).ThenBy(x => x.TableName).ToListAsync());
        }


        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            FilterVM.FKLookUpCategoryId = await _db.lookUpCategories.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();
            //FilterVM.FKLookUpMasterId = await _db.lookUpMasters.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();

            List<string> tbllist = new List<string>();

            string sqlQuery = $"EXEC SLI_Filters @mAction='SELALLTABLES'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {
                for (int i = 0; i < result.FieldCount; i++)
                {
                    tbllist.Add(result.GetValue(i).ToString());
                }
            }
            ViewBag.TableNames = tbllist;
            return View(FilterVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                FilterVM.FKLookUpCategoryId = await _db.lookUpCategories.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();
                FilterVM.FKLookUpMasterId = await _db.lookUpMasters.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();
                return View(FilterVM);
            }

            //var companyInfo = await _db.companyInfos.ToListAsync();
            FilterVM.Filter.LookUPCategory = _db.lookUpCategories.Where(x => x.Id == FilterVM.Filter.FKLookUpCategory).FirstOrDefault().Description;
            FilterVM.Filter.LookUPMaster = _db.lookUpMasters.Where(x => x.Id == FilterVM.Filter.FKLookUpMaster).FirstOrDefault().Description;

            _db.Filters.Add(FilterVM.Filter);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Create));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int Id)
        {

            List<string> tbllist = new List<string>();

            string sqlQuery = $"EXEC SLI_Filters @mAction='SELALLTABLES'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {
                for (int i = 0; i < result.FieldCount; i++)
                {
                    tbllist.Add(result.GetValue(i).ToString());
                }
            }
            ViewBag.TableNames = tbllist;

            FilterVM.Filter = await _db.Filters.SingleOrDefaultAsync(m => m.Id == Id);
            FilterVM.FKLookUpCategoryId = await _db.lookUpCategories.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();
            FilterVM.FKLookUpMasterId = await _db.lookUpMasters.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();

            return View(FilterVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FilterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var filterfromDb = await _db.Filters.FindAsync(id);

                filterfromDb.ControllerName = model.Filter.ControllerName;
                filterfromDb.ActionMethod = model.Filter.ActionMethod;
                filterfromDb.TableName = model.Filter.TableName;
                filterfromDb.FKLookUpCategory = model.Filter.FKLookUpCategory;
                filterfromDb.LookUPCategory = _db.lookUpCategories.Where(x => x.Id == model.Filter.FKLookUpCategory).FirstOrDefault().Description;
                filterfromDb.FKLookUpMaster = model.Filter.FKLookUpMaster;
                filterfromDb.LookUPMaster = _db.lookUpMasters.Where(x => x.Id == model.Filter.FKLookUpMaster).FirstOrDefault().Description;
                filterfromDb.ConditionIn = model.Filter.ConditionIn;
                filterfromDb.ConditionNotIn = model.Filter.ConditionNotIn;
                filterfromDb.ModifiedBy = model.Filter.ModifiedBy;
                filterfromDb.ModifiedDate = model.Filter.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int Id)
        {

            //List<string> tbllist = new List<string>();

            //string sqlQuery = $"EXEC SLI_Filters @mAction='SELALLTABLES'";
            //var cmd = _db.Database.GetDbConnection().CreateCommand();
            //cmd.CommandText = sqlQuery;
            //_db.Database.OpenConnection();

            //var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //while (result.Read())
            //{
            //    for (int i = 0; i < result.FieldCount; i++)
            //    {
            //        tbllist.Add(result.GetValue(i).ToString());
            //    }
            //}
            //ViewBag.TableNames = tbllist;

            FilterVM.Filter = await _db.Filters.SingleOrDefaultAsync(m => m.Id == Id);
            //FilterVM.FKLookUpCategoryId = await _db.lookUpCategories.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();
            //FilterVM.FKLookUpMasterId = await _db.lookUpMasters.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();

            return View(FilterVM);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int Id)
        {

            //List<string> tbllist = new List<string>();

            //string sqlQuery = $"EXEC SLI_Filters @mAction='SELALLTABLES'";
            //var cmd = _db.Database.GetDbConnection().CreateCommand();
            //cmd.CommandText = sqlQuery;
            //_db.Database.OpenConnection();

            //var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //while (result.Read())
            //{
            //    for (int i = 0; i < result.FieldCount; i++)
            //    {
            //        tbllist.Add(result.GetValue(i).ToString());
            //    }
            //}
            //ViewBag.TableNames = tbllist;

            FilterVM.Filter = await _db.Filters.SingleOrDefaultAsync(m => m.Id == Id);
            //FilterVM.FKLookUpCategoryId = await _db.lookUpCategories.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();
            //FilterVM.FKLookUpMasterId = await _db.lookUpMasters.OrderBy(s => s.Description).Where(s => s.IsActive == true).ToListAsync();

            return View(FilterVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var filter = await _db.Filters.FindAsync(id);

            if (filter == null)
            {
                return View();
            }
            _db.Filters.Remove(filter);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
