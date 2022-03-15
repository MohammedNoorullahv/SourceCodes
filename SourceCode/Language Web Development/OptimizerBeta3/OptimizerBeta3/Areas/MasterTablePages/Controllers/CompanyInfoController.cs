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
    public class CompanyInfoController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CompanyInfoViewModel companyInfoVM { get; set; }
        public CompanyInfoController(ApplicationDbContext db)
        {
            _db = db;
            companyInfoVM = new CompanyInfoViewModel()
            {
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                CompanyInfo = new Models.MasterTables.CompanyInfo()
            };
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.companyInfos.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            companyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            companyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            companyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            companyInfoVM.FKState = await _db.StateMasters.ToListAsync();

            return View(companyInfoVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(companyInfoVM);
            }

            string codechar = companyInfoVM.CompanyInfo.CompanyName.Substring(0, 2).ToUpper();
            var maxcode = 0;

            if (_db.companyInfos.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Count > 0)
            {
                maxcode = _db.companyInfos.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Max();
            }

            companyInfoVM.CompanyInfo.Code = codechar + String.Format("{0:0000}", (maxcode + 1));

            _db.companyInfos.Add(companyInfoVM.CompanyInfo);
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

            var CompanyInfo = await _db.companyInfos.SingleOrDefaultAsync(m => m.Id == id);

            if (CompanyInfo == null)
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

            //companyInfoVM.CompanyInfo = await _db.companyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).SingleOrDefaultAsync(m => m.Id == id);
            companyInfoVM.CompanyInfo = await _db.companyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).SingleOrDefaultAsync(m => m.Id == id);
            companyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            companyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            companyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            companyInfoVM.FKState = await _db.StateMasters.ToListAsync();

            if (companyInfoVM.CompanyInfo == null)
            {
                return NotFound();
            }
            return View(companyInfoVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesCompanyInfoExist = _db.companyInfos.Include(s => s.CompanyName).Where(s => s.CompanyName == model.CompanyInfo.CompanyName && s.Address1 == model.CompanyInfo.Address1);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var CompanyInfofromDb = await _db.companyInfos.FindAsync(id);

                CompanyInfofromDb.Code = model.CompanyInfo.Code;
                CompanyInfofromDb.CompanyName = model.CompanyInfo.CompanyName;
                CompanyInfofromDb.ShortName = model.CompanyInfo.ShortName;
                CompanyInfofromDb.Address1 = model.CompanyInfo.Address1;
                CompanyInfofromDb.Address2 = model.CompanyInfo.Address2;
                CompanyInfofromDb.FKArea = model.CompanyInfo.FKArea;
                CompanyInfofromDb.FKCity = model.CompanyInfo.FKCity;
                CompanyInfofromDb.FKPincode = model.CompanyInfo.FKPincode;
                CompanyInfofromDb.FKState = model.CompanyInfo.FKState;
                CompanyInfofromDb.ContactPersonName = model.CompanyInfo.ContactPersonName;
                CompanyInfofromDb.ContactNo = model.CompanyInfo.ContactNo;
                CompanyInfofromDb.MailId = model.CompanyInfo.MailId;
                CompanyInfofromDb.PANNumber = model.CompanyInfo.PANNumber;
                CompanyInfofromDb.GSTNumber = model.CompanyInfo.GSTNumber;
                CompanyInfofromDb.ModifiedBy = model.CompanyInfo.ModifiedBy;
                CompanyInfofromDb.ModifiedDate = model.CompanyInfo.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            CompanyInfoViewModel modelVM = new CompanyInfoViewModel()
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

            var CompanyInfo = await _db.companyInfos.SingleOrDefaultAsync(m => m.Id == id);

            if (CompanyInfo == null)
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

            companyInfoVM.CompanyInfo = await _db.companyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).SingleOrDefaultAsync(m => m.Id == id);
            companyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            companyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            companyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            companyInfoVM.FKState = await _db.StateMasters.ToListAsync();

            if (companyInfoVM.CompanyInfo == null)
            {
                return NotFound();
            }
            return View(companyInfoVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CompanyInfo = await _db.companyInfos.SingleOrDefaultAsync(m => m.Id == id);

            if (CompanyInfo == null)
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

            companyInfoVM.CompanyInfo = await _db.companyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).SingleOrDefaultAsync(m => m.Id == id);
            companyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            companyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            companyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            companyInfoVM.FKState = await _db.StateMasters.ToListAsync();

            if (companyInfoVM.CompanyInfo == null)
            {
                return NotFound();
            }
            return View(companyInfoVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var companyInfo = await _db.companyInfos.FindAsync(id);

            if (companyInfo == null)
            {
                return View();
            }
            _db.companyInfos.Remove(companyInfo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

