using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class MaterialPurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public MaterialPurchaseOrderViewModel materialPurchaseOrderVM { get; set; }
        public MaterialPurchaseOrderDetailViewModel materialPurchaseOrderDetailVM { get; set; }

        public static int nFKPO;
        public static string sPurchaseOrderNo;

        public MaterialPurchaseOrderController(ApplicationDbContext db)
        {
            _db = db;
            materialPurchaseOrderVM = new MaterialPurchaseOrderViewModel()
            {
                FKTypeOfOrder = _db.lookUpMasters,
                FKSeason = _db.seasons,
                FKSource = _db.lookUpMasters,
                FKUnit = _db.unitMasters,
                FKParty = _db.partyInfos,
                FKDepartment = _db.lookUpMasters,
                FKPOType = _db.lookUpMasters,
                FKOrderStatus = _db.lookUpMasters,
                FKPaymentTerms = _db.lookUpMasters,
                FKCurrency = _db.lookUpMasters,
                FKModeofTransport = _db.lookUpMasters,
                FKDeliveryTo = _db.unitMasters,
                MaterialPurchaseOrder = new Models.TransactionTables.MaterialPurchaseOrder()
            };

            materialPurchaseOrderDetailVM = new MaterialPurchaseOrderDetailViewModel()
            {
                MaterialId = _db.materials,
                FKOrderStatus = _db.lookUpMasters,
                MaterialPurchaseOrderDetail = new Models.TransactionTables.MaterialPurchaseOrderDetails()
            };
        }
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.materialPurchaseOrders.OrderByDescending(s => s.Id).Where(x => x.PurchaseOrderDt >= effectStartDate && x.PurchaseOrderDt <= effectEndDate).ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "MaterialPurchaseOrder", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            var lastPO = await _db.materialPurchaseOrders.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            TempData["LastPO"] = lastPO;

            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            materialPurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            //materialPurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKDeliveryTo = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            materialPurchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            //materialPurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();

            List<PartyInfo> suppliers = new List<PartyInfo>();
            string sqlQuery = $"EXEC SLI_Filters @mAction='SELSUPPINPOFW', @mControllerName='{controllerName}', @mActionMethod='{actionName}'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {
                PartyInfo supplier = new PartyInfo
                {
                    Id = (int)result["Id"],
                    CompanyName = result["CompanyName"].ToString()
                };
                suppliers.Add(supplier);
            }
            materialPurchaseOrderVM.FKParty = suppliers.ToList();
            //materialPurchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();

            List<LookUpMaster> lkPOTypes = new List<LookUpMaster>();
            string sqlQuery2 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='35'";
            var cmd2 = _db.Database.GetDbConnection().CreateCommand();
            cmd2.CommandText = sqlQuery2;
            _db.Database.OpenConnection();

            var result2 = cmd2.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result2.Read())
            {
                LookUpMaster lkPOType = new LookUpMaster
                {
                    Id = (int)result2["Id"],
                    Description = result2["Description"].ToString()
                };
                lkPOTypes.Add(lkPOType);
            }

            materialPurchaseOrderVM.FKPOType = lkPOTypes.ToList();
            //materialPurchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKSeason = await _db.seasons.OrderByDescending(c => c.Id).ThenBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();

            List<LookUpMaster> lmTypeofOrders = new List<LookUpMaster>();
            string sqlQuery1 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='49'";
            var cmd1 = _db.Database.GetDbConnection().CreateCommand();
            cmd1.CommandText = sqlQuery1;
            _db.Database.OpenConnection();

            var result1 = cmd1.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result1.Read())
            {
                LookUpMaster lmTypeofOrder = new LookUpMaster
                {
                    Id = (int)result1["Id"],
                    Description = result1["Description"].ToString()
                };
                lmTypeofOrders.Add(lmTypeofOrder);
            }
            materialPurchaseOrderVM.FKTypeOfOrder = lmTypeofOrders.ToList();

            //materialPurchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();

            List<LookUpMaster> lkms = new List<LookUpMaster>();
            string sqlQuery3 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='24'";
            var cmd3 = _db.Database.GetDbConnection().CreateCommand();
            cmd3.CommandText = sqlQuery3;
            _db.Database.OpenConnection();

            var result3 = cmd3.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result3.Read())
            {
                LookUpMaster lkm = new LookUpMaster
                {
                    Id = (int)result3["Id"],
                    Description = result3["Description"].ToString()
                };
                lkms.Add(lkm);
            }

            materialPurchaseOrderVM.FKCategory = lkms.ToList();
            //materialPurchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();

            return View(materialPurchaseOrderVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string Save)
        {
            if (!ModelState.IsValid)
            {
                return View(materialPurchaseOrderVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sTypeofOrder = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            string sSource = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKSource).FirstOrDefault().Description;
            string sPOType = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKPOType).FirstOrDefault().Description;
            string sDepartment = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKDepartment).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(materialPurchaseOrderVM.MaterialPurchaseOrder.FKSeason);
            string sSeason = season.Code;
            materialPurchaseOrderVM.MaterialPurchaseOrder.Category = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKCategory).FirstOrDefault().Description;
            materialPurchaseOrderVM.MaterialPurchaseOrder.FLAM = materialPurchaseOrderVM.MaterialPurchaseOrder.Category.ToString().Substring(0, 1);
            string sFLAM = materialPurchaseOrderVM.MaterialPurchaseOrder.FLAM;
            var companyInfo = await _db.companyInfos.ToListAsync();
            materialPurchaseOrderVM.MaterialPurchaseOrder.DeliveryTo = companyInfo.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKDeliveryTo).FirstOrDefault().CompanyName;
            materialPurchaseOrderVM.MaterialPurchaseOrder.UnitName = companyInfo.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKUnit).FirstOrDefault().CompanyName;
            materialPurchaseOrderVM.MaterialPurchaseOrder.POType = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKPOType).FirstOrDefault().Description;

            string sUnitCode = companyInfo.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKUnit).FirstOrDefault().Code;
            
            string codechar = (sUnitCode.Substring(0, 3) + sFLAM.Substring(0, 1) + sTypeofOrder.Substring(0, 2) + sSeason.Substring(0, 4) + sSource.Substring(0, 1)).ToUpper();
            //string codechar = (sTypeofOrder.Substring(0, 2) + sPOType.Substring(0, 2) + sDepartment.Substring(0, 2) + sSource.Substring(0, 1) + sSeason.Substring(0, 4)).ToUpper();
            var maxcode = 0;

            if (_db.materialPurchaseOrders.Where(x => x.PurchaseOrderNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.materialPurchaseOrders.Where(x => x.PurchaseOrderNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderNo.Substring(13, 4))).ToList().Max();
            }

            materialPurchaseOrderVM.MaterialPurchaseOrder.PurchaseOrderNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));


            materialPurchaseOrderVM.MaterialPurchaseOrder.Currency = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKCurrency).FirstOrDefault().Description;
            materialPurchaseOrderVM.MaterialPurchaseOrder.Department = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKDepartment).FirstOrDefault().Description;
            materialPurchaseOrderVM.MaterialPurchaseOrder.ModeofTransport = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKModeofTransport).FirstOrDefault().Description;
            materialPurchaseOrderVM.MaterialPurchaseOrder.OrderStatus = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            
            materialPurchaseOrderVM.MaterialPurchaseOrder.PaymentTerms = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKPaymentTerms).FirstOrDefault().Description;
            materialPurchaseOrderVM.MaterialPurchaseOrder.Source = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKSource).FirstOrDefault().Description;
            materialPurchaseOrderVM.MaterialPurchaseOrder.TypeofOrder = lookUpMaster.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            
            materialPurchaseOrderVM.MaterialPurchaseOrder.Season = season.Code;
            var partyInfo = await _db.partyInfos.ToListAsync();
            materialPurchaseOrderVM.MaterialPurchaseOrder.SupplierName = partyInfo.Where(x => x.Id == materialPurchaseOrderVM.MaterialPurchaseOrder.FKParty).FirstOrDefault().CompanyName; ;
            


            _db.materialPurchaseOrders.Add(materialPurchaseOrderVM.MaterialPurchaseOrder);
            await _db.SaveChangesAsync();

            if (Save == "Save & New Po")
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                var po = await _db.materialPurchaseOrders.Where(x => x.PurchaseOrderNo == materialPurchaseOrderVM.MaterialPurchaseOrder.PurchaseOrderNo).FirstOrDefaultAsync();
                return RedirectToAction("MPODetailCreate", "MaterialPurchaseOrder", new { Id = po.Id });
            }
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var CompanyInfo = await _db.companyInfos.SingleOrDefaultAsync(m => m.Id == id);

            //if (CompanyInfo == null)
            //{
            //    return NotFound();
            //}

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            //materialPurchaseOrderVM.MaterialPurchaseOrder = await _db.materialPurchaseOrders.Include(m => m.FKCurrency)
            //    .Include(m => m.FKDeliveryTo).Include(m => m.FKDepartment).Include(m => m.FKDepartment).Include(m => m.FKModeofTransport)
            //    .Include(m => m.FKOrderStatus).Include(m => m.FKParty).Include(m => m.FKPaymentTerms).Include(m => m.FKPOType)
            //    .Include(m => m.FKSeason).Include(m => m.FKSource).Include(m => m.FKTypeOfOrder).Include(m => m.FKUnit).SingleOrDefaultAsync(m => m.Id == id);
            materialPurchaseOrderVM.MaterialPurchaseOrder = await _db.materialPurchaseOrders.SingleOrDefaultAsync(m => m.Id == id);
            materialPurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKDeliveryTo = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            materialPurchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            //materialPurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            //materialPurchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();

            return View(materialPurchaseOrderVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaterialPurchaseOrderViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var mPOfromDb = await _db.materialPurchaseOrders.FindAsync(id);
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            mPOfromDb.FKOrderStatus = model.MaterialPurchaseOrder.FKOrderStatus;
            mPOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            mPOfromDb.FKPaymentTerms = model.MaterialPurchaseOrder.FKPaymentTerms;
            mPOfromDb.PaymentTerms = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrder.FKPaymentTerms).FirstOrDefault().Description;
            mPOfromDb.FKCurrency = model.MaterialPurchaseOrder.FKCurrency;
            mPOfromDb.Currency = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrder.FKCurrency).FirstOrDefault().Description;
            mPOfromDb.FKModeofTransport = model.MaterialPurchaseOrder.FKModeofTransport;
            mPOfromDb.ModeofTransport = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrder.FKModeofTransport).FirstOrDefault().Description;
            mPOfromDb.Remarks = model.MaterialPurchaseOrder.Remarks;
            mPOfromDb.FKDeliveryTo = model.MaterialPurchaseOrder.FKDeliveryTo;
            var companyInfo = await _db.companyInfos.ToListAsync();
            mPOfromDb.DeliveryTo = companyInfo.Where(x => x.Id == model.MaterialPurchaseOrder.FKDeliveryTo).FirstOrDefault().CompanyName;

            mPOfromDb.DeliveryTo = model.MaterialPurchaseOrder.DeliveryTo;
            mPOfromDb.ModifiedBy = model.MaterialPurchaseOrder.ModifiedBy;
            mPOfromDb.ModifiedDate = model.MaterialPurchaseOrder.ModifiedDate;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //}
            //return RedirectToAction(nameof(Index));
            //CompanyInfoViewModel modelVM = new CompanyInfoViewModel()
            //{
            //    //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    //LookUpMasters = model.LookUpMasters,
            //    ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
            //    //StatusMessage = StatusMessage
            //};
            //return View(modelVM);
        }

        //GET - CANCEL
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //materialPurchaseOrderVM.MaterialPurchaseOrder = await _db.materialPurchaseOrders.Include(m => m.FKCurrency)
            //    .Include(m => m.FKDeliveryTo).Include(m => m.FKDepartment).Include(m => m.FKDepartment).Include(m => m.FKModeofTransport)
            //    .Include(m => m.FKOrderStatus).Include(m => m.FKParty).Include(m => m.FKPaymentTerms).Include(m => m.FKPOType)
            //    .Include(m => m.FKSeason).Include(m => m.FKSource).Include(m => m.FKTypeOfOrder).Include(m => m.FKUnit).SingleOrDefaultAsync(m => m.Id == id);
            materialPurchaseOrderVM.MaterialPurchaseOrder = await _db.materialPurchaseOrders.SingleOrDefaultAsync(m => m.Id == id);
            materialPurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKDeliveryTo = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            materialPurchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).OrderBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();
            materialPurchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();

            return View(materialPurchaseOrderVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, MaterialPurchaseOrderViewModel model)
        {
            var mPOfromDb = await _db.materialPurchaseOrders.FindAsync(id);

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            mPOfromDb.FKOrderStatus = model.MaterialPurchaseOrder.FKOrderStatus;
            mPOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            mPOfromDb.Remarks = model.MaterialPurchaseOrder.Remarks;
            mPOfromDb.DeleteBy = model.MaterialPurchaseOrder.DeleteBy;
            mPOfromDb.DeletedDate = model.MaterialPurchaseOrder.DeletedDate;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //TODO: ORDER'S DTLS TO BE DONE'
        }


        public async Task<IActionResult> MPODetailIndex(int Id)
        {
            var mpo = await _db.materialPurchaseOrders.FindAsync(Id);
            TempData["PurchaseOrder"] = mpo;
            nFKPO = mpo.Id;
            sPurchaseOrderNo = mpo.PurchaseOrderNo;

            return View(await _db.materialPurchaseOrderDetails.Where(x => x.FKPurchaseOrder == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> MPODetailCreate(int Id)
        {
            var mpo = await _db.materialPurchaseOrders.FindAsync(Id);
            TempData["PurchaseOrder"] = mpo;

            materialPurchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderDetailVM.MaterialId = await _db.materials.OrderBy(c => c.Description).Where(c => c.IsActive == true && c.FKCategory == mpo.FKCategory).ToListAsync();
            return View(materialPurchaseOrderDetailVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MPODetailCreate(string SaveDtl,MaterialPurchaseOrderDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(materialPurchaseOrderDetailVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            var materials = await _db.materials.ToListAsync();

            model.MaterialPurchaseOrderDetail.Material = materials.Where(x => x.Id == model.MaterialPurchaseOrderDetail.FKMaterial).FirstOrDefault().Description; ;
            int nFKUOM = materials.Where(x => x.Id == model.MaterialPurchaseOrderDetail.FKMaterial).FirstOrDefault().FKUom;
            model.MaterialPurchaseOrderDetail.UOM = lookUpMaster.Where(x => x.Id == nFKUOM).FirstOrDefault().Description;
            model.MaterialPurchaseOrderDetail.BalanceQuantity = model.MaterialPurchaseOrderDetail.Quantity;
            model.MaterialPurchaseOrderDetail.OrderStatus = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description; ;

            _db.materialPurchaseOrderDetails.Add(model.MaterialPurchaseOrderDetail);
            await _db.SaveChangesAsync();

            decimal npoQty = _db.materialPurchaseOrderDetails.Where(x => x.FKPurchaseOrder == model.MaterialPurchaseOrderDetail.FKPurchaseOrder).Select(x => x.Quantity).ToList().Sum();
            var poFromdb = await _db.materialPurchaseOrders.Where(x => x.Id == model.MaterialPurchaseOrderDetail.FKPurchaseOrder).FirstAsync();
            poFromdb.TotalOrderQty = Convert.ToDecimal(npoQty);
            _db.SaveChanges();


            if (SaveDtl == "Save & Continue")
            {
                return RedirectToAction("MPODetailCreate", "MaterialPurchaseOrder", new { Id = model.MaterialPurchaseOrderDetail.FKPurchaseOrder });
            }
            else
            {
                var mPOfromDb = await _db.materialPurchaseOrders.FindAsync(model.MaterialPurchaseOrderDetail.FKPurchaseOrder);
                mPOfromDb.IsEntryCompleted = true;
                mPOfromDb.ModifiedDate = DateTime.Now;
                await _db.SaveChangesAsync();

                return RedirectToAction("MPODetailIndex", "MaterialPurchaseOrder", new { Id = model.MaterialPurchaseOrderDetail.FKPurchaseOrder });
            }
        }

      
        //GET - EDIT
        public async Task<IActionResult> MPODetailEdit(int Id)
        {
            var mpo = await _db.materialPurchaseOrders.FindAsync(nFKPO);
            TempData["PurchaseOrder"] = mpo;

            if (Id == null)
            {
                return NotFound();
            }

            materialPurchaseOrderDetailVM.MaterialPurchaseOrderDetail = await _db.materialPurchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);
            materialPurchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            materialPurchaseOrderDetailVM.MaterialId = await _db.materials.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            return View(materialPurchaseOrderDetailVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MPODetailEdit(int id, MaterialPurchaseOrderDetailViewModel model)
        {
            var mPODtlfromDb = await _db.materialPurchaseOrderDetails.FindAsync(id);

            mPODtlfromDb.OrderReferenceNo = model.MaterialPurchaseOrderDetail.OrderReferenceNo;
            mPODtlfromDb.FKMaterial = model.MaterialPurchaseOrderDetail.FKMaterial;
            mPODtlfromDb.Material = model.MaterialPurchaseOrderDetail.Material;
            mPODtlfromDb.UOM = model.MaterialPurchaseOrderDetail.UOM;
            mPODtlfromDb.Quantity = model.MaterialPurchaseOrderDetail.Quantity;
            mPODtlfromDb.Rate = model.MaterialPurchaseOrderDetail.Rate;
            mPODtlfromDb.Value = model.MaterialPurchaseOrderDetail.Value;
            mPODtlfromDb.DeliveryDate = model.MaterialPurchaseOrderDetail.DeliveryDate;
            mPODtlfromDb.IsPartDeliveryAllowed = model.MaterialPurchaseOrderDetail.IsPartDeliveryAllowed;
            mPODtlfromDb.ModifiedBy = model.MaterialPurchaseOrderDetail.ModifiedBy;
            mPODtlfromDb.ModifiedDate = model.MaterialPurchaseOrderDetail.ModifiedDate;

            await _db.SaveChangesAsync();
            return RedirectToAction("MPODetailIndex", "MaterialPurchaseOrder", new { Id = nFKPO });
        }

        //GET - CANCEL
        public async Task<IActionResult> MPODetailCancel(int Id)
        {
            var mpo = await _db.materialPurchaseOrders.FindAsync(nFKPO);
            TempData["PurchaseOrder"] = mpo;

            if (Id == null)
            {
                return NotFound();
            }

            materialPurchaseOrderDetailVM.MaterialPurchaseOrderDetail = await _db.materialPurchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);
            materialPurchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();
            materialPurchaseOrderDetailVM.MaterialId = await _db.materials.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            return View(materialPurchaseOrderDetailVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MPODetailCancel(int id, MaterialPurchaseOrderDetailViewModel model)
        {
            var mPODtlfromDb = await _db.materialPurchaseOrderDetails.FindAsync(id);
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            mPODtlfromDb.FKOrderStatus = model.MaterialPurchaseOrderDetail.FKOrderStatus;
            mPODtlfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;
            mPODtlfromDb.ModifiedBy = model.MaterialPurchaseOrderDetail.ModifiedBy;
            mPODtlfromDb.ModifiedDate = model.MaterialPurchaseOrderDetail.ModifiedDate;

            await _db.SaveChangesAsync();

            if (_db.materialPurchaseOrderDetails.Where(x => x.FKOrderStatus != model.MaterialPurchaseOrderDetail.FKOrderStatus).ToList().Count == 0)
            {
                var mPOfromDb = await _db.materialPurchaseOrders.FindAsync(nFKPO);

                mPOfromDb.FKOrderStatus = model.MaterialPurchaseOrderDetail.FKOrderStatus;
                mPOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.MaterialPurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;
                mPOfromDb.ModifiedBy = model.MaterialPurchaseOrderDetail.ModifiedBy;
                mPOfromDb.ModifiedDate = model.MaterialPurchaseOrderDetail.ModifiedDate;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("MPODetailIndex", "MaterialPurchaseOrder", new { Id = nFKPO });
        }
    }
}
