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
    public class BankController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public BankViewModel BankVM { get; set; }
        public BankController(ApplicationDbContext db)
        {
            _db = db;
            BankVM = new BankViewModel()
            {
                FKBankName = _db.lookUpMasters,
                bank = new Models.MasterTables.Bank()
            };
        }
        public async Task<IActionResult> Index()
        {
            var bank = await _db.banks.Include(s => s.LookUpMasterBank).ToListAsync();
            return View(bank);
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            BankVM.FKBankName = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 16).ToListAsync();

            return View(BankVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(BankVM);
            }

            _db.banks.Add(BankVM.bank);
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

            var Bank = await _db.banks.SingleOrDefaultAsync(m => m.Id == id);

            if (Bank == null)
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

            BankVM.bank = await _db.banks.Include(m => m.LookUpMasterBank).SingleOrDefaultAsync(m => m.Id == id);
            BankVM.FKBankName = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 16).ToListAsync();

            if (BankVM.bank == null)
            {
                return NotFound();
            }
            return View(BankVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesBankExist = _db.banks.Include(s => s.BranchName).Where(s => s.BranchName == model.bank.BranchName);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var BankfromDb = await _db.banks.FindAsync(id);

                BankfromDb.FKBankName = model.bank.FKBankName;
                BankfromDb.BranchName = model.bank.BranchName;
                BankfromDb.BranchCode = model.bank.BranchCode;
                BankfromDb.IFSCCode = model.bank.IFSCCode;
                BankfromDb.ModifiedBy = model.bank.ModifiedBy;
                BankfromDb.ModifiedDate = model.bank.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            BankViewModel modelVM = new BankViewModel()
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

            var Bank = await _db.banks.SingleOrDefaultAsync(m => m.Id == id);

            if (Bank == null)
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

            BankVM.bank = await _db.banks.Include(m => m.LookUpMasterBank).SingleOrDefaultAsync(m => m.Id == id);
            BankVM.FKBankName = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 16).ToListAsync();

            if (BankVM.bank == null)
            {
                return NotFound();
            }
            return View(BankVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Bank = await _db.banks.SingleOrDefaultAsync(m => m.Id == id);

            if (Bank == null)
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

            BankVM.bank = await _db.banks.Include(m => m.LookUpMasterBank).SingleOrDefaultAsync(m => m.Id == id);
            BankVM.FKBankName = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 16).ToListAsync();

            if (BankVM.bank == null)
            {
                return NotFound();
            }
            return View(BankVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var Bank = await _db.banks.FindAsync(id);

            if (Bank == null)
            {
                return View();
            }
            _db.banks.Remove(Bank);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
