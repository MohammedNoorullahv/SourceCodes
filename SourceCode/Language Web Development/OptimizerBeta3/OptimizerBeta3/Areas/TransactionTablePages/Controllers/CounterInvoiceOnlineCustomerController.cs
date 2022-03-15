using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class CounterInvoiceOnlineCustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private string ipaddress;

        [BindProperty]
        public CounterInvoiceViewModel InvoiceVM { get; set; }
        public CounterInvoiceDetailViewModel InvoiceDetailVM { get; set; }
        public StocksViewModel StocksVM { get; set; }
        public AllTransactionViewModel AllTransactionVM { get; set; }
        public CustomerPersonViewModel CustomerPersonVM { get; set; }

        public CounterInvoiceOnlineCustomerController(ApplicationDbContext db)
        {
            _db = db;
            InvoiceVM = new CounterInvoiceViewModel()
            {
                FKTypeOfInvoice = _db.lookUpMasters,
                FKSeason = _db.seasons,
                FKUnit = _db.unitMasters,
                FKPerson = _db.customerPerson,
                FKDeliveryTo = _db.partyInfoDtls,
                FKPaymentTerms = _db.lookUpMasters,
                FKDepartment = _db.lookUpMasters,
                FKCurrency = _db.lookUpMasters,
                FKModeofTransport = _db.lookUpMasters,
                FKLocation = _db.locations,
                FKDestination = _db.lookUpMasters,
                FKCategory = _db.lookUpMasters,
                InvoiceToPerson = new Models.TransactionTables.InvoiceToPerson()

            };

            InvoiceDetailVM = new CounterInvoiceDetailViewModel()
            {
                Invoice = new Models.TransactionTables.InvoiceToPerson(),
                InvoiceDetail = new Models.TransactionTables.InvoiceToPersonDetail()
            };

            StocksVM = new StocksViewModel()
            {
                Stock = new Models.TransactionTables.Stock()
            };

            AllTransactionVM = new AllTransactionViewModel()
            {
                AllTransaction = new Models.TransactionTables.AllTransaction()
            };

            CustomerPersonVM = new CustomerPersonViewModel()
            {
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                FKCountry = _db.lookUpMasters,
                FKGender = _db.lookUpMasters,
                FKMaritalStatus = _db.lookUpMasters,
                CustomerPerson = new Models.MasterTables.CustomerPerson()
            };
        }

        #region "INVOICE"
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

            var delTmpInv = _db.TempInvoiceDtlEANCodes.Where(x => x.IPAddress == ipaddress);
            _db.TempInvoiceDtlEANCodes.RemoveRange(delTmpInv);
            await _db.SaveChangesAsync();

            var delTmpInv1 = _db.TempInvoiceDtls.Where(x => x.IPAddress == ipaddress);
            _db.TempInvoiceDtls.RemoveRange(delTmpInv1);
            await _db.SaveChangesAsync();

            return View(await _db.InvoiceToPersons.OrderByDescending(x => x.Id).Where(x => x.InvoiceDt >= effectStartDate && x.InvoiceDt <= effectEndDate && x.InvoiceTo == "OC").ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "CounterInvoiceOnlineCustomer", new { fromDate = fromDate, toDate = toDate });
        }
        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            InvoiceVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKPerson = await _db.customerPerson.OrderBy(c => c.PersonName).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKDeliveryTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            //InvoiceVM.FKLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive == true).ToListAsync();

            List<LookUpMaster> lkInvCategorys = new List<LookUpMaster>();
            string sqlQuery2 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='56'";
            var cmd2 = _db.Database.GetDbConnection().CreateCommand();
            cmd2.CommandText = sqlQuery2;
            _db.Database.OpenConnection();

            var result2 = cmd2.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result2.Read())
            {
                LookUpMaster lkInvCategory = new LookUpMaster
                {
                    Id = (int)result2["Id"],
                    Description = result2["Description"].ToString()
                };
                lkInvCategorys.Add(lkInvCategory);
            }

            InvoiceVM.FKCategory = lkInvCategorys.ToList();
            //InvoiceVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 56 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            List<LookUpMaster> lkInvTypes = new List<LookUpMaster>();
            string sqlQuery1 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='45'";
            var cmd1 = _db.Database.GetDbConnection().CreateCommand();
            cmd1.CommandText = sqlQuery1;
            _db.Database.OpenConnection();

            var result1 = cmd1.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result1.Read())
            {
                LookUpMaster lkInvType = new LookUpMaster
                {
                    Id = (int)result1["Id"],
                    Description = result1["Description"].ToString()
                };
                lkInvTypes.Add(lkInvType);
            }

            InvoiceVM.FKTypeOfInvoice = lkInvTypes.ToList();
            //InvoiceVM.FKTypeOfInvoice = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 45 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();

            return View(InvoiceVM);

        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string Save)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(InvoiceVM);
            //}

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sTypeofInvoice = lookUpMaster.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKTypeOfInvoice).FirstOrDefault().Description;
            string sDestination = lookUpMaster.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKDestination).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(InvoiceVM.InvoiceToPerson.FKSeason);
            string sSeason = season.Code;

            string codechar = (sTypeofInvoice.Substring(0, 2) + sSeason.Substring(0, 4) + sDestination.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.InvoiceToPersons.Where(x => x.InvoiceNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.InvoiceToPersons.Where(x => x.InvoiceNo.Contains(codechar)).Select(x => int.Parse(x.InvoiceNo.Substring(8, 4))).ToList().Max();
            }

            InvoiceVM.InvoiceToPerson.InvoiceNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));
