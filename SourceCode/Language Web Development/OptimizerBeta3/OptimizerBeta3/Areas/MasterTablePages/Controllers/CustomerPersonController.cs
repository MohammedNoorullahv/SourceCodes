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
    public class CustomerPersonController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CustomerPersonViewModel CustomerPersonVM { get; set; }

        public CustomerPersonController(ApplicationDbContext db)
        {
            _db = db;
            CustomerPersonVM = new CustomerPersonViewModel()
            {
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                FKCountry = _db.lookUpMasters,
                FKGender = _db.lookUpMasters,
                FKMaritalStatus= _db.lookUpMasters,
                CustomerPerson = new Models.MasterTables.CustomerPerson()
            };
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.customerPerson.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            CustomerPersonVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            CustomerPersonVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            CustomerPersonVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            CustomerPersonVM.FKState = await _db.StateMasters.ToListAsync();
            CustomerPersonVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();
            CustomerPersonVM.FKCustomerOf = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 48).ToListAsync();

            return View(CustomerPersonVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerPersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(CustomerPersonVM);
            }

            _db.customerPerson.Add(CustomerPersonVM.CustomerPerson);
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

            var person = await _db.customerPerson.SingleOrDefaultAsync(m => m.Id == id);

            if (person == null)
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

            CustomerPersonVM.CustomerPerson = await _db.customerPerson.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).SingleOrDefaultAsync(m => m.Id == id);
            CustomerPersonVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            CustomerPersonVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            CustomerPersonVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            CustomerPersonVM.FKState = await _db.StateMasters.ToListAsync();
            CustomerPersonVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();
            CustomerPersonVM.FKCustomerOf = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 48).ToListAsync();

            if (CustomerPersonVM.CustomerPerson == null)
            {
                return NotFound();
            }
            return View(CustomerPersonVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerPersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var doesCompanyInfoExist = _db.companyInfos.Include(s => s.CompanyName).Where(s => s.CompanyName == model.CompanyInfo.CompanyName && s.Address1 == model.CompanyInfo.Address1);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var CustomerPersonfromDb = await _db.customerPerson.FindAsync(id);

                CustomerPersonfromDb.PersonName = model.CustomerPerson.PersonName;
                CustomerPersonfromDb.Gender = model.CustomerPerson.Gender;
                CustomerPersonfromDb.Address = model.CustomerPerson.Address;
                CustomerPersonfromDb.FKArea = model.CustomerPerson.FKArea;
                CustomerPersonfromDb.FKCity = model.CustomerPerson.FKCity;
                CustomerPersonfromDb.FKPincode = model.CustomerPerson.FKPincode;
                CustomerPersonfromDb.FKState = model.CustomerPerson.FKState;
                CustomerPersonfromDb.FKCountry = model.CustomerPerson.FKCountry;
                CustomerPersonfromDb.OfficePhoneNo = model.CustomerPerson.OfficePhoneNo;
                CustomerPersonfromDb.HomePhoneNo = model.CustomerPerson.HomePhoneNo;
                CustomerPersonfromDb.MobileNo = model.CustomerPerson.MobileNo;
                CustomerPersonfromDb.BirthDate = model.CustomerPerson.BirthDate;
                //CustomerPersonfromDb.IsMarried = model.CustomerPerson.IsMarried;
                CustomerPersonfromDb.WeddingDate = model.CustomerPerson.WeddingDate;
                CustomerPersonfromDb.FKCustomerOf = model.CustomerPerson.FKCustomerOf;
                CustomerPersonfromDb.ModifiedBy = model.CustomerPerson.ModifiedBy;
                CustomerPersonfromDb.ModifiedDate = model.CustomerPerson.ModifiedDate;

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

        //GET - Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _db.customerPerson.SingleOrDefaultAsync(m => m.Id == id);

            if (person == null)
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

            CustomerPersonVM.CustomerPerson = await _db.customerPerson.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).SingleOrDefaultAsync(m => m.Id == id);
            CustomerPersonVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            CustomerPersonVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            CustomerPersonVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            CustomerPersonVM.FKState = await _db.StateMasters.ToListAsync();
            CustomerPersonVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();
            CustomerPersonVM.FKCustomerOf = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 48).ToListAsync();

            if (CustomerPersonVM.CustomerPerson == null)
            {
                return NotFound();
            }
            return View(CustomerPersonVM);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _db.customerPerson.SingleOrDefaultAsync(m => m.Id == id);

            if (person == null)
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

            CustomerPersonVM.CustomerPerson = await _db.customerPerson.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).SingleOrDefaultAsync(m => m.Id == id);
            CustomerPersonVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            CustomerPersonVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            CustomerPersonVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            CustomerPersonVM.FKState = await _db.StateMasters.ToListAsync();
            CustomerPersonVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();
            CustomerPersonVM.FKCustomerOf = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 48).ToListAsync();

            if (CustomerPersonVM.CustomerPerson == null)
            {
                return NotFound();
            }
            return View(CustomerPersonVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var person = await _db.customerPerson.FindAsync(id);

            if (person == null)
            {
                return View();
            }
            _db.customerPerson.Remove(person);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
