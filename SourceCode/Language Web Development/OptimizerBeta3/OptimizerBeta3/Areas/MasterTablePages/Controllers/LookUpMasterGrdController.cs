using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class LookUpMasterGrdController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LookUpMasterGrdController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            //var lookUpMaster = await _db.lookUpMasters.OrderBy(c => c.LookUpCategory.Description).Include(s => s.LookUpCategory).ToListAsync();
            //return View(lookUpMaster);
            return View();
        }

        public JsonResult GetLookUpMasterLists(string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var lookUpMaster =  _db.lookUpMasters.OrderBy(c => c.LookUpCategory.Description).Include(s => s.LookUpCategory);
            var todoListsResults = lookUpMaster.Select(
                    a => new
                    {
                        a.Id,
                        //a.LookUpCategory.Description,
                        a.Description,
                        a.ShortCode
                    });
            int totalRecords = todoListsResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                todoListsResults = todoListsResults.OrderByDescending(s => s.Description);
                todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                todoListsResults = todoListsResults.OrderBy(s => s.Description);
                todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = todoListsResults
            };
            return Json(jsonData, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
    }
}