InvoiceVM.InvoiceToPerson.TypeofInvoice = sTypeofInvoice;
            InvoiceVM.InvoiceToPerson.Destination = lookUpMaster.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKDestination).FirstOrDefault().Description;
            InvoiceVM.InvoiceToPerson.Category = lookUpMaster.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKCategory).FirstOrDefault().Description;
            InvoiceVM.InvoiceToPerson.Season = season.Code;
            var companyInfo = await _db.unitMasters.ToListAsync();
            InvoiceVM.InvoiceToPerson.UnitName = companyInfo.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKUnit).FirstOrDefault().CompanyName;
            InvoiceVM.InvoiceToPerson.FKFromState = companyInfo.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKUnit).FirstOrDefault().FKState;
            var location = await _db.locations.ToListAsync();
            InvoiceVM.InvoiceToPerson.Location = location.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKLocation).FirstOrDefault().LocationName;
            var customer = await _db.customerPerson.ToListAsync();
            InvoiceVM.InvoiceToPerson.PersonName = customer.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKPerson).FirstOrDefault().PersonName;
            InvoiceVM.InvoiceToPerson.FKToState = customer.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKPerson).FirstOrDefault().FKState;
            var partyInfo = await _db.partyInfoDtls.ToListAsync();

            if (InvoiceVM.InvoiceToPerson.IncludeDeliveryTo == true)
                InvoiceVM.InvoiceToPerson.DeliveryToCustomerName = partyInfo.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKDeliveryTo).FirstOrDefault().CompanyName;
            else
            {
                InvoiceVM.InvoiceToPerson.FKDeliveryTo = 0;
                InvoiceVM.InvoiceToPerson.DeliveryToCustomerName = "";
            }
            InvoiceVM.InvoiceToPerson.FKFromState = companyInfo.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKUnit).FirstOrDefault().FKState;
            InvoiceVM.InvoiceToPerson.FKToState = customer.Where(x => x.Id == InvoiceVM.InvoiceToPerson.FKPerson).FirstOrDefault().FKState;

            _db.InvoiceToPersons.Add(InvoiceVM.InvoiceToPerson);
            await _db.SaveChangesAsync();

            if (Save == "Save & New Inv")
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                var invoice = await _db.InvoiceToPersons.Where(x => x.InvoiceNo == InvoiceVM.InvoiceToPerson.InvoiceNo).FirstOrDefaultAsync();
                return RedirectToAction("InvoiceDetailCreate", "CounterInvoiceOnlineCustomer", new { Id = invoice.Id });
            }
        }

        //GET - CREATE
        public async Task<IActionResult> NewCustomer()
        {
            CustomerPersonVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1 && s.IsActive == true).ToListAsync();
            CustomerPersonVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2 && s.IsActive == true).ToListAsync();
            CustomerPersonVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3 && s.IsActive == true).ToListAsync();
            CustomerPersonVM.FKState = await _db.StateMasters.ToListAsync();
            CustomerPersonVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21 && s.IsActive == true).ToListAsync();
            CustomerPersonVM.FKCustomerOf = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 48 && s.IsActive == true).ToListAsync();
            CustomerPersonVM.FKMaritalStatus = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 13 && s.IsActive == true).ToListAsync();
            CustomerPersonVM.FKGender = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 67 && s.IsActive == true).ToListAsync();

            return View(CustomerPersonVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCustomer(CustomerPersonViewModel CustomerPersonVM)
        {
            if (!ModelState.IsValid)
            {
                return View(CustomerPersonVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            CustomerPersonVM.CustomerPerson.Gender = (lookUpMaster.Where(x => x.Id == CustomerPersonVM.CustomerPerson.FKGender).FirstOrDefault().Description).ToString().Substring(0, 1).ToUpper();
            _db.customerPerson.Add(CustomerPersonVM.CustomerPerson);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Create));
        }
        #endregion

        #region "INVOICE DETAIL"
        public async Task<IActionResult> InvoiceDetailIndex(int Id)
        {
            var inv = await _db.InvoiceToPersons.FindAsync(Id);
            TempData["Invoice"] = inv;

            return View(await _db.InvoiceToPersonDetails.Where(x => x.FKInvoiceNo == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> InvoiceDetailCreate(int Id)
        {
            var inv = await _db.InvoiceToPersons.FindAsync(Id);
            TempData["Invoice"] = inv;

            var result = _db.InvoiceToPersonDetails.Where(x => x.FKInvoiceNo == Id).ToList();
            ViewBag.InvDtls = _db.InvoiceToPersonDetails.Where(x => x.FKInvoiceNo == Id).ToList();
            return View(result);

        }

        //POST - InvoiceDetailCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvoiceDetailCreate()
        {

            int nFKinvoice = Convert.ToInt32(Request.Form["FKInvoice"]);
            var inv = await _db.InvoiceToPersons.FindAsync(nFKinvoice);
            string sEANCode = Request.Form["EANCode"];
            int nNFKHSNCode;
            decimal nGSTPercentage;
            decimal nValue;
            decimal nSGSTPercentage, nCGSTPercentage, nIGSTPercentage;
            decimal nSGSTValue, nCGSTValue, nIGSTValue, nGSTValue;

            //int nRowCount = _db.stocks.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode && x.Quantity > 0 && x.FKUnit == inv.FKUnit && x.FKLocation == inv.FKLocation).ToList().Count;
            int nRowCount = _db.stocks.Where(x => (x.EANCode == sEANCode || x.StockNo == sEANCode) && x.Quantity > 0 && x.FKUnit == inv.FKUnit && x.FKLocation == inv.FKLocation).ToList().Count;
            if (nRowCount == 0)
            {
                //ALERT MESSAGE TO BE DISPLAYED
            }
            else
            {
                var stock = await _db.stocks.OrderBy(x => x.Id).Where(x => (x.EANCode == sEANCode || x.StockNo == sEANCode) && x.Quantity > 0 && x.FKUnit == inv.FKUnit && x.FKLocation == inv.FKLocation).FirstOrDefaultAsync();
                decimal nStockQuantity = stock.Quantity;

                int nRowCount1 = _db.InvoiceToPersonDetails.Where(x => x.FKInvoiceNo == nFKinvoice && x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count;
                if (nRowCount1 == 0)
                {

                    if (stock.FKMaterial > 0)
                    {
                        nNFKHSNCode = 0;
                        nGSTPercentage = 0;
                    }
                    else if (stock.FKArticleDetail > 0)
                    {
                        nNFKHSNCode = _db.articleDetails.Where(x => x.Id == stock.FKArticleDetail).FirstOrDefault().FKHSNCode;
                        nGSTPercentage = _db.HSNCodeMasters.Where(x => x.Id == nNFKHSNCode).FirstOrDefault().GSTPercentage;

                    }
                    else
                    {
                        nNFKHSNCode = 0;
                        nGSTPercentage = 0;
                    }

                    InvoiceDetailVM.InvoiceDetail.Id = 0;
                    InvoiceDetailVM.InvoiceDetail.FKInvoiceNo = inv.Id;
                    InvoiceDetailVM.InvoiceDetail.FKMaterial = stock.FKMaterial;
                    InvoiceDetailVM.InvoiceDetail.FKArticle = stock.FKArticleDetail;
                    InvoiceDetailVM.InvoiceDetail.Description = stock.Description;
                    InvoiceDetailVM.InvoiceDetail.Colour = stock.Colour;
                    InvoiceDetailVM.InvoiceDetail.Size = stock.Size;
                    InvoiceDetailVM.InvoiceDetail.HSNCode = 0;
                    InvoiceDetailVM.InvoiceDetail.Quantity = 1;
                    InvoiceDetailVM.InvoiceDetail.FKUOM = stock.FKUOM;
                    decimal nRate = stock.MRP - ((stock.MRP * nGSTPercentage) / (100 + nGSTPercentage));
                    InvoiceDetailVM.InvoiceDetail.Rate = nRate;
                    nValue = nRate;
                    InvoiceDetailVM.InvoiceDetail.Value = nValue;
                    InvoiceDetailVM.InvoiceDetail.ValueinINR = nValue;

                    if (inv.FKFromState == inv.FKToState)
                    {
                        nSGSTPercentage = nGSTPercentage / 2;
                        nCGSTPercentage = nGSTPercentage / 2;
                        nIGSTPercentage = 0;
                    }
                    else
                    {
                        nSGSTPercentage = 0;
                        nCGSTPercentage = 0;
                        nIGSTPercentage = nGSTPercentage;
                    }
                    nSGSTValue = nValue * nSGSTPercentage / 100;
                    nCGSTValue = nValue * nCGSTPercentage / 100;
                    nIGSTValue = nValue * nIGSTPercentage / 100;
                    nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                    InvoiceDetailVM.InvoiceDetail.DiscountPercentage = 0;
                    InvoiceDetailVM.InvoiceDetail.DiscountValue = 0;
                    InvoiceDetailVM.InvoiceDetail.GrossValue = nValue;
                    InvoiceDetailVM.InvoiceDetail.SGSTPercentage = nSGSTPercentage;
                    InvoiceDetailVM.InvoiceDetail.SGSTValue = nSGSTValue;
                    InvoiceDetailVM.InvoiceDetail.CGSTPercentage = nCGSTPercentage;
                    InvoiceDetailVM.InvoiceDetail.CGSTValue = nCGSTValue;
                    InvoiceDetailVM.InvoiceDetail.IGSTPercentage = nIGSTPercentage;
                    InvoiceDetailVM.InvoiceDetail.IGSTValue = nIGSTValue;
                    InvoiceDetailVM.InvoiceDetail.GSTTotalValue = nGSTValue;
                    InvoiceDetailVM.InvoiceDetail.OthersValuePlus = 0;
                    InvoiceDetailVM.InvoiceDetail.OthersValueMinus = 0;
                    InvoiceDetailVM.InvoiceDetail.ItemNettValue = nValue + nGSTValue;
                    InvoiceDetailVM.InvoiceDetail.IsEntryCompleted = false;
                    InvoiceDetailVM.InvoiceDetail.IsActive = true;
                    InvoiceDetailVM.InvoiceDetail.CreatedBy = 1;
                    InvoiceDetailVM.InvoiceDetail.CreatedDate = DateTime.Now;
                    InvoiceDetailVM.InvoiceDetail.InvoiceDt = inv.InvoiceDt;
                    InvoiceDetailVM.InvoiceDetail.InvoiceNo = inv.InvoiceNo;
                    InvoiceDetailVM.InvoiceDetail.EANCode = sEANCode;
                    InvoiceDetailVM.InvoiceDetail.FKCustomer = inv.FKPerson;
                    InvoiceDetailVM.InvoiceDetail.MaterialorFinishedProduct = stock.MaterialorFinishedProduct;
                    InvoiceDetailVM.InvoiceDetail.StockNo = stock.StockNo;

                    _db.InvoiceToPersonDetails.Add(InvoiceDetailVM.InvoiceDetail);
                    _db.SaveChanges();

                }
                else
                {
                    decimal nQuantity = _db.InvoiceDetails.Where(x => x.FKInvoiceNo == nFKinvoice && x.EANCode == sEANCode || x.StockNo == sEANCode).Select(x => x.Quantity).ToList().Sum();
                    if (nQuantity + 1 > nStockQuantity)
                    {
                        //ALERT MESSAGE TO BE DISPLAYED
                    }
                    else
                    {
                        var TempFromdb = await _db.InvoiceToPersonDetails.Where(x => x.FKInvoiceNo == nFKinvoice && x.EANCode == sEANCode || x.StockNo == sEANCode).FirstOrDefaultAsync();
                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                        TempFromdb.Value = TempFromdb.Quantity * TempFromdb.Rate;

                        if (stock.FKMaterial > 0)
                        {
                            nNFKHSNCode = 0;
                            nGSTPercentage = 0;
                        }
                        else if (stock.FKArticleDetail > 0)
                        {
                            nNFKHSNCode = _db.articleDetails.Where(x => x.Id == stock.FKArticleDetail).FirstOrDefault().FKHSNCode;
                            nGSTPercentage = _db.HSNCodeMasters.Where(x => x.Id == nNFKHSNCode).FirstOrDefault().GSTPercentage;
                        }
                        else
                        {
                            nNFKHSNCode = 0;
                            nGSTPercentage = 0;
                        }

                        if (inv.FKFromState == inv.FKToState)
                        {
                            nSGSTPercentage = nGSTPercentage / 2;
                            nCGSTPercentage = nGSTPercentage / 2;
                            nIGSTPercentage = 0;
                        }
                        else
                        {
                            nSGSTPercentage = 0;
                            nCGSTPercentage = 0;
                            nIGSTPercentage = nGSTPercentage;
                        }
                        nSGSTValue = TempFromdb.Value * nSGSTPercentage / 100;
                        nCGSTValue = TempFromdb.Value * nCGSTPercentage / 100;
                        nIGSTValue = TempFromdb.Value * nIGSTPercentage / 100;
                        nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                        TempFromdb.SGSTValue = nSGSTValue;
                        TempFromdb.CGSTValue = nCGSTValue;
                        TempFromdb.IGSTValue = nIGSTValue;
                        TempFromdb.GSTTotalValue = nGSTValue;
                        TempFromdb.ItemNettValue = TempFromdb.Value + nGSTValue;
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction(nameof(InvoiceDetailCreate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertInvoiceDetail()
        {
            int nFKinvoice = Convert.ToInt32(Request.Form["FKInvoice"]);
            var InvoiceDtls = await _db.InvoiceToPersonDetails.OrderBy(c => c.Id).Where(x => x.FKInvoiceNo == nFKinvoice).ToListAsync();
            var Invoice = await _db.InvoiceToPersons.Where(x => x.Id == nFKinvoice).FirstOrDefaultAsync();

            int nFKStage, nFKQuality, nFKStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = 0;
            nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;


            foreach (var item in InvoiceDtls)
            {

                int nStckRow = _db.stocks.Where(x => x.EANCode == item.EANCode && x.Rate == item.Rate && x.Quantity > 0 && x.FKUnit == Invoice.FKUnit &&
                x.FKLocation == Invoice.FKLocation && x.FKStatus == nFKStatus).ToList().Count;
                if (nStckRow == 0)
                {

                }
                else
                {
                    var stck = await _db.stocks.Where(x => x.EANCode == item.EANCode && x.Rate == item.Rate && x.Quantity > 0 && x.FKUnit == Invoice.FKUnit &&
                    x.FKLocation == Invoice.FKLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                    nFKStage = stck.FKStage;
                    stck.Quantity = stck.Quantity - item.Quantity;
                    decimal rate = stck.Rate;
                    decimal value = stck.Quantity * rate;
                    stck.Value = value;
                    stck.ValueInINR = value;
                    stck.LastTranDate = DateTime.Now;
                    _db.SaveChanges();
                }


                int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == Invoice.FKUnit && x.FKTranLocation == Invoice.FKLocation && x.EANCode == item.EANCode).ToList().Count;
                decimal nClosing;
                if (nRowCount == 0)
                {
                    nClosing = 0;
                }
                else
                {
                    var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == Invoice.FKUnit && x.FKTranLocation == Invoice.FKLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
                    nClosing = AllTran.BalanceQuantity;
                }

                //lookUpMaster.Where(x => x.FKLookUpCategory == 39 && x.SetAsDefault == true).FirstOrDefault().Id

                AllTransactionVM.AllTransaction.Id = 0;
                AllTransactionVM.AllTransaction.TransactionType = "Dispatched";
                AllTransactionVM.AllTransaction.TranId = item.FKInvoiceNo;
                AllTransactionVM.AllTransaction.TranRefNo = item.InvoiceNo;
                AllTransactionVM.AllTransaction.TranDate = item.InvoiceDt;
                AllTransactionVM.AllTransaction.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                AllTransactionVM.AllTransaction.FKMaterial = item.FKMaterial;
                AllTransactionVM.AllTransaction.FKArticle = item.FKArticle;
                AllTransactionVM.AllTransaction.Description = item.Description;
                AllTransactionVM.AllTransaction.Colour = item.Colour;
                AllTransactionVM.AllTransaction.Size = item.Size.ToString();
                AllTransactionVM.AllTransaction.FKFromUnit = Invoice.FKUnit;
                AllTransactionVM.AllTransaction.FKFromLocation = Invoice.FKLocation;
                AllTransactionVM.AllTransaction.FKFromStage = nFKStage;
                AllTransactionVM.AllTransaction.FKToUnit = item.FKCustomer;
                AllTransactionVM.AllTransaction.FKToLocation = 0;
                AllTransactionVM.AllTransaction.FKToStage = 0;
                AllTransactionVM.AllTransaction.FKQuality = nFKQuality;
                AllTransactionVM.AllTransaction.HSNCode = item.HSNCode;
                AllTransactionVM.AllTransaction.FKUOM = item.FKUOM;
                AllTransactionVM.AllTransaction.InwardQuantity = 0;
                AllTransactionVM.AllTransaction.OutwardQuantity = item.Quantity;
                AllTransactionVM.AllTransaction.BalanceQuantity = nClosing - item.Quantity;
                AllTransactionVM.AllTransaction.Rate = item.Rate;
                AllTransactionVM.AllTransaction.Value = item.Value;
                AllTransactionVM.AllTransaction.FKStatus = nFKStatus;
                AllTransactionVM.AllTransaction.IsActive = true;
                AllTransactionVM.AllTransaction.CreatedBy = 0;
                AllTransactionVM.AllTransaction.CreatedDate = DateTime.Now;
                AllTransactionVM.AllTransaction.ModifiedBy = 0;
                AllTransactionVM.AllTransaction.FKTranLocation = Invoice.FKLocation;
                AllTransactionVM.AllTransaction.FKTranUnit = Invoice.FKUnit;
                AllTransactionVM.AllTransaction.EANCode = item.EANCode;
                AllTransactionVM.AllTransaction.StockNo = item.StockNo;

                _db.AllTransactions.Add(AllTransactionVM.AllTransaction);
                await _db.SaveChangesAsync();

                var stkWArtfromdb = await _db.stockWithArticles.Where(x => x.EANCode == item.EANCode).FirstAsync();
                stkWArtfromdb.SoldQty = stkWArtfromdb.SoldQty + Convert.ToInt32(item.Quantity);
                stkWArtfromdb.BalQty = stkWArtfromdb.ArrivedQty - stkWArtfromdb.SoldQty;
                _db.SaveChanges();

            }

            decimal nInvoiceQty = _db.InvoiceToPersonDetails.Where(x => x.FKInvoiceNo == nFKinvoice).Select(x => x.Quantity).ToList().Sum();
            var InvfromDb = await _db.InvoiceToPersons.Where(x => x.Id == nFKinvoice).FirstAsync();
            InvfromDb.Quantity = Convert.ToInt32(nInvoiceQty);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
