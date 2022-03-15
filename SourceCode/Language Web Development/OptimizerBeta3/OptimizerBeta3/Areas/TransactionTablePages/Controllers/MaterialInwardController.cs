
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class MaterialInwardController : Controller
    {
        ApplicationDbContext _db;
        [BindProperty]
        public InwardViewModel inwardVM { get; set; }
        public InwardDetailViewModel inwardDetailVM { get; set; }
        public StocksViewModel StocksVM { get; set; }
        public AllTransactionViewModel AllTransactionVM { get; set; }
        public static int nFKInward;
        public static string sScanningMode, sInwardNo;
        public static string sIpAddress;
        public static string ipaddress = string.Empty;

        public MaterialInwardController(ApplicationDbContext db)
        {
            _db = db;
            inwardVM = new InwardViewModel()
            {
                FKSeason = _db.seasons,
                FKSource = _db.lookUpMasters,
                FKUnit = _db.unitMasters,
                FKParty = _db.partyInfos,
                FKDepartment = _db.lookUpMasters,
                FKPOType = _db.lookUpMasters,
                FKCurrency = _db.lookUpMasters,
                FKDocumentType = _db.lookUpMasters,
                Inward = new Models.TransactionTables.Inward()
            };
            inwardDetailVM = new InwardDetailViewModel()
            {
                Inward = new Models.TransactionTables.Inward(),
                InwardDetails = new Models.TransactionTables.InwardDetails()
            };

            StocksVM = new StocksViewModel()
            {
                Stock = new Models.TransactionTables.Stock()
            };

            AllTransactionVM = new AllTransactionViewModel()
            {
                AllTransaction = new Models.TransactionTables.AllTransaction()
            };
        }
      
        #region INWARD
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            IPAddress ip = Request.HttpContext.Connection.RemoteIpAddress;
            ipaddress = string.Empty;
            if (ip != null)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    ip = Dns.GetHostEntry(ip).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                ipaddress = ip.ToString();
            }
            //return View(await _db.inwards.ToListAsync());
            return View(await _db.inwards.OrderByDescending(x => x.Id).Where(x => x.InwardDt >= effectStartDate && x.InwardDt <= effectEndDate && (x.FLAM == "A" || x.FLAM == "M")).ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "MaterialInward", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            inwardVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            inwardVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();

            List<PartyInfo> suppliers = new List<PartyInfo>();
            string sqlQuery = $"EXEC SLI_Filters @mAction='SELSUPPININW', @mControllerName='{controllerName}', @mActionMethod='{actionName}'";
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

            inwardVM.FKParty = suppliers.ToList();

            //inwardVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            inwardVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            inwardVM.FKDocumentType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 43 && c.IsActive == true).ToListAsync();
            inwardVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            inwardVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            inwardVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            //inwardVM.FKLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();

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

            inwardVM.FKCategory = lkms.ToList();

            return View(inwardVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string Save)
        {
            if (!ModelState.IsValid)
            {
                return View(inwardVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sSource = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKSource).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(inwardVM.Inward.FKSeason);
            string sSeason = season.Code;
            var locationInfo = await _db.locations.ToListAsync();
            string sLocationCode = locationInfo.Where(x => x.Id == inwardVM.Inward.FKLocation).FirstOrDefault().Code;

            var companyInfo = await _db.companyInfos.ToListAsync();
            inwardVM.Inward.UnitName = companyInfo.Where(x => x.Id == inwardVM.Inward.FKUnit).FirstOrDefault().CompanyName;
            string sUnitCode = companyInfo.Where(x => x.Id == inwardVM.Inward.FKUnit).FirstOrDefault().Code;
            inwardVM.Inward.POType = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKPOType).FirstOrDefault().Description;
            inwardVM.Inward.Category = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKCategory).FirstOrDefault().Description;
            inwardVM.Inward.FLAM = inwardVM.Inward.Category.ToString().Substring(0, 1).ToUpper();
            string sFLAM = inwardVM.Inward.FLAM;

            string codechar = (sUnitCode.Substring(0, 3) + sFLAM.Substring(0, 1) + sLocationCode.Substring(0, 2) + sSeason.Substring(0, 4) + sSource.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.inwards.Where(x => x.InwardNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.inwards.Where(x => x.InwardNo.Contains(codechar)).Select(x => int.Parse(x.InwardNo.Substring(12, 4))).ToList().Max();
            }

            inwardVM.Inward.InwardNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            inwardVM.Inward.Season = sSeason;
            inwardVM.Inward.Currency = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKCurrency).FirstOrDefault().Description;
            inwardVM.Inward.POType = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKPOType).FirstOrDefault().Description;
            inwardVM.Inward.DocumentType = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKDocumentType).FirstOrDefault().Description;
            inwardVM.Inward.Source = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKSource).FirstOrDefault().Description;
            
            inwardVM.Inward.Season = season.Code;
            var partyInfo = await _db.partyInfos.ToListAsync();
            inwardVM.Inward.SupplierName = partyInfo.Where(x => x.Id == inwardVM.Inward.FKParty).FirstOrDefault().CompanyName;
            inwardVM.Inward.Department = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKDepartment).FirstOrDefault().Description;
            
            inwardVM.Inward.Location = locationInfo.Where(x => x.Id == inwardVM.Inward.FKLocation).FirstOrDefault().LocationName;
            _db.inwards.Add(inwardVM.Inward);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
        #endregion

        #region INWARD DETAIL
        public async Task<IActionResult> InwardDetailIndex(int Id)
        {
            var inw = await _db.inwards.FindAsync(Id);
            TempData["Inward"] = inw;
            nFKInward = inw.Id;
            sInwardNo = inw.InwardNo;

            return View(await _db.inwardDetails.Where(x => x.FKInwardNo == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> InwardDetailCreate(int Id)
        {
            var inw = await _db.inwards.FindAsync(Id);
            TempData["Inward"] = inw;

            List<MaterialPurchaseOrderDetails> mpods = new List<MaterialPurchaseOrderDetails>();
            string sqlQuery = $"EXEC SLI_Inwards @mAction='SELMATPENPO', @mPKId='{nFKInward}',@mFKCategory='{inw.FKCategory}'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {
                MaterialPurchaseOrderDetails mpod = new MaterialPurchaseOrderDetails
                {
                    Id = (int)result["Id"],
                    FKPurchaseOrder =(int)result["FKPurchaseOrder"],
                    PurchaseOrderNo = result["PurchaseOrderNo"].ToString(),
                    Material = result["Material"].ToString(),
                    UOM = result["UOM"].ToString(),
                    Quantity = (decimal)result["Quantity"],
                    ReceivedQuantity = (Decimal)result["ReceivedQuantity"],
                    Rate = (decimal)result["Rate"]
                };
                mpods.Add(mpod);
            }
            ViewBag.MatInwardDtls = mpods;
            return View(inwardDetailVM);
        }

        //GET - CREATE
        public async Task<IActionResult> InwDtlsCreate(int Id)
        {
            var inw = await _db.inwards.FindAsync(nFKInward);
            TempData["Inward"] = inw;

            var poDtls = await _db.materialPurchaseOrderDetails.FindAsync(Id);
            ViewBag.PODtls = poDtls;

            return View(inwardDetailVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InwDtlsCreate(int Id,InwardDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(inwardVM);
            }
            
            _db.inwardDetails.Add(model.InwardDetails);
            await _db.SaveChangesAsync();

            decimal nArrivedQty = _db.inwardDetails.Where(x => x.FKInwardNo == model.InwardDetails.FKInwardNo).Select(x => x.Quantity).ToList().Sum();
            var InwardFromdb = await _db.inwards.Where(x => x.Id == model.InwardDetails.FKInwardNo).FirstAsync();
            InwardFromdb.ArrivedQuantity = Convert.ToInt32(nArrivedQty);
            _db.SaveChanges();

            var lookUPMst = await _db.lookUpMasters.Where(x => x.FKLookUpCategory == 36 && x.Description == "Close").FirstAsync();

            decimal npoDtlRcvdQty = _db.inwardDetails.Where(x => x.FKPurchaseOrderDetail == model.InwardDetails.FKPurchaseOrderDetail).Select(x => x.Quantity).ToList().Sum();
            var poDtFromdb = await _db.materialPurchaseOrderDetails.Where(x => x.Id == model.InwardDetails.FKPurchaseOrderDetail).FirstAsync();
            poDtFromdb.ReceivedQuantity = Convert.ToDecimal (npoDtlRcvdQty);
            if (npoDtlRcvdQty >= poDtFromdb.Quantity)
            {
                poDtFromdb.FKOrderStatus = lookUPMst.Id;
                poDtFromdb.OrderStatus = lookUPMst.Description;
            }
            _db.SaveChanges();

            decimal npoQty = _db.inwardDetails.Where(x => x.FKPurchaseOrder == model.InwardDetails.FKPurchaseOrder).Select(x => x.Quantity).ToList().Sum();
            var poFromdb = await _db.materialPurchaseOrders.Where(x => x.Id == model.InwardDetails.FKPurchaseOrder).FirstAsync();
            poFromdb.RecievedQty = Convert.ToDecimal(npoQty);
            if (npoQty >= poFromdb.TotalOrderQty)
            {
                poFromdb.FKOrderStatus = lookUPMst.Id;
                poFromdb.OrderStatus = lookUPMst.Description;
            }
            _db.SaveChanges();

            return RedirectToAction(nameof(Create));
        }

        //POST - CREATE
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> InwDtlsCompleteSave(int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(inwardVM);
            }

            //int inwardId = Convert.ToInt32(Request.Form["InwardId"]);
            //var td = TempData["InwardId"];
            int ninwardId = Convert.ToInt32(TempData["InwardId"]);
            //int nPodId = Convert.ToInt32(TempData["PODId"]);

            var poDetails = await _db.materialPurchaseOrderDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var material = await _db.materials.Where(x => x.Id == poDetails.FKMaterial).FirstOrDefaultAsync();
            var po = await _db.materialPurchaseOrders.Where(x => x.Id == poDetails.FKPurchaseOrder).FirstOrDefaultAsync();
            
            var companyInfo = await _db.unitMasters.Where(x => x.Id == po.FKUnit).FirstOrDefaultAsync();
            int nFromState = companyInfo.FKState;

            var supplierInfo = await _db.partyInfos.Where(x => x.Id == po.FKParty).FirstOrDefaultAsync();
            int nToState = supplierInfo.FKState;

            var hsncode = await _db.HSNCodeMasters.Where(x => x.HSNCode == material.HSNCode).FirstOrDefaultAsync();
            var inward = await _db.inwards.Where(x => x.Id == ninwardId).FirstOrDefaultAsync();
            decimal gstpercentage = Convert.ToDecimal(hsncode.GSTPercentage);

            var inwardDtl = new InwardDetails();
            inwardDtl.FKInwardNo = inward.Id;
            inwardDtl.FKMaterial = poDetails.FKMaterial;
            inwardDtl.FKArticle = 0;
            inwardDtl.Description = poDetails.Material;
            inwardDtl.Colour = "";
            inwardDtl.HSNCode = hsncode.HSNCode;
            inwardDtl.Quantity = poDetails.Quantity;
            inwardDtl.Rate = poDetails.Rate;
            inwardDtl.Value = poDetails.Value;
            inwardDtl.ValueinINR = poDetails.Value;
            inwardDtl.DiscountPercentage = 0;
            inwardDtl.DiscountValue = 0;
            inwardDtl.GrossValue = poDetails.Value;

            if (nFromState == nToState)
            {
                inwardDtl.SGSTPercentage = gstpercentage / 2;
                inwardDtl.CGSTPercentage = gstpercentage / 2;
                inwardDtl.IGSTPercentage = 0;

                inwardDtl.SGSTValue = poDetails.Value * inwardDtl.SGSTPercentage / 100;
                inwardDtl.CGSTValue = poDetails.Value * inwardDtl.CGSTPercentage / 100;
                inwardDtl.IGSTValue = 0;
            }
            else
            {
                inwardDtl.SGSTPercentage = 0;
                inwardDtl.CGSTPercentage = 0;
                inwardDtl.IGSTPercentage = gstpercentage;

                inwardDtl.SGSTValue = 0;
                inwardDtl.CGSTValue = 0;
                inwardDtl.IGSTValue = poDetails.Value * inwardDtl.CGSTPercentage / 100; ;
            }
            inwardDtl.GSTTotalValue = inwardDtl.SGSTValue + inwardDtl.CGSTValue + inwardDtl.IGSTValue;
            inwardDtl.OthersValuePlus = 0;
            inwardDtl.ItemNettValue = inwardDtl.GrossValue + inwardDtl.GSTTotalValue;
            inwardDtl.IsEntryCompleted = true;
            inwardDtl.IsActive = true;
            inwardDtl.InwardDt = inward.InwardDt;
            inwardDtl.InwardNo = inward.InwardNo;
            inwardDtl.FKSupplier = inward.FKParty;
            inwardDtl.FKPurchaseOrder = poDetails.FKPurchaseOrder;
            inwardDtl.FKPurchaseOrderDetail = poDetails.Id;
            inwardDtl.FKPurchaseOrderMain = 0;
            inwardDtl.FLAM = inward.FLAM;
            inwardDtl.StockNo = material.Code;

            _db.inwardDetails.Add(inwardDtl);
            await _db.SaveChangesAsync();

            #region "Quantity Update in Inward"
            decimal nArrivedQty = _db.inwardDetails.Where(x => x.FKInwardNo == inward.Id).Select(x => x.Quantity).ToList().Sum();
            var InwardFromdb = await _db.inwards.Where(x => x.Id == inward.Id).FirstAsync();
            InwardFromdb.ArrivedQuantity = Convert.ToInt32(nArrivedQty);
            _db.SaveChanges();
            #endregion

            var lookUPMst = await _db.lookUpMasters.Where(x => x.FKLookUpCategory == 36 && x.Description == "Close").FirstAsync();

            #region "Quantity Update in Purchase Order Detail"
            decimal npoDtlRcvdQty = _db.inwardDetails.Where(x => x.FKPurchaseOrderDetail == poDetails.Id).Select(x => x.Quantity).ToList().Sum();
            var poDtFromdb = await _db.materialPurchaseOrderDetails.Where(x => x.Id == poDetails.Id).FirstAsync();
            poDtFromdb.ReceivedQuantity = Convert.ToDecimal(npoDtlRcvdQty);
            poDtFromdb.BalanceQuantity = poDetails.Quantity - (poDetails.CancelledQuantity + Convert.ToDecimal(npoDtlRcvdQty));
            if (npoDtlRcvdQty >= poDtFromdb.Quantity)
            {
                poDtFromdb.FKOrderStatus = lookUPMst.Id;
                poDtFromdb.OrderStatus = lookUPMst.Description;
            }
            _db.SaveChanges();
            #endregion

            #region "Quantity Update in Purchase Order"
            decimal npoQty = _db.inwardDetails.Where(x => x.FKPurchaseOrder == poDetails.FKPurchaseOrder).Select(x => x.Quantity).ToList().Sum();
            var poFromdb = await _db.materialPurchaseOrders.Where(x => x.Id == poDetails.FKPurchaseOrder).FirstAsync();
            poFromdb.RecievedQty = Convert.ToDecimal(npoQty);
            if (npoQty >= poFromdb.TotalOrderQty)
            {
                poFromdb.FKOrderStatus = lookUPMst.Id;
                poFromdb.OrderStatus = lookUPMst.Description;
            }
            _db.SaveChanges();
            #endregion

            int nFKStage, nFKQuality, nFKStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = lookUpMaster.Where(x => x.FKLookUpCategory == 39 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;
            string sInwardNo = "";

            #region "Stock Update"
            int nStckRow = _db.stocks.Where(x => x.FKUnit == inward.FKUnit && x.FKLocation == inward.FKLocation && x.StockNo == material.Code && x.FKSupplier == po.FKParty && x.Rate == poDetails.Rate && x.Quantity > 0).ToList().Count;
            if (nStckRow == 0)
            {
                StocksVM.Stock.Id = 0;
                StocksVM.Stock.MaterialorFinishedProduct = inward.FLAM;
                StocksVM.Stock.FKMaterial = poDetails.FKMaterial;
                StocksVM.Stock.FKArticleDetail = 0;
                StocksVM.Stock.Description = poDetails.Material;
                StocksVM.Stock.Colour = "";
                StocksVM.Stock.Size = "";
                StocksVM.Stock.OrderReferenceNo = "";
                StocksVM.Stock.StockInitiatedDate = DateTime.Now;
                StocksVM.Stock.FKUnit = inward.FKUnit;
                StocksVM.Stock.FKLocation = inward.FKLocation;
                StocksVM.Stock.FKStage = nFKStage;
                StocksVM.Stock.Quantity = poDetails.Quantity;
                StocksVM.Stock.Rate = poDetails.Rate;
                StocksVM.Stock.Value = poDetails.Value;
                StocksVM.Stock.FKCurrency = 1; //TODO : Fetch FKCurrency
                StocksVM.Stock.ExchangeRate = 0; //TODO
                StocksVM.Stock.ValueInINR = poDetails.Value;
                StocksVM.Stock.LandedCost = 0;
                StocksVM.Stock.LandedRate = 0;
                StocksVM.Stock.FKUOM = material.FKUom;
                StocksVM.Stock.FKIIUOM = 0;
                StocksVM.Stock.FKSource = material.FKSource;
                StocksVM.Stock.FKQuality = nFKQuality;
                StocksVM.Stock.FKStatus = nFKStatus;
                StocksVM.Stock.IsActive = true;
                StocksVM.Stock.EnteredSystemId = "";
                StocksVM.Stock.CreatedBy = 1;
                StocksVM.Stock.CreatedDate = DateTime.Now;
                StocksVM.Stock.ModifiedBy = 1;
                StocksVM.Stock.ModifiedDate = DateTime.Now;
                StocksVM.Stock.EANCode = "";
                StocksVM.Stock.StockNo = material.Code;
                StocksVM.Stock.FKSupplier = po.FKParty;
                StocksVM.Stock.FKHSNCode = material.FKHSNCode;
                StocksVM.Stock.DiscountPercentageforSales = 0;
                StocksVM.Stock.MRP = poDetails.Rate;
                StocksVM.Stock.FKOffer = 0;
                StocksVM.Stock.OfferType = "";
                StocksVM.Stock.FKCategory = material.FKCategory;
                StocksVM.Stock.FLAM = inward.FLAM;

                _db.stocks.Add(StocksVM.Stock);
                await _db.SaveChangesAsync();
            }
            else
            {
                var stck = await _db.stocks.Where(x => x.FKUnit == inward.FKUnit && x.FKLocation == inward.FKLocation && x.StockNo == material.Code && x.FKSupplier == po.FKParty && x.Rate == poDetails.Rate && x.Quantity > 0).FirstOrDefaultAsync();
                stck.Quantity = stck.Quantity + poDetails.Quantity;
                decimal rate = stck.Rate;
                decimal value = stck.Quantity * rate;
                stck.Value = value;
                stck.ValueInINR = value;
                stck.LastTranDate = DateTime.Now;
                _db.SaveChanges();
            }
            #endregion

            #region "All Transactions"
            int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == inward.FKUnit && x.FKTranLocation == inward.FKLocation && x.StockNo == material.Code).ToList().Count;
            decimal nClosing;
            if (nRowCount == 0)
            {
                nClosing = 0;
            }
            else
            {
                var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == inward.FKUnit && x.FKTranLocation == inward.FKLocation && x.StockNo == material.Code).FirstOrDefaultAsync();
                nClosing = AllTran.BalanceQuantity;
            }

            AllTransactionVM.AllTransaction.Id = 0;
            AllTransactionVM.AllTransaction.TransactionType = "NEW ARRIVAL";
            AllTransactionVM.AllTransaction.TranId = inward.Id;
            AllTransactionVM.AllTransaction.TranRefNo = inward.InwardNo;
            AllTransactionVM.AllTransaction.TranDate = inward.InwardDt;
            AllTransactionVM.AllTransaction.MaterialorFinishedProduct = inward.FLAM;
            AllTransactionVM.AllTransaction.FKMaterial = poDetails.FKMaterial;
            AllTransactionVM.AllTransaction.FKArticle = 0;
            AllTransactionVM.AllTransaction.Description = material.Description;
            AllTransactionVM.AllTransaction.Colour = "";
            AllTransactionVM.AllTransaction.Size = "";
            AllTransactionVM.AllTransaction.FKFromUnit = po.FKParty;
            AllTransactionVM.AllTransaction.FKFromLocation = 0;
            AllTransactionVM.AllTransaction.FKFromStage = 0;
            AllTransactionVM.AllTransaction.FKToUnit = inward.FKUnit;
            AllTransactionVM.AllTransaction.FKToLocation = inward.FKLocation;
            AllTransactionVM.AllTransaction.FKToStage = nFKStage;
            AllTransactionVM.AllTransaction.FKQuality = nFKQuality;
            AllTransactionVM.AllTransaction.HSNCode = material.HSNCode;
            AllTransactionVM.AllTransaction.FKUOM = material.FKBrand;
            AllTransactionVM.AllTransaction.InwardQuantity = poDetails.Quantity;
            AllTransactionVM.AllTransaction.OutwardQuantity = 0;
            AllTransactionVM.AllTransaction.BalanceQuantity = nClosing + poDetails.Quantity;
            AllTransactionVM.AllTransaction.Rate = poDetails.Rate;
            AllTransactionVM.AllTransaction.Value = poDetails.Value;
            AllTransactionVM.AllTransaction.FKStatus = nFKStage;
            AllTransactionVM.AllTransaction.IsActive = true;
            AllTransactionVM.AllTransaction.CreatedBy = 0;
            AllTransactionVM.AllTransaction.CreatedDate = DateTime.Now;
            AllTransactionVM.AllTransaction.ModifiedBy = 0;
            AllTransactionVM.AllTransaction.FKTranLocation = inward.FKLocation;
            AllTransactionVM.AllTransaction.FKTranUnit = inward.FKUnit;
            AllTransactionVM.AllTransaction.EANCode = "";
            AllTransactionVM.AllTransaction.StockNo = material.Code;

            _db.AllTransactions.Add(AllTransactionVM.AllTransaction);
            await _db.SaveChangesAsync();
            #endregion
            


            return RedirectToAction("InwardDetailCreate", "MaterialInward", new { inward.Id });
        }
        #endregion
    }
}
