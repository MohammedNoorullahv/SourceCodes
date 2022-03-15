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
    public class InwardController : Controller
    {
        ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public InwardViewModel inwardVM { get; set; }
        public InwardDetailViewModel inwardDetailVM { get; set; }
        public StocksViewModel StocksVM { get; set; }
        public AllTransactionViewModel AllTransactionVM { get; set; }
        //public static int nFKInward;
        public static string sScanningMode; 
            //sInwardNo;
        public static string sIpAddress;
        public static string ipaddress = string.Empty;


        public InwardController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
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
                FKQuality = _db.lookUpMasters,
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
            var delTmpArr = _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress);
            _db.TempArticalArrivalEANCodes.RemoveRange(delTmpArr);
            await _db.SaveChangesAsync();

            var delTmpInw = _db.TempInwardDtls.Where(x => x.IPAddress == ipaddress);
            _db.TempInwardDtls.RemoveRange(delTmpInw);
            await _db.SaveChangesAsync();

            return View(await _db.inwards.OrderByDescending(x => x.Id).Where(x => x.InwardDt >= effectStartDate && x.InwardDt <= effectEndDate && x.FLAM == "F").ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "Inward", new { fromDate = fromDate, toDate = toDate });
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
            inwardVM.FKQuality = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 41 && c.IsActive == true).ToListAsync();
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
        public async Task<IActionResult> CreatePost(string Save,IFormFile formFile)
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
            
            inwardVM.Inward.DocumentType = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKDocumentType).FirstOrDefault().Description;
            inwardVM.Inward.Source = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKSource).FirstOrDefault().Description;
            
            inwardVM.Inward.Season = season.Code;
            var partyInfo = await _db.partyInfos.ToListAsync();
            inwardVM.Inward.SupplierName = partyInfo.Where(x => x.Id == inwardVM.Inward.FKParty).FirstOrDefault().CompanyName;
            inwardVM.Inward.Department = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKDepartment).FirstOrDefault().Description;
            inwardVM.Inward.Category = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKCategory).FirstOrDefault().Description;
            inwardVM.Inward.Location = locationInfo.Where(x => x.Id == inwardVM.Inward.FKLocation).FirstOrDefault().LocationName;
            _db.inwards.Add(inwardVM.Inward);
            await _db.SaveChangesAsync();

            if( Save == "Save & New Po")
            {
                return RedirectToAction(nameof(Create));
            }
            else if (Save == "Save & Insert Dtl")
            {
                var inward = await _db.inwards.Where(x => x.InwardNo == inwardVM.Inward.InwardNo).FirstOrDefaultAsync();
                //TempData["Inward"] = inward;
                return RedirectToAction("CreateInwardDetailForScanning", "Inward", new { Id = inward.Id});
            }
            else 
            {
                var inward = await _db.inwards.Where(x => x.InwardNo == inwardVM.Inward.InwardNo).FirstOrDefaultAsync();

                var scndlist = _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress);
                _db.TempArticalArrivalEANCodes.RemoveRange(scndlist);
                await _db.SaveChangesAsync();

                var tmpInvLst = _db.TempInwardDtls.Where(x => x.FKInwardNo == inward.Id);
                _db.TempInwardDtls.RemoveRange(tmpInvLst);
                await _db.SaveChangesAsync();

                ////string path = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
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

                MdlTempArticalArrivalEANCode model = new MdlTempArticalArrivalEANCode();

                foreach (var EANCode in scannedlist)
                {
                    int nRowCnt = _db.stockWithArticles.Where(x => x.EANCode == EANCode).ToList().Count;
                    if (nRowCnt == 0)
                    {
                        model.IPAddress = ipaddress;
                        model.InwardNo = inward.InwardNo;
                        model.EANCode = EANCode;
                        model.Quantity = 1;
                        model.IsMatching = false;
                        model.Reason = "Invalid Barcode";
                        model.Id = 0;
                        _db.TempArticalArrivalEANCodes.Add(model);
                        _db.SaveChanges();
                    }
                    else
                    {
                        var stockwithArticle = await _db.stockWithArticles.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                        int nBaltoReceive = stockwithArticle.Quantity - stockwithArticle.ArrivedQty;
                        if (nBaltoReceive > 0)
                        {
                            int nRowCount = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode).ToList().Count;
                            if (nRowCount > 0)
                            {
                                if (nRowCount == 1)
                                {
                                    var TempArriEAN = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                    int nExistingQty = TempArriEAN.Quantity;

                                    if (nExistingQty + 1 <= nBaltoReceive)
                                    {
                                        var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        int nRowCount1 = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                                        if (nRowCount1 == 0)
                                        {
                                            model.IPAddress = ipaddress;
                                            model.InwardNo = inward.InwardNo;
                                            model.EANCode = EANCode;
                                            model.Quantity = 1;
                                            model.IsMatching = false;
                                            model.Id = 0;
                                            model.Reason = "Part X's Qty";
                                            _db.TempArticalArrivalEANCodes.Add(model);
                                            await _db.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                            TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                            _db.SaveChanges();
                                        }

                                    }
                                }
                                else
                                {
                                    var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                    TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                    _db.SaveChanges();
                                }

                            }
                            else
                            {
                                model.IPAddress = ipaddress;
                                model.InwardNo = inward.InwardNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = true;
                                model.Reason = "";
                                model.Id = 0;
                                _db.TempArticalArrivalEANCodes.Add(model);
                                _db.SaveChanges();
                                //await _db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            int nRowCount1 = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                            if (nRowCount1 == 0)
                            {
                                model.IPAddress = ipaddress;
                                model.InwardNo = inward.InwardNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = false;
                                model.Id = 0;
                                model.Reason = "X's Qty";
                                _db.TempArticalArrivalEANCodes.Add(model);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                _db.SaveChanges();
                            }
                        }

                    }

                    //model.Id = 0;
                };

                #region UPDATING TEMPORARY INWARD DETAILS

                List<MdlTempInwardDtls> tempInwardDtls = new List<MdlTempInwardDtls>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_Inwards @mAction='SELINWARD', @mInwardNo='{inward.InwardNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                Boolean nReadyforImport;
                string sReason = "";
                while (result.Read())
                {

                    var swa = new MdlTempInwardDtls
                    {
                        //Id = 0,
                        IPAddress = ipaddress,
                        FKInwardNo = result.GetInt32(0),
                        InwardNo = inward.InwardNo,
                        InwardDt = DateTime.Now,
                        FKMaterial = result.GetInt32(1),
                        FKArticle = result.GetInt32(2),
                        Description = result.GetString(3),
                        Colour = result.GetString(4),
                        Size = result.GetDecimal(5),
                        HSNCode = result.GetInt32(6),
                        Quantity = result.GetInt32(7),
                        IIQuantity = result.GetInt32(8),
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
                        FKSupplier = result.GetInt32(33),
                        MaterialorFinishedProduct = result.GetString(36),
                        FKHSNCode = result.GetInt32(37),
                        DiscountPercentageforSales = 0,
                        MRP = result.GetDecimal(38),
                        FKOffer = 0,
                        OfferType = "",
                        FKCategory = result.GetInt32(39),
                        FKPurchaseOrder = result.GetInt32(42),
                        FKPurchaseOrderMain = result.GetInt32(41),
                        FKPurchaseOrderDtl = result.GetInt32(40),
                        FKQuality = inward.FKQuality
                    };
                    tempInwardDtls.Add(swa);
                }

                result.Close();

                _db.TempInwardDtls.AddRange(tempInwardDtls);
                await _db.SaveChangesAsync();

                var TempEANCodeDtls = await _db.TempArticalArrivalEANCodes.OrderBy(c => c.Id).Where(x => x.InwardNo == inward.InwardNo).ToListAsync();
                string sEANCode;
                foreach (var EANCode in TempEANCodeDtls)
                {
                    sEANCode = EANCode.EANCode;
                    //var TempEANCodesFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();

                    if (_db.TempInwardDtls.Where(x => x.EANCode == sEANCode).ToList().Count > 0)
                    {
                        var TempInwFromdb = await _db.TempInwardDtls.Where(x => x.EANCode == sEANCode).FirstAsync();
                        nReadyforImport = TempInwFromdb.ReadyforImport;
                        sReason = TempInwFromdb.Reason;
                    }
                    else
                    {
                        nReadyforImport = false;
                        sReason = "Wrong Barcode";

                    }
                    //_db.TempInwardDtls.Update(TempEANCodeDtls);
                    var TempEANCodeDtls1 = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == sEANCode).FirstOrDefaultAsync();
                    TempEANCodeDtls1.IsMatching = nReadyforImport;
                    TempEANCodeDtls1.Reason = sReason;
                    await _db.SaveChangesAsync();
                }

                #endregion

                return RedirectToAction("LoadUploadedInwardeDetail", "Inward", new { Id = inward.Id });
                
            }

        }

        public async Task<IActionResult> LoadUploadedInwardeDetail(int Id)
        {
            //var inv = await _db.Invoices.FindAsync(nFKInvoice);
            var inw = await _db.inwards.FindAsync(Id);
            TempData["Inwards"] = inw;

            var TempInwardDtlsresult = _db.TempInwardDtls.ToList();
            ViewBag.TempInwardDtls = _db.TempInwardDtls.ToList();
            ViewBag.TempEANCodeDtls = _db.TempArticalArrivalEANCodes.Where(x => x.IsMatching == false).ToList();
            return View(TempInwardDtlsresult);

        }


        ////POST - CREATE
        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SaveAndInsertDtl(bool flag = true)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(inwardVM);
        //    }

        //    var lookUpMaster = await _db.lookUpMasters.ToListAsync();

        //    string sSource = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKSource).FirstOrDefault().Description;
        //    var season = await _db.seasons.FindAsync(inwardVM.Inward.FKSeason);
        //    string sSeason = season.Code;
        //    var locationInfo = await _db.locations.ToListAsync();
        //    string sLocationCode = locationInfo.Where(x => x.Id == inwardVM.Inward.FKLocation).FirstOrDefault().Code;

        //    string codechar = ((sLocationCode.Substring(0, 3)) + inwardVM.Inward.MaterialorFinishedProduct + (sSeason.Substring(0, 4) + sSource.Substring(0, 1))).ToString().ToUpper();
        //    var maxcode = 0;

        //    if (_db.inwards.Where(x => x.InwardNo.Contains(codechar)).ToList().Count > 0)
        //    {
        //        maxcode = _db.inwards.Where(x => x.InwardNo.Contains(codechar)).Select(x => int.Parse(x.InwardNo.Substring(11, 4))).ToList().Max();
        //    }

        //    inwardVM.Inward.InwardNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

        //    inwardVM.Inward.Season = sSeason;
        //    inwardVM.Inward.Currency = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKCurrency).FirstOrDefault().Description;
        //    inwardVM.Inward.POType = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKPOType).FirstOrDefault().Description;
        //    inwardVM.Inward.DocumentType = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKDocumentType).FirstOrDefault().Description;
        //    inwardVM.Inward.Source = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKSource).FirstOrDefault().Description;
        //    var companyInfo = await _db.companyInfos.ToListAsync();
        //    inwardVM.Inward.UnitName = companyInfo.Where(x => x.Id == inwardVM.Inward.FKUnit).FirstOrDefault().CompanyName;
        //    inwardVM.Inward.Season = season.Code;
        //    var partyInfo = await _db.partyInfos.ToListAsync();
        //    inwardVM.Inward.SupplierName = partyInfo.Where(x => x.Id == inwardVM.Inward.FKParty).FirstOrDefault().CompanyName;
        //    inwardVM.Inward.Department = lookUpMaster.Where(x => x.Id == inwardVM.Inward.FKDepartment).FirstOrDefault().Description;

        //    inwardVM.Inward.Location = locationInfo.Where(x => x.Id == inwardVM.Inward.FKLocation).FirstOrDefault().LocationName;
        //    _db.inwards.Add(inwardVM.Inward);
        //    await _db.SaveChangesAsync();

        //    var inward = await _db.inwards.Where(x => x.InwardNo == inwardVM.Inward.InwardNo).FirstOrDefaultAsync();
        //    nFKInward = inward.Id;
        //    return RedirectToAction(nameof(InwardDetailCreate));
        //}
        #endregion

        #region INWARD DETAIL
        public async Task<IActionResult> InwardDetailIndex(int Id)
        {
            var inw = await _db.inwards.FindAsync(Id);
            TempData["Inward"] = inw;
          
            return View(await _db.inwardDetails.Where(x => x.FKInwardNo == Id).ToListAsync());
        }

        //////////GET - CREATE
        ////////public async Task<IActionResult> InwardDetailCreate()
        ////////{
        ////////    var inw = await _db.inwards.FindAsync(nFKInward);
        ////////    TempData["Inward"] = inw;

        ////////    return View(inwardDetailVM);
        ////////}

        //////////POST - CREATE
        ////////[HttpPost]
        ////////[ValidateAntiForgeryToken]
        ////////public async Task<IActionResult> InwardDetailCreate(InwardDetailViewModel inwardDetailVM)
        ////////{
        ////////    if (!ModelState.IsValid)
        ////////    {
        ////////        return View(inwardVM);
        ////////    }

        ////////    _db.inwardDetails.Add(inwardDetailVM.InwardDetails);
        ////////    await _db.SaveChangesAsync();
        ////////    return RedirectToAction(nameof(Create));
        ////////}

        //GET - CREATE INWARD DETAIL For Scanning
        public async Task<IActionResult> CreateInwardDetailForScanning(int Id)
        {
            var inw = await _db.inwards.FindAsync(Id);
            TempData["Inward"] = inw;

            TempData["InwardId"] = inw.Id;
            if (sScanningMode != "Re Entries")
                sScanningMode = "New Entries";

            TempData["ScanningMode"] = sScanningMode;

            //maxcode = _db.inwards.Where(x => x.InwardNo.Contains(codechar)).Select(x => int.Parse(x.InwardNo.Substring(11, 4))).ToList().Max();

            //int nRowCount1 = _db.inwardDetails.Where(x => x.FKInwardNo == nFKInward).ToList().Count;
            decimal nRowCount1 = _db.inwardDetails.Where(x => x.FKInwardNo == inw.Id).Select(x => x.Quantity).ToList().Sum();
            decimal nRowCount = 0;
            if (sScanningMode == "Re Entries")
            {
                //nRowCount = _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress).ToList().Count;
                nRowCount = _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress).Select(x => x.Quantity).ToList().Sum();
                //TempData["PreviousCount"] = nRowCount;
            }
            else
            {
                //TempData["PreviousCount"] = 0;
            }

            TempData["PreviousCount"] = nRowCount + nRowCount1;

            var tmpInwdtls = _db.TempInwardDtls.Where(x => x.IPAddress == ipaddress);
            _db.TempInwardDtls.RemoveRange(tmpInwdtls);
            await _db.SaveChangesAsync();


            return View();
        }

        public async Task<IActionResult> CreateInwardDetailForReScanning(int Id)
        {
            //TODO: To remove the Wrong Barcodes in the Temporary List
            var scndlist = await _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress && x.IsMatching == false).ToListAsync();
            _db.TempArticalArrivalEANCodes.RemoveRange(scndlist);
            await _db.SaveChangesAsync();
            
            sScanningMode = "Re Entries";
            return RedirectToAction("CreateInwardDetailForScanning", "Inward", new { Id = Id });
        }

        //POST - SCANNED BARCODES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InwardDetailScannedCodes(MdlTempArticalArrivalEANCode model)
        {
            int Id = 0;
            if (sScanningMode != "Updates")
            {
                if (sScanningMode == "New Entries")
                {
                    var scndlist = _db.TempArticalArrivalEANCodes.Where(x => x.IPAddress == ipaddress);

                    //_db.TempArticalArrivalEANCodes.Remove((MdlTempArticalArrivalEANCode)scndlist);
                    _db.TempArticalArrivalEANCodes.RemoveRange(scndlist);
                    await _db.SaveChangesAsync();
                }
                var scannedlist = Request.Form["liContent"];
                string sScanningMod = Request.Form["ScanningMode"];
                Id = Convert.ToInt32(Request.Form["InwardId"]);
                //var res = scannedlist.FirstOrDefault();

                var inwardfromDb = await _db.inwards.FindAsync(Id);
                string sInwardNo = inwardfromDb.InwardNo;
                int nFKSupplier = inwardfromDb.FKParty;

                foreach (var EANCode in scannedlist)
                {
                    int nRowCnt = _db.stockWithArticles.Where(x => x.EANCode == EANCode && x.FKSupplier == nFKSupplier).ToList().Count;
                    if (nRowCnt == 0)
                    {
                        int  nRowCnt1 = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.InwardNo == sInwardNo).ToList().Count;
                        if (nRowCnt1 == 0)
                        {
                            model.IPAddress = ipaddress;
                            model.InwardNo = sInwardNo;
                            model.EANCode = EANCode;
                            model.Quantity = 1;
                            model.IsMatching = false;
                            model.Reason = "Invalid Barcode";
                            model.Id = 0;
                            _db.TempArticalArrivalEANCodes.Add(model);
                            _db.SaveChanges();
                        }
                        else
                        {
                            var tmpArr = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.InwardNo == sInwardNo).FirstOrDefaultAsync();
                            tmpArr.Quantity = tmpArr.Quantity + 1;
                            _db.SaveChanges();
                        }
                        
                    }
                    else
                    {
                        var stockwithArticle = await _db.stockWithArticles.Where(x => x.EANCode == EANCode && x.FKSupplier == nFKSupplier).FirstOrDefaultAsync();
                        int nBaltoReceive = stockwithArticle.Quantity - stockwithArticle.ArrivedQty;
                        if (nBaltoReceive > 0)
                        {
                            int nRowCount = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode).ToList().Count;
                            if (nRowCount > 0)
                            {
                                if (nRowCount == 1)
                                {
                                    var TempArriEAN = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                    int nExistingQty = TempArriEAN.Quantity;

                                    //var stockwithArticle = await _db.stockWithArticles.Where(x => x.EANCode == model.EANCode).FirstOrDefaultAsync();
                                    //int nBaltoReceive = stockwithArticle.Quantity - stockwithArticle.ArrivedQty;

                                    if (nExistingQty + 1 <= nBaltoReceive)
                                    {
                                        var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                                        TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                        //_db.TempArticalArrivalEANCodes.Update(TempFromdb);
                                        //_db.Entry(TempFromdb).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                    else
                                    {
                                        int nRowCount1 = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                                        if (nRowCount1 == 0)
                                        {
                                            model.IPAddress = ipaddress;
                                            model.InwardNo = sInwardNo;
                                            model.EANCode = EANCode;
                                            model.Quantity = 1;
                                            model.IsMatching = false;
                                            model.Id = 0;
                                            model.Reason = "Part X's Qty";
                                            _db.TempArticalArrivalEANCodes.Add(model);
                                            await _db.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                            TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                            _db.SaveChanges();
                                        }

                                    }
                                }
                                else
                                {
                                    var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                    TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                    _db.SaveChanges();
                                }

                                //partyInfo.Where(x => x.Id == inwardVM.Inward.FKParty).FirstOrDefault().CompanyName;
                                //var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();
                                //TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                //_db.SaveChanges();
                            }
                            else
                            {
                                model.IPAddress = ipaddress;
                                model.InwardNo = sInwardNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = true;
                                model.Reason = "";
                                model.Id = 0;
                                _db.TempArticalArrivalEANCodes.Add(model);
                                _db.SaveChanges();
                                //await _db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            int nRowCount1 = _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).ToList().Count;
                            if (nRowCount1 == 0)
                            {
                                model.IPAddress = ipaddress;
                                model.InwardNo = sInwardNo;
                                model.EANCode = EANCode;
                                model.Quantity = 1;
                                model.IsMatching = false;
                                model.Id = 0;
                                model.Reason = "X's Qty";
                                _db.TempArticalArrivalEANCodes.Add(model);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                var TempFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == EANCode && x.IsMatching == false).FirstAsync();
                                TempFromdb.Quantity = TempFromdb.Quantity + 1;
                                _db.SaveChanges();
                            }
                        }

                    }

                    //model.Id = 0;
                };

                #region UPDATING TEMPORARY INWARD DETAILS
                
                List<MdlTempInwardDtls> tempInwardDtls = new List<MdlTempInwardDtls>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_Inwards @mAction='SELINWARD', @mInwardNo='{sInwardNo}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                Boolean nReadyforImport;
                string sReason = "";
                while (result.Read())
                {
                    //if (result.GetBoolean(34) == true)
                    //{
                    //    var stockwithArticle = await _db.stockWithArticles.Where(x => x.EANCode == result.GetString(24)).FirstOrDefaultAsync();
                    //    int nBaltoReceive = stockwithArticle.Quantity - stockwithArticle.ArrivedQty;
                    //    if (result.GetInt32(7) > nBaltoReceive)
                    //    {
                    //        nReadyforImport = false;
                    //        sReason = "X's Qty";
                    //    }
                    //    else
                    //    {
                    //        nReadyforImport = true;
                    //        sReason = "";
                    //    }
                    //}
                    //else
                    //{
                    //    nReadyforImport = false;
                    //    sReason = result.GetString(35);
                    //}




                    var swa = new MdlTempInwardDtls
                    {
                        //Id = 0,
                        IPAddress = ipaddress,
                        FKInwardNo = result.GetInt32(0),
                        InwardNo = sInwardNo,
                        InwardDt = DateTime.Now,
                        FKMaterial = result.GetInt32(1),
                        FKArticle = result.GetInt32(2),
                        Description = result.GetString(3),
                        Colour = result.GetString(4),
                        Size = result.GetDecimal(5),
                        HSNCode = result.GetInt32(6),
                        Quantity = result.GetInt32(7),
                        IIQuantity = result.GetInt32(8),
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
                        FKSupplier = result.GetInt32(33),
                        MaterialorFinishedProduct = result.GetString(36),
                        FKHSNCode = result.GetInt32(37),
                        DiscountPercentageforSales = 0,
                        MRP = result.GetDecimal(38),
                        FKOffer = 0,
                        OfferType = "",
                        FKCategory = result.GetInt32(39),
                        FKPurchaseOrder = result.GetInt32(42),
                        FKPurchaseOrderMain = result.GetInt32(41),
                        FKPurchaseOrderDtl = result.GetInt32(40),
                        FKQuality = inwardfromDb.FKQuality
                    };
                    tempInwardDtls.Add(swa);
                }

                result.Close();

                _db.TempInwardDtls.AddRange(tempInwardDtls);
                await _db.SaveChangesAsync();

                var TempEANCodeDtls = await _db.TempArticalArrivalEANCodes.OrderBy(c => c.Id).Where(x => x.InwardNo == sInwardNo).ToListAsync();
                string sEANCode;
                foreach (var EANCode in TempEANCodeDtls)
                {
                    sEANCode = EANCode.EANCode;
                    //var TempEANCodesFromdb = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == model.EANCode).FirstAsync();

                    if (_db.TempInwardDtls.Where(x => x.EANCode == sEANCode).ToList().Count > 0)
                    {
                        var TempInwFromdb = await _db.TempInwardDtls.Where(x => x.EANCode == sEANCode).FirstAsync();
                        nReadyforImport = TempInwFromdb.ReadyforImport;
                        sReason = TempInwFromdb.Reason;
                    }
                    else
                    {
                        nReadyforImport = false;
                        sReason = "Wrong Barcode";

                    }
                    //_db.TempInwardDtls.Update(TempEANCodeDtls);
                    var TempEANCodeDtls1 = await _db.TempArticalArrivalEANCodes.Where(x => x.EANCode == sEANCode).FirstOrDefaultAsync();
                    TempEANCodeDtls1.IsMatching = nReadyforImport;
                    TempEANCodeDtls1.Reason = sReason;
                    await _db.SaveChangesAsync();
                }

                #endregion
            }

            var TempInwardDtlsresult = _db.TempInwardDtls.ToList();
            ViewBag.TempInwardDtls = _db.TempInwardDtls.ToList();
            ViewBag.TempEANCodeDtls = _db.TempArticalArrivalEANCodes.Where(x => x.IsMatching == false).ToList();
            var inw = await _db.inwards.Where(x => x.Id == Id).FirstOrDefaultAsync();
            TempData["Inwards"] = inw;
            return View(TempInwardDtlsresult);
        }

        //POST - SCANNED BARCODES
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertInwardDetail(int Id)
        {
            var TempInwardDtls = await _db.TempInwardDtls.OrderBy(c => c.Id).Where(x => x.FKInwardNo == Id && x.ReadyforImport == true).ToListAsync();

            int nFKStage,  nFKStatus;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            nFKStage = lookUpMaster.Where(x => x.FKLookUpCategory == 39 && x.SetAsDefault == true).FirstOrDefault().Id;
            //nFKQuality = lookUpMaster.Where(x => x.FKLookUpCategory == 41 && x.SetAsDefault == true).FirstOrDefault().Id;
            nFKStatus = lookUpMaster.Where(x => x.FKLookUpCategory == 42 && x.SetAsDefault == true).FirstOrDefault().Id;
            string sInwardNo = "";

            foreach (var item in TempInwardDtls)
            {
                inwardDetailVM.InwardDetails.Id = 0;
                inwardDetailVM.InwardDetails.FKInwardNo = item.FKInwardNo;
                inwardDetailVM.InwardDetails.FKMaterial = item.FKMaterial;
                inwardDetailVM.InwardDetails.FKArticle = item.FKArticle;
                inwardDetailVM.InwardDetails.Description = item.Description;
                inwardDetailVM.InwardDetails.Colour = item.Colour;
                inwardDetailVM.InwardDetails.Size = item.Size.ToString();
                inwardDetailVM.InwardDetails.HSNCode = item.HSNCode;
                inwardDetailVM.InwardDetails.Quantity = item.Quantity;
                inwardDetailVM.InwardDetails.IIQuantity = item.IIQuantity;
                inwardDetailVM.InwardDetails.Rate = item.Rate;
                inwardDetailVM.InwardDetails.Value = item.Value;
                inwardDetailVM.InwardDetails.ValueinINR = item.ValueinINR;
                inwardDetailVM.InwardDetails.DiscountPercentage = item.DiscountPercentage;
                inwardDetailVM.InwardDetails.DiscountValue = item.DiscountValue;
                inwardDetailVM.InwardDetails.GrossValue = item.GrossValue;
                inwardDetailVM.InwardDetails.SGSTPercentage = item.SGSTPercentage;
                inwardDetailVM.InwardDetails.SGSTValue = item.SGSTValue;
                inwardDetailVM.InwardDetails.CGSTPercentage = item.CGSTPercentage;
                inwardDetailVM.InwardDetails.CGSTValue = item.CGSTValue;
                inwardDetailVM.InwardDetails.IGSTPercentage = item.IGSTPercentage;
                inwardDetailVM.InwardDetails.IGSTValue = item.IGSTValue;
                inwardDetailVM.InwardDetails.GSTTotalValue = item.GSTTotalValue;
                inwardDetailVM.InwardDetails.OthersValuePlus = item.OthersValuePlus;
                inwardDetailVM.InwardDetails.ItemNettValue = item.ItemNettValue;
                inwardDetailVM.InwardDetails.InwardNo = item.InwardNo;
                sInwardNo = item.InwardNo;
                inwardDetailVM.InwardDetails.InwardDt = item.InwardDt;
                inwardDetailVM.InwardDetails.EANCode = item.EANCode;
                inwardDetailVM.InwardDetails.StockNo = item.StockNo;
                inwardDetailVM.InwardDetails.FKSupplier = item.FKSupplier;
                inwardDetailVM.InwardDetails.FKPurchaseOrder = item.FKPurchaseOrder;
                inwardDetailVM.InwardDetails.FKPurchaseOrderMain = item.FKPurchaseOrderMain;
                inwardDetailVM.InwardDetails.FKPurchaseOrderDetail = item.FKPurchaseOrderDtl;
                inwardDetailVM.InwardDetails.FLAM = item.MaterialorFinishedProduct;
                inwardDetailVM.InwardDetails.FKQuality = item.FKQuality;

                _db.inwardDetails.Add(inwardDetailVM.InwardDetails);
                await _db.SaveChangesAsync();

                int nStckRow = _db.stocks.Where(x => x.EANCode == item.EANCode && x.FKSupplier == item.FKSupplier && x.Rate == item.Rate && x.Quantity > 0).ToList().Count;
                if (nStckRow == 0)
                {
                    var artdtl = await _db.articleDetails.Where(x => x.Id == item.FKArticle).FirstOrDefaultAsync();
                    decimal dRate = artdtl.CostPrice;
                    decimal dvalue = item.Quantity * dRate;

                    StocksVM.Stock.Id = 0;
                    StocksVM.Stock.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                    StocksVM.Stock.FKMaterial = item.FKMaterial;
                    StocksVM.Stock.FKArticleDetail = item.FKArticle;
                    StocksVM.Stock.Description = item.Description;
                    StocksVM.Stock.Colour = item.Colour;
                    StocksVM.Stock.Size = item.Size.ToString();
                    StocksVM.Stock.OrderReferenceNo = item.OrderReferenceNo;
                    StocksVM.Stock.StockInitiatedDate = DateTime.Now;
                    StocksVM.Stock.FKUnit = item.FKUnit;
                    StocksVM.Stock.FKLocation = item.FKLocation;
                    StocksVM.Stock.FKStage = nFKStage;
                    StocksVM.Stock.Quantity = item.Quantity;
                    StocksVM.Stock.Rate = item.Rate;
                    StocksVM.Stock.Value = item.Value;
                    StocksVM.Stock.FKCurrency = item.FKCurrency;
                    StocksVM.Stock.ExchangeRate = 0; //TODO
                    StocksVM.Stock.ValueInINR = item.ValueinINR;
                    StocksVM.Stock.LandedCost = 0;
                    StocksVM.Stock.LandedRate = 0;
                    StocksVM.Stock.FKUOM = item.FKUOM;
                    StocksVM.Stock.FKIIUOM = 0;
                    StocksVM.Stock.FKSource = item.FKSource;
                    StocksVM.Stock.FKQuality = item.FKQuality; //nFKQuality;
                    StocksVM.Stock.FKStatus = nFKStatus;
                    StocksVM.Stock.IsActive = true;
                    StocksVM.Stock.EnteredSystemId = "";
                    StocksVM.Stock.CreatedBy = 1;
                    StocksVM.Stock.CreatedDate = DateTime.Now;
                    StocksVM.Stock.ModifiedBy = 1;
                    StocksVM.Stock.ModifiedDate = DateTime.Now;
                    StocksVM.Stock.EANCode = item.EANCode;
                    StocksVM.Stock.StockNo = item.StockNo;
                    StocksVM.Stock.FKSupplier = item.FKSupplier;
                    StocksVM.Stock.FKHSNCode = item.FKHSNCode;
                    StocksVM.Stock.DiscountPercentageforSales = item.DiscountPercentageforSales;
                    StocksVM.Stock.MRP = item.MRP;
                    StocksVM.Stock.FKOffer = 0;
                    StocksVM.Stock.OfferType = "";
                    StocksVM.Stock.FKCategory = item.FKCategory;
                    StocksVM.Stock.FLAM = item.MaterialorFinishedProduct;
                    StocksVM.Stock.LastTranDate = DateTime.Now;

                    _db.stocks.Add(StocksVM.Stock);
                        await _db.SaveChangesAsync();
                }
                else
                {
                    var stck = await _db.stocks.Where(x => x.FKUnit == item.FKUnit && x.FKLocation == item.FKLocation && x.EANCode == item.EANCode && x.FKSupplier == item.FKSupplier && x.Rate == item.Rate && x.Quantity > 0).FirstOrDefaultAsync();
                    stck.Quantity = stck.Quantity + item.Quantity;
                    decimal rate = stck.Rate;
                    decimal value = stck.Quantity * rate;
                    stck.Value = value;
                    stck.ValueInINR = value;
                    stck.LastTranDate = DateTime.Now;
                    _db.SaveChanges();
                }


                int nRowCount = _db.AllTransactions.Where(x => x.FKTranUnit == item.FKUnit && x.FKTranLocation == item.FKLocation && x.EANCode == item.EANCode).ToList().Count;
                decimal nClosing;
                if(nRowCount == 0)
                {
                    nClosing = 0;
                }
                else
                {
                    var AllTran = await _db.AllTransactions.OrderByDescending(x =>x.Id).Where(x => x.FKTranUnit == item.FKUnit && x.FKTranLocation == item.FKLocation && x.EANCode == item.EANCode).FirstOrDefaultAsync();
                    nClosing = AllTran.BalanceQuantity; 
                }

                AllTransactionVM.AllTransaction.Id = 0;
                AllTransactionVM.AllTransaction.TransactionType = "NEW ARRIVAL";
                AllTransactionVM.AllTransaction.TranId = item.FKInwardNo;
                AllTransactionVM.AllTransaction.TranRefNo = item.InwardNo;
                AllTransactionVM.AllTransaction.TranDate = item.InwardDt;
                AllTransactionVM.AllTransaction.MaterialorFinishedProduct = item.MaterialorFinishedProduct;
                AllTransactionVM.AllTransaction.FKMaterial = item.FKMaterial;
                AllTransactionVM.AllTransaction.FKArticle = item.FKArticle;
                AllTransactionVM.AllTransaction.Description = item.Description;
                AllTransactionVM.AllTransaction.Colour = item.Colour;
                AllTransactionVM.AllTransaction.Size = item.Size.ToString();
                AllTransactionVM.AllTransaction.FKFromUnit = item.FKSupplier;
                AllTransactionVM.AllTransaction.FKFromLocation = 0;
                AllTransactionVM.AllTransaction.FKFromStage = 0;
                AllTransactionVM.AllTransaction.FKToUnit = item.FKUnit;
                AllTransactionVM.AllTransaction.FKToLocation = item.FKLocation;
                AllTransactionVM.AllTransaction.FKToStage = nFKStage;
                AllTransactionVM.AllTransaction.FKQuality = item.FKQuality; // nFKQuality;
                AllTransactionVM.AllTransaction.HSNCode = item.HSNCode;
                AllTransactionVM.AllTransaction.FKUOM = item.FKUOM;
                AllTransactionVM.AllTransaction.InwardQuantity = item.Quantity;
                AllTransactionVM.AllTransaction.OutwardQuantity = 0;
                AllTransactionVM.AllTransaction.BalanceQuantity = nClosing + item.Quantity;
                AllTransactionVM.AllTransaction.Rate = item.Rate;
                AllTransactionVM.AllTransaction.Value = item.Value;
                AllTransactionVM.AllTransaction.FKStatus = nFKStage;
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
                stkWArtfromdb.ArrivedQty = stkWArtfromdb.ArrivedQty + Convert.ToInt32(item.Quantity);
                stkWArtfromdb.BalQty = stkWArtfromdb.ArrivedQty - stkWArtfromdb.SoldQty;
                _db.SaveChanges();

            }

            var tmpInwdtls = _db.TempInwardDtls.Where(x => x.FKInwardNo == Id);
            _db.TempInwardDtls.RemoveRange(tmpInwdtls);
            await _db.SaveChangesAsync();

            var tmpEANCodes = _db.TempArticalArrivalEANCodes.Where(x => x.InwardNo == sInwardNo);
            _db.TempArticalArrivalEANCodes.RemoveRange(tmpEANCodes);
            await _db.SaveChangesAsync();

            decimal nArrivedQty = _db.inwardDetails.Where(x => x.FKInwardNo == Id).Select(x => x.Quantity).ToList().Sum();
            var InwardFromdb = await _db.inwards.Where(x => x.Id == Id).FirstAsync();
            InwardFromdb.ArrivedQuantity = Convert.ToInt32(nArrivedQty);
            _db.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        //GET - EDIT TEMP INWARD DETAIL
        public async Task<IActionResult> EditTempInwardDetail(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var TempInwDtl = await _db.TempInwardDtls.SingleOrDefaultAsync(m => m.Id == Id);
            return View(TempInwDtl);

            //return View();
        }

        //POST - EDIT TEMP INWARD DETAIL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTempInwardDetail(int? Id, MdlTempInwardDtls mdlTempInwardDtls)
        {
            //if (ModelState.IsValid)
            //{

            var tempInwardfromDb = await _db.TempInwardDtls.FindAsync(Id);

            tempInwardfromDb.DiscountPercentage = mdlTempInwardDtls.DiscountPercentage;
            tempInwardfromDb.DiscountValue = mdlTempInwardDtls.DiscountValue;
            tempInwardfromDb.GrossValue = mdlTempInwardDtls.GrossValue;
            tempInwardfromDb.SGSTValue = mdlTempInwardDtls.SGSTValue;
            tempInwardfromDb.CGSTValue = mdlTempInwardDtls.CGSTValue;
            tempInwardfromDb.IGSTValue = mdlTempInwardDtls.IGSTValue;
            tempInwardfromDb.GSTTotalValue = mdlTempInwardDtls.GSTTotalValue;
            tempInwardfromDb.OthersValuePlus = mdlTempInwardDtls.OthersValuePlus;
            tempInwardfromDb.ItemNettValue = mdlTempInwardDtls.ItemNettValue;

            await _db.SaveChangesAsync();
            sScanningMode = "Updates";
            //return RedirectToAction(nameof(InwardDetailScannedCodes(MdlTempArticalArrivalEANCode)));
            return RedirectToAction("InwardDetailScannedCodes", "Inward", null);
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

        //GET - DELETE TEMP INWARD DETAIL
        public async Task<IActionResult> DeleteTempInwardDetail(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var TempInwDtl = await _db.TempInwardDtls.SingleOrDefaultAsync(m => m.Id == Id);
            return View(TempInwDtl);

            //return View();
        }

        //POST - DELETE TEMP INWARD DETAIL
        [HttpPost, ActionName("DeleteTempInwardDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTempInwardDetail(int? Id)
        {
            //if (ModelState.IsValid)
            //{
            var tmpInwDtl = await _db.TempInwardDtls.FindAsync(Id);
            if (tmpInwDtl == null)
            {
                return View();
            }
            _db.TempInwardDtls.Remove(tmpInwDtl);
            await _db.SaveChangesAsync();
            sScanningMode = "Updates";
            return RedirectToAction("InwardDetailScannedCodes", "Inward", null);

        }

        //POST - Update Discount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDiscount()
        {
            var scannedlist = Request.Form["testChar"];



            //var TempInwDtl = await _db.TempInwardDtls.SingleOrDefaultAsync(m => m.Id == Id);
            return View();

            //return View();
        }
        #endregion
    }
}
