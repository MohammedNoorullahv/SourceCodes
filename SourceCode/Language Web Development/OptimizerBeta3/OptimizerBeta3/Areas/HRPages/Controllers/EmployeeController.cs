using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.HRTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.HRPages.Controllers
{
    [Area("HRPages")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public EmployeeViewModel EmployeeVM { get; set; }

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
            EmployeeVM = new EmployeeViewModel()
            {
                FKUnit = _db.unitMasters,
                FKSalutation = _db.lookUpMasters,
                FKMaritalStatus = _db.lookUpMasters,
                FKDOBProofType = _db.lookUpMasters,
                FKDepartment = _db.lookUpMasters,
                FKDesignation = _db.lookUpMasters,
                FKEmpCategory = _db.lookUpMasters,
                FKReligion = _db.lookUpMasters,
                FKQualification = _db.lookUpMasters,
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                FKGender = _db.lookUpMasters,
                Employee = new Models.HRTables.Employee()
            };
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Employees.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            EmployeeVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            EmployeeVM.FKSalutation = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 6).ToListAsync();
            EmployeeVM.FKMaritalStatus = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 13).ToListAsync();
            EmployeeVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            EmployeeVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            EmployeeVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            EmployeeVM.FKState = await _db.StateMasters.ToListAsync();
            EmployeeVM.FKDOBProofType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 66).ToListAsync();
            EmployeeVM.FKDepartment = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 9).ToListAsync();
            EmployeeVM.FKDesignation = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 8).ToListAsync();
            EmployeeVM.FKEmpCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 10).ToListAsync();
            EmployeeVM.FKReligion = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 15).ToListAsync();
            EmployeeVM.FKQualification = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 14).ToListAsync();
            EmployeeVM.FKGender = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 67).ToListAsync();
            return View(EmployeeVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(EmployeeVM);
            }
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            var unitInfo = await _db.unitMasters.ToListAsync();

            string sUnitCode = unitInfo.Where(x => x.Id == EmployeeVM.Employee.FKUnit).FirstOrDefault().Code;
            string codechar = sUnitCode.Substring(0, 2).ToUpper();
            var maxcode = 0;

            if (_db.Employees.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Count > 0)
            {
                maxcode = _db.Employees.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Max();
            }

            EmployeeVM.Employee.Code = codechar + String.Format("{0:0000}", (maxcode + 1));
            EmployeeVM.Employee.UnitName = unitInfo.Where(x => x.Id == EmployeeVM.Employee.FKUnit).FirstOrDefault().CompanyName;
            EmployeeVM.Employee.Gender = (lookUpMaster.Where(x => x.Id == EmployeeVM.Employee.FKGender).FirstOrDefault().Description).ToString().Substring(0,1).ToUpper();
            _db.Employees.Add(EmployeeVM.Employee);
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

            var employee = await _db.Employees.SingleOrDefaultAsync(m => m.Id == id);

            if (employee == null)
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

            //employeeVM.employee = await _db.employees.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).SingleOrDefaultAsync(m => m.Id == id);
            EmployeeVM.Employee = await _db.Employees.SingleOrDefaultAsync(m => m.Id == id);
            EmployeeVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            EmployeeVM.FKSalutation = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 6).ToListAsync();
            EmployeeVM.FKMaritalStatus = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 13).ToListAsync();
            EmployeeVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            EmployeeVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            EmployeeVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            EmployeeVM.FKState = await _db.StateMasters.ToListAsync();
            EmployeeVM.FKDOBProofType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 66).ToListAsync();
            EmployeeVM.FKDepartment = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 9).ToListAsync();
            EmployeeVM.FKDesignation = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 8).ToListAsync();
            EmployeeVM.FKEmpCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 10).ToListAsync();
            EmployeeVM.FKReligion = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 15).ToListAsync();
            EmployeeVM.FKQualification = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 14).ToListAsync();
            EmployeeVM.FKGender = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 67).ToListAsync();

            if (EmployeeVM.Employee == null)
            {
                return NotFound();
            }
            return View(EmployeeVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var doesemployeeExist = _db.employees.Include(s => s.CompanyName).Where(s => s.CompanyName == model.employee.CompanyName && s.Address1 == model.employee.Address1);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var employeefromDb = await _db.Employees.FindAsync(id);

                //employeefromDb.Id = model.Employee.Id;
                employeefromDb.FKUnit = model.Employee.FKUnit;
                employeefromDb.UnitName = model.Employee.UnitName;
                employeefromDb.FKSalutation = model.Employee.FKSalutation;
                employeefromDb.FullName = model.Employee.FullName;
                employeefromDb.FirstName = model.Employee.FirstName;
                employeefromDb.MiddleName = model.Employee.MiddleName;
                employeefromDb.LastName = model.Employee.LastName;
                employeefromDb.Initials = model.Employee.Initials;
                employeefromDb.FKMaritalStatus = model.Employee.FKMaritalStatus;
                employeefromDb.HorFName = model.Employee.HorFName;
                employeefromDb.Gender = model.Employee.Gender;
                employeefromDb.Address1 = model.Employee.Address1;
                employeefromDb.Address2 = model.Employee.Address2;
                employeefromDb.FKArea = model.Employee.FKArea;
                employeefromDb.FKCity = model.Employee.FKCity;
                employeefromDb.FKPincode = model.Employee.FKPincode;
                employeefromDb.FKState = model.Employee.FKState;
                employeefromDb.MobileNo = model.Employee.MobileNo;
                employeefromDb.EMailId = model.Employee.EMailId;
                employeefromDb.DOB = model.Employee.DOB;
                employeefromDb.FKDOBProofType = model.Employee.FKDOBProofType;
                employeefromDb.DOJ = model.Employee.DOJ;
                employeefromDb.FKDepartment = model.Employee.FKDepartment;
                employeefromDb.FKDesignation = model.Employee.FKDesignation;
                employeefromDb.FKEmpCategory = model.Employee.FKEmpCategory;
                employeefromDb.FKReligion = model.Employee.FKReligion;
                employeefromDb.FKQualification = model.Employee.FKQualification;
                employeefromDb.NoofDependants = model.Employee.NoofDependants;
                employeefromDb.PFNo = model.Employee.PFNo;
                employeefromDb.ESINo = model.Employee.ESINo;
                employeefromDb.PANNo = model.Employee.PANNo;
                employeefromDb.BankAccountNo = model.Employee.BankAccountNo;
                employeefromDb.ResignDate = model.Employee.ResignDate;
                employeefromDb.ModifiedBy = model.Employee.ModifiedBy;
                employeefromDb.ModifiedDate = model.Employee.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            EmployeeViewModel modelVM = new EmployeeViewModel()
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

            var employee = await _db.Employees.SingleOrDefaultAsync(m => m.Id == id);

            if (employee == null)
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

            EmployeeVM.Employee = await _db.Employees.SingleOrDefaultAsync(m => m.Id == id);
            EmployeeVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            EmployeeVM.FKSalutation = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 6).ToListAsync();
            EmployeeVM.FKMaritalStatus = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 13).ToListAsync();
            EmployeeVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            EmployeeVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            EmployeeVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            EmployeeVM.FKState = await _db.StateMasters.ToListAsync();
            EmployeeVM.FKDOBProofType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 66).ToListAsync();
            EmployeeVM.FKDepartment = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 9).ToListAsync();
            EmployeeVM.FKDesignation = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 8).ToListAsync();
            EmployeeVM.FKEmpCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 10).ToListAsync();
            EmployeeVM.FKReligion = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 15).ToListAsync();
            EmployeeVM.FKQualification = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 14).ToListAsync();
            EmployeeVM.FKGender = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 67).ToListAsync();

            if (EmployeeVM.Employee == null)
            {
                return NotFound();
            }
            return View(EmployeeVM);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            TempData["ErrorMessage"] = "Deletion of Employee Not allowed";
            return RedirectToAction(nameof(Index));
        }

    }
}
