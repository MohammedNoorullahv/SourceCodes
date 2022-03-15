using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
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
    public class StockTransferOutController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public StockTransferViewModel stockTransferVM { get; set; }
        public StockTransferDetailViewModel stockTransferDetailVM { get; set; }
        public StocksViewModel StocksVM { get; set; }
        public AllTransactionViewModel AllTransactionVM { get; set; }
        //public static int nFKStockTransfer;
        //public static string sStockTransferNo;

        public static string sScanningMode;
        public static string sIpAddress;
        public static string ipaddress = string.Empty;
        public StockTransferOutController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            stockTransferVM = new StockTransferViewModel()
            {
                FKOutwardType = _db.lookUpMasters,
                FKSeason = _db.seasons,
                FKFromUnit = _db.unitMasters,
                FKFromLocation = _db.locations,
                FKToUnit = _db.unitMasters,
                FKToLocation = _db.locations,
                FKQuality = _db.lookUpMasters,
                StockTransfer = new Models.TransactionTables.StockTransfer()
            };

            stockTransferDetailVM = new StockTransferDetailViewModel()
            {
                FKQuality = _db.lookUpMasters,
                StockTransferDetail = new Models.TransactionTables.StockTransferDetail()
            };

            AllTransactionVM = new AllTransactionViewModel()
            {
                AllTransaction = new Models.TransactionTables.AllTransaction()
            };

            StocksVM = new StocksViewModel()
            {
                Stock = new Models.TransactionTables.Stock()
            };
        }

        #region STOCK TRANSFER OUT
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

            //var delTmpInv = _db.TempInvoiceDtlEANCodes.Where(x => x.IPAddress == ipaddress);
            //_db.TempInvoiceDtlEANCodes.RemoveRange(delTmpInv);
            //await _db.SaveChangesAsync();

            //var delTmpInv1 = _db.TempInvoiceDtls.Where(x => x.IPAddress == ipaddress);
            //_db.TempInvoiceDtls.RemoveRange(delTmpInv1);
            //await _db.SaveChangesAsync();

            return View(await _db.StockTransfers.OrderByDescending(x => x.Id).Where(x => x.STDt >= effectStartDate && x.STDt <= effectEndDate).ToListAsync());
            //return View(await _db.StockTransfers.ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "StockTransferOut", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            stockTransferVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            List<LookUpMaster> lkInvTypes = new List<LookUpMaster>();
            string sqlQuery1 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='44'";
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

            stockTransferVM.FKOutwardType = lkInvTypes.ToList();
            //stockTransferVM.FKOutwardType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            stockTransferVM.FKFromUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            //stockTransferVM.FKFromLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
            stockTransferVM.FKToUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            //stockTransferVM.FKToLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
            stockTransferVM.FKQuality = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 41 && c.IsActive == true).ToListAsync();

            return View(stockTransferVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string Save, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                stockTransferVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
                stockTransferVM.FKOutwardType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
                stockTransferVM.FKFromUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
                stockTransferVM.FKFromLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
                stockTransferVM.FKToUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
                stockTransferVM.FKToLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
                stockTransferVM.FKQuality = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 41 && c.IsActive == true).ToListAsync();

                return View(stockTransferVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sTypeofOrder = lookUpMaster.Where(x => x.Id == stockTransferVM.StockTransfer.FKOutwardType).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(stockTransferVM.StockTransfer.FKSeason);
            string sSeason = season.Code;

            string codechar = (sTypeofOrder.Substring(0, 2) + sSeason.Substring(0, 4));
            var maxcode = 0;

            if (_db.StockTransfers.Where(x => x.STNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.StockTransfers.Where(x => x.STNo.Contains(codechar)).Select(x => int.Parse(x.STNo.Substring(7, 4))).ToList().Max();
            }

            stockTransferVM.StockTransfer.STNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            var unitmaster = await _db.unitMasters.ToListAsync();
            var location = await _db.locations.ToListAsync();
            stockTransferVM.StockTransfer.FromUnitName = unitmaster.Where(x => x.Id == stockTransferVM.StockTransfer.FKFromUnit).FirstOrDefault().CompanyName;
            stockTransferVM.StockTransfer.FromLocation = location.Where(x => x.Id == stockTransferVM.StockTransfer.FKFromLocation).FirstOrDefault().LocationName;
            stockTransferVM.StockTransfer.FKFromState = unitmaster.Where(x => x.Id == stockTransferVM.StockTransfer.FKFromUnit).FirstOrDefault().FKState;

            stockTransferVM.StockTransfer.ToUnitName = unitmaster.Where(x => x.Id == stockTransferVM.StockTransfer.FKToUnit).FirstOrDefault().CompanyName;
            stockTransferVM.StockTransfer.ToLocation = location.Where(x => x.Id == stockTransferVM.StockTransfer.FKToLocation).FirstOrDefault().LocationName;
            stockTransferVM.StockTransfer.FKToState = unitmaster.Where(x => x.Id == stockTransferVM.StockTransfer.FKFromUnit).FirstOrDefault().FKState;
            stockTransferVM.StockTransfer.Quality = lookUpMaster.Where(x => x.Id == stockTransferVM.StockTransfer.FKQuality).FirstOrDefault().Description;

            stockTransferVM.StockTransfer.Season = season.Code;

            _db.StockTransfers.Add(stockTransferVM.StockTransfer);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Create));
            if (Save == "Save & New Trn")
            {
                return RedirectToAction(nameof(Create));
            }
            else if (Save == "Save & Insert Dtl")
            {
                var stktrn = await _db.StockTransfers.Where(x => x.STNo == stockTransferVM.StockTransfer.STNo).FirstOrDefaultAsync();
                return RedirectToAction("CreateTransferDetailForScanning", "StockTransferOut", new { Id = stktrn.Id });
            }
            else
            {
                var stktrn = await _db.StockTransfers.Where(x => x.STNo == stockTransferVM.StockTransfer.STNo).FirstOrDefaultAsync();

                var scndlist = _db.TempTransferDtlEANCodes.Where(x => x.IPAddress == ipaddress);
                _db.TempTransferDtlEANCodes.RemoveRange(scndlist);
                await _db.SaveChangesAsync();

                var tmpInvLst = _db.TempTransferDtls.Where(x => x.FKTransferNo == stktrn.Id);
                _db.TempTransferDtls.RemoveRange(tmpInvLst);
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

                TempTransferDtlEANCode model = new TempTransferDtlEANCode();

                var TransferfromDb = await _db.StockTransfers.FindAsync(stktrn.Id);
                string sTransferNo = TransferfromDb.STNo;
                int nFKLocation = TransferfromDb.FKFromLocation;
                int nFKUnit = TransferfromDb.FKFromUnit;

                int nFKStatus;
                nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;

                foreach (var EANCode in scannedlist)
                {
                    int nRowCnt = _db.stocks.Where(x => x.EANCode == EANCode || x.StockNo == EANCode).ToList().Count;
                    if (nRowCnt == 0)
                    {
                        model.IPAddress = ipaddress;
                        model.TransferNo = sTransferNo;
                        model.EANCode = EANCode;
                        model.Quantity = 1;
                        model.IsMatching = false;
                        model.Reason = "Invalid Barcode";
                        model.Id = 0;
                        _db.TempTransferDtlEANCodes.Add(model);
                        _db.SaveChanges();
                    }
                    else
                    {

                        var nStockQty = _db.stocks.Where(x => (x.EANCode == EANCode || x.StockNo == EANCode) && x.FKUnit == nFKUnit && x.FKLocation == nFKLocation && x.FKStatus == nFKStatus).Select(x => x.Quantity).ToList().Sum();
                        if (nStockQty > 0)
                        {
                            int nRowCount = _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode).ToList().Count;
                            if (nRowCount > 0)
                            {
                                if (nRowCount == 1)
                                {
                                    var TempArriEAN = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                    decimal nExistingQty = TempArriEAN.Quantity;

                                    if (nExistingQty + 1 <= nStockQty)
                                    {
                                        var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        int nRowCount1 = _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                                        if (nRowCount1 == 0)
                                        {
                                            model.IPAddress = ipaddress;
                                            model.TransferNo = sTransferNo;
                                            model.EANCode = EANCode;
                                            model.Quantity = 1;
                                            model.IsMatching = false;
                                            model.Id = 0;
                                            model.Reason = "Part X's Qty";
                                            _db.TempTransferDtlEANCodes.Add(model);
                                            await _db.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                            TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                            _db.SaveChanges();
                                        }

                                    }
                                }
                                else
                                {
                                    var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                    TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                    _db.SaveChanges();
                                }
                            }
                            else
                            {
                                model.IPAddress = ipaddress;
                                model.TransferNo = sTransferNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = true;
                                model.Reason = "";
                                model.Id = 0;
                                _db.TempTransferDtlEANCodes.Add(model);
                                _db.SaveChanges();
                            }
                        }
                        else
                        {
                            int nRowCount1 = _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                            if (nRowCount1 == 0)
                            {
                                model.IPAddress = ipaddress;
                                model.TransferNo = sTransferNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = false;
                                model.Id = 0;
                                model.Reason = "X's Qty";
                                _db.TempTransferDtlEANCodes.Add(model);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                _db.SaveChanges();
                            }
                        }

                    }

                    //model.Id = 0;
                };

                #region UPDATING TEMPORARY Transfer DETAILS
                int nFromState, nToState;
                decimal dGSTPercentage;
                List<TempTransferDtl> tempTransferDtls = new List<TempTransferDtl>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_Transfer @mAction='SELTransfer', @mTransferNo='{sTransferNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                Boolean nReadyforImport;
                string sReason = "";
                while (result.Read())
                {
                    nFromState = result.GetInt32(32);
                    nToState = result.GetInt32(33);
                    dGSTPercentage = result.GetDecimal(34);
                    decimal dMRP = result.GetDecimal(9);
                    dMRP = (dMRP / (100 + dGSTPercentage)) * 100;
                    decimal dValue = result.GetDecimal(7) * dMRP;
                    decimal dValueinINR = dValue;
                    decimal dDiscountPercentage = 0;
                    decimal dDiscountValue = 0;
                    decimal dGrossValue = dValueinINR - dDiscountValue;
                    //dGrossValue = (dGrossValue / (100 + dGSTPercentage)) * 100;
                    decimal dSGSTPercentage, dCGSTPercentage, dIGSTPercentage;
                    decimal dSGSTValue, dCGSTValue, dIGSTValue, dGSTValue;
                    decimal dItemNettValue;
                    if (nFromState == nToState)
                    {
                        dSGSTPercentage = dGSTPercentage / 2;
                        dCGSTPercentage = dGSTPercentage / 2;
                        dIGSTPercentage = 0;
                    }
                    else
                    {
                        dSGSTPercentage = 0;
                        dCGSTPercentage = 0;
                        dIGSTPercentage = dGSTPercentage;
                    }
                    dSGSTValue = dGrossValue * dSGSTPercentage / 100;
                    dCGSTValue = dGrossValue * dCGSTPercentage / 100;
                    dIGSTValue = dGrossValue * dIGSTPercentage / 100;
                    dGSTValue = dSGSTValue + dCGSTValue + dIGSTValue;
                    dItemNettValue = dGrossValue + dGSTValue;
                    var swa = new TempTransferDtl
                    {
                        Id = 0,
                        IPAddress = ipaddress,
                        FKTransferNo = result.GetInt32(0),
                        TransferNo = sTransferNo,
                        TransferDt = DateTime.Now,
                        FKMaterial = result.GetInt32(1),
                        FKArticle = result.GetInt32(2),
                        Description = result.GetString(3),
                        Colour = result.GetString(4),
                        Size = result.GetDecimal(5).ToString(),
                        HSNCode = result.GetInt32(6),
                        Quantity = result.GetDecimal(7),
                        IIQuantity = result.GetDecimal(8),
                        Rate = dMRP, //Rate = result.GetDecimal(9),
                        Value = dValue, //Value = result.GetDecimal(10),
                        ValueinINR = dValueinINR,//ValueinINR = result.GetDecimal(11),
                        DiscountPercentage = dDiscountPercentage, //DiscountPercentage = result.GetDecimal(12),
                        DiscountValue = dDiscountValue, //DiscountValue = result.GetDecimal(13),
                        GrossValue = dGrossValue, //GrossValue = result.GetDecimal(14),
                        SGSTPercentage = dSGSTPercentage, //SGSTPercentage = result.GetDecimal(15),
                        SGSTValue = dSGSTValue, //SGSTValue = result.GetDecimal(16),
                        CGSTPercentage = dCGSTPercentage, //CGSTPercentage = result.GetDecimal(17),
                        CGSTValue = dCGSTValue,  //CGSTValue = result.GetDecimal(18),
                        IGSTPercentage = dIGSTPercentage, //IGSTPercentage = result.GetDecimal(19),
                        IGSTValue = dIGSTValue, //IGSTValue = result.GetDecimal(20),
                        GSTTotalValue = dGSTValue, //GSTTotalValue = result.GetDecimal(21),
                        OthersValuePlus = result.GetDecimal(19),
                        ItemNettValue = dItemNettValue, //ItemNettValue = result.GetDecimal(23),
                        EANCode = result.GetString(21),
                        ReadyforImport = result.GetBoolean(30),//nReadyforImport,
                        Reason = result.GetString(31),//sReason,
                        OrderReferenceNo = result.GetString(23),
                        FKUnit = result.GetInt32(24),
                        FKLocation = result.GetInt32(25),
                        FKUOM = result.GetInt32(26),
                        FKCurrency = 0,// result.GetInt32(30),
                        FKSource = 0,//result.GetInt32(31),
                        StockNo = result.GetString(29),
                        FKCustomer = result.GetInt32(27),
                        MaterialorFinishedProduct = result.GetString(36),
                        FKIIUom = 0,
                        OthersValueMinus = 0
                    };
                    tempTransferDtls.Add(swa);
                    // 42 Items
                }

                result.Close();

                _db.TempTransferDtls.AddRange(tempTransferDtls);
                await _db.SaveChangesAsync();

                var TempEANCodeDtls = await _db.TempTransferDtlEANCodes.OrderBy(c => c.Id).Where(x => x.TransferNo == sTransferNo).ToListAsync();
                string sEANCode;
                foreach (var EANCode in TempEANCodeDtls)
                {
                    sEANCode = EANCode.EANCode;
                    //var TempEANCodesFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();

                    if (_db.TempTransferDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count > 0)
                    {
                        var TempInwFromdb = await _db.TempTransferDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).FirstAsync();
                        nReadyforImport = TempInwFromdb.ReadyforImport;
                        sReason = TempInwFromdb.Reason;
                    }
                    else
                    {
                        nReadyforImport = false;
                        sReason = "Wrong Barcode";

                    }
                    //_db.TempTransferDtls.Update(TempEANCodeDtls);
                    var TempEANCodeDtls1 = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == sEANCode).FirstOrDefaultAsync();
                    TempEANCodeDtls1.IsMatching = nReadyforImport;
                    TempEANCodeDtls1.Reason = sReason;
                    await _db.SaveChangesAsync();
                }

                #endregion

                return RedirectToAction("LoadTempTransferDtls", "StockTransferOut", new { Id = stktrn.Id });
            }
        }

        #endregion

        #region STOCK TRANSFER DETAIL OUT
        public async Task<IActionResult> STDetailsIndex(int Id)
        {
            var st = await _db.StockTransfers.FindAsync(Id);
            TempData["StockTransfer"] = st;

            return View(await _db.StockTransferDetails.Where(x => x.FKSTNo == Id).ToListAsync());
        }


        #region "BULK TRANSACTION"
        //GET - CREATE Invoice DETAIL For Scanning
        public async Task<IActionResult> CreateTransferDetailForScanning(int Id)
        {
            var trn = await _db.StockTransfers.FindAsync(Id);
            TempData["Transfer"] = trn;

            TempData["TransferId"] = Id;
            if (sScanningMode != "Re Entries")
                sScanningMode = "New Entries";

            TempData["ScanningMode"] = sScanningMode;

            decimal nRowCount1 = _db.StockTransferDetails.Where(x => x.FKSTNo == Id).Select(x => x.DispatchedQuantity).ToList().Sum();
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

            var tmpInwdtls = _db.TempTransferDtls.Where(x => x.IPAddress == ipaddress);
            _db.TempTransferDtls.RemoveRange(tmpInwdtls);
            await _db.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> CreateTransferDetailForReScanning(int Id)
        {
            //TODO: To remove the Wrong Barcodes in the Temporary List
            var scndlist = await _db.TempTransferDtlEANCodes.Where(x => x.IPAddress == ipaddress && x.IsMatching == false).ToListAsync();
            _db.TempTransferDtlEANCodes.RemoveRange(scndlist);
            await _db.SaveChangesAsync();

            sScanningMode = "Re Entries";
            return RedirectToAction("CreateTransferDetailForScanning", "StockTransferOut", new { Id = Id });
        }

        //POST - SCANNED BARCODES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferDetailScannedCodes(TempTransferDtlEANCode model)
        {
            var TransferfromDb = await _db.StockTransfers.FindAsync(Convert.ToInt32(TempData["TransferId"]));
            if (sScanningMode != "Updates")
            {
                if (sScanningMode == "New Entries")
                {
                    var scndlist = _db.TempTransferDtlEANCodes.Where(x => x.IPAddress == ipaddress);
                    _db.TempTransferDtlEANCodes.RemoveRange(scndlist);
                    await _db.SaveChangesAsync();
                }
                var scannedlist = Request.Form["liContent"];
                string sScanningMod = Request.Form["ScanningMode"];

                //if (Convert.ToInt32(TempData["TransferId"]) != 0 )
                //{
                //    nFKTransfer = 
                //}
                string sTransferNo = TransferfromDb.STNo;
                int nFKLocation = TransferfromDb.FKFromLocation;
                int nFKUnit = TransferfromDb.FKFromUnit;

                int nFKStatus;
                var lookUpMaster = await _db.lookUpMasters.ToListAsync();
                nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;

                foreach (var EANCode in scannedlist)
                {
                    int nRowCnt = _db.stocks.Where(x => x.EANCode == EANCode || x.StockNo == EANCode).ToList().Count;
                    if (nRowCnt == 0)
                    {
                        model.IPAddress = ipaddress;
                        model.TransferNo = sTransferNo;
                        model.EANCode = EANCode;
                        model.Quantity = 1;
                        model.IsMatching = false;
                        model.Reason = "Invalid Barcode";
                        model.Id = 0;
                        _db.TempTransferDtlEANCodes.Add(model);
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
                            int nRowCount = _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode).ToList().Count;
                            if (nRowCount > 0)
                            {
                                if (nRowCount == 1)
                                {
                                    var TempArriEAN = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                    decimal nExistingQty = TempArriEAN.Quantity;

                                    //var stockwithArticle = await _db.stockWithArticles.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                    //int nBaltoReceive = stockwithArticle.Quantity - stockwithArticle.ArrivedQty;

                                    if (nExistingQty + 1 <= nStockQty)
                                    {
                                        var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                        //_db.TempArticalArrivalEANCodes.Update(TempFromdb);
                                        //_db.Entry(TempFromdb).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        int nRowCount1 = _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                                        if (nRowCount1 == 0)
                                        {
                                            model.IPAddress = ipaddress;
                                            model.TransferNo = sTransferNo;
                                            model.EANCode = EANCode;
                                            model.Quantity = 1;
                                            model.IsMatching = false;
                                            model.Id = 0;
                                            model.Reason = "Part X's Qty";
                                            _db.TempTransferDtlEANCodes.Add(model);
                                            await _db.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                            TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                            _db.SaveChanges();
                                        }

                                    }
                                }
                                else
                                {
                                    var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                    TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                    _db.SaveChanges();
                                }

                                //partyInfo.Where(x => x.Id == TransferVM.Transfer.FKParty).FirstOrDefault().CompanyName;
                                //var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();
                                //TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                //_db.SaveChanges();
                            }
                            else
                            {
                                model.IPAddress = ipaddress;
                                model.TransferNo = sTransferNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = true;
                                model.Reason = "";
                                model.Id = 0;
                                _db.TempTransferDtlEANCodes.Add(model);
                                _db.SaveChanges();
                                //await _db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            int nRowCount1 = _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                            if (nRowCount1 == 0)
                            {
                                model.IPAddress = ipaddress;
                                model.TransferNo = sTransferNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = false;
                                model.Id = 0;
                                model.Reason = "X's Qty";
                                _db.TempTransferDtlEANCodes.Add(model);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                var TempFromdb = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                _db.SaveChanges();
                            }
                        }

                    }

                    //model.Id = 0;
                };

                #region UPDATING TEMPORARY Transfer DETAILS

                int nFromState, nToState;
                decimal dGSTPercentage;
                List<TempTransferDtl> tempTransferDtls = new List<TempTransferDtl>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_Transfer @mAction='SELTRANSFER', @mTransferNo='{sTransferNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                Boolean nReadyforImport;
                string sReason = "";
                while (result.Read())
                {
                    nFromState = result.GetInt32(32);
                    nToState = result.GetInt32(33);
                    dGSTPercentage = result.GetDecimal(34);
                    decimal dMRP = result.GetDecimal(9);
                    dMRP = (dMRP / (100 + dGSTPercentage)) * 100;
                    decimal dValue = result.GetDecimal(7) * dMRP;
                    decimal dValueinINR = dValue;
                    decimal dDiscountPercentage = 0;
                    decimal dDiscountValue = 0;
                    decimal dGrossValue = dValueinINR - dDiscountValue;
                    //dGrossValue = (dGrossValue / (100 + dGSTPercentage)) * 100;
                    decimal dSGSTPercentage, dCGSTPercentage, dIGSTPercentage;
                    decimal dSGSTValue, dCGSTValue, dIGSTValue, dGSTValue;
                    decimal dItemNettValue;
                    if (nFromState == nToState)
                    {
                        dSGSTPercentage = dGSTPercentage / 2;
                        dCGSTPercentage = dGSTPercentage / 2;
                        dIGSTPercentage = 0;
                    }
                    else
                    {
                        dSGSTPercentage = 0;
                        dCGSTPercentage = 0;
                        dIGSTPercentage = dGSTPercentage;
                    }
                    dSGSTValue = dGrossValue * dSGSTPercentage / 100;
                    dCGSTValue = dGrossValue * dCGSTPercentage / 100;
                    dIGSTValue = dGrossValue * dIGSTPercentage / 100;
                    dGSTValue = dSGSTValue + dCGSTValue + dIGSTValue;
                    dItemNettValue = dGrossValue + dGSTValue;
                    var swa = new TempTransferDtl
                    {
                        Id = 0,
                        IPAddress = ipaddress,
                        FKTransferNo = result.GetInt32(0),
                        TransferNo = sTransferNo,
                        TransferDt = DateTime.Now,
                        FKMaterial = result.GetInt32(1),
                        FKArticle = result.GetInt32(2),
                        Description = result.GetString(3),
                        Colour = result.GetString(4),
                        Size = result.GetDecimal(5).ToString(),
                        HSNCode = result.GetInt32(6),
                        Quantity = result.GetDecimal(7),
                        IIQuantity = result.GetDecimal(8),
                        Rate = dMRP, //Rate = result.GetDecimal(9),
                        Value = dValue, //Value = result.GetDecimal(10),
                        ValueinINR = dValueinINR,//ValueinINR = result.GetDecimal(11),
                        DiscountPercentage = dDiscountPercentage, //DiscountPercentage = result.GetDecimal(12),
                        DiscountValue = dDiscountValue, //DiscountValue = result.GetDecimal(13),
                        GrossValue = dGrossValue, //GrossValue = result.GetDecimal(14),
                        SGSTPercentage = dSGSTPercentage, //SGSTPercentage = result.GetDecimal(15),
                        SGSTValue = dSGSTValue, //SGSTValue = result.GetDecimal(16),
                        CGSTPercentage = dCGSTPercentage, //CGSTPercentage = result.GetDecimal(17),
                        CGSTValue = dCGSTValue,  //CGSTValue = result.GetDecimal(18),
                        IGSTPercentage = dIGSTPercentage, //IGSTPercentage = result.GetDecimal(19),
                        IGSTValue = dIGSTValue, //IGSTValue = result.GetDecimal(20),
                        GSTTotalValue = dGSTValue, //GSTTotalValue = result.GetDecimal(21),
                        OthersValuePlus = result.GetDecimal(19),
                        ItemNettValue = dItemNettValue, //ItemNettValue = result.GetDecimal(23),
                        EANCode = result.GetString(21),
                        ReadyforImport = result.GetBoolean(30),//nReadyforImport,
                        Reason = result.GetString(31),//sReason,
                        OrderReferenceNo = result.GetString(23),
                        FKUnit = result.GetInt32(24),
                        FKLocation = result.GetInt32(25),
                        FKUOM = result.GetInt32(26),
                        FKCurrency = 0,// result.GetInt32(30),
                        FKSource = 0,//result.GetInt32(31),
                        StockNo = result.GetString(29),
                        FKCustomer = result.GetInt32(27),
                        MaterialorFinishedProduct = result.GetString(36),
                        FKIIUom = 0,
                        OthersValueMinus = 0,
                        FKQuality = TransferfromDb.FKQuality
                    };
                    tempTransferDtls.Add(swa);
                    // 42 Items
                }

                result.Close();

                _db.TempTransferDtls.AddRange(tempTransferDtls);
                await _db.SaveChangesAsync();

                var TempEANCodeDtls = await _db.TempTransferDtlEANCodes.OrderBy(c => c.Id).Where(x => x.TransferNo == sTransferNo).ToListAsync();
                string sEANCode;
                foreach (var EANCode in TempEANCodeDtls)
                {
                    sEANCode = EANCode.EANCode;
                    //var TempEANCodesFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();

                    if (_db.TempTransferDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count > 0)
                    {
                        var TempInwFromdb = await _db.TempTransferDtls.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode).FirstAsync();
                        nReadyforImport = TempInwFromdb.ReadyforImport;
                        sReason = TempInwFromdb.Reason;
                    }
                    else
                    {
                        nReadyforImport = false;
                        sReason = "Wrong Barcode";

                    }
                    //_db.TempTransferDtls.Update(TempEANCodeDtls);
                    var TempEANCodeDtls1 = await _db.TempTransferDtlEANCodes.Where(x => x.EANCode == sEANCode).FirstOrDefaultAsync();
                    TempEANCodeDtls1.IsMatching = nReadyforImport;
                    TempEANCodeDtls1.Reason = sReason;
                    await _db.SaveChangesAsync();
                }

                #endregion
            }

            return RedirectToAction("LoadTempTransferDtls", "StockTransferOut", new { Id = TransferfromDb.Id });
            //var TempTransferDtlsresult = _db.TempTransferDtls.ToList();
            //ViewBag.TempTransferDtls = _db.TempTransferDtls.ToList();
            //ViewBag.TempEANCodeDtls = _db.TempTransferDtlEANCodes.Where(x => x.IsMatching == false).ToList();
            //return View(TempTransferDtlsresult);
        }

        public async Task<IActionResult> LoadTempTransferDtls(int Id)
        {
            TempData["TransferId"] = Id;

            var TempTransferDtlsresult = await _db.TempTransferDtls.ToListAsync();
            ViewBag.TempTransferDtls = await _db.TempTransferDtls.ToListAsync();
            ViewBag.TempEANCodeDtls = await _db.TempTransferDtlEANCodes.Where(x => x.IsMatching == false).ToListAsync();
            return View(TempTransferDtlsresult);
        }

        //POST - SCANNED BARCODES
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertTransferDetail(int Id)
        {
            //int Id = Convert.ToInt32(TempData["TransferId"]);
            var TempTransferDtls = await _db.TempTransferDtls.OrderBy(c => c.Id).Where(x => x.FKTransferNo == Id && x.ReadyforImport == true).ToListAsync();
            var transfer = await _db.StockTransfers.OrderBy(c => c.Id).Where(x => x.Id == Id).FirstOrDefaultAsync();

            int nFKStage,  nFKStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = 0;
            //nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;


            foreach (var item in TempTransferDtls)
            {

                stockTransferDetailVM.StockTransferDetail.Id = 0;
                stockTransferDetailVM.StockTransferDetail.FKSTNo = item.FKTransferNo;
                stockTransferDetailVM.StockTransferDetail.FKMaterial = item.FKMaterial;
                stockTransferDetailVM.StockTransferDetail.FKArticleDetail = item.FKArticle;
                stockTransferDetailVM.StockTransferDetail.Description = item.Description;
                stockTransferDetailVM.StockTransferDetail.Colour = item.Colour;
                stockTransferDetailVM.StockTransferDetail.Size = item.Size.ToString();
                stockTransferDetailVM.StockTransferDetail.HSNCode = item.HSNCode;
                stockTransferDetailVM.StockTransferDetail.DispatchedQuantity = item.Quantity;
                stockTransferDetailVM.StockTransferDetail.FKUOM = item.FKUOM;
                stockTransferDetailVM.StockTransferDetail.IIQuantity = item.IIQuantity;
                stockTransferDetailVM.StockTransferDetail.Rate = item.Rate;
                stockTransferDetailVM.StockTransferDetail.Value = item.Value;
                stockTransferDetailVM.StockTransferDetail.SGSTPercentage = item.SGSTPercentage;
                stockTransferDetailVM.StockTransferDetail.SGSTValue = item.SGSTValue;
                stockTransferDetailVM.StockTransferDetail.CGSTPercentage = item.CGSTPercentage;
                stockTransferDetailVM.StockTransferDetail.CGSTValue = item.CGSTValue;
                stockTransferDetailVM.StockTransferDetail.IGSTPercentage = item.IGSTPercentage;
                stockTransferDetailVM.StockTransferDetail.IGSTValue = item.IGSTValue;
                stockTransferDetailVM.StockTransferDetail.GSTTotalValue = item.GSTTotalValue;
                stockTransferDetailVM.StockTransferDetail.OthersValuePlus = item.OthersValuePlus;
                stockTransferDetailVM.StockTransferDetail.ItemNettValue = item.ItemNettValue;
                stockTransferDetailVM.StockTransferDetail.STNo = item.TransferNo;
                stockTransferDetailVM.StockTransferDetail.STDt = item.TransferDt;
                stockTransferDetailVM.StockTransferDetail.EANCode = item.EANCode;
                stockTransferDetailVM.StockTransferDetail.StockNo = item.StockNo;
                //stockTransferDetailVM.StockTransferDetail.FKCustomer = item.FKCustomer;
                stockTransferDetailVM.StockTransferDetail.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                stockTransferDetailVM.StockTransferDetail.FKQuality = item.FKQuality;

                _db.StockTransferDetails.Add(stockTransferDetailVM.StockTransferDetail);
                await _db.SaveChangesAsync();

                string sEANCode = item.EANCode;
                decimal nDispatchQuantity = item.Quantity;
                int nFKFromUnit = transfer.FKFromUnit;
                int nFKFromLocation = transfer.FKFromLocation;
                int nFKToUnit = transfer.FKToUnit;
                int nFKToLocation = transfer.FKToLocation;

                #region DEDUCTION OF STOCK FROM TRANSFERRED LOCATION
                var stockAtFromLocation = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKFromUnit && x.FKLocation == nFKFromLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                stockAtFromLocation.Quantity = stockAtFromLocation.Quantity - nDispatchQuantity;
                decimal rate = stockAtFromLocation.Rate;
                decimal value = stockAtFromLocation.Quantity * rate;
                stockAtFromLocation.Value = value;
                stockAtFromLocation.ValueInINR = value;
                _db.SaveChanges();

                int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == nFKFromUnit && x.FKTranLocation == nFKFromLocation && x.EANCode == item.EANCode).ToList().Count;
                decimal nClosing;
                if (nRowCount == 0)
                {
                    nClosing = 0;
                }
                else
                {
                    var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == nFKFromUnit && x.FKTranLocation == nFKFromLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
                    nClosing = AllTran.BalanceQuantity;
                }

                AllTransactionVM.AllTransaction.Id = 0;
                AllTransactionVM.AllTransaction.TransactionType = "Stock Transfer Out";
                AllTransactionVM.AllTransaction.TranId = item.FKTransferNo;
                AllTransactionVM.AllTransaction.TranRefNo = transfer.STNo;
                AllTransactionVM.AllTransaction.TranDate = transfer.STDt;
                AllTransactionVM.AllTransaction.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                AllTransactionVM.AllTransaction.FKMaterial = item.FKMaterial;
                AllTransactionVM.AllTransaction.FKArticle = item.FKArticle;
                AllTransactionVM.AllTransaction.Description = item.Description;
                AllTransactionVM.AllTransaction.Colour = item.Colour;
                AllTransactionVM.AllTransaction.Size = item.Size.ToString();
                AllTransactionVM.AllTransaction.FKFromUnit = nFKFromUnit;
                AllTransactionVM.AllTransaction.FKFromLocation = nFKFromLocation;
                AllTransactionVM.AllTransaction.FKFromStage = nFKStage;
                AllTransactionVM.AllTransaction.FKToUnit = nFKToUnit;
                AllTransactionVM.AllTransaction.FKToLocation = nFKToLocation;
                AllTransactionVM.AllTransaction.FKToStage = nFKStage;
                AllTransactionVM.AllTransaction.FKQuality = item.FKQuality;
                AllTransactionVM.AllTransaction.HSNCode = item.HSNCode;
                AllTransactionVM.AllTransaction.FKUOM = item.FKUOM;
                AllTransactionVM.AllTransaction.InwardQuantity = 0;
                AllTransactionVM.AllTransaction.OutwardQuantity = nDispatchQuantity;
                AllTransactionVM.AllTransaction.BalanceQuantity = nClosing - nDispatchQuantity;
                AllTransactionVM.AllTransaction.Rate = item.Rate;
                AllTransactionVM.AllTransaction.Value = item.Value;
                AllTransactionVM.AllTransaction.FKStatus = nFKStatus;
                AllTransactionVM.AllTransaction.IsActive = true;
                AllTransactionVM.AllTransaction.CreatedBy = 0;
                AllTransactionVM.AllTransaction.CreatedDate = DateTime.Now;
                AllTransactionVM.AllTransaction.FKTranLocation = nFKFromLocation;
                AllTransactionVM.AllTransaction.FKTranUnit = nFKFromUnit;
                AllTransactionVM.AllTransaction.EANCode = item.EANCode;
                AllTransactionVM.AllTransaction.StockNo = item.StockNo;

                _db.AllTransactions.Add(AllTransactionVM.AllTransaction);
                await _db.SaveChangesAsync();
                #endregion

                #region INCREMENT OF STOCK @ RECEIVED LOCATION
                int nFKTransitStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.Description.ToUpper() == "IN TRANSIT").FirstOrDefault().Id;
                int nStckRow = _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKTransitStatus).ToList().Count;
                if (nStckRow == 0)
                {
                    StocksVM.Stock.Id = 0;
                    StocksVM.Stock.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                    StocksVM.Stock.FKMaterial = item.FKMaterial;
                    StocksVM.Stock.FKArticleDetail = item.FKArticle;
                    StocksVM.Stock.Description = item.Description;
                    StocksVM.Stock.Colour = item.Colour;
                    StocksVM.Stock.Size = item.Size.ToString();
                    StocksVM.Stock.OrderReferenceNo = stockAtFromLocation.OrderReferenceNo;
                    StocksVM.Stock.StockInitiatedDate = DateTime.Now;
                    StocksVM.Stock.FKUnit = nFKToUnit;
                    StocksVM.Stock.FKLocation = nFKToLocation;
                    StocksVM.Stock.FKStage = stockAtFromLocation.FKStage;
                    StocksVM.Stock.Quantity = item.Quantity;
                    StocksVM.Stock.Rate = item.Rate;
                    StocksVM.Stock.Value = item.Value;
                    StocksVM.Stock.FKCurrency = stockAtFromLocation.FKCurrency;
                    StocksVM.Stock.ExchangeRate = 1; //TODO
                    StocksVM.Stock.ValueInINR = item.Value;
                    StocksVM.Stock.LandedCost = 0;
                    StocksVM.Stock.LandedRate = 0;
                    StocksVM.Stock.FKUOM = item.FKUOM;
                    StocksVM.Stock.FKIIUOM = 0;
                    StocksVM.Stock.FKSource = stockAtFromLocation.FKSource;
                    StocksVM.Stock.FKQuality = item.FKQuality;
                    StocksVM.Stock.FKStatus = nFKTransitStatus;
                    StocksVM.Stock.IsActive = true;
                    StocksVM.Stock.CreatedBy = 1;
                    StocksVM.Stock.CreatedDate = DateTime.Now;
                    StocksVM.Stock.EANCode = item.EANCode;
                    StocksVM.Stock.StockNo = item.StockNo;
                    StocksVM.Stock.FKSupplier = stockAtFromLocation.FKSupplier;
                    StocksVM.Stock.FKHSNCode = stockAtFromLocation.FKHSNCode;
                    StocksVM.Stock.DiscountPercentageforSales = stockAtFromLocation.DiscountPercentageforSales;
                    StocksVM.Stock.MRP = stockAtFromLocation.MRP;
                    StocksVM.Stock.FKOffer = 0;
                    StocksVM.Stock.OfferType = "";
                    StocksVM.Stock.FKCategory = stockAtFromLocation.FKCategory;
                    StocksVM.Stock.FLAM = stockAtFromLocation.FLAM;
                    StocksVM.Stock.LastTranDate = DateTime.Now;

                    _db.stocks.Add(StocksVM.Stock);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var stockAtToLocation = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKTransitStatus).FirstOrDefaultAsync();
                    stockAtToLocation.Quantity = stockAtToLocation.Quantity + nDispatchQuantity;
                    decimal rate1 = stockAtToLocation.Rate;
                    decimal value1 = stockAtFromLocation.Quantity * rate;
                    stockAtFromLocation.Value = value1;
                    stockAtFromLocation.ValueInINR = value1;
                    _db.SaveChanges();
                }
            }

            //var stDetails = await _db.StockTransferDetails.Where(x => x.FKSTNo == nFKStockTransfer).FirstOrDefaultAsync();
            //stDetails.IsEntryCompleted = true;
            //_db.SaveChanges();

            //var stockTransfer = await _db.StockTransfers.Where(x => x.Id == nFKStockTransfer).FirstOrDefaultAsync();
            //stockTransfer.IsEntryCompleted = true;
            //_db.SaveChanges();
            #endregion

            ////////int nStckRow = _db.stocks.Where(x => x.EANCode == item.EANCode && x.Rate == item.Rate && x.Quantity > 0 && x.FKUnit == item.FKUnit &&
            ////////x.FKLocation == item.FKLocation && x.FKStatus == nFKStatus).ToList().Count;
            ////////if (nStckRow == 0)
            ////////{

            ////////}
            ////////else
            ////////{
            ////////    var stck = await _db.stocks.Where(x => x.EANCode == item.EANCode && x.Rate == item.Rate && x.Quantity > 0 && x.FKUnit == item.FKUnit &&
            ////////    x.FKLocation == item.FKLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
            ////////    nFKStage = stck.FKStage;
            ////////    stck.Quantity = stck.Quantity - item.Quantity;
            ////////    decimal rate = stck.Rate;
            ////////    decimal value = stck.Quantity * rate;
            ////////    stck.Value = value;
            ////////    stck.ValueInINR = value;
            ////////    stck.LastTranDate = DateTime.Now;
            ////////    _db.SaveChanges();
            ////////}


            ////////int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == item.FKUnit && x.FKTranLocation == item.FKLocation && x.EANCode == item.EANCode).ToList().Count;
            ////////decimal nClosing;
            ////////if (nRowCount == 0)
            ////////{
            ////////    nClosing = 0;
            ////////}
            ////////else
            ////////{
            ////////    var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == item.FKUnit && x.FKTranLocation == item.FKLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
            ////////    nClosing = AllTran.BalanceQuantity;
            ////////}

            //////////lookUpMaster.Where(x => x.FKLookUpCategory == 39 && x.SetAsDefault == true).FirstOrDefault().Id

            ////////AllTransactionVM.AllTransaction.Id = 0;
            ////////AllTransactionVM.AllTransaction.TransactionType = "Stock Transfer";
            ////////AllTransactionVM.AllTransaction.TranId = item.FKTransferNo;
            ////////AllTransactionVM.AllTransaction.TranRefNo = item.TransferNo;
            ////////AllTransactionVM.AllTransaction.TranDate = item.TransferDt;
            ////////AllTransactionVM.AllTransaction.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
            ////////AllTransactionVM.AllTransaction.FKMaterial = item.FKMaterial;
            ////////AllTransactionVM.AllTransaction.FKArticle = item.FKArticle;
            ////////AllTransactionVM.AllTransaction.Description = item.Description;
            ////////AllTransactionVM.AllTransaction.Colour = item.Colour;
            ////////AllTransactionVM.AllTransaction.Size = item.Size.ToString();
            ////////AllTransactionVM.AllTransaction.FKFromUnit = item.FKUnit;
            ////////AllTransactionVM.AllTransaction.FKFromLocation = item.FKLocation;
            ////////AllTransactionVM.AllTransaction.FKFromStage = nFKStage;
            ////////AllTransactionVM.AllTransaction.FKToUnit = item.FKCustomer;
            ////////AllTransactionVM.AllTransaction.FKToLocation = 0;
            ////////AllTransactionVM.AllTransaction.FKToStage = 0;
            ////////AllTransactionVM.AllTransaction.FKQuality = nFKQuality;
            ////////AllTransactionVM.AllTransaction.HSNCode = item.HSNCode;
            ////////AllTransactionVM.AllTransaction.FKUOM = item.FKUOM;
            ////////AllTransactionVM.AllTransaction.InwardQuantity = 0;
            ////////AllTransactionVM.AllTransaction.OutwardQuantity = item.Quantity;
            ////////AllTransactionVM.AllTransaction.BalanceQuantity = nClosing - item.Quantity;
            ////////AllTransactionVM.AllTransaction.Rate = item.Rate;
            ////////AllTransactionVM.AllTransaction.Value = item.Value;
            ////////AllTransactionVM.AllTransaction.FKStatus = nFKStatus;
            ////////AllTransactionVM.AllTransaction.IsActive = true;
            ////////AllTransactionVM.AllTransaction.CreatedBy = 0;
            ////////AllTransactionVM.AllTransaction.CreatedDate = DateTime.Now;
            ////////AllTransactionVM.AllTransaction.ModifiedBy = 0;
            ////////AllTransactionVM.AllTransaction.FKTranLocation = item.FKLocation;
            ////////AllTransactionVM.AllTransaction.FKTranUnit = item.FKUnit;
            ////////AllTransactionVM.AllTransaction.EANCode = item.EANCode;
            ////////AllTransactionVM.AllTransaction.StockNo = item.StockNo;

            ////////_db.AllTransactions.Add(AllTransactionVM.AllTransaction);
            ////////await _db.SaveChangesAsync();

            //////////decimal nClosing = _db.AllTransactions.Where(x => x.EANCode == item.EANCode && x.FKSupplier == item.FKSupplier && x.Rate == item.Rate && x.Quantity > 0).ToList().Count;


            ////////var stkWArtfromdb = await _db.stockWithArticles.Where(x => x.EANCode == item.EANCode).FirstAsync();
            ////////stkWArtfromdb.SoldQty = stkWArtfromdb.SoldQty + Convert.ToInt32(item.Quantity);
            ////////stkWArtfromdb.BalQty = stkWArtfromdb.ArrivedQty - stkWArtfromdb.SoldQty;
            ////////_db.SaveChanges();


            var tmpInwdtls = _db.TempTransferDtls.Where(x => x.FKTransferNo == Id);
            _db.TempTransferDtls.RemoveRange(tmpInwdtls);
            await _db.SaveChangesAsync();

            var inv = await _db.StockTransfers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var tmpEANCodes = _db.TempTransferDtlEANCodes.Where(x => x.TransferNo == inv.STNo);
            _db.TempTransferDtlEANCodes.RemoveRange(tmpEANCodes);
            await _db.SaveChangesAsync();

            var stDetails = await _db.StockTransferDetails.Where(x => x.FKSTNo == Id).FirstOrDefaultAsync();
            stDetails.IsEntryCompleted = true;
            _db.SaveChanges();

            decimal nTransferQty = _db.StockTransferDetails.Where(x => x.FKSTNo == Id).Select(x => x.DispatchedQuantity).ToList().Sum();
            var stockTransfer = await _db.StockTransfers.Where(x => x.Id == Id).FirstAsync();
            stockTransfer.TransferredQty = Convert.ToInt32(nTransferQty);
            stockTransfer.IsEntryCompleted = true;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
        //GET - CREATE
        public async Task<IActionResult> STDetailsCreate(int Id)
        {
            var st = await _db.StockTransfers.FindAsync(Id);
            TempData["StockTransfer"] = st;

            var result = _db.StockTransferDetails.Where(x => x.FKSTNo == Id).ToList();
            ViewBag.TempEANCodeDtls = _db.StockTransferDetails.Where(x => x.FKSTNo == Id).ToList();
            return View(result);

        }

        //POST - Update Discount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> STDetailsCreateSingle(int Id)
        {
            var st = await _db.StockTransfers.FindAsync(Id);
            string sEANCode = Request.Form["EANCode"];
            int nNFKHSNCode;
            decimal nGSTPercentage;
            decimal nValue;
            decimal nSGSTPercentage, nCGSTPercentage, nIGSTPercentage;
            decimal nSGSTValue, nCGSTValue, nIGSTValue, nGSTValue;

            int nRowCount = _db.stocks.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode && x.Quantity > 0).ToList().Count;
            if (nRowCount == 0)
            {
                //ALERT MESSAGE TO BE DISPLAYED
            }
            else
            {
                var stock = await _db.stocks.Where(x => x.EANCode == sEANCode || x.StockNo == sEANCode && x.Quantity > 0).FirstOrDefaultAsync();
                decimal nStockQuantity = stock.Quantity;

                int nRowCount1 = _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count;
                if (nRowCount1 == 0)
                {
                    stockTransferDetailVM.StockTransferDetail.Id = 0;
                    stockTransferDetailVM.StockTransferDetail.FKSTNo = st.Id;
                    stockTransferDetailVM.StockTransferDetail.STNo = st.STNo;
                    stockTransferDetailVM.StockTransferDetail.STDt = st.STDt;
                    stockTransferDetailVM.StockTransferDetail.FKMaterial = stock.FKMaterial;
                    stockTransferDetailVM.StockTransferDetail.FKArticleDetail = stock.FKArticleDetail;
                    stockTransferDetailVM.StockTransferDetail.EANCode = stock.EANCode;
                    stockTransferDetailVM.StockTransferDetail.StockNo = stock.StockNo;
                    stockTransferDetailVM.StockTransferDetail.MaterialorFinishedProduct = stock.MaterialorFinishedProduct;
                    stockTransferDetailVM.StockTransferDetail.Description = stock.Description;
                    stockTransferDetailVM.StockTransferDetail.Colour = stock.Colour;
                    stockTransferDetailVM.StockTransferDetail.Size = stock.Size;

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
                    stockTransferDetailVM.StockTransferDetail.HSNCode = nNFKHSNCode;
                    stockTransferDetailVM.StockTransferDetail.DispatchedQuantity = 1;
                    stockTransferDetailVM.StockTransferDetail.ReceivedQuantity = 0;
                    stockTransferDetailVM.StockTransferDetail.DifferenceQuantity = 0;
                    stockTransferDetailVM.StockTransferDetail.FKUOM = stock.FKUOM;
                    stockTransferDetailVM.StockTransferDetail.IIQuantity = 0;
                    stockTransferDetailVM.StockTransferDetail.FKIIUom = 0;
                    stockTransferDetailVM.StockTransferDetail.Rate = stock.Rate;
                    nValue = stock.Rate;
                    stockTransferDetailVM.StockTransferDetail.Value = nValue;

                    if (st.FKFromState == st.FKToState)
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
                    stockTransferDetailVM.StockTransferDetail.SGSTPercentage = nSGSTPercentage;
                    stockTransferDetailVM.StockTransferDetail.SGSTValue = nSGSTValue;
                    stockTransferDetailVM.StockTransferDetail.CGSTPercentage = nCGSTPercentage;
                    stockTransferDetailVM.StockTransferDetail.CGSTValue = nCGSTValue;
                    stockTransferDetailVM.StockTransferDetail.IGSTPercentage = nIGSTPercentage;
                    stockTransferDetailVM.StockTransferDetail.IGSTValue = nIGSTValue;
                    stockTransferDetailVM.StockTransferDetail.GSTTotalValue = nGSTValue;
                    stockTransferDetailVM.StockTransferDetail.OthersValuePlus = 0; //stock.OthersValuePlus;
                    stockTransferDetailVM.StockTransferDetail.OthersValueMinus = 0; //stock.OthersValueMinus;
                    stockTransferDetailVM.StockTransferDetail.ItemNettValue = nValue + nGSTValue;
                    stockTransferDetailVM.StockTransferDetail.IsEntryCompleted = false; //stock.IsEntryCompleted;
                    stockTransferDetailVM.StockTransferDetail.IsActive = stock.IsActive;
                    stockTransferDetailVM.StockTransferDetail.EnteredSystemId = ""; //stock.EnteredSystemId;
                    stockTransferDetailVM.StockTransferDetail.CreatedBy = 0; //stock.CreatedBy;
                    stockTransferDetailVM.StockTransferDetail.CreatedDate = DateTime.Now;
                    stockTransferDetailVM.StockTransferDetail.IsAcknowledged = false;

                    _db.StockTransferDetails.Add(stockTransferDetailVM.StockTransferDetail);
                    _db.SaveChanges();

                }
                else
                {
                    decimal nDispatchedQty = _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.EANCode == sEANCode || x.StockNo == sEANCode).Select(x => x.DispatchedQuantity).ToList().Sum();
                    if (nDispatchedQty + 1 > nStockQuantity)
                    {
                        //ALERT MESSAGE TO BE DISPLAYED
                    }
                    else
                    {
                        var TempFromdb = await _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.EANCode == sEANCode || x.StockNo == sEANCode).FirstOrDefaultAsync();
                        TempFromdb.DispatchedQuantity = TempFromdb.DispatchedQuantity + 1;
                        TempFromdb.Value = TempFromdb.DispatchedQuantity * TempFromdb.Rate;

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

                        if (st.FKFromState == st.FKToState)
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
            return RedirectToAction(nameof(STDetailsCreate));
        }

        //POST - UPDATE STOCK TRANSFER DETAILS
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertStockTransferDetail(int Id)
        {
            var st = await _db.StockTransfers.FindAsync(Id);
            int nFKFromUnit, nFKFromDepartment, nFKFromLocation;
            int nFKToUnit, nFKToDepartment, nFKToLocation;
            nFKFromUnit = st.FKFromUnit;
            nFKFromDepartment = st.FKFromDepartment;
            nFKFromLocation = st.FKFromLocation;

            nFKToUnit = st.FKToUnit;
            nFKToDepartment = st.FKToDepartment;
            nFKToLocation = st.FKToLocation;
            var stckTrnDtls = await _db.StockTransferDetails.Where(x => x.FKSTNo == Id).ToListAsync();

            string sEANCode;
            decimal nDispatchQuantity;

            int nFKStage, nFKQuality, nFKStatus, nFKTransitStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = 0;
            nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;

            foreach (var item in stckTrnDtls)
            {
                sEANCode = item.EANCode;
                nDispatchQuantity = item.DispatchedQuantity;

                #region DEDUCTION OF STOCK FROM TRANSFERRED LOCATION
                var stockAtFromLocation = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKFromUnit && x.FKLocation == nFKFromLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                stockAtFromLocation.Quantity = stockAtFromLocation.Quantity - nDispatchQuantity;
                decimal rate = stockAtFromLocation.Rate;
                decimal value = stockAtFromLocation.Quantity * rate;
                stockAtFromLocation.Value = value;
                stockAtFromLocation.ValueInINR = value;
                _db.SaveChanges();

                int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == nFKFromUnit && x.FKTranLocation == nFKFromLocation && x.EANCode == item.EANCode).ToList().Count;
                decimal nClosing;
                if (nRowCount == 0)
                {
                    nClosing = 0;
                }
                else
                {
                    var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == nFKFromUnit && x.FKTranLocation == nFKFromLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
                    nClosing = AllTran.BalanceQuantity;
                }

                AllTransactionVM.AllTransaction.Id = 0;
                AllTransactionVM.AllTransaction.TransactionType = "Stock Transfer Out";
                AllTransactionVM.AllTransaction.TranId = st.Id;
                AllTransactionVM.AllTransaction.TranRefNo = st.STNo;
                AllTransactionVM.AllTransaction.TranDate = st.STDt;
                AllTransactionVM.AllTransaction.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                AllTransactionVM.AllTransaction.FKMaterial = item.FKMaterial;
                AllTransactionVM.AllTransaction.FKArticle = item.FKArticleDetail;
                AllTransactionVM.AllTransaction.Description = item.Description;
                AllTransactionVM.AllTransaction.Colour = item.Colour;
                AllTransactionVM.AllTransaction.Size = item.Size.ToString();
                AllTransactionVM.AllTransaction.FKFromUnit = nFKFromUnit;
                AllTransactionVM.AllTransaction.FKFromLocation = nFKFromLocation;
                AllTransactionVM.AllTransaction.FKFromStage = nFKStage;
                AllTransactionVM.AllTransaction.FKToUnit = nFKToUnit;
                AllTransactionVM.AllTransaction.FKToLocation = nFKToLocation;
                AllTransactionVM.AllTransaction.FKToStage = nFKStage;
                AllTransactionVM.AllTransaction.FKQuality = nFKQuality;
                AllTransactionVM.AllTransaction.HSNCode = item.HSNCode;
                AllTransactionVM.AllTransaction.FKUOM = item.FKUOM;
                AllTransactionVM.AllTransaction.InwardQuantity = 0;
                AllTransactionVM.AllTransaction.OutwardQuantity = nDispatchQuantity;
                AllTransactionVM.AllTransaction.BalanceQuantity = nClosing - nDispatchQuantity;
                AllTransactionVM.AllTransaction.Rate = item.Rate;
                AllTransactionVM.AllTransaction.Value = item.Value;
                AllTransactionVM.AllTransaction.FKStatus = nFKStatus;
                AllTransactionVM.AllTransaction.IsActive = true;
                AllTransactionVM.AllTransaction.CreatedBy = 0;
                AllTransactionVM.AllTransaction.CreatedDate = DateTime.Now;
                AllTransactionVM.AllTransaction.FKTranLocation = nFKFromLocation;
                AllTransactionVM.AllTransaction.FKTranUnit = nFKFromUnit;
                AllTransactionVM.AllTransaction.EANCode = item.EANCode;
                AllTransactionVM.AllTransaction.StockNo = item.StockNo;

                _db.AllTransactions.Add(AllTransactionVM.AllTransaction);
                await _db.SaveChangesAsync();
                #endregion

                #region INCREMENT OF STOCK @ RECEIVED LOCATION
                nFKTransitStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.Description.ToUpper() == "IN TRANSIT").FirstOrDefault().Id;
                int nStckRow = _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKTransitStatus).ToList().Count;
                if (nStckRow == 0)
                {
                    StocksVM.Stock.Id = 0;
                    StocksVM.Stock.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                    StocksVM.Stock.FKMaterial = item.FKMaterial;
                    StocksVM.Stock.FKArticleDetail = item.FKArticleDetail;
                    StocksVM.Stock.Description = item.Description;
                    StocksVM.Stock.Colour = item.Colour;
                    StocksVM.Stock.Size = item.Size.ToString();
                    StocksVM.Stock.OrderReferenceNo = stockAtFromLocation.OrderReferenceNo;
                    StocksVM.Stock.StockInitiatedDate = DateTime.Now;
                    StocksVM.Stock.FKUnit = nFKToUnit;
                    StocksVM.Stock.FKLocation = nFKToLocation;
                    StocksVM.Stock.FKStage = stockAtFromLocation.FKStage;
                    StocksVM.Stock.Quantity = item.DispatchedQuantity;
                    StocksVM.Stock.Rate = item.Rate;
                    StocksVM.Stock.Value = item.Value;
                    StocksVM.Stock.FKCurrency = stockAtFromLocation.FKCurrency;
                    StocksVM.Stock.ExchangeRate = 1; //TODO
                    StocksVM.Stock.ValueInINR = item.Value;
                    StocksVM.Stock.LandedCost = 0;
                    StocksVM.Stock.LandedRate = 0;
                    StocksVM.Stock.FKUOM = item.FKUOM;
                    StocksVM.Stock.FKIIUOM = 0;
                    StocksVM.Stock.FKSource = stockAtFromLocation.FKSource;
                    StocksVM.Stock.FKQuality = nFKQuality;
                    StocksVM.Stock.FKStatus = nFKTransitStatus;
                    StocksVM.Stock.IsActive = true;
                    StocksVM.Stock.CreatedBy = 1;
                    StocksVM.Stock.CreatedDate = DateTime.Now;
                    StocksVM.Stock.EANCode = item.EANCode;
                    StocksVM.Stock.StockNo = item.StockNo;
                    StocksVM.Stock.FKSupplier = stockAtFromLocation.FKSupplier;
                    StocksVM.Stock.FKHSNCode = stockAtFromLocation.FKHSNCode;
                    StocksVM.Stock.DiscountPercentageforSales = stockAtFromLocation.DiscountPercentageforSales;
                    StocksVM.Stock.MRP = stockAtFromLocation.MRP;
                    StocksVM.Stock.FKOffer = 0;
                    StocksVM.Stock.OfferType = "";
                    StocksVM.Stock.FKCategory = stockAtFromLocation.FKCategory;
                    StocksVM.Stock.FLAM = stockAtFromLocation.FLAM;
                    StocksVM.Stock.LastTranDate = DateTime.Now;

                    _db.stocks.Add(StocksVM.Stock);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var stockAtToLocation = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKTransitStatus).FirstOrDefaultAsync();
                    stockAtToLocation.Quantity = stockAtToLocation.Quantity + nDispatchQuantity;
                    decimal rate1 = stockAtToLocation.Rate;
                    decimal value1 = stockAtFromLocation.Quantity * rate;
                    stockAtFromLocation.Value = value1;
                    stockAtFromLocation.ValueInINR = value1;
                    _db.SaveChanges();
                }
            }

            var stDetails = await _db.StockTransferDetails.Where(x => x.FKSTNo == Id).FirstOrDefaultAsync();
            stDetails.IsEntryCompleted = true;
            _db.SaveChanges();

            var stockTransfer = await _db.StockTransfers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            stockTransfer.IsEntryCompleted = true;
            _db.SaveChanges();
            #endregion

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region STOCK TRANSFER IN
        public async Task<IActionResult> Pending(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.StockTransfers.OrderByDescending(x => x.Id).Where(x => x.STDt >= effectStartDate && x.STDt <= effectEndDate && x.IsAcknowledged == false).ToListAsync());
        }

        [HttpPost]
        public IActionResult PendingFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Pending", "StockTransferOut", new { fromDate = fromDate, toDate = toDate });
        }


        public async Task<IActionResult> STDetailsAcknowledge(int Id)
        {
            var st = await _db.StockTransfers.FindAsync(Id);
            TempData["StockTransfer"] = st;

            return View(await _db.StockTransferDetails.Where(x => x.FKSTNo == Id).ToListAsync());

        }

        //GET - Acknowledge
        public async Task<IActionResult> STDAcknowledge(int Id)
        {
            var std = await _db.StockTransferDetails.FindAsync(Id);
            var st = await _db.StockTransfers.FindAsync(std.FKSTNo);
            TempData["StockTransfer"] = st;

            return View(await _db.StockTransferDetails.Where(x => x.Id == Id).FirstOrDefaultAsync());
        }

        //POST - Acknowledge
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> STDAcknowledge(int Id, StockTransferDetail model)
        {
            var stdfromDb = await _db.StockTransferDetails.FindAsync(Id);
            
            stdfromDb.ReceivedQuantity = model.ReceivedQuantity;
            stdfromDb.ModifiedBy = model.ModifiedBy;
            stdfromDb.ModifiedDate = model.ModifiedDate;

            await _db.SaveChangesAsync();
            return RedirectToAction("STDetailsAcknowledge", "StockTransferOut", new { Id = stdfromDb.FKSTNo });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> STDAcknowledgeTotalQty(int Id)
        {
            var stdfromDb = await _db.StockTransferDetails.FindAsync(Id);

            stdfromDb.ReceivedQuantity = stdfromDb.DispatchedQuantity;
            await _db.SaveChangesAsync();
            return RedirectToAction("STDetailsAcknowledge", "StockTransferOut", new { Id = stdfromDb.FKSTNo });
        }

        //POST - UPDATE STOCK TRANSFER DETAILS
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStockTransferIn(int Id)
        {
            var st = await _db.StockTransfers.FindAsync(Id);
            int nFKFromUnit, nFKFromDepartment, nFKFromLocation;
            int nFKToUnit, nFKToDepartment, nFKToLocation;
            nFKFromUnit = st.FKFromUnit;
            nFKFromDepartment = st.FKFromDepartment;
            nFKFromLocation = st.FKFromLocation;

            nFKToUnit = st.FKToUnit;
            nFKToDepartment = st.FKToDepartment;
            nFKToLocation = st.FKToLocation;
            var stckTrnDtls = await _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.ReceivedQuantity > 0 && x.IsAcknowledged == false).ToListAsync();

            string sEANCode;
            decimal nDispatchQuantity, nReceivedQuantity;

            int nFKStage, nFKQuality, nFKStatus, nFKTransitStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = 0;
            nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;

            foreach (var item in stckTrnDtls)
            {
                sEANCode = item.EANCode;
                nDispatchQuantity = item.DispatchedQuantity;
                nReceivedQuantity = item.ReceivedQuantity;

                #region DEDUCTION OF STOCK FROM TRANSFERRED LOCATION
                //var stockAtFromLocation = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKFromUnit && x.FKLocation == nFKFromLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                //stockAtFromLocation.Quantity = stockAtFromLocation.Quantity - nDispatchQuantity;
                //decimal rate = stockAtFromLocation.Rate;
                //decimal value = stockAtFromLocation.Quantity * rate;
                //stockAtFromLocation.Value = value;
                //stockAtFromLocation.ValueInINR = value;
                //_db.SaveChanges();


                #endregion

                #region INCREMENT OF STOCK @ RECEIVED LOCATION
                nFKTransitStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.Description.ToUpper() == "IN TRANSIT").FirstOrDefault().Id;

                var stockAtToLocation = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKTransitStatus).FirstOrDefaultAsync();
                stockAtToLocation.Quantity = stockAtToLocation.Quantity - nReceivedQuantity;
                decimal rate1 = stockAtToLocation.Rate; // TO CHECK FOR TRANSFER OUT
                decimal value1 = stockAtToLocation.Quantity * rate1;
                stockAtToLocation.Value = value1;
                stockAtToLocation.ValueInINR = value1;
                _db.SaveChanges();

                int nStckRow = _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKStatus).ToList().Count;
                if (nStckRow == 0)
                {
                    StocksVM.Stock.Id = 0;
                    StocksVM.Stock.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                    StocksVM.Stock.FKMaterial = item.FKMaterial;
                    StocksVM.Stock.FKArticleDetail = item.FKArticleDetail;
                    StocksVM.Stock.Description = item.Description;
                    StocksVM.Stock.Colour = item.Colour;
                    StocksVM.Stock.Size = item.Size.ToString();
                    StocksVM.Stock.OrderReferenceNo = stockAtToLocation.OrderReferenceNo;
                    StocksVM.Stock.StockInitiatedDate = DateTime.Now;
                    StocksVM.Stock.FKUnit = nFKToUnit;
                    StocksVM.Stock.FKLocation = nFKToLocation;
                    StocksVM.Stock.FKStage = stockAtToLocation.FKStage;
                    StocksVM.Stock.Quantity = nReceivedQuantity;
                    StocksVM.Stock.Rate = item.Rate;
                    StocksVM.Stock.Value = nReceivedQuantity * item.Rate;
                    StocksVM.Stock.FKCurrency = stockAtToLocation.FKCurrency;
                    StocksVM.Stock.ExchangeRate = 1; //TODO
                    StocksVM.Stock.ValueInINR = nReceivedQuantity * item.Rate;
                    StocksVM.Stock.LandedCost = 0;
                    StocksVM.Stock.LandedRate = 0;
                    StocksVM.Stock.FKUOM = item.FKUOM;
                    StocksVM.Stock.FKIIUOM = 0;
                    StocksVM.Stock.FKSource = stockAtToLocation.FKSource;
                    StocksVM.Stock.FKQuality = nFKQuality;
                    StocksVM.Stock.FKStatus = nFKStatus;
                    StocksVM.Stock.IsActive = true;
                    StocksVM.Stock.CreatedBy = 1;
                    StocksVM.Stock.CreatedDate = DateTime.Now;
                    StocksVM.Stock.EANCode = item.EANCode;
                    StocksVM.Stock.StockNo = item.StockNo;
                    StocksVM.Stock.FKSupplier = stockAtToLocation.FKSupplier;
                    StocksVM.Stock.FKHSNCode = stockAtToLocation.FKHSNCode;
                    StocksVM.Stock.DiscountPercentageforSales = stockAtToLocation.DiscountPercentageforSales;
                    StocksVM.Stock.MRP = stockAtToLocation.MRP;
                    StocksVM.Stock.FKOffer = 0;
                    StocksVM.Stock.OfferType = "";
                    StocksVM.Stock.FKCategory = stockAtToLocation.FKCategory;
                    StocksVM.Stock.FLAM = stockAtToLocation.FLAM;
                    StocksVM.Stock.LastTranDate = DateTime.Now;
                    
                    _db.stocks.Add(StocksVM.Stock);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var stock = await _db.stocks.Where(x => x.EANCode == sEANCode && x.FKUnit == nFKToUnit && x.FKLocation == nFKToLocation && x.FKStatus == nFKStatus).FirstOrDefaultAsync();
                    stock.Quantity = stock.Quantity + nReceivedQuantity;
                    decimal rate2 = stock.Rate; // TO CHECK FOR TRANSFER OUT
                    decimal value2 = stock.Quantity * rate2;
                    stock.Value = value1;
                    stock.ValueInINR = value1;
                    stock.LastTranDate = DateTime.Now;
                    _db.SaveChanges();
                }

                int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == nFKToUnit && x.FKTranLocation == nFKToLocation && x.EANCode == item.EANCode).ToList().Count;
                decimal nClosing;
                if (nRowCount == 0)
                {
                    nClosing = 0;
                }
                else
                {
                    var AllTran = await _db.AllTransactions.OrderByDescending(x => x.Id).Where(x => x.FKTranUnit == nFKToUnit && x.FKTranLocation == nFKToLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
                    nClosing = AllTran.BalanceQuantity;
                }

                AllTransactionVM.AllTransaction.Id = 0;
                AllTransactionVM.AllTransaction.TransactionType = "Stock Transfer In";
                AllTransactionVM.AllTransaction.TranId = st.Id;
                AllTransactionVM.AllTransaction.TranRefNo = st.STNo;
                AllTransactionVM.AllTransaction.TranDate = st.STDt;
                AllTransactionVM.AllTransaction.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                AllTransactionVM.AllTransaction.FKMaterial = item.FKMaterial;
                AllTransactionVM.AllTransaction.FKArticle = item.FKArticleDetail;
                AllTransactionVM.AllTransaction.Description = item.Description;
                AllTransactionVM.AllTransaction.Colour = item.Colour;
                AllTransactionVM.AllTransaction.Size = item.Size.ToString();
                AllTransactionVM.AllTransaction.FKFromUnit = nFKFromUnit;
                AllTransactionVM.AllTransaction.FKFromLocation = nFKFromLocation;
                AllTransactionVM.AllTransaction.FKFromStage = nFKStage;
                AllTransactionVM.AllTransaction.FKToUnit = nFKToUnit;
                AllTransactionVM.AllTransaction.FKToLocation = nFKToLocation;
                AllTransactionVM.AllTransaction.FKToStage = nFKStage;
                AllTransactionVM.AllTransaction.FKQuality = nFKQuality;
                AllTransactionVM.AllTransaction.HSNCode = item.HSNCode;
                AllTransactionVM.AllTransaction.FKUOM = item.FKUOM;
                AllTransactionVM.AllTransaction.InwardQuantity = nReceivedQuantity;
                AllTransactionVM.AllTransaction.OutwardQuantity = 0;
                AllTransactionVM.AllTransaction.BalanceQuantity = nClosing + nReceivedQuantity;
                AllTransactionVM.AllTransaction.Rate = item.Rate;
                AllTransactionVM.AllTransaction.Value = item.Value;
                AllTransactionVM.AllTransaction.FKStatus = nFKStatus;
                AllTransactionVM.AllTransaction.IsActive = true;
                AllTransactionVM.AllTransaction.CreatedBy = 0;
                AllTransactionVM.AllTransaction.CreatedDate = DateTime.Now;
                AllTransactionVM.AllTransaction.FKTranLocation = nFKToLocation;
                AllTransactionVM.AllTransaction.FKTranUnit = nFKToUnit;
                AllTransactionVM.AllTransaction.EANCode = item.EANCode;
                AllTransactionVM.AllTransaction.StockNo = item.StockNo;

                _db.AllTransactions.Add(AllTransactionVM.AllTransaction);
                await _db.SaveChangesAsync();

                var stdfromDb = await _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.EANCode == sEANCode).FirstOrDefaultAsync(); ;
                stdfromDb.IsAcknowledged = true;
                await _db.SaveChangesAsync();
                #endregion
            }

            decimal nReceivedQty = _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.IsAcknowledged == true).Select(x => x.ReceivedQuantity).ToList().Sum();
            int nPendingAcknowledgments = _db.StockTransferDetails.Where(x => x.FKSTNo == Id && x.IsAcknowledged == false).ToList().Count();

            var stfromDb = await _db.StockTransfers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            stfromDb.ReceivedQty = nReceivedQty;
            if (nPendingAcknowledgments == 0)
            {
                stfromDb.IsAcknowledged = true;
            }
            else
            {
                stfromDb.IsAcknowledged = false;
            }
            await _db.SaveChangesAsync();
            //TODO To Start codeing from Here
            //decimal nDispatchedQty = _db.StockTransferDetails.Where(x => x.FKSTNo == nFKStockTransfer && x.EANCode == sEANCode || x.StockNo == sEANCode).Select(x => x.DispatchedQuantity).ToList().Sum();
            //stdfromDb.IsAcknowledged = true;
            //await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Pending));
            #endregion
        }
    }
}
