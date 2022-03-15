using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using OptimizerBeta3.Models.ViewModels.ReportsViewModel;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class LGPurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public LGPurchaseOrderViewModel LGpurchaseOrderVM { get; set; }
        public LGPurchaseOrderMainViewModel LGpurchaseOrderMainVM { get; set; }
        public LGPurchaseOrderDetailViewModel LGpurchaseOrderDetailVM { get; set; }

        public LGPurchaseOrderController(ApplicationDbContext db)
        {
            _db = db;
            LGpurchaseOrderVM = new LGPurchaseOrderViewModel()
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
                FKCategory = _db.lookUpMasters,
                LGPurchaseOrder = new Models.TransactionTables.LGPurchaseOrder()
            };

            LGpurchaseOrderMainVM = new LGPurchaseOrderMainViewModel()
            {
                FKModeofTransport = _db.lookUpMasters,
                FKDeliveryTo = _db.lookUpMasters,
                GetLeatherGoodsGroup = _db.LeatherGoodsGroups,
                LGPurchaseOrderMain = new Models.TransactionTables.LGPurchaseOrderMain()
            };

            LGpurchaseOrderDetailVM = new LGPurchaseOrderDetailViewModel()
            {
                GetLeatherGoodsDetail = _db.leatherGoodsDetails,
                FKOrderStatus = _db.lookUpMasters,
                FKLGSize = _db.SizeMasterforLeatherGoods,
                FKUom = _db.lookUpMasters,
                LGPurchaseOrderDetail = new Models.TransactionTables.LGPurchaseOrderDetails()
            };
        }

        #region PURCHASE ORDER
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.LGPurchaseOrders.OrderByDescending(s => s.Id).Where(x => x.PurchaseOrderDt >= effectStartDate && x.PurchaseOrderDt <= effectEndDate && x.FLAM == "L").ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "LGPurchaseOrder", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            LGpurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();

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

            LGpurchaseOrderVM.FKParty = suppliers.ToList();

            //LGpurchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();

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
            LGpurchaseOrderVM.FKTypeOfOrder = lmTypeofOrders.ToList();

            //LGpurchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();

            List<LookUpMaster> lkms = new List<LookUpMaster>();
            string sqlQuery2 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='24'";
            var cmd2 = _db.Database.GetDbConnection().CreateCommand();
            cmd2.CommandText = sqlQuery2;
            _db.Database.OpenConnection();

            var result2 = cmd2.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result2.Read())
            {
                LookUpMaster lkm = new LookUpMaster
                {
                    Id = (int)result2["Id"],
                    Description = result2["Description"].ToString()
                };
                lkms.Add(lkm);
            }

            LGpurchaseOrderVM.FKCategory = lkms.ToList();

            //LGpurchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            return View(LGpurchaseOrderVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string Save)
        {
            if (!ModelState.IsValid)
            {
                return View(LGpurchaseOrderVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sTypeofOrder = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            string sSource = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKSource).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(LGpurchaseOrderVM.LGPurchaseOrder.FKSeason);
            string sSeason = season.Code;

            var unitInfo = await _db.unitMasters.ToListAsync();
            //LGpurchaseOrderVM.PurchaseOrder.DeliveryTo = companyInfo.Where(x => x.Id == LGpurchaseOrderVM.PurchaseOrder.FKDeliveryTo).FirstOrDefault().CompanyName;
            LGpurchaseOrderVM.LGPurchaseOrder.UnitName = unitInfo.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKUnit).FirstOrDefault().CompanyName;
            string sUnitCode = unitInfo.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKUnit).FirstOrDefault().Code;
            string sFLAM = LGpurchaseOrderVM.LGPurchaseOrder.FLAM;

            string codechar = (sUnitCode.Substring(0, 3) + sFLAM.Substring(0, 1) + sTypeofOrder.Substring(0, 2) + sSeason.Substring(0, 4) + sSource.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.LGPurchaseOrders.Where(x => x.PurchaseOrderNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.LGPurchaseOrders.Where(x => x.PurchaseOrderNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderNo.Substring(12, 4))).ToList().Max();
            }

            LGpurchaseOrderVM.LGPurchaseOrder.PurchaseOrderNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));


            LGpurchaseOrderVM.LGPurchaseOrder.Currency = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKCurrency).FirstOrDefault().Description;
            //LGpurchaseOrderVM.PurchaseOrder.ModeofTransport = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.PurchaseOrder.FKModeofTransport).FirstOrDefault().Description;
            LGpurchaseOrderVM.LGPurchaseOrder.OrderStatus = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            LGpurchaseOrderVM.LGPurchaseOrder.PaymentTerms = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKPaymentTerms).FirstOrDefault().Description;
            LGpurchaseOrderVM.LGPurchaseOrder.Source = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKSource).FirstOrDefault().Description;
            LGpurchaseOrderVM.LGPurchaseOrder.TypeofOrder = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;

            LGpurchaseOrderVM.LGPurchaseOrder.Season = season.Code;
            var partyInfo = await _db.partyInfos.ToListAsync();
            LGpurchaseOrderVM.LGPurchaseOrder.SupplierName = partyInfo.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKParty).FirstOrDefault().CompanyName; ;
            LGpurchaseOrderVM.LGPurchaseOrder.Category = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKCategory).FirstOrDefault().Description;

            _db.LGPurchaseOrders.Add(LGpurchaseOrderVM.LGPurchaseOrder);
            await _db.SaveChangesAsync();
            
            if (Save == "Save & New Po")
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                var po = await _db.LGPurchaseOrders.Where(x => x.PurchaseOrderNo == LGpurchaseOrderVM.LGPurchaseOrder.PurchaseOrderNo).FirstOrDefaultAsync();
                return RedirectToAction("POMainCreate", "LGPurchaseOrder", new { Id = po.Id });
            }
        }

        //GET - CREATE
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LGpurchaseOrderVM.LGPurchaseOrder = await _db.LGPurchaseOrders.SingleOrDefaultAsync(m => m.Id == id);
            LGpurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            LGpurchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            return View(LGpurchaseOrderVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LGPurchaseOrderViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            //string sTypeofOrder = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            //string sSource = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKSource).FirstOrDefault().Description;


            var pOfromDb = await _db.LGPurchaseOrders.FindAsync(id);

            pOfromDb.FKOrderStatus = model.LGPurchaseOrder.FKOrderStatus;
            pOfromDb.FKPaymentTerms = model.LGPurchaseOrder.FKPaymentTerms;
            pOfromDb.FKCurrency = model.LGPurchaseOrder.FKCurrency;
            //pOfromDb.FKModeofTransport = model.PurchaseOrder.FKModeofTransport;
            pOfromDb.Remarks = model.LGPurchaseOrder.Remarks;
            //pOfromDb.FKDeliveryTo = model.PurchaseOrder.FKDeliveryTo;
            //var companyInfo = await _db.companyInfos.ToListAsync();
            //pOfromDb.DeliveryTo = companyInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKDeliveryTo).FirstOrDefault().CompanyName;
            //pOfromDb.DeliveryTo = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKDeliveryTo).FirstOrDefault().Description;
            //pOfromDb.ModeofTransport = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKModeofTransport).FirstOrDefault().Description;
            pOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            pOfromDb.PaymentTerms = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKPaymentTerms).FirstOrDefault().Description;
            pOfromDb.Currency = lookUpMaster.Where(x => x.Id == LGpurchaseOrderVM.LGPurchaseOrder.FKCurrency).FirstOrDefault().Description;
            pOfromDb.ModifiedBy = model.LGPurchaseOrder.ModifiedBy;
            pOfromDb.ModifiedDate = model.LGPurchaseOrder.ModifiedDate;

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
            LGpurchaseOrderVM.LGPurchaseOrder = await _db.LGPurchaseOrders.SingleOrDefaultAsync(m => m.Id == id);
            LGpurchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            LGpurchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            return View(LGpurchaseOrderVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, LGPurchaseOrderViewModel model)
        {
            var pOfromDb = await _db.LGPurchaseOrders.FindAsync(id);

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            pOfromDb.FKOrderStatus = model.LGPurchaseOrder.FKOrderStatus;
            pOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            pOfromDb.Remarks = model.LGPurchaseOrder.Remarks;
            pOfromDb.DeleteBy = model.LGPurchaseOrder.DeleteBy;
            pOfromDb.DeletedDate = model.LGPurchaseOrder.DeletedDate;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //TODO: ORDER'S DTLS TO BE DONE'
        }

        //GET - POPRINT
        public async Task<IActionResult> POPrint(int Id)
        {
            List<PurchaseOrderReportViewModel> reports = _db.purchaseOrders.Where(p => p.Id == Id).Join(_db.purchaseOrderMains, PO => PO.Id, POM => POM.FKPurchaseOrder, (PO, POM) => new
            { PO, POM }).Join(_db.purchaseOrderDetails, POPOM => POPOM.POM.Id, POD => POD.FKPurchaseOrderMain, (POPOM, POD) => new
            { POPOM, POD }).Join(_db.articleDetails, POD => POD.POD.FKArticle, AD => AD.Id, (POD, AD) => new PurchaseOrderReportViewModel
            {
                POPurchaseOrderNo = POD.POPOM.PO.PurchaseOrderNo,
                POPurchaseOrderDt = POD.POPOM.PO.PurchaseOrderDt,
                POSeason = POD.POPOM.PO.Season,
                POSource = POD.POPOM.PO.Source,
                POSupplierName = POD.POPOM.PO.SupplierName,
                POUnitName = POD.POPOM.PO.UnitName,
                POOrderStatus = POD.POPOM.PO.OrderStatus,
                POPaymentTerms = POD.POPOM.PO.PaymentTerms,
                POMArticleGroup = POD.POPOM.POM.ArticleGroup,
                POMArticle = POD.POPOM.POM.Article,
                // POD.Id,
                //PODArticleDescription = POD.FKArticle,
                PODArticleDescription = POD.POD.ArticleDescription,
                PODArticleColor = POD.POD.ArticleColor,
                PODOrderReferenceNo = POD.POD.OrderReferenceNo,
                PODTotalOrderQuantity = POD.POD.TotalOrderQuantity,
                PODSize01 = POD.POD.Size01,
                PODQuantity01 = POD.POD.Quantity01,
                PODSize02 = POD.POD.Size02,
                PODQuantity02 = POD.POD.Quantity02,
                PODSize03 = POD.POD.Size03,
                PODQuantity03 = POD.POD.Quantity03,
                PODSize04 = POD.POD.Size04,
                PODQuantity04 = POD.POD.Quantity04,
                PODSize05 = POD.POD.Size05,
                PODQuantity05 = POD.POD.Quantity05,
                PODSize06 = POD.POD.Size06,
                PODQuantity06 = POD.POD.Quantity06,
                PODSize07 = POD.POD.Size07,
                PODQuantity07 = POD.POD.Quantity07,
                PODSize08 = POD.POD.Size08,
                PODQuantity08 = POD.POD.Quantity08,
                PODSize09 = POD.POD.Size09,
                PODQuantity09 = POD.POD.Quantity09,
                PODSize10 = POD.POD.Size10,
                PODQuantity10 = POD.POD.Quantity10,
                PODSize11 = POD.POD.Size11,
                PODQuantity11 = POD.POD.Quantity11,
                PODSize12 = POD.POD.Size12,
                PODQuantity12 = POD.POD.Quantity12,
                PODSize13 = POD.POD.Size13,
                PODQuantity13 = POD.POD.Quantity13,
                PODSize14 = POD.POD.Size14,
                PODQuantity14 = POD.POD.Quantity14,
                PODSize15 = POD.POD.Size15,
                PODQuantity15 = POD.POD.Quantity15,
                PODSize16 = POD.POD.Size16,
                PODQuantity16 = POD.POD.Quantity16,
                PODSize17 = POD.POD.Size17,
                PODQuantity17 = POD.POD.Quantity17,
                PODSize18 = POD.POD.Size18,
                PODQuantity18 = POD.POD.Quantity18,
                PODPurchaseOrderDtlNo = POD.POD.PurchaseOrderDtlNo,

                ARTDescription = AD.Description
            }).ToList();

            return View(reports);
        }

        //GET - POPRINT
        public async Task<IActionResult> GenStck(int Id)
        {
            var pOfromDb = await _db.LGPurchaseOrders.FindAsync(Id);

            int nRowCount = _db.stockWithArticles.Where(x => x.FKOrderId == Id).ToList().Count;
            if (nRowCount == 0)
            {
                List<StockWithArticle> stockWithArticles = new List<StockWithArticle>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='GENLGSTCK', @mPurchaseOrderNo='{pOfromDb.PurchaseOrderNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (result.Read())
                {
                    var swa = new StockWithArticle
                    {
                        //Id = 0,
                        StockNo = result.GetString(0),
                        Brand = result.GetString(1),
                        Product = result.GetString(2),
                        ArticleNo = result.GetString(3),
                        ArticleDescription = result.GetString(4),
                        Variant = result.GetString(5),
                        ColorDescription = result.GetString(6),
                        Size = result.GetDecimal(7),
                        ItemDescription = result.GetString(8),
                        ArticleName = result.GetString(9),
                        LeatherType = result.GetString(10),
                        Group = result.GetString(11),
                        Dept = result.GetString(12),
                        EANCode = result.GetString(13),
                        Type = result.GetString(14),
                        Category = result.GetString(15),
                        Vendor = result.GetString(16),
                        DOM = result.GetDateTime(17),
                        SICM = result.GetString(18),
                        MRP = result.GetDecimal(19),
                        DealerPrice = result.GetDecimal(20),
                        CostPrice = result.GetDecimal(21),
                        ProductTax = result.GetDecimal(22),
                        Quantity = result.GetInt32(23),
                        ArrivedQty = result.GetInt32(24),
                        FKArticleDetailId = result.GetInt32(25),
                        FKOrderDetailId = result.GetInt32(26),
                        BalQty = result.GetInt32(27),
                        SoldQty = result.GetInt32(28),
                        FKOffer = result.GetInt32(29),
                        OfferType = result.GetString(30),
                        FKCategory = result.GetInt32(31),
                        FKOrderId = result.GetInt32(32),
                        FKOrderMainId = result.GetInt32(33),
                        FLAM = "L",
                        SizeinString = result.GetString(34),
                        FKSupplier = result.GetInt32(35)
                    };
                    stockWithArticles.Add(swa);
                }

                result.Close();

                _db.stockWithArticles.AddRange(stockWithArticles);
                await _db.SaveChangesAsync();

                pOfromDb.IsArticleStockGenerated = true;
                pOfromDb.ModifiedDate = DateTime.Now;
                await _db.SaveChangesAsync();

                //_db.LGPurchaseOrders.Add(LGpurchaseOrderVM.LGPurchaseOrder);
                //await _db.SaveChangesAsync();

                
            }
            var stkwitharticle = await _db.stockWithArticles.Where(x => x.FKOrderId == Id).ToListAsync();
            return View(stkwitharticle);
        }
        #endregion

        #region PURCHASE ORDER MAIN
        public async Task<IActionResult> POMainIndex(int Id)
        {
            var po = await _db.LGPurchaseOrders.FindAsync(Id);
            TempData["LGPurchaseOrder"] = po;

            return View(await _db.LGPurchaseOrderMains.Where(x => x.FKPurchaseOrder == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> POMainCreate(int Id)
        {
            var po = await _db.LGPurchaseOrders.FindAsync(Id);
            TempData["LGPurchaseOrder"] = po;

            LGpurchaseOrderMainVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderMainVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderMainVM.GetLeatherGoodsGroup = await _db.LeatherGoodsGroups.OrderBy(c => c.Description).Where(c => c.IsActive && c.FKCategory == po.FKCategory).ToListAsync();
            LGpurchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderMainVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            return View(LGpurchaseOrderMainVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> POMainCreate(string Save, LGPurchaseOrderMainViewModel LGpurchaseOrderMainVM)
        {
            if (!ModelState.IsValid)
            {
                return View(LGpurchaseOrderMainVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string codechar = (LGpurchaseOrderMainVM.LGPurchaseOrderMain.PurchaseOrderNo).ToUpper();
            var maxcode = 0;

            if (_db.LGPurchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.LGPurchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderMainNo.Substring(18, 2))).ToList().Max();
            }

            LGpurchaseOrderMainVM.LGPurchaseOrderMain.PurchaseOrderMainNo = codechar + "-" + String.Format("{0:00}", (maxcode + 1));


            var LeatherGoodsgroup = await _db.LeatherGoodsGroups.ToListAsync();
            int nFKArticleGroup = LeatherGoodsgroup.Where(x => x.Id == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKLeatherGoodsGroup).FirstOrDefault().FKGroup;
            LGpurchaseOrderMainVM.LGPurchaseOrderMain.Article = LeatherGoodsgroup.Where(x => x.Id == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKLeatherGoodsGroup).FirstOrDefault().ArticleName;
            LGpurchaseOrderMainVM.LGPurchaseOrderMain.LeatherGoodsGroup = lookUpMaster.Where(x => x.Id == nFKArticleGroup).FirstOrDefault().Description;
            LGpurchaseOrderMainVM.LGPurchaseOrderMain.OrderStatus = lookUpMaster.Where(x => x.Id == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;
            LGpurchaseOrderMainVM.LGPurchaseOrderMain.ModeofTransport = lookUpMaster.Where(x => x.Id == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKModeofTransport).FirstOrDefault().Description;
            LGpurchaseOrderMainVM.LGPurchaseOrderMain.DeliveryTo = lookUpMaster.Where(x => x.Id == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKDeliveryTo).FirstOrDefault().Description;
            LGpurchaseOrderMainVM.LGPurchaseOrderMain.Destination = lookUpMaster.Where(x => x.Id == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKDestination).FirstOrDefault().Description;

            _db.LGPurchaseOrderMains.Add(LGpurchaseOrderMainVM.LGPurchaseOrderMain);
            await _db.SaveChangesAsync();

            var pOfromDb = await _db.LGPurchaseOrders.FindAsync(LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKPurchaseOrder);
            pOfromDb.MainEnteredQuantity = (int?)_db.LGPurchaseOrderMains.Where(x => x.FKPurchaseOrder == LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKPurchaseOrder).Sum(x => x.TotalOrderQuantity);
            await _db.SaveChangesAsync();


            if (Save == "Save & Continue")
            {
                if (pOfromDb.MainEnteredQuantity < pOfromDb.TotalOrderQuantity)
                {
                    return RedirectToAction("POMainCreate", "LGPurchaseOrder", new { Id = LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKPurchaseOrder });
                }
                else
                {
                    return RedirectToAction("POMainIndex", "LGPurchaseOrder", new { Id = LGpurchaseOrderMainVM.LGPurchaseOrderMain.FKPurchaseOrder });
                }
            }
            else
            {
                var po = await _db.LGPurchaseOrderMains.Where(x => x.PurchaseOrderMainNo == LGpurchaseOrderMainVM.LGPurchaseOrderMain.PurchaseOrderMainNo).FirstOrDefaultAsync();
                return RedirectToAction("PODtlCreate", "LGPurchaseOrder", new { Id = po.Id });
            }

        }

        //GET - EDIT
        public async Task<IActionResult> POMainEdit(int Id)
        {
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.LGPurchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            
            LGpurchaseOrderMainVM.LGPurchaseOrderMain = await _db.LGPurchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);
            LGpurchaseOrderMainVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderMainVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderMainVM.GetLeatherGoodsGroup = await _db.LeatherGoodsGroups.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            LGpurchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderMainVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            return View(LGpurchaseOrderMainVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> POMainEdit(int id, LGPurchaseOrderMainViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var pOMainfromDb = await _db.LGPurchaseOrderMains.FindAsync(id);

            pOMainfromDb.FKOrderStatus = model.LGPurchaseOrderMain.FKOrderStatus;
            pOMainfromDb.FKModeofTransport = model.LGPurchaseOrderMain.FKModeofTransport;
            pOMainfromDb.FKDeliveryTo = model.LGPurchaseOrderMain.FKDeliveryTo;
            pOMainfromDb.FKDestination = model.LGPurchaseOrderMain.FKDestination;

            pOMainfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;
            pOMainfromDb.ModeofTransport = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderMain.FKModeofTransport).FirstOrDefault().Description;
            pOMainfromDb.DeliveryTo = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderMain.FKDeliveryTo).FirstOrDefault().Description;
            pOMainfromDb.Destination = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderMain.FKDestination).FirstOrDefault().Description;

            pOMainfromDb.OrderReferenceNo = model.LGPurchaseOrderMain.OrderReferenceNo;
            pOMainfromDb.TotalOrderQuantity = model.LGPurchaseOrderMain.TotalOrderQuantity;
            pOMainfromDb.DeliveryDate = model.LGPurchaseOrderMain.DeliveryDate;
            pOMainfromDb.IsPartDeliveryAllowed = model.LGPurchaseOrderMain.IsPartDeliveryAllowed;
            pOMainfromDb.ModifiedBy = model.LGPurchaseOrderMain.ModifiedBy;
            pOMainfromDb.ModifiedDate = model.LGPurchaseOrderMain.ModifiedDate;

            await _db.SaveChangesAsync();

            var pOfromDb = await _db.LGPurchaseOrders.FindAsync(model.LGPurchaseOrderMain.FKPurchaseOrder);
            pOfromDb.MainEnteredQuantity = (int?)_db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == model.LGPurchaseOrderMain.FKPurchaseOrder).Sum(x => x.TotalOrderQuantity);
            await _db.SaveChangesAsync();
            return RedirectToAction("POMainIndex", "LGPurchaseOrder", new { Id = model.LGPurchaseOrderMain.FKPurchaseOrder });

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
        public async Task<IActionResult> POMainCancel(int Id)
        {
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            
            LGpurchaseOrderMainVM.LGPurchaseOrderMain = await _db.LGPurchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);
            LGpurchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();


            return View(LGpurchaseOrderMainVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> POMainCancel(int id, LGPurchaseOrderMainViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var pOMainfromDb = await _db.LGPurchaseOrderMains.FindAsync(id);

            pOMainfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;
            pOMainfromDb.DeleteBy = model.LGPurchaseOrderMain.DeleteBy;
            pOMainfromDb.DeletedDate = model.LGPurchaseOrderMain.DeletedDate;
            await _db.SaveChangesAsync();

            if (_db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == model.LGPurchaseOrderMain.FKPurchaseOrder && x.OrderStatus != "Cancel").ToList().Count == 0)
            {
                var pOfromDb = await _db.purchaseOrders.FindAsync(model.LGPurchaseOrderMain.FKPurchaseOrder);
                pOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;

                pOfromDb.DeleteBy = model.LGPurchaseOrderMain.DeleteBy;
                pOfromDb.DeletedDate = model.LGPurchaseOrderMain.DeletedDate;

                await _db.SaveChangesAsync();
            }



            return RedirectToAction("POMainIndex", "LGPurchaseOrder", new { Id = model.LGPurchaseOrderMain.FKPurchaseOrder });



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
        //public async Task<IActionResult> POMainDetail(int Id)
        //{
        //    var po = await _db.purchaseOrders.FindAsync(nFKPO);
        //    TempData["PurchaseOrder"] = po;

        //    purchaseOrderMainVM.PurchaseOrderMain = await _db.purchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);

        //    return View(purchaseOrderMainVM);
        //}

        #endregion

        #region PURCHASE ORDER DETAIL
        public async Task<IActionResult> PODetailIndex(int Id)
        {
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(Id);
            TempData["LGPurchaseOrderMain"] = poMain;

            var po = await _db.LGPurchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["LGPurchaseOrder"] = po;

            return View(await _db.LGPurchaseOrderDetails.Where(x => x.FKPurchaseOrderMain == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> PODtlCreate(int Id)
        {
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(Id);
            TempData["LGPurchaseOrderMain"] = poMain;
            var po = await _db.LGPurchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["LGPurchaseOrder"] = po;

            LGpurchaseOrderDetailVM.GetLeatherGoodsDetail = await _db.leatherGoodsDetails.OrderBy(c => c.Description).Where(c => c.FKArticleGroup == poMain.FKLeatherGoodsGroup && c.IsActive).ToListAsync();
            LGpurchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            LGpurchaseOrderDetailVM.FKLGSize = await _db.SizeMasterforLeatherGoods.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            LGpurchaseOrderDetailVM.FKUom = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 29 && c.IsActive == true).ToListAsync();

            return View(LGpurchaseOrderDetailVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PODtlCreate(string SaveDtl, LGPurchaseOrderDetailViewModel purchaseOrderDetailVM)
        {
            if (!ModelState.IsValid)
            {
                return View(purchaseOrderDetailVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string codechar = (purchaseOrderDetailVM.LGPurchaseOrderDetail.PurchaseOrderMainNo).ToUpper();
            var maxcode = 0;

            if (_db.LGPurchaseOrderDetails.Where(x => x.PurchaseOrderDtlNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.LGPurchaseOrderDetails.Where(x => x.PurchaseOrderDtlNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderDtlNo.Substring(21, 3))).ToList().Max();
            }

            purchaseOrderDetailVM.LGPurchaseOrderDetail.PurchaseOrderDtlNo = codechar + "-" + String.Format("{0:000}", (maxcode + 1));


            var LGdetail = await _db.leatherGoodsDetails.ToListAsync();
            purchaseOrderDetailVM.LGPurchaseOrderDetail.LeatherGoodsDescription = LGdetail.Where(x => x.Id == purchaseOrderDetailVM.LGPurchaseOrderDetail.FKLeatherGoods).FirstOrDefault().Description;
            purchaseOrderDetailVM.LGPurchaseOrderDetail.OrderStatus = lookUpMaster.Where(x => x.Id == purchaseOrderDetailVM.LGPurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;

            _db.LGPurchaseOrderDetails.Add(purchaseOrderDetailVM.LGPurchaseOrderDetail);
            await _db.SaveChangesAsync();

            var pOMainfromDb = await _db.LGPurchaseOrderMains.FindAsync(purchaseOrderDetailVM.LGPurchaseOrderDetail.FKPurchaseOrderMain);
            pOMainfromDb.EnteredQuantity = _db.LGPurchaseOrderDetails.Where(x => x.FKPurchaseOrderMain == purchaseOrderDetailVM.LGPurchaseOrderDetail.FKPurchaseOrderMain).Sum(x => x.Quantity);
            await _db.SaveChangesAsync();

            var pOfromDb = await _db.LGPurchaseOrders.FindAsync(pOMainfromDb.FKPurchaseOrder);
            pOfromDb.DtlEnteredQuantity = _db.LGPurchaseOrderDetails.Where(x => x.PurchaseOrderNo == purchaseOrderDetailVM.LGPurchaseOrderDetail.PurchaseOrderNo).Sum(x => x.Quantity);
            pOfromDb.POValue = _db.LGPurchaseOrderDetails.Where(x => x.PurchaseOrderNo == purchaseOrderDetailVM.LGPurchaseOrderDetail.PurchaseOrderNo).Sum(x => x.Value);
            await _db.SaveChangesAsync();

            if (SaveDtl == "Save & Continue")
            {
                if (pOMainfromDb.EnteredQuantity < pOMainfromDb.TotalOrderQuantity)
                {
                    return RedirectToAction("PODtlCreate", "LGPurchaseOrder", new { Id = purchaseOrderDetailVM.LGPurchaseOrderDetail.FKPurchaseOrderMain });
                }
                else
                {
                    return RedirectToAction("PODetailIndex", "LGPurchaseOrder", new { Id = purchaseOrderDetailVM.LGPurchaseOrderDetail.FKPurchaseOrderMain });
                }
            }
            else
            {
                return RedirectToAction("PODetailIndex", "LGPurchaseOrder", new { Id = purchaseOrderDetailVM.LGPurchaseOrderDetail.FKPurchaseOrderMain });
            }
        }

        //GET - EDIT
        public async Task<IActionResult> PODtlEdit(int? Id)
        {
            var poDetail = await _db.LGPurchaseOrderDetails.FindAsync(Id);
            TempData["PurchaseOrderDetail"] = poDetail;
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(poDetail.FKPurchaseOrderMain);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.LGPurchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            

            LGpurchaseOrderDetailVM.LGPurchaseOrderDetail = await _db.LGPurchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);
            LGpurchaseOrderDetailVM.GetLeatherGoodsDetail = await _db.leatherGoodsDetails.OrderBy(c => c.Description).Where(c => c.FKArticleGroup == poMain.FKLeatherGoodsGroup && c.IsActive).ToListAsync();
            LGpurchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();

            //purchaseOrderMainVM.PurchaseOrderMain = await _db.purchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);
            //purchaseOrderMainVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            //purchaseOrderMainVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            //purchaseOrderMainVM.FKSizeMaster = await _db.sizeMasters.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            //purchaseOrderMainVM.GetArticleGroup = await _db.articleGroups.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            //purchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            //purchaseOrderMainVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            //return View(purchaseOrderMainVM);

            return View(LGpurchaseOrderDetailVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PODtlEdit(int id, LGPurchaseOrderDetailViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var LGdetail = await _db.leatherGoodsDetails.ToListAsync();

            var pODtlfromDb = await _db.LGPurchaseOrderDetails.FindAsync(id);

            pODtlfromDb.FKOrderStatus = model.LGPurchaseOrderDetail.FKOrderStatus;
            pODtlfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;
            pODtlfromDb.FKLeatherGoods = model.LGPurchaseOrderDetail.FKLeatherGoods;
            //////////pODtlfromDb.LeatherGoodsDescription = LeatherGoodsDetail.Where(x => x.Id == model.LGPurchaseOrderDetail.FKLeatherGoods).FirstOrDefault().Description;
            pODtlfromDb.Quantity = model.LGPurchaseOrderDetail.Quantity;
            pODtlfromDb.Rate = model.LGPurchaseOrderDetail.Rate;
            pODtlfromDb.Value = model.LGPurchaseOrderDetail.Value;
            
            pODtlfromDb.DeliveryDate = model.LGPurchaseOrderDetail.DeliveryDate;
            pODtlfromDb.IsPartDeliveryAllowed = model.LGPurchaseOrderDetail.IsPartDeliveryAllowed;
            pODtlfromDb.ModifiedBy = model.LGPurchaseOrderDetail.ModifiedBy;
            pODtlfromDb.ModifiedDate = model.LGPurchaseOrderDetail.ModifiedDate;
            await _db.SaveChangesAsync();

            var pOMainfromDb = await _db.LGPurchaseOrderMains.FindAsync(model.LGPurchaseOrderDetail.FKPurchaseOrderMain);
            pOMainfromDb.EnteredQuantity = _db.LGPurchaseOrderDetails.Where(x => x.FKPurchaseOrderMain == model.LGPurchaseOrderDetail.FKPurchaseOrderMain).Sum(x => x.Quantity);
            await _db.SaveChangesAsync();

            return RedirectToAction("PODetailIndex", "LGPurchaseOrder", new { Id = model.LGPurchaseOrderDetail.FKPurchaseOrderMain });
        }

        //GET - CANCEL
        public async Task<IActionResult> PODtlCancel(int? Id)
        {
            var poDetail = await _db.LGPurchaseOrderDetails.FindAsync(Id);
            TempData["PurchaseOrderDetail"] = poDetail;
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(poDetail.FKPurchaseOrderMain);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.LGPurchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            
            LGpurchaseOrderDetailVM.LGPurchaseOrderDetail = await _db.LGPurchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);
            LGpurchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();

            return View(LGpurchaseOrderDetailVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PODtlCancel(int id, LGPurchaseOrderDetailViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var pODtlfromDb = await _db.LGPurchaseOrderDetails.FindAsync(id);

            pODtlfromDb.FKOrderStatus = model.LGPurchaseOrderDetail.FKOrderStatus;
            pODtlfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.LGPurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;
            pODtlfromDb.DeleteBy = model.LGPurchaseOrderDetail.DeleteBy;
            pODtlfromDb.DeletedDate = model.LGPurchaseOrderDetail.DeletedDate;
            await _db.SaveChangesAsync();

            return RedirectToAction("PODetailIndex", "LGPurchaseOrder", new { Id = model.LGPurchaseOrderDetail.FKPurchaseOrderMain });
        }

        //GET - DETAIL
        public async Task<IActionResult> PODtlDetail(int? Id)
        {
            var poDetail = await _db.LGPurchaseOrderDetails.FindAsync(Id);
            TempData["PurchaseOrderDetail"] = poDetail;
            var poMain = await _db.LGPurchaseOrderMains.FindAsync(poDetail.FKPurchaseOrderMain);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.LGPurchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;

            LGpurchaseOrderDetailVM.LGPurchaseOrderDetail = await _db.LGPurchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);

            return View(LGpurchaseOrderDetailVM);
        }
        #endregion
    }
}
