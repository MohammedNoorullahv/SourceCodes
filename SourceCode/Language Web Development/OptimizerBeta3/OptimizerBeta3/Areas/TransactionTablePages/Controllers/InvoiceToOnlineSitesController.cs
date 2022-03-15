using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class InvoiceToOnlineSitesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public InvoiceViewModel InvoiceVM { get; set; }
        public InvoiceDetailViewModel InvoiceDetailVM { get; set; }
        public StocksViewModel StocksVM { get; set; }
        public AllTransactionViewModel AllTransactionVM { get; set; }

        public static string sScanningMode;
        public static string sIpAddress;
        public static string ipaddress = string.Empty;

        public InvoiceToOnlineSitesController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            InvoiceVM = new InvoiceViewModel()
            {
                FKTypeOfInvoice = _db.lookUpMasters,
                FKSeason = _db.seasons,
                FKUnit = _db.unitMasters,
                FKParty = _db.partyInfos,
                FKBillTo = _db.partyInfoDtls,
                FKNotifyTo = _db.partyInfoDtls,
                FKDeliveryTo = _db.partyInfoDtls,
                FKPaymentTerms = _db.lookUpMasters,
                FKDepartment = _db.lookUpMasters,
                FKCurrency = _db.lookUpMasters,
                FKModeofTransport = _db.lookUpMasters,
                FKLocation = _db.locations,
                FKDestination = _db.lookUpMasters,
                FKCategory = _db.lookUpMasters,
                Invoice = new Models.TransactionTables.Invoice()

            };

            InvoiceDetailVM = new InvoiceDetailViewModel()
            {
                Invoice = new Models.TransactionTables.Invoice(),
                InvoiceDetail = new Models.TransactionTables.InvoiceDetail()
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

            return View(await _db.Invoices.OrderByDescending(x => x.Id).Where(x => x.InvoiceDt >= effectStartDate && x.InvoiceDt <= effectEndDate && x.InvoiceTo == "OS").ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "Invoice", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            InvoiceVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();

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
            InvoiceVM.FKParty = suppliers.ToList();
            //InvoiceVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();

            InvoiceVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKBillTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKNotifyTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKDeliveryTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive == true).ToListAsync();

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
        public async Task<IActionResult> CreatePost(string Save, IFormFile formFile)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(InvoiceVM);
            //}

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sTypeofInvoice = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKTypeOfInvoice).FirstOrDefault().Description;
            string sDestination = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKDestination).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(InvoiceVM.Invoice.FKSeason);
            string sSeason = season.Code;

            string codechar = (sTypeofInvoice.Substring(0, 2) + sSeason.Substring(0, 4) + sDestination.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.Invoices.Where(x => x.InvoiceNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.Invoices.Where(x => x.InvoiceNo.Contains(codechar)).Select(x => int.Parse(x.InvoiceNo.Substring(8, 4))).ToList().Max();
            }

            InvoiceVM.Invoice.InvoiceNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            InvoiceVM.Invoice.Currency = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKCurrency).FirstOrDefault().Description;
            InvoiceVM.Invoice.ModeofTransport = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKModeofTransport).FirstOrDefault().Description;
            InvoiceVM.Invoice.PaymentTerms = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKPaymentTerms).FirstOrDefault().Description;
            InvoiceVM.Invoice.TypeofInvoice = sTypeofInvoice;
            InvoiceVM.Invoice.Currency = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKCurrency).FirstOrDefault().Description;
            InvoiceVM.Invoice.Destination = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKDestination).FirstOrDefault().Description;
            InvoiceVM.Invoice.Category = lookUpMaster.Where(x => x.Id == InvoiceVM.Invoice.FKCategory).FirstOrDefault().Description;
            InvoiceVM.Invoice.Season = season.Code;
            var companyInfo = await _db.unitMasters.ToListAsync();
            InvoiceVM.Invoice.UnitName = companyInfo.Where(x => x.Id == InvoiceVM.Invoice.FKUnit).FirstOrDefault().CompanyName;
            var location = await _db.locations.ToListAsync();
            InvoiceVM.Invoice.Location = location.Where(x => x.Id == InvoiceVM.Invoice.FKLocation).FirstOrDefault().LocationName;
            var customer = await _db.partyInfos.ToListAsync();
            InvoiceVM.Invoice.CustomerName = customer.Where(x => x.Id == InvoiceVM.Invoice.FKParty).FirstOrDefault().CompanyName;
            var partyInfo = await _db.partyInfoDtls.ToListAsync();
            if (InvoiceVM.Invoice.IncludeBillTo == true)
                InvoiceVM.Invoice.BillToCustomerName = partyInfo.Where(x => x.Id == InvoiceVM.Invoice.FKBillTo).FirstOrDefault().CompanyName;
            else
            {
                InvoiceVM.Invoice.FKBillTo = 0;
                InvoiceVM.Invoice.BillToCustomerName = "";
            }

            if (InvoiceVM.Invoice.IncludeDeliveryTo == true)
                InvoiceVM.Invoice.DeliveryToCustomerName = partyInfo.Where(x => x.Id == InvoiceVM.Invoice.FKDeliveryTo).FirstOrDefault().CompanyName;
            else
            {
                InvoiceVM.Invoice.FKDeliveryTo = 0;
                InvoiceVM.Invoice.DeliveryToCustomerName = "";
            }

            if (InvoiceVM.Invoice.IncludeNotifyTo == true)
                InvoiceVM.Invoice.NotifyToCustomerName = partyInfo.Where(x => x.Id == InvoiceVM.Invoice.FKNotifyTo).FirstOrDefault().CompanyName;
            else
            {
                InvoiceVM.Invoice.FKNotifyTo = 0;
                InvoiceVM.Invoice.NotifyToCustomerName = "";
            }
            InvoiceVM.Invoice.FKFromState = companyInfo.Where(x => x.Id == InvoiceVM.Invoice.FKUnit).FirstOrDefault().FKState;
            InvoiceVM.Invoice.FKToState = customer.Where(x => x.Id == InvoiceVM.Invoice.FKParty).FirstOrDefault().FKState;
            InvoiceVM.Invoice.FKDeliveryChallan = 0;
            

            _db.Invoices.Add(InvoiceVM.Invoice);
            await _db.SaveChangesAsync();
            if (Save == "Save & New Inv")
            {
                return RedirectToAction(nameof(Create));
            }
            else if (Save == "Save & Insert Dtl")
            {
                var invoice = await _db.Invoices.Where(x => x.InvoiceNo == InvoiceVM.Invoice.InvoiceNo).FirstOrDefaultAsync();
                return RedirectToAction("CreateInvoiceDetailForScanning", "InvoiceToOnlineSites", new { Id = invoice.Id });
            }
            else
            {
                var invoice = await _db.Invoices.Where(x => x.InvoiceNo == InvoiceVM.Invoice.InvoiceNo).FirstOrDefaultAsync();

                var scndlist = _db.TempInvoiceDtlEANCodes.Where(x => x.IPAddress == ipaddress);
                _db.TempInvoiceDtlEANCodes.RemoveRange(scndlist);
                await _db.SaveChangesAsync();

                var tmpInvLst = _db.TempInvoiceDtls.Where(x => x.FKInvoiceNo == invoice.Id);
                _db.TempInvoiceDtls.RemoveRange(tmpInvLst);
                await _db.SaveChangesAsync();

                //string path = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
                string path = $"D:\\Language\\Uploads";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(formFile.FileName);
                string filePath = Path.Combine(path, fileName);
                var fileExt = System.IO.Path.GetExtension(formFile.FileName).Substring(1);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                var scannedlist = new List<string>();

                List<MdlTempArticalArrivalEANCode> res = new List<MdlTempArticalArrivalEANCode>();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (fileExt == "txt")
                {
                    string line;
                    //System.IO.StreamReader file = new System.IO.StreamReader(@"D:\LanguageData.txt");
                    StreamReader file = new StreamReader(filePath);
                    while ((line = file.ReadLine()) != null)
                    {
                        scannedlist.Add(line);
                    }

                    ViewBag.File = scannedlist;
                }
                else
                {
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            while (reader.Read()) //Each row of the file
                            {
                                scannedlist.Add(reader.GetValue(0).ToString());
                            }
                        }
                    }

                    ViewBag.File = scannedlist;
                }

                //string fullPath = Request.MapPath("~/Images/Cakes/" + photoName);
                //if (System.IO.File.Exists(fullPath))
                //{
                //    System.IO.File.Delete(fullPath);
                //}

                TempInvoiceDtlEANCode model = new TempInvoiceDtlEANCode();

                var InvoicefromDb = await _db.Invoices.FindAsync(invoice.Id);
                string sInvoiceNo = InvoicefromDb.InvoiceNo;
                int nFKLocation = InvoicefromDb.FKLocation;
                int nFKUnit = InvoicefromDb.FKUnit;

                int nFKStatus;
                nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;

                foreach (var EANCode in scannedlist)
                {
                    int nRowCnt = _db.stocks.Where(x => x.EANCode == EANCode || x.StockNo == EANCode).ToList().Count;
                    if (nRowCnt == 0)
                    {
                        model.IPAddress = ipaddress;
                        model.InvoiceNo = sInvoiceNo;
                        model.EANCode = EANCode;
                        model.Quantity = 1;
                        model.IsMatching = false;
                        model.Reason = "Invalid Barcode";
                        model.Id = 0;
                        _db.TempInvoiceDtlEANCodes.Add(model);
                        _db.SaveChanges();
                    }
                    else
                    {

                        var nStockQty = _db.stocks.Where(x => (x.EANCode == EANCode || x.StockNo == EANCode) && x.FKUnit == nFKUnit && x.FKLocation == nFKLocation && x.FKStatus == nFKStatus).Select(x => x.Quantity).ToList().Sum();
                        if (nStockQty > 0)
                        {
                            int nRowCount = _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode).ToList().Count;
                            if (nRowCount > 0)
                            {
                                if (nRowCount == 1)
                                {
                                    var TempArriEAN = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                    decimal nExistingQty = TempArriEAN.Quantity;

                                    if (nExistingQty + 1 <= nStockQty)
                                    {
                                        var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        int nRowCount1 = _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                                        if (nRowCount1 == 0)
                                        {
                                            model.IPAddress = ipaddress;
                                            model.InvoiceNo = sInvoiceNo;
                                            model.EANCode = EANCode;
                                            model.Quantity = 1;
                                            model.IsMatching = false;
                                            model.Id = 0;
                                            model.Reason = "Part X's Qty";
                                            _db.TempInvoiceDtlEANCodes.Add(model);
                                            await _db.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                            TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                            _db.SaveChanges();
                                        }

                                    }
                                }
                                else
                                {
                                    var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                    TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                    _db.SaveChanges();
                                }
                            }
                            else
                            {
                                model.IPAddress = ipaddress;
                                model.InvoiceNo = sInvoiceNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = true;
                                model.Reason = "";
                                model.Id = 0;
                                _db.TempInvoiceDtlEANCodes.Add(model);
                                _db.SaveChanges();
                            }
                        }
                        else
                        {
                            int nRowCount1 = _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                            if (nRowCount1 == 0)
                            {
                                model.IPAddress = ipaddress;
                                model.InvoiceNo = sInvoiceNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = false;
                                model.Id = 0;
                                model.Reason = "X's Qty";
                                _db.TempInvoiceDtlEANCodes.Add(model);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                _db.SaveChanges();
                            }
                        }

                    }

                    //model.Id = 0;
                };

                #region UPDATING TEMPORARY Invoice DETAILS

                List<TempInvoiceDtl> tempInvoiceDtls = new List<TempInvoiceDtl>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_Invoice @mAction='SELINVOICE', @mInvoiceNo='{sInvoiceNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                Boolean nReadyforImport;
                string sReason = "";
                while (result.Read())
                {

                    var swa = new TempInvoiceDtl
                    {
                        Id = 0,
                        IPAddress = ipaddress,
                        FKInvoiceNo = result.GetInt32(0),
                        InvoiceNo = sInvoiceNo,
                        InvoiceDt = DateTime.Now,
                        FKMaterial = result.GetInt32(1),
                        FKArticle = result.GetInt32(2),
                        Description = result.GetString(3),
                        Colour = result.GetString(4),
                        Size = result.GetDecimal(5).ToString(),
                        HSNCode = result.GetInt32(6),
                        Quantity = result.GetDecimal(7),
                        IIQuantity = result.GetDecimal(8),
                        Rate = result.GetDecimal(9),
                        Value = result.GetDecimal(10),
                        ValueinINR = result.GetDecimal(11),
                        DiscountPercentage = result.GetDecimal(12),
                        DiscountValue = result.GetDecimal(13),
                        GrossValue = result.GetDecimal(14),
                        SGSTPercentage = result.GetDecimal(15),
                        SGSTValue = result.GetDecimal(16),
                        CGSTPercentage = result.GetDecimal(17),
                        CGSTValue = result.GetDecimal(18),
                        IGSTPercentage = result.GetDecimal(19),
                        IGSTValue = result.GetDecimal(20),
                        GSTTotalValue = result.GetDecimal(21),
                        OthersValuePlus = result.GetDecimal(22),
                        ItemNettValue = result.GetDecimal(23),
                        EANCode = result.GetString(24),
                        ReadyforImport = result.GetBoolean(34),//nReadyforImport,
                        Reason = result.GetString(35),//sReason,
                        OrderReferenceNo = result.GetString(26),
                        FKUnit = result.GetInt32(27),
                        FKLocation = result.GetInt32(28),
                        FKUOM = result.GetInt32(29),
                        FKCurrency = result.GetInt32(30),
                        FKSource = result.GetInt32(31),
                        StockNo = result.GetString(32),
                        FKCustomer = result.GetInt32(33),
                        MaterialorFinishedProduct = result.GetString(36),
                        FKIIUom = 0,
                        OthersValueMinus = 0
                    };
                    tempInvoiceDtls.Add(swa);
                }

                result.Close();

                _db.TempInvoiceDtls.AddRange(tempInvoiceDtls);
                await _db.SaveChangesAsync();

                var TempEANCodeDtls = await _db.TempInvoiceDtlEANCodes.OrderBy(c => c.Id).Where(x => x.InvoiceNo == sInvoiceNo).ToListAsync();
                string sEANCode;
                foreach (var EANCode in TempEANCodeDtls)
                {
                    sEANCode = EANCode.EANCode;
                    //var TempEANCodesFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();

                    if (_db.TempInvoiceDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count > 0)
                    {
                        var TempInwFromdb = await _db.TempInvoiceDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).FirstAsync();
                        nReadyforImport = TempInwFromdb.ReadyforImport;
                        sReason = TempInwFromdb.Reason;
                    }
                    else
                    {
                        nReadyforImport = false;
                        sReason = "Wrong Barcode";

                    }
                    //_db.TempInvoiceDtls.Update(TempEANCodeDtls);
                    var TempEANCodeDtls1 = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == sEANCode).FirstOrDefaultAsync();
                    TempEANCodeDtls1.IsMatching = nReadyforImport;
                    TempEANCodeDtls1.Reason = sReason;
                    await _db.SaveChangesAsync();
                }

                #endregion

                return RedirectToAction("LoadTempInvoiceDtls", "InvoiceToOnlineSites", new { Id = invoice.Id });
            }
        }

        //GET - CREATE
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            InvoiceVM.Invoice = await _db.Invoices.SingleOrDefaultAsync(m => m.Id == id);
            InvoiceVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKBillTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKNotifyTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKDeliveryTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();

            InvoiceVM.FKTypeOfInvoice = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 45 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();

            return View(InvoiceVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvoiceViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            //string sTypeofOrder = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            //string sSource = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKSource).FirstOrDefault().Description;


            var invoicefromDb = await _db.Invoices.FindAsync(id);

            invoicefromDb.FKTypeOfInvoice = model.Invoice.FKTypeOfInvoice;
            invoicefromDb.FKSeason = model.Invoice.FKSeason;
            invoicefromDb.FKParty = model.Invoice.FKParty;
            invoicefromDb.FKBillTo = model.Invoice.FKBillTo;
            invoicefromDb.FKNotifyTo = model.Invoice.FKNotifyTo;
            invoicefromDb.FKDeliveryTo = model.Invoice.FKDeliveryTo;
            invoicefromDb.FKPaymentTerms = model.Invoice.FKPaymentTerms;
            invoicefromDb.FKCurrency = model.Invoice.FKCurrency;
            invoicefromDb.FKModeofTransport = model.Invoice.FKModeofTransport;
            invoicefromDb.Remarks = model.Invoice.Remarks;
            invoicefromDb.ModifiedBy = model.Invoice.ModifiedBy;
            invoicefromDb.ModifiedDate = model.Invoice.ModifiedDate;

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
        #endregion

        #region "INVOICE DETAIL"
        public async Task<IActionResult> InvoiceDetailIndex(int Id)
        {
            var inv = await _db.Invoices.FindAsync(Id);
            TempData["Invoice"] = inv;

            return View(await _db.InvoiceDetails.Where(x => x.FKInvoiceNo == Id).ToListAsync());
        }

        //GET - CREATE Invoice DETAIL For Scanning
        public async Task<IActionResult> CreateInvoiceDetailForScanning(int Id)
        {
            var inv = await _db.Invoices.FindAsync(Id);
            TempData["Invoice"] = inv;

            TempData["InvoiceId"] = Id;
            if (sScanningMode != "Re Entries")
                sScanningMode = "New Entries";

            TempData["ScanningMode"] = sScanningMode;

            decimal nRowCount1 = _db.InvoiceDetails.Where(x => x.FKInvoiceNo == Id).Select(x => x.Quantity).ToList().Sum();
            decimal nRowCount = 0;
            if (sScanningMode == "Re Entries")
            {
                nRowCount = _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress).Select(x => x.Quantity).ToList().Sum();
            }
            else
            {
                //TempData["PreviousCount"] = 0;
            }

            TempData["PreviousCount"] = nRowCount + nRowCount1;

            var tmpInwdtls = _db.TempInvoiceDtls.Where(x => x.IPAddress == ipaddress);
            _db.TempInvoiceDtls.RemoveRange(tmpInwdtls);
            await _db.SaveChangesAsync();


            return View();
        }

        //POST - SCANNED BARCODES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvoiceDetailScannedCodes(TempInvoiceDtlEANCode model)
        {
            var InvoicefromDb = await _db.Invoices.FindAsync(Convert.ToInt32(TempData["InvoiceId"]));
            if (sScanningMode != "Updates")
            {
                if (sScanningMode == "New Entries")
                {
                    var scndlist = _db.TempInvoiceDtlEANCodes.Where(x => x.IPAddress == ipaddress);
                    _db.TempInvoiceDtlEANCodes.RemoveRange(scndlist);
                    await _db.SaveChangesAsync();
                }
                var scannedlist = Request.Form["liContent"];
                string sScanningMod = Request.Form["ScanningMode"];

                //if (Convert.ToInt32(TempData["InvoiceId"]) != 0 )
                //{
                //    nFKInvoice = 
                //}
                string sInvoiceNo = InvoicefromDb.InvoiceNo;
                int nFKLocation = InvoicefromDb.FKLocation;
                int nFKUnit = InvoicefromDb.FKUnit;

                int nFKStatus;
                var lookUpMaster = await _db.lookUpMasters.ToListAsync();
                nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;

                foreach (var EANCode in scannedlist)
                {
                    int nRowCnt = _db.stocks.Where(x => x.EANCode == EANCode || x.StockNo == EANCode).ToList().Count;
                    if (nRowCnt == 0)
                    {
                        model.IPAddress = ipaddress;
                        model.InvoiceNo = sInvoiceNo;
                        model.EANCode = EANCode;
                        model.Quantity = 1;
                        model.IsMatching = false;
                        model.Reason = "Invalid Barcode";
                        model.Id = 0;
                        _db.TempInvoiceDtlEANCodes.Add(model);
                        _db.SaveChanges();
                    }
                    else
                    {

                        //_db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress).Select(x => x.Quantity).ToList().Sum();
                        //var stockwithArticle = await _db.stocks.Where(x => x.EANCode == EANCode && x.FKUnit == item.FKUnit && x.FKLocation == nFKLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                        //int nStockQty = Convert.ToInt32(stockwithArticle.Quantity);
                        var nStockQty = _db.stocks.Where(x => (x.EANCode == EANCode || x.StockNo == EANCode) && x.FKUnit == nFKUnit && x.FKLocation == nFKLocation && x.FKStatus == nFKStatus).Select(x => x.Quantity).ToList().Sum();
                        if (nStockQty > 0)
                        {
                            int nRowCount = _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode).ToList().Count;
                            if (nRowCount > 0)
                            {
                                if (nRowCount == 1)
                                {
                                    var TempArriEAN = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                    decimal nExistingQty = TempArriEAN.Quantity;

                                    //var stockwithArticle = await _db.stockWithArticles.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                    //int nBaltoReceive = stockwithArticle.Quantity - stockwithArticle.ArrivedQty;

                                    if (nExistingQty + 1 <= nStockQty)
                                    {
                                        var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                        //_db.TempArticalArrivalEANCodes.Update(TempFromdb);
                                        //_db.Entry(TempFromdb).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        int nRowCount1 = _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                                        if (nRowCount1 == 0)
                                        {
                                            model.IPAddress = ipaddress;
                                            model.InvoiceNo = sInvoiceNo;
                                            model.EANCode = EANCode;
                                            model.Quantity = 1;
                                            model.IsMatching = false;
                                            model.Id = 0;
                                            model.Reason = "Part X's Qty";
                                            _db.TempInvoiceDtlEANCodes.Add(model);
                                            await _db.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                            TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                            _db.SaveChanges();
                                        }

                                    }
                                }
                                else
                                {
                                    var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                    TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                    _db.SaveChanges();
                                }

                                //partyInfo.Where(x => x.Id == InvoiceVM.Invoice.FKParty).FirstOrDefault().CompanyName;
                                //var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();
                                //TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                //_db.SaveChanges();
                            }
                            else
                            {
                                model.IPAddress = ipaddress;
                                model.InvoiceNo = sInvoiceNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = true;
                                model.Reason = "";
                                model.Id = 0;
                                _db.TempInvoiceDtlEANCodes.Add(model);
                                _db.SaveChanges();
                                //await _db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            int nRowCount1 = _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                            if (nRowCount1 == 0)
                            {
                                model.IPAddress = ipaddress;
                                model.InvoiceNo = sInvoiceNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = false;
                                model.Id = 0;
                                model.Reason = "X's Qty";
                                _db.TempInvoiceDtlEANCodes.Add(model);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                var TempFromdb = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                _db.SaveChanges();
                            }
                        }

                    }

                    //model.Id = 0;
                };

                #region UPDATING TEMPORARY Invoice DETAILS

                List<TempInvoiceDtl> tempInvoiceDtls = new List<TempInvoiceDtl>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_Invoice @mAction='SELINVOICE', @mInvoiceNo='{sInvoiceNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                Boolean nReadyforImport;
                string sReason = "";
                while (result.Read())
                {

                    var swa = new TempInvoiceDtl
                    {
                        Id = 0,
                        IPAddress = ipaddress,
                        FKInvoiceNo = result.GetInt32(0),
                        InvoiceNo = sInvoiceNo,
                        InvoiceDt = DateTime.Now,
                        FKMaterial = result.GetInt32(1),
                        FKArticle = result.GetInt32(2),
                        Description = result.GetString(3),
                        Colour = result.GetString(4),
                        Size = result.GetDecimal(5).ToString(),
                        HSNCode = result.GetInt32(6),
                        Quantity = result.GetDecimal(7),
                        IIQuantity = result.GetDecimal(8),
                        Rate = result.GetDecimal(9),
                        Value = result.GetDecimal(10),
                        ValueinINR = result.GetDecimal(11),
                        DiscountPercentage = result.GetDecimal(12),
                        DiscountValue = result.GetDecimal(13),
                        GrossValue = result.GetDecimal(14),
                        SGSTPercentage = result.GetDecimal(15),
                        SGSTValue = result.GetDecimal(16),
                        CGSTPercentage = result.GetDecimal(17),
                        CGSTValue = result.GetDecimal(18),
                        IGSTPercentage = result.GetDecimal(19),
                        IGSTValue = result.GetDecimal(20),
                        GSTTotalValue = result.GetDecimal(21),
                        OthersValuePlus = result.GetDecimal(22),
                        ItemNettValue = result.GetDecimal(23),
                        EANCode = result.GetString(24),
                        ReadyforImport = result.GetBoolean(34),//nReadyforImport,
                        Reason = result.GetString(35),//sReason,
                        OrderReferenceNo = result.GetString(26),
                        FKUnit = result.GetInt32(27),
                        FKLocation = result.GetInt32(28),
                        FKUOM = result.GetInt32(29),
                        FKCurrency = result.GetInt32(30),
                        FKSource = result.GetInt32(31),
                        StockNo = result.GetString(32),
                        FKCustomer = result.GetInt32(33),
                        MaterialorFinishedProduct = result.GetString(36),
                        FKIIUom = 0,
                        OthersValueMinus = 0
                    };
                    tempInvoiceDtls.Add(swa);
                    // 42 Items
                }

                result.Close();

                _db.TempInvoiceDtls.AddRange(tempInvoiceDtls);
                await _db.SaveChangesAsync();

                var TempEANCodeDtls = await _db.TempInvoiceDtlEANCodes.OrderBy(c => c.Id).Where(x => x.InvoiceNo == sInvoiceNo).ToListAsync();
                string sEANCode;
                foreach (var EANCode in TempEANCodeDtls)
                {
                    sEANCode = EANCode.EANCode;
                    //var TempEANCodesFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();

                    if (_db.TempInvoiceDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count > 0)
                    {
                        var TempInwFromdb = await _db.TempInvoiceDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).FirstAsync();
                        nReadyforImport = TempInwFromdb.ReadyforImport;
                        sReason = TempInwFromdb.Reason;
                    }
                    else
                    {
                        nReadyforImport = false;
                        sReason = "Wrong Barcode";

                    }
                    //_db.TempInvoiceDtls.Update(TempEANCodeDtls);
                    var TempEANCodeDtls1 = await _db.TempInvoiceDtlEANCodes.Where(x => x.EANCode == sEANCode).FirstOrDefaultAsync();
                    TempEANCodeDtls1.IsMatching = nReadyforImport;
                    TempEANCodeDtls1.Reason = sReason;
                    await _db.SaveChangesAsync();
                }

                #endregion
            }

            return RedirectToAction("LoadTempInvoiceDtls", "InvoiceToOnlineSites", new { Id = InvoicefromDb.Id });
            //var TempInvoiceDtlsresult = _db.TempInvoiceDtls.ToList();
            //ViewBag.TempInvoiceDtls = _db.TempInvoiceDtls.ToList();
            //ViewBag.TempEANCodeDtls = _db.TempInvoiceDtlEANCodes.Where(x => x.IsMatching == false).ToList();
            //return View(TempInvoiceDtlsresult);
        }

        public async Task<IActionResult> LoadTempInvoiceDtls(int Id)
        {
            TempData["InvoiceId"] = Id;

            var TempInvoiceDtlsresult = await _db.TempInvoiceDtls.ToListAsync();
            ViewBag.TempInvoiceDtls = await _db.TempInvoiceDtls.ToListAsync();
            ViewBag.TempEANCodeDtls = await _db.TempInvoiceDtlEANCodes.Where(x => x.IsMatching == false).ToListAsync();
            return View(TempInvoiceDtlsresult);
        }
        //POST - SCANNED BARCODES
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertInvoiceDetail(int Id)
        {
            //int Id = Convert.ToInt32(TempData["InvoiceId"]);
            var TempInvoiceDtls = await _db.TempInvoiceDtls.OrderBy(c => c.Id).Where(x => x.FKInvoiceNo == Id && x.ReadyforImport == true).ToListAsync();

            int nFKStage, nFKQuality, nFKStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = 0;
            nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;


            foreach (var item in TempInvoiceDtls)
            {
                InvoiceDetailVM.InvoiceDetail.Id = 0;
                InvoiceDetailVM.InvoiceDetail.FKInvoiceNo = item.FKInvoiceNo;
                InvoiceDetailVM.InvoiceDetail.FKMaterial = item.FKMaterial;
                InvoiceDetailVM.InvoiceDetail.FKArticle = item.FKArticle;
                InvoiceDetailVM.InvoiceDetail.Description = item.Description;
                InvoiceDetailVM.InvoiceDetail.Colour = item.Colour;
                InvoiceDetailVM.InvoiceDetail.Size = item.Size.ToString();
                InvoiceDetailVM.InvoiceDetail.HSNCode = item.HSNCode;
                InvoiceDetailVM.InvoiceDetail.Quantity = item.Quantity;
                InvoiceDetailVM.InvoiceDetail.FKUOM = item.FKUOM;
                InvoiceDetailVM.InvoiceDetail.IIQuantity = item.IIQuantity;
                InvoiceDetailVM.InvoiceDetail.Rate = item.Rate;
                InvoiceDetailVM.InvoiceDetail.Value = item.Value;
                InvoiceDetailVM.InvoiceDetail.ValueinINR = item.ValueinINR;
                InvoiceDetailVM.InvoiceDetail.DiscountPercentage = item.DiscountPercentage;
                InvoiceDetailVM.InvoiceDetail.DiscountValue = item.DiscountValue;
                InvoiceDetailVM.InvoiceDetail.GrossValue = item.GrossValue;
                InvoiceDetailVM.InvoiceDetail.SGSTPercentage = item.SGSTPercentage;
                InvoiceDetailVM.InvoiceDetail.SGSTValue = item.SGSTValue;
                InvoiceDetailVM.InvoiceDetail.CGSTPercentage = item.CGSTPercentage;
                InvoiceDetailVM.InvoiceDetail.CGSTValue = item.CGSTValue;
                InvoiceDetailVM.InvoiceDetail.IGSTPercentage = item.IGSTPercentage;
                InvoiceDetailVM.InvoiceDetail.IGSTValue = item.IGSTValue;
                InvoiceDetailVM.InvoiceDetail.GSTTotalValue = item.GSTTotalValue;
                InvoiceDetailVM.InvoiceDetail.OthersValuePlus = item.OthersValuePlus;
                InvoiceDetailVM.InvoiceDetail.ItemNettValue = item.ItemNettValue;
                InvoiceDetailVM.InvoiceDetail.InvoiceNo = item.InvoiceNo;
                InvoiceDetailVM.InvoiceDetail.InvoiceDt = item.InvoiceDt;
                InvoiceDetailVM.InvoiceDetail.EANCode = item.EANCode;
                InvoiceDetailVM.InvoiceDetail.StockNo = item.StockNo;
                InvoiceDetailVM.InvoiceDetail.FKCustomer = item.FKCustomer;
                InvoiceDetailVM.InvoiceDetail.MaterialorFinishedProduct = item.MaterialorFinishedProduct;

                _db.InvoiceDetails.Add(InvoiceDetailVM.InvoiceDetail);
                await _db.SaveChangesAsync();

                int nStckRow = _db.stocks.Where(x => x.EANCode == item.EANCode && x.Rate == item.Rate && x.Quantity > 0 && x.FKUnit == item.FKUnit &&
                x.FKLocation == item.FKLocation && x.FKStatus == nFKStatus).ToList().Count;
                if (nStckRow == 0)
                {
                    //StocksVM.Stock.Id = 0;
                    //StocksVM.Stock.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                    //StocksVM.Stock.FKMaterial = item.FKMaterial;
                    //StocksVM.Stock.FKArticleDetail = item.FKArticle;
                    //StocksVM.Stock.Description = item.Description;
                    //StocksVM.Stock.Colour = item.Colour;
                    //StocksVM.Stock.Size = item.Size.ToString();
                    //StocksVM.Stock.OrderReferenceNo = item.OrderReferenceNo;
                    //StocksVM.Stock.StockInitiatedDate = DateTime.Now;
                    //StocksVM.Stock.FKUnit = item.FKUnit;
                    //StocksVM.Stock.FKLocation = item.FKLocation;
                    //StocksVM.Stock.FKStage = nFKStage;
                    //StocksVM.Stock.Quantity = item.Quantity;
                    //StocksVM.Stock.Rate = item.Rate;
                    //StocksVM.Stock.Value = item.Value;
                    //StocksVM.Stock.FKCurrency = item.FKCurrency;
                    //StocksVM.Stock.ExchangeRate = 0; //TODO
                    //StocksVM.Stock.ValueInINR = item.ValueinINR;
                    //StocksVM.Stock.LandedCost = 0;
                    //StocksVM.Stock.LandedRate = 0;
                    //StocksVM.Stock.FKUOM = item.FKUOM;
                    //StocksVM.Stock.FKIIUOM = 0;
                    //StocksVM.Stock.FKSource = item.FKSource;
                    //StocksVM.Stock.FKQuality = nFKQuality;
                    //StocksVM.Stock.FKStatus = nFKStatus;
                    //StocksVM.Stock.IsActive = true;
                    //StocksVM.Stock.EnteredSystemId = "";
                    //StocksVM.Stock.CreatedBy = 1;
                    //StocksVM.Stock.CreatedDate = DateTime.Now;
                    //StocksVM.Stock.ModifiedBy = 1;
                    //StocksVM.Stock.ModifiedDate = DateTime.Now;
                    //StocksVM.Stock.EANCode = item.EANCode;
                    //StocksVM.Stock.StockNo = item.StockNo;
                    //StocksVM.Stock.FKSupplier = item.FKSupplier;
                    //_db.stocks.Add(StocksVM.Stock);
                    //await _db.SaveChangesAsync();
                }
                else
                {
                    var stck = await _db.stocks.Where(x => x.EANCode == item.EANCode && x.Rate == item.Rate && x.Quantity > 0 && x.FKUnit == item.FKUnit &&
                    x.FKLocation == item.FKLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                    nFKStage = stck.FKStage;
                    stck.Quantity = stck.Quantity - item.Quantity;
                    decimal rate = stck.Rate;
                    decimal value = stck.Quantity * rate;
                    stck.Value = value;
                    stck.ValueInINR = value;
                    stck.LastTranDate = DateTime.Now;
                    _db.SaveChanges();
                }


                int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == item.FKUnit && x.FKTranLocation == item.FKLocation && x.EANCode == item.EANCode).ToList().Count;
                decimal nClosing;
                if (nRowCount == 0)
                {
                    nClosing = 0;
                }
                else
                {
                    var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == item.FKUnit && x.FKTranLocation == item.FKLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
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
                AllTransactionVM.AllTransaction.FKFromUnit = item.FKUnit;
                AllTransactionVM.AllTransaction.FKFromLocation = item.FKLocation;
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
                AllTransactionVM.AllTransaction.FKTranLocation = item.FKLocation;
                AllTransactionVM.AllTransaction.FKTranUnit = item.FKUnit;
                AllTransactionVM.AllTransaction.EANCode = item.EANCode;
                AllTransactionVM.AllTransaction.StockNo = item.StockNo;

                _db.AllTransactions.Add(AllTransactionVM.AllTransaction);
                await _db.SaveChangesAsync();

                //decimal nClosing = _db.AllTransactions.Where(x => x.EANCode == item.EANCode && x.FKSupplier == item.FKSupplier && x.Rate == item.Rate && x.Quantity > 0).ToList().Count;


                var stkWArtfromdb = await _db.stockWithArticles.Where(x => x.EANCode == item.EANCode).FirstAsync();
                stkWArtfromdb.SoldQty = stkWArtfromdb.SoldQty + Convert.ToInt32(item.Quantity);
                stkWArtfromdb.BalQty = stkWArtfromdb.ArrivedQty - stkWArtfromdb.SoldQty;
                _db.SaveChanges();

            }

            var tmpInwdtls = _db.TempInvoiceDtls.Where(x => x.FKInvoiceNo == Id);
            _db.TempInvoiceDtls.RemoveRange(tmpInwdtls);
            await _db.SaveChangesAsync();

            var inv = await _db.Invoices.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var tmpEANCodes = _db.TempInvoiceDtlEANCodes.Where(x => x.InvoiceNo == inv.InvoiceNo);
            _db.TempInvoiceDtlEANCodes.RemoveRange(tmpEANCodes);
            await _db.SaveChangesAsync();

            decimal nInvoiceQty = _db.InvoiceDetails.Where(x => x.FKInvoiceNo == Id).Select(x => x.Quantity).ToList().Sum();
            var InvfromDb = await _db.Invoices.Where(x => x.Id == Id).FirstAsync();
            InvfromDb.Quantity = Convert.ToInt32(nInvoiceQty);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
