using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using OptimizerBeta3.Models.ViewModels.ReportsViewModel;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public PurchaseOrderViewModel purchaseOrderVM { get; set; }
        public PurchaseOrderMainViewModel purchaseOrderMainVM { get; set; }
        public PurchaseOrderDetailViewModel purchaseOrderDetailVM { get; set; }

        public static string sIpAddress;
        public static string ipaddress = string.Empty;
        public PurchaseOrderController(ApplicationDbContext db)
        {
            _db = db;
            purchaseOrderVM = new PurchaseOrderViewModel()
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
                PurchaseOrder = new Models.TransactionTables.PurchaseOrder()
            };

            purchaseOrderMainVM = new PurchaseOrderMainViewModel()
            {
                FKModeofTransport = _db.lookUpMasters,
                FKDeliveryTo = _db.lookUpMasters,
                FKSizeMaster = _db.sizeMasters,
                GetArticleGroup = _db.articleGroups,
                PurchaseOrderMain = new Models.TransactionTables.PurchaseOrderMain()
            };

            purchaseOrderDetailVM = new PurchaseOrderDetailViewModel()
            {
                GetArticle = _db.articleDetails,
                FKOrderStatus = _db.lookUpMasters,
                FKUOM = _db.lookUpMasters,
                PurchaseOrderDetail = new Models.TransactionTables.PurchaseOrderDetails()
            };
        }

        #region PURCHASE ORDER
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;


            return View(await _db.purchaseOrders.OrderByDescending(s => s.Id).Where(x => x.PurchaseOrderDt >= effectStartDate && x.PurchaseOrderDt <= effectEndDate && x.FLAM == "F").ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "PurchaseOrder", new { fromDate = fromDate, toDate = toDate });
        }


        //GET - CREATE
        public async Task<IActionResult> Create()
        {
        
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            purchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();

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

            purchaseOrderVM.FKParty = suppliers.ToList();

            //purchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKOrderMethod = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 64 && c.IsActive == true).ToListAsync();

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
            purchaseOrderVM.FKTypeOfOrder = lmTypeofOrders.ToList();

            //purchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();

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

            purchaseOrderVM.FKCategory = lkms.ToList();

            List<LookUpMaster> lmOrderStatuss = new List<LookUpMaster>();
            string sqlQuery3 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='36'";
            var cmd3 = _db.Database.GetDbConnection().CreateCommand();
            cmd3.CommandText = sqlQuery3;
            _db.Database.OpenConnection();

            var result3 = cmd3.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result3.Read())
            {
                LookUpMaster lmOrderStatus = new LookUpMaster
                {
                    Id = (int)result3["Id"],
                    Description = result3["Description"].ToString()
                };
                lmOrderStatuss.Add(lmOrderStatus);
            }
            purchaseOrderVM.FKOrderStatus = lmOrderStatuss.ToList();
            //purchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();

            ViewBag.lastpo = await _db.purchaseOrders.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            //ViewBag.TempEANCodeDtls = await _db.TempInvoiceDtlEANCodes.Where(x => x.IsMatching == false).ToListAsync();
            //purchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            return View(purchaseOrderVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string Save, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                return View(purchaseOrderVM);
            }

            if (Save == "Save & Import Dtl")
            {
                string path = $"D:\\Language\\Uploads";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(formFile.FileName);
                string filePath = Path.Combine(path, fileName);
                var fileExt = System.IO.Path.GetExtension(formFile.FileName).Substring(1);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                if (fileExt == "xlsx" || fileExt == "xls")
                {
                    string sStatus = "";
                    int nOrderQuantity = 0;
                    int nSlNo = 1;
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            int nRowNo = 1;
                            string sOrderNo, sSizeCode;
                            while (reader.Read()) //Each row of the file
                            {
                                if (nRowNo == 1)
                                {
                                    sOrderNo = reader.GetValue(1).ToString();
                                    int nRowCount = _db.purchaseOrderMains.Where(x => x.OrderReferenceNo == sOrderNo).ToList().Count;
                                    if (nRowCount > 0)
                                    {
                                        sStatus = "ERROR # " + nSlNo.ToString() + ". This Order No already exists.";
                                        nSlNo += 1;
                                    }
                                }
                                if (nRowNo == 2)
                                {
                                    sSizeCode = reader.GetValue(1).ToString();
                                    int nRowCount = _db.sizeMasters.Where(x => x.Description == sSizeCode).ToList().Count;
                                    if (nRowCount == 0)
                                    {
                                        sStatus = sStatus + Environment.NewLine + "ERROR # " + nSlNo.ToString() + ". This Size Code Does Not exists.";
                                        nSlNo += 1;
                                    }
                                }

                                if (nRowNo >= 5)
                                {

                                    if (reader.GetValue(0) == null)
                                    {
                                        break;
                                    }
                                    string sArticleCode = ""; if (reader.GetValue(1) != null) { sArticleCode = reader.GetValue(1).ToString(); }
                                    string sSKU = ""; if (reader.GetValue(2) != null) { sSKU = reader.GetValue(2).ToString(); }

                                    int nRowCount = _db.articleDetails.Where(x => x.ArticleCode == sArticleCode).ToList().Count;
                                    if (nRowCount == 0)
                                    {
                                        sStatus = sStatus + Environment.NewLine + "ERROR # " + nSlNo.ToString() + ". The Article Code Does Not exists, For the SKU [ " + sSKU + " ].";
                                        nSlNo += 1;
                                    }
                                    nOrderQuantity += Convert.ToInt32(reader.GetValue(24));
                                }
                                nRowNo += 1;
                            }
                        }
                        if (nOrderQuantity != purchaseOrderVM.PurchaseOrder.TotalOrderQuantity)
                        {
                            sStatus = sStatus + Environment.NewLine + "ERROR # " + nSlNo.ToString() + ". Entered Order Quantity Mismatches with the Quantities available in the Excel.";
                            nSlNo += 1;
                        }
                        if (sStatus != "")
                        {
                            TempData["ErrorMessage"] = sStatus;
                            return RedirectToAction(nameof(Create));
                        }
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "! ERROR ! Invalid File Format Selected. Only Excel File has to be Loaded !!";
                    return RedirectToAction(nameof(Create));
                }
            }
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sTypeofOrder = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            string sSource = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKSource).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(purchaseOrderVM.PurchaseOrder.FKSeason);
            string sSeason = season.Code;

            var unitInfo = await _db.unitMasters.ToListAsync();
            //purchaseOrderVM.PurchaseOrder.DeliveryTo = companyInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKDeliveryTo).FirstOrDefault().CompanyName;
            purchaseOrderVM.PurchaseOrder.UnitName = unitInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKUnit).FirstOrDefault().CompanyName;
            string sUnitCode = unitInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKUnit).FirstOrDefault().Code;
            string sFLAM = purchaseOrderVM.PurchaseOrder.FLAM;

            string codechar = (sUnitCode.Substring(0, 3) + sFLAM.Substring(0, 1) + sTypeofOrder.Substring(0, 2) + sSeason.Substring(0, 4) + sSource.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.purchaseOrders.Where(x => x.PurchaseOrderNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.purchaseOrders.Where(x => x.PurchaseOrderNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderNo.Substring(12, 4))).ToList().Max();
            }

            purchaseOrderVM.PurchaseOrder.PurchaseOrderNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));


            purchaseOrderVM.PurchaseOrder.Currency = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKCurrency).FirstOrDefault().Description;
            //purchaseOrderVM.PurchaseOrder.ModeofTransport = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKModeofTransport).FirstOrDefault().Description;
            purchaseOrderVM.PurchaseOrder.OrderStatus = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            purchaseOrderVM.PurchaseOrder.PaymentTerms = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKPaymentTerms).FirstOrDefault().Description;
            purchaseOrderVM.PurchaseOrder.Source = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKSource).FirstOrDefault().Description;
            purchaseOrderVM.PurchaseOrder.TypeofOrder = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            purchaseOrderVM.PurchaseOrder.OrderMethod = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKOrderMethod).FirstOrDefault().Description;

            purchaseOrderVM.PurchaseOrder.Season = season.Code;
            var partyInfo = await _db.partyInfos.ToListAsync();
            purchaseOrderVM.PurchaseOrder.SupplierName = partyInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKParty).FirstOrDefault().CompanyName; ;
            purchaseOrderVM.PurchaseOrder.Category = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKCategory).FirstOrDefault().Description;

            _db.purchaseOrders.Add(purchaseOrderVM.PurchaseOrder);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Create));
            if (Save == "Save & New Po")
            {
                return RedirectToAction(nameof(Create));
            }
            else if (Save == "Save & Insert Dtl")
            {
                var po = await _db.purchaseOrders.Where(x => x.PurchaseOrderNo == purchaseOrderVM.PurchaseOrder.PurchaseOrderNo).FirstOrDefaultAsync();
                return RedirectToAction("POMainCreate", "PurchaseOrder", new { Id = po.Id });
            }
            else if (Save == "Save & Import Dtl")
            {
                decimal nSize01, nSize02, nSize03, nSize04, nSize05, nSize06, nSize07, nSize08, nSize09;
                decimal nSize10, nSize11, nSize12, nSize13, nSize14, nSize15, nSize16, nSize17, nSize18;

                nSize01 = 0; nSize02 = 0; nSize03 = 0; nSize04 = 0; nSize05 = 0; nSize06 = 0; nSize07 = 0; nSize08 = 0; nSize09 = 0;
                nSize10 = 0; nSize11 = 0; nSize12 = 0; nSize13 = 0; nSize14 = 0; nSize15 = 0; nSize16 = 0; nSize17 = 0; nSize18 = 0;
                var po = await _db.purchaseOrders.Where(x => x.PurchaseOrderNo == purchaseOrderVM.PurchaseOrder.PurchaseOrderNo).FirstOrDefaultAsync();

                var scndlist = _db.TempFootWearOrderImports.Where(x => x.IPAddress == "");
                _db.TempFootWearOrderImports.RemoveRange(scndlist);
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
                    List<TempFootWearOrderImport> tempFootWearOrderImports = new List<TempFootWearOrderImport>();

                    int nRowNo = 1;
                    int nSlNo = 0;
                    string sOrderNo = "";
                    string sSizeCode = "";
                    DateTime dOrderDate = DateTime.Now;
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            while (reader.Read()) //Each row of the file
                            {
                                if (nRowNo == 1)
                                {
                                    sOrderNo = reader.GetValue(1).ToString();
                                    dOrderDate = Convert.ToDateTime(reader.GetValue(5));
                                }
                                if (nRowNo == 2)
                                {
                                    sSizeCode = reader.GetValue(1).ToString();
                                }
                                if (nRowNo == 4)
                                {
                                    //string sSKU = ""; if (reader.GetValue(0) != null) { sSKU = reader.GetValue(0).ToString(); }
                                    if (reader.GetValue(6) != null) { nSize01 = Convert.ToDecimal(reader.GetValue(6)); }
                                    if (reader.GetValue(7) != null) { nSize02 = Convert.ToDecimal(reader.GetValue(7)); }
                                    if (reader.GetValue(8) != null) { nSize03 = Convert.ToDecimal(reader.GetValue(8)); }
                                    if (reader.GetValue(9) != null) { nSize04 = Convert.ToDecimal(reader.GetValue(9)); }
                                    if (reader.GetValue(10) != null) { nSize05 = Convert.ToDecimal(reader.GetValue(10)); }
                                    if (reader.GetValue(11) != null) { nSize06 = Convert.ToDecimal(reader.GetValue(11)); }
                                    if (reader.GetValue(12) != null) { nSize07 = Convert.ToDecimal(reader.GetValue(12)); }
                                    if (reader.GetValue(13) != null) { nSize08 = Convert.ToDecimal(reader.GetValue(13)); }
                                    if (reader.GetValue(14) != null) { nSize09 = Convert.ToDecimal(reader.GetValue(14)); }
                                    if (reader.GetValue(15) != null) { nSize10 = Convert.ToDecimal(reader.GetValue(15)); }
                                    if (reader.GetValue(16) != null) { nSize11 = Convert.ToDecimal(reader.GetValue(16)); }
                                    if (reader.GetValue(17) != null) { nSize12 = Convert.ToDecimal(reader.GetValue(17)); }
                                    if (reader.GetValue(18) != null) { nSize13 = Convert.ToDecimal(reader.GetValue(18)); }
                                    if (reader.GetValue(19) != null) { nSize14 = Convert.ToDecimal(reader.GetValue(19)); }
                                    if (reader.GetValue(20) != null) { nSize15 = Convert.ToDecimal(reader.GetValue(20)); }
                                    if (reader.GetValue(21) != null) { nSize16 = Convert.ToDecimal(reader.GetValue(21)); }
                                    if (reader.GetValue(22) != null) { nSize17 = Convert.ToDecimal(reader.GetValue(22)); }
                                    if (reader.GetValue(23) != null) { nSize18 = Convert.ToDecimal(reader.GetValue(23)); }

                                }
                                if (nRowNo >= 5)
                                {
                                    nSlNo += 1;
                                    if (reader.GetValue(0) == null)
                                    {
                                        break;
                                    }
                                    string sArticleCode = ""; if (reader.GetValue(1) != null) { sArticleCode = reader.GetValue(1).ToString(); }
                                    string sSKU = ""; if (reader.GetValue(2) != null) { sSKU = reader.GetValue(2).ToString(); }
                                    string sArticle = ""; if (reader.GetValue(3) != null) { sArticle = reader.GetValue(3).ToString(); }
                                    string sColoor = ""; if (reader.GetValue(4) != null) { sColoor = reader.GetValue(4).ToString(); }
                                    string sGroup = ""; if (reader.GetValue(5) != null) { sGroup = reader.GetValue(5).ToString(); }
                                    
                                    DateTime dDeliveryDate = DateTime.Now; if (reader.GetValue(25) != null) { dDeliveryDate = Convert.ToDateTime(reader.GetValue(25)); }// = Convert.ToDateTime(reader.GetValue(30)),
                                    string sRemarks1 = ""; if (reader.GetValue(26) != null) { sRemarks1 = reader.GetValue(26).ToString(); }// = reader.GetValue(31).ToString(),
                                    string sRemarks2 = ""; if (reader.GetValue(27) != null) { sRemarks2 = reader.GetValue(27).ToString(); }// = reader.GetValue(32).ToString(),
                                    string sRemarks3 = ""; if (reader.GetValue(28) != null) { sRemarks3 = reader.GetValue(28).ToString(); }// = reader.GetValue(33).ToString(),
                                    string sRemarks4 = ""; if (reader.GetValue(29) != null) { sRemarks4 = reader.GetValue(29).ToString(); }// = reader.GetValue(34).ToString(),
                                    string sRemarks5 = ""; if (reader.GetValue(30) != null) { sRemarks5 = reader.GetValue(30).ToString(); }// = reader.GetValue(35).ToString()
                                    string sStatus = "Ready for Import";

                                    int nRowCount = _db.articleDetails.Where(x => x.ArticleCode == sArticleCode).ToList().Count;
                                    if (nRowCount == 0)
                                    {
                                        sStatus = "Invalid Article Code";
                                    }
                                    //scannedlist.Add(reader.GetValue(0).ToString());
                                    var podtl = new TempFootWearOrderImport
                                    {
                                        Id = 0,
                                        SlNo = nSlNo,
                                        OrderNo = sOrderNo,
                                        OrderDt = dOrderDate,
                                        SizeCode = sSizeCode,
                                        ArticleCode = sArticleCode,
                                        SKU = sSKU,//reader.GetValue(0).ToString(),
                                        Article = sArticle,//reader.GetValue(2).ToString(),
                                        Color = sColoor,//reader.GetValue(3).ToString(),
                                        Group = sGroup,//reader.GetValue(5).ToString(),
                                        Size01 = nSize01,
                                        Size02 = nSize02,
                                        Size03 = nSize03,
                                        Size04 = nSize04,
                                        Size05 = nSize05,
                                        Size06 = nSize06,
                                        Size07 = nSize07,
                                        Size08 = nSize08,
                                        Size09 = nSize09,
                                        Size10 = nSize10,
                                        Size11 = nSize11,
                                        Size12 = nSize12,
                                        Size13 = nSize13,
                                        Size14 = nSize14,
                                        Size15 = nSize15,
                                        Size16 = nSize16,
                                        Size17 = nSize17,
                                        Size18 = nSize18,
                                        Quantity01 = Convert.ToInt32(reader.GetValue(6)),
                                        Quantity02 = Convert.ToInt32(reader.GetValue(7)),
                                        Quantity03 = Convert.ToInt32(reader.GetValue(8)),
                                        Quantity04 = Convert.ToInt32(reader.GetValue(9)),
                                        Quantity05 = Convert.ToInt32(reader.GetValue(10)),
                                        Quantity06 = Convert.ToInt32(reader.GetValue(11)),
                                        Quantity07 = Convert.ToInt32(reader.GetValue(12)),
                                        Quantity08 = Convert.ToInt32(reader.GetValue(13)),
                                        Quantity09 = Convert.ToInt32(reader.GetValue(14)),
                                        Quantity10 = Convert.ToInt32(reader.GetValue(15)),
                                        Quantity11 = Convert.ToInt32(reader.GetValue(16)),
                                        Quantity12 = Convert.ToInt32(reader.GetValue(17)),
                                        Quantity13 = Convert.ToInt32(reader.GetValue(18)),
                                        Quantity14 = Convert.ToInt32(reader.GetValue(19)),
                                        Quantity15 = Convert.ToInt32(reader.GetValue(20)),
                                        Quantity16 = Convert.ToInt32(reader.GetValue(21)),
                                        Quantity17 = Convert.ToInt32(reader.GetValue(22)),
                                        Quantity18 = Convert.ToInt32(reader.GetValue(23)),
                                        Total = Convert.ToInt32(reader.GetValue(24)),
                                        DeliveryDate = Convert.ToDateTime(reader.GetValue(25)),
                                        Remarks1 = sRemarks1,
                                        Remarks2 =sRemarks2,
                                        Remarks3 = sRemarks3,
                                        Remarks4 = sRemarks4,
                                        Remarks5 = sRemarks5,
                                        status = sStatus,
                                        IPAddress = ipaddress,
                                        POId = po.Id
                                    };
                                    tempFootWearOrderImports.Add(podtl);
                                }
                                nRowNo += 1;
                            }
                        }
                    }
                    _db.TempFootWearOrderImports.AddRange(tempFootWearOrderImports);
                    await _db.SaveChangesAsync();
                }

                return RedirectToAction("UploadPODetail", "PurchaseOrder", new { Id = po.Id });






                //return RedirectToAction("POMainCreate", "PurchaseOrder", new { Id = po.Id });
            }
            else
            {
                return RedirectToAction(nameof(Create));
            }
        }


        public async Task<IActionResult> UploadPODetail(int? Id)
        {
            //FROM PURCHASE ORDERS
            var po = await _db.purchaseOrders.Where(x => x.Id == Id).FirstOrDefaultAsync();
            int nFKPurchaseOrder = po.Id;
            string sPurchaseOrderNo = po.PurchaseOrderNo;
            string sPurchaseOrderMainNo = "";

            //DEFAULT QUANTITY
            int nReceivedQuantity = 0;
            int nCancelledQuantity = 0;
            bool bIsActive = true;
            //EnteredSystemId
            //CreatedBy
            //CreatedDate
            //ModifiedBy
            //ModifiedDate
            //DeleteBy
            //DeletedDate

            //FROM PO MAIN DEFAULT SETTINGS
            //FROM PURCHASE ORDERS
            var poMdefault = await _db.POMainDefaults.FirstOrDefaultAsync();
            int nFKDestination = poMdefault.FKDestination;
            string sDestination = poMdefault.Destination;
            int nFKOrderStatus = poMdefault.FKOrderStatus;
            string sOrderStatus = poMdefault.OrderStatus;
            int nFKDeliveryTo = poMdefault.FKDeliveryTo;
            string sDeliveryTo = poMdefault.DeliveryTo;
            int nFKModeofTransport = poMdefault.FKModeofTransport;
            string sModeofTransport = poMdefault.ModeofTransport;





            //TO BE FINALIZED
            //IsPartDeliveryAllowed



            var tmporddtl = await _db.TempFootWearOrderImports.OrderBy(x => x.SlNo).Where(x => x.POId == po.Id).ToListAsync();

            int nPOMainSlNo = 0;
            int nEntQty = 0;
            foreach (var item in tmporddtl)
            {
                string sSKU = item.SKU;
                var ad = await _db.articleDetails.Where(x => x.ArticleCode == item.ArticleCode).FirstOrDefaultAsync();
                //FROM IMPORTED DATA
                int nFKArticleGroup = ad.FKArticleGroup;
                string sArticleGroup = ad.ArtGrp;
                //OrderReferenceNo
                int nTotalOrderQuantity = item.Total;
                decimal nRate = ad.CostPrice;
                decimal nValue = item.Total * ad.CostPrice;
                DateTime dDeliveryDate = item.DeliveryDate;
                string sSizeCode = item.SizeCode;
                int nEnteredQuantity = item.Total;
                string sArticle = ad.Description;
                int nBalanceQuantity = item.Total;

                var sizeinfo = await _db.sizeMasters.Where(x => x.Description == sSizeCode).FirstOrDefaultAsync();
                int nFKSizeMaster = sizeinfo.Id;

                string sMode = "ADD";

                string codecharMain = (sPurchaseOrderNo).ToUpper();
                var maxcodeMain = 0;

                //if (_db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).ToList().Count > 0)
                //{
                //    maxcodeMain = _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).Select(x => int.Parse(x.PurchaseOrderMainNo.Substring(14, 2))).ToList().Max();
                //}

                //if (_db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).ToList().Count > 0)
                //{
                //    maxcodeMain = _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).Select(x => int.Parse(x.PurchaseOrderMainNo.Substring(14, 2))).ToList().Max();
                //}

                //sPurchaseOrderMainNo = codecharMain + "-" + String.Format("{0:00}", (maxcodeMain + 1));
                //sPurchaseOrderMainNo = codecharMain + "-" + String.Format("{0:00}", (nPOMainSlNo + 1));
                //nPOMainSlNo += 1;

                int nRowCount = _db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == nFKPurchaseOrder).ToList().Count;
                if (nRowCount == 0)
                {
                    //sPurchaseOrderMainNo = sPurchaseOrderNo + "01";
                    if (_db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).ToList().Count > 0)
                    {
                        maxcodeMain = _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).Select(x => int.Parse(x.PurchaseOrderMainNo.Substring(14, 2))).ToList().Max();
                    }
                    sMode = "ADD";
                }
                else
                {
                    int nRowCount1 = _db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == nFKPurchaseOrder && x.FKArticleGroup == nFKArticleGroup).ToList().Count();
                    if (nRowCount1 == 0)
                    {
                        //if (nRowCount > 0 && nRowCount < 10)
                        //{
                        //    sPurchaseOrderMainNo = sPurchaseOrderNo + "0" + (nRowCount + 1).ToString();
                        //}
                        //else
                        //{
                        //    sPurchaseOrderMainNo = sPurchaseOrderNo + (nRowCount + 1).ToString();
                        //}
                        if (_db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).ToList().Count > 0)
                        {
                            maxcodeMain = _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codecharMain)).Select(x => int.Parse(x.PurchaseOrderMainNo.Substring(17, 2))).ToList().Max();
                        }
                        sMode = "ADD";
                    }
                    else
                    {
                        var pomain = await _db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == nFKPurchaseOrder && x.FKArticleGroup == nFKArticleGroup).FirstOrDefaultAsync();
                        pomain.TotalOrderQuantity = pomain.TotalOrderQuantity + nTotalOrderQuantity;
                        pomain.BalanceQuantity = pomain.BalanceQuantity + nTotalOrderQuantity;
                        await _db.SaveChangesAsync();
                        sMode = "APPEND";
                    }
                    
                }
                if (sMode == "ADD")
                    {
                    sPurchaseOrderMainNo = codecharMain + "-" + String.Format("{0:00}", (maxcodeMain + 1));
                    var inspomain = new PurchaseOrderMain();
                        inspomain.FKPurchaseOrder = nFKPurchaseOrder;
                        inspomain.PurchaseOrderNo = sPurchaseOrderNo;
                        inspomain.FKArticleGroup = nFKArticleGroup;
                        inspomain.ArticleGroup = sArticleGroup;
                        inspomain.OrderReferenceNo = item.OrderNo;
                        inspomain.TotalOrderQuantity = nTotalOrderQuantity;
                        inspomain.Value = nValue;
                        inspomain.DeliveryDate = dDeliveryDate;
                        inspomain.IsPartDeliveryAllowed = true; // TODO Coding Pending
                        inspomain.ReceivedQuantity = nReceivedQuantity;
                        inspomain.CancelledQuantity = nCancelledQuantity;
                        inspomain.BalanceQuantity = nBalanceQuantity;
                        inspomain.FKDestination = nFKDestination;
                        inspomain.FKOrderStatus = nFKOrderStatus;
                        inspomain.OrderStatus = sOrderStatus;
                        inspomain.IsActive = true;
                        //inspomain.EnteredSystemId =
                        //inspomain.CreatedBy
                        //inspomain.CreatedDate
                        //inspomain.ModifiedBy
                        //inspomain.ModifiedDate
                        //inspomain.DeleteBy
                        //inspomain.DeletedDate
                        inspomain.DeliveryTo = sDeliveryTo;
                        inspomain.FKDeliveryTo = nFKDeliveryTo;
                        inspomain.FKModeofTransport = nFKModeofTransport;
                        inspomain.FKSizeMaster = nFKSizeMaster;
                        inspomain.ModeofTransport = sModeofTransport;
                        inspomain.PurchaseOrderMainNo = sPurchaseOrderMainNo;
                        inspomain.Destination = sDestination;
                        inspomain.SizeCode = sSizeCode;
                        inspomain.EnteredQuantity = nEnteredQuantity;
                        inspomain.Article = sArticle;

                        _db.purchaseOrderMains.Add(inspomain);
                        await _db.SaveChangesAsync();
                    }
                    else if (sMode == "APPEND")
                    {
                        //TODO : TODO Coding Pending 
                    }


                //}

                var pomain4dtl = await _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo == sPurchaseOrderMainNo).FirstOrDefaultAsync();
                //int nFKPOMain = pomain4dtl.Id;

                
                string codechar = pomain4dtl.PurchaseOrderMainNo.ToUpper();
                var maxcode = 0;

                if (_db.purchaseOrderDetails.Where(x => x.PurchaseOrderDtlNo.Contains(codechar)).ToList().Count > 0)
                {
                    maxcode = _db.purchaseOrderDetails.Where(x => x.PurchaseOrderDtlNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderDtlNo.Substring(21, 3))).ToList().Max();
                }

                string sPurchaseOrderDtlNo = codechar + "-" + String.Format("{0:000}", (maxcode + 1));


                int nFKPurchaseOrderMain = pomain4dtl.Id;
                //string sArticleGroup = s

                int nFKAssortmentId = 0;
                int nNoofCartons = 0;
                decimal dSize01 = sizeinfo.Size01;
                decimal dSize02 = sizeinfo.Size02;
                decimal dSize03 = Convert.ToDecimal(sizeinfo.Size03);
                decimal dSize04 = Convert.ToDecimal(sizeinfo.Size04);
                decimal dSize05 = Convert.ToDecimal(sizeinfo.Size05);
                decimal dSize06 = Convert.ToDecimal(sizeinfo.Size06);
                decimal dSize07 = Convert.ToDecimal(sizeinfo.Size07);
                decimal dSize08 = Convert.ToDecimal(sizeinfo.Size08);
                decimal dSize09 = Convert.ToDecimal(sizeinfo.Size09);
                decimal dSize10 = Convert.ToDecimal(sizeinfo.Size10);
                decimal dSize11 = Convert.ToDecimal(sizeinfo.Size11);
                decimal dSize12 = Convert.ToDecimal(sizeinfo.Size12);
                decimal dSize13 = Convert.ToDecimal(sizeinfo.Size13);
                decimal dSize14 = Convert.ToDecimal(sizeinfo.Size14);
                decimal dSize15 = Convert.ToDecimal(sizeinfo.Size15);
                decimal dSize16 = Convert.ToDecimal(sizeinfo.Size16);
                decimal dSize17 = Convert.ToDecimal(sizeinfo.Size17);
                decimal dSize18 = Convert.ToDecimal(sizeinfo.Size18);

                int nQuantity01 = item.Quantity01;
                int nQuantity02 = item.Quantity02;
                int nQuantity03 = item.Quantity03;
                int nQuantity04 = item.Quantity04;
                int nQuantity05 = item.Quantity05;
                int nQuantity06 = item.Quantity06;
                int nQuantity07 = item.Quantity07;
                int nQuantity08 = item.Quantity08;
                int nQuantity09 = item.Quantity09;
                int nQuantity10 = item.Quantity10;
                int nQuantity11 = item.Quantity11;
                int nQuantity12 = item.Quantity12;
                int nQuantity13 = item.Quantity13;
                int nQuantity14 = item.Quantity14;
                int nQuantity15 = item.Quantity11;
                int nQuantity16 = item.Quantity16;
                int nQuantity17 = item.Quantity17;
                int nQuantity18 = item.Quantity18;

                var podtl = new PurchaseOrderDetails();
                podtl.FKPurchaseOrderMain = pomain4dtl.Id;
                podtl.ArticleGroup = sArticleGroup;
                podtl.FKArticle = ad.Id;
                podtl.ArticleDescription = ad.Description;
                podtl.ArticleColor = ad.ColorDescription;
                podtl.OrderReferenceNo = item.OrderNo; // TODO Coding Pending
                podtl.TotalOrderQuantity = item.Total;
                podtl.Rate = nRate;
                podtl.Value = nValue;
                podtl.FKAssortmentId = nFKAssortmentId;
                podtl.NoofCartons = nNoofCartons;
                podtl.Size01 = dSize01;
                podtl.Quantity01 = nQuantity01;
                podtl.Size02 = dSize02;
                podtl.Quantity02 = nQuantity02;
                podtl.Size03 = dSize03;
                podtl.Quantity03 = nQuantity03;
                podtl.Size04 = dSize04;
                podtl.Quantity04 = nQuantity04;
                podtl.Size05 = dSize05;
                podtl.Quantity05 = nQuantity05;
                podtl.Size06 = dSize06;
                podtl.Quantity06 = nQuantity06;
                podtl.Size07 = dSize07;
                podtl.Quantity07 = nQuantity07;
                podtl.Size08 = dSize08;
                podtl.Quantity08 = nQuantity08;
                podtl.Size09 = dSize09;
                podtl.Quantity09 = nQuantity09;
                podtl.Size10 = dSize10;
                podtl.Quantity10 = nQuantity10;
                podtl.Size11 = dSize11;
                podtl.Quantity11 = nQuantity11;
                podtl.Size12 = dSize12;
                podtl.Quantity12 = nQuantity12;
                podtl.Size13 = dSize13;
                podtl.Quantity13 = nQuantity13;
                podtl.Size14 = dSize14;
                podtl.Quantity14 = nQuantity14;
                podtl.Size15 = dSize15;
                podtl.Quantity15 = nQuantity15;
                podtl.Size16 = dSize16;
                podtl.Quantity16 = nQuantity16;
                podtl.Size17 = dSize17;
                podtl.Quantity17 = nQuantity17;
                podtl.Size18 = dSize18;
                podtl.Quantity18 = nQuantity18;
                podtl.DeliveryDate = item.DeliveryDate;
                podtl.IsPartDeliveryAllowed = true;
                podtl.ReceivedQuantity = nReceivedQuantity;
                podtl.CancelledQuantity = nCancelledQuantity;
                podtl.BalanceQuantity = item.Total;
                podtl.FKOrderStatus = nFKOrderStatus;
                podtl.OrderStatus = sOrderStatus;
                podtl.IsActive = bIsActive;
                //podtl.EnteredSystemId
                //podtl.CreatedBy
                //podtl.CreatedDate
                //podtl.ModifiedBy
                //podtl.ModifiedDate
                //podtl.DeleteBy
                //podtl.DeletedDate
                podtl.PurchaseOrderDtlNo = sPurchaseOrderDtlNo;
                podtl.PurchaseOrderMainNo = sPurchaseOrderMainNo;
                podtl.PurchaseOrderNo = sPurchaseOrderNo;
                podtl.FKUOM = 1;

                nEntQty += item.Total;
                //FKUOM

                _db.purchaseOrderDetails.Add(podtl);
                await _db.SaveChangesAsync();
            }

            var pOfromDb = await _db.purchaseOrders.FindAsync(Id);
            po.EnteredQuantity = nEntQty;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - CREATE

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            purchaseOrderVM.PurchaseOrder = await _db.purchaseOrders.SingleOrDefaultAsync(m => m.Id == id);
            purchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            purchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            return View(purchaseOrderVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseOrderViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            //string sTypeofOrder = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKTypeOfOrder).FirstOrDefault().Description;
            //string sSource = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKSource).FirstOrDefault().Description;


            var pOfromDb = await _db.purchaseOrders.FindAsync(id);

            pOfromDb.FKOrderStatus = model.PurchaseOrder.FKOrderStatus;
            pOfromDb.FKPaymentTerms = model.PurchaseOrder.FKPaymentTerms;
            pOfromDb.FKCurrency = model.PurchaseOrder.FKCurrency;
            //pOfromDb.FKModeofTransport = model.PurchaseOrder.FKModeofTransport;
            pOfromDb.Remarks = model.PurchaseOrder.Remarks;
            //pOfromDb.FKDeliveryTo = model.PurchaseOrder.FKDeliveryTo;
            //var companyInfo = await _db.companyInfos.ToListAsync();
            //pOfromDb.DeliveryTo = companyInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKDeliveryTo).FirstOrDefault().CompanyName;
            //pOfromDb.DeliveryTo = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKDeliveryTo).FirstOrDefault().Description;
            //pOfromDb.ModeofTransport = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKModeofTransport).FirstOrDefault().Description;
            pOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            pOfromDb.PaymentTerms = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKPaymentTerms).FirstOrDefault().Description;
            pOfromDb.Currency = lookUpMaster.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKCurrency).FirstOrDefault().Description;
            pOfromDb.ModifiedBy = model.PurchaseOrder.ModifiedBy;
            pOfromDb.ModifiedDate = model.PurchaseOrder.ModifiedDate;

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
            purchaseOrderVM.PurchaseOrder = await _db.purchaseOrders.SingleOrDefaultAsync(m => m.Id == id);
            purchaseOrderVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKOrderStatus = await _db.lookUpMasters.Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKPOType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 35 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 28 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKTypeOfOrder = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
            purchaseOrderVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            purchaseOrderVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            return View(purchaseOrderVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, PurchaseOrderViewModel model)
        {
            var pOfromDb = await _db.purchaseOrders.FindAsync(id);

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            pOfromDb.FKOrderStatus = model.PurchaseOrder.FKOrderStatus;
            pOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.PurchaseOrder.FKOrderStatus).FirstOrDefault().Description;
            pOfromDb.Remarks = model.PurchaseOrder.Remarks;
            pOfromDb.DeleteBy = model.PurchaseOrder.DeleteBy;
            pOfromDb.DeletedDate = model.PurchaseOrder.DeletedDate;

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
            var pOfromDb = await _db.purchaseOrders.FindAsync(Id);

            int nRowCount = _db.stockWithArticles.Where(x => x.FKOrderId == Id).ToList().Count;
            if (nRowCount == 0)
            {
                List<StockWithArticle> stockWithArticles = new List<StockWithArticle>();
                DbDataReader result;

                string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='GENSTCK', @mPurchaseOrderNo='{pOfromDb.PurchaseOrderNo}'";
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
                        FLAM = "F",
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

                //_db.purchaseOrders.Add(purchaseOrderVM.PurchaseOrder);
                //await _db.SaveChangesAsync();

                //var query = // Ensure `query` is `IQueryable<T>` instead of using `IEnumerable<T>`. But this code has to use `var` because its type-argument is an anonymous-type.
                //            from po in _db.purchaseOrders
                //            join pom in _db.purchaseOrderMains
                //            on po.Id equals pom.FKPurchaseOrder
                //            join pod in _db.purchaseOrderDetails
                //            on pom.Id equals pod.FKPurchaseOrderMain
                //            join stk in _db.stockWithArticles
                //            on pod.Id equals stk.FKOrderDetailId
                //            where po.Id == Id
                //            select new
                //            {
                //                stk.Id,
                //                stk.StockNo,
                //                stk.Brand,
                //                stk.Product,
                //                stk.ArticleNo,
                //                stk.ArticleDescription,
                //                stk.Variant,
                //                stk.ColorDescription,
                //                stk.Size,
                //                stk.ItemDescription,
                //                stk.ArticleName,
                //                stk.LeatherType,
                //                stk.Group,
                //                stk.Dept,
                //                stk.EANCode,
                //                stk.Type,
                //                stk.Category,
                //                stk.Vendor,
                //                stk.DOM,
                //                stk.SICM,
                //                stk.MRP,
                //                stk.DealerPrice,
                //                stk.CostPrice,
                //                stk.ProductTax,
                //                stk.Quantity,
                //                stk.FKArticleDetailId,
                //                stk.FKOrderDetailId,
                //                stk.ArrivedQty,
                //                stk.BalQty,
                //                stk.SoldQty,
                //                stk.LastTranDate,
                //                stk.StockInitiatedDate,
                //                stk.FKOffer,
                //                stk.OfferType,
                //                stk.FKCategory
                //            };
                //var list = await query.ToListAsync().ConfigureAwait(false);
                //ViewBag.StockWithArticles = list.ToList();
            }
            var stkwitharticle = await _db.stockWithArticles.Where(x => x.FKOrderId == Id).ToListAsync();
            return View(stkwitharticle);
        }
        #endregion

        #region PURCHASE ORDER MAIN
        public async Task<IActionResult> POMainIndex(int Id)
        {
            var po = await _db.purchaseOrders.FindAsync(Id);
            TempData["PurchaseOrder"] = po;
            //nFKPO = po.Id;
            //sPurchaseOrderNo = po.PurchaseOrderNo;

            return View(await _db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> POMainCreate(int Id)
        {
            var po = await _db.purchaseOrders.FindAsync(Id);
            TempData["PurchaseOrder"] = po;
            //sPurchaseOrderNo = po.PurchaseOrderNo;

            purchaseOrderMainVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            purchaseOrderMainVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            purchaseOrderMainVM.FKSizeMaster = await _db.sizeMasters.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            purchaseOrderMainVM.GetArticleGroup = await _db.articleGroups.OrderBy(c => c.Description).Where(c => c.IsActive && c.FKCategory == po.FKCategory).ToListAsync();
            purchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            purchaseOrderMainVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            return View(purchaseOrderMainVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> POMainCreate(string Save, PurchaseOrderMainViewModel purchaseOrderMainVM)
        {
            if (!ModelState.IsValid)
            {
                return View(purchaseOrderMainVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string codechar = (purchaseOrderMainVM.PurchaseOrderMain.PurchaseOrderNo).ToUpper();
            var maxcode = 0;

            if (_db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderMainNo.Substring(14, 2))).ToList().Max();
            }

            purchaseOrderMainVM.PurchaseOrderMain.PurchaseOrderMainNo = codechar + "-" + String.Format("{0:00}", (maxcode + 1));


            var articlegroup = await _db.articleGroups.ToListAsync();
            int nFKArticleGroup = articlegroup.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKArticleGroup).FirstOrDefault().FKGroup;
            purchaseOrderMainVM.PurchaseOrderMain.Article = articlegroup.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKArticleGroup).FirstOrDefault().ArticleName;
            purchaseOrderMainVM.PurchaseOrderMain.ArticleGroup = lookUpMaster.Where(x => x.Id == nFKArticleGroup).FirstOrDefault().Description;
            purchaseOrderMainVM.PurchaseOrderMain.OrderStatus = lookUpMaster.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;
            purchaseOrderMainVM.PurchaseOrderMain.ModeofTransport = lookUpMaster.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKModeofTransport).FirstOrDefault().Description;
            purchaseOrderMainVM.PurchaseOrderMain.DeliveryTo = lookUpMaster.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKDeliveryTo).FirstOrDefault().Description;
            purchaseOrderMainVM.PurchaseOrderMain.Destination = lookUpMaster.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKDestination).FirstOrDefault().Description;

            var sizemaster = await _db.sizeMasters.ToListAsync();
            purchaseOrderMainVM.PurchaseOrderMain.SizeCode = sizemaster.Where(x => x.Id == purchaseOrderMainVM.PurchaseOrderMain.FKSizeMaster).FirstOrDefault().Description;

            //var partyInfo = await _db.partyInfos.ToListAsync();
            //purchaseOrderVM.PurchaseOrder.SupplierName = partyInfo.Where(x => x.Id == purchaseOrderVM.PurchaseOrder.FKParty).FirstOrDefault().CompanyName; ;

            _db.purchaseOrderMains.Add(purchaseOrderMainVM.PurchaseOrderMain);
            await _db.SaveChangesAsync();

            var pOfromDb = await _db.purchaseOrders.FindAsync(purchaseOrderMainVM.PurchaseOrderMain.FKPurchaseOrder);
            pOfromDb.EnteredQuantity = (int?)_db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == pOfromDb.Id).Sum(x => x.TotalOrderQuantity);
            await _db.SaveChangesAsync();


            if (Save == "Save & Continue")
            {
                if (pOfromDb.EnteredQuantity < pOfromDb.TotalOrderQuantity)
                {
                    return RedirectToAction("POMainCreate", "PurchaseOrder", new { Id = purchaseOrderMainVM.PurchaseOrderMain.FKPurchaseOrder });
                }
                else
                {
                    return RedirectToAction("POMainIndex", "PurchaseOrder", new { Id = purchaseOrderMainVM.PurchaseOrderMain.FKPurchaseOrder });
                }
            }
            else
            {
                var po = await _db.purchaseOrderMains.Where(x => x.PurchaseOrderMainNo == purchaseOrderMainVM.PurchaseOrderMain.PurchaseOrderMainNo).FirstOrDefaultAsync();
                return RedirectToAction("PODtlCreate", "PurchaseOrder", new { Id = po.Id });
            }

        }

        //GET - EDIT
        public async Task<IActionResult> POMainEdit(int Id)
        {
            var poMain = await _db.purchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;

            purchaseOrderMainVM.PurchaseOrderMain = await _db.purchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);
            purchaseOrderMainVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            purchaseOrderMainVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            purchaseOrderMainVM.FKSizeMaster = await _db.sizeMasters.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            purchaseOrderMainVM.GetArticleGroup = await _db.articleGroups.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            purchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            purchaseOrderMainVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            return View(purchaseOrderMainVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> POMainEdit(int id, PurchaseOrderMainViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var pOMainfromDb = await _db.purchaseOrderMains.FindAsync(id);

            pOMainfromDb.FKOrderStatus = model.PurchaseOrderMain.FKOrderStatus;
            pOMainfromDb.FKModeofTransport = model.PurchaseOrderMain.FKModeofTransport;
            pOMainfromDb.FKDeliveryTo = model.PurchaseOrderMain.FKDeliveryTo;
            pOMainfromDb.FKDestination = model.PurchaseOrderMain.FKDestination;

            pOMainfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.PurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;
            pOMainfromDb.ModeofTransport = lookUpMaster.Where(x => x.Id == model.PurchaseOrderMain.FKModeofTransport).FirstOrDefault().Description;
            pOMainfromDb.DeliveryTo = lookUpMaster.Where(x => x.Id == model.PurchaseOrderMain.FKDeliveryTo).FirstOrDefault().Description;
            pOMainfromDb.Destination = lookUpMaster.Where(x => x.Id == model.PurchaseOrderMain.FKDestination).FirstOrDefault().Description;

            pOMainfromDb.OrderReferenceNo = model.PurchaseOrderMain.OrderReferenceNo;
            pOMainfromDb.TotalOrderQuantity = model.PurchaseOrderMain.TotalOrderQuantity;
            pOMainfromDb.DeliveryDate = model.PurchaseOrderMain.DeliveryDate;
            pOMainfromDb.IsPartDeliveryAllowed = model.PurchaseOrderMain.IsPartDeliveryAllowed;
            pOMainfromDb.ModifiedBy = model.PurchaseOrderMain.ModifiedBy;
            pOMainfromDb.ModifiedDate = model.PurchaseOrderMain.ModifiedDate;

            await _db.SaveChangesAsync();

            var pOfromDb = await _db.purchaseOrders.FindAsync(pOMainfromDb.FKPurchaseOrder);
            pOfromDb.EnteredQuantity = (int?)_db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == pOMainfromDb.FKPurchaseOrder).Sum(x => x.TotalOrderQuantity);
            await _db.SaveChangesAsync();
            return RedirectToAction("POMainIndex", "PurchaseOrder", new { Id = pOMainfromDb.FKPurchaseOrder });



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
            var poMain = await _db.purchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;

            purchaseOrderMainVM.PurchaseOrderMain = await _db.purchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);
            purchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();


            return View(purchaseOrderMainVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> POMainCancel(int id, PurchaseOrderMainViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var pOMainfromDb = await _db.purchaseOrderMains.FindAsync(id);

            pOMainfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.PurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;
            pOMainfromDb.DeleteBy = model.PurchaseOrderMain.DeleteBy;
            pOMainfromDb.DeletedDate = model.PurchaseOrderMain.DeletedDate;
            await _db.SaveChangesAsync();

            if (_db.purchaseOrderMains.Where(x => x.FKPurchaseOrder == pOMainfromDb.FKPurchaseOrder && x.OrderStatus != "Cancel").ToList().Count == 0)
            {
                var pOfromDb = await _db.purchaseOrders.FindAsync(pOMainfromDb.FKPurchaseOrder);
                pOfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.PurchaseOrderMain.FKOrderStatus).FirstOrDefault().Description;

                pOfromDb.DeleteBy = model.PurchaseOrderMain.DeleteBy;
                pOfromDb.DeletedDate = model.PurchaseOrderMain.DeletedDate;

                await _db.SaveChangesAsync();
            }



            return RedirectToAction("POMainIndex", "PurchaseOrder", new { Id = pOMainfromDb.FKPurchaseOrder });



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
        public async Task<IActionResult> POMainDetail(int Id)
        {
            var poMain = await _db.purchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;

            purchaseOrderMainVM.PurchaseOrderMain = await _db.purchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);

            return View(purchaseOrderMainVM);
        }
        #endregion

        #region PURCHASE ORDER DETAIL
        public async Task<IActionResult> PODetailIndex(int Id)
        {
            var poMain = await _db.purchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            //nFKPOMain = poMain.Id;
            //nFKArticleGroup = poMain.FKArticleGroup;
            //sPurchaseOrderMainNo = poMain.PurchaseOrderMainNo;

            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;

            return View(await _db.purchaseOrderDetails.Where(x => x.FKPurchaseOrderMain == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> PODtlCreate(int Id)
        {
            var poMain = await _db.purchaseOrderMains.FindAsync(Id);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            var sizemaster = await _db.sizeMasters.FindAsync(poMain.FKSizeMaster);
            TempData["SizeMaster"] = sizemaster;
            ViewBag.Supplier = await _db.partyInfos.FindAsync(po.FKParty);

            purchaseOrderDetailVM.GetArticle = await _db.articleDetails.OrderBy(c => c.Description).Where(c => c.FKArticleGroup == poMain.FKArticleGroup && c.IsActive).ToListAsync();
            purchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            purchaseOrderDetailVM.FKUOM = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 29 && c.IsActive == true).ToListAsync();
            return View(purchaseOrderDetailVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PODtlCreate(string SaveDtl, PurchaseOrderDetailViewModel purchaseOrderDetailVM)
        {
            if (!ModelState.IsValid)
            {
                return View(purchaseOrderDetailVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string codechar = (purchaseOrderDetailVM.PurchaseOrderDetail.PurchaseOrderMainNo).ToUpper();
            var maxcode = 0;

            if (_db.purchaseOrderDetails.Where(x => x.PurchaseOrderDtlNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.purchaseOrderDetails.Where(x => x.PurchaseOrderDtlNo.Contains(codechar)).Select(x => int.Parse(x.PurchaseOrderDtlNo.Substring(21, 3))).ToList().Max();
            }

            purchaseOrderDetailVM.PurchaseOrderDetail.PurchaseOrderDtlNo = codechar + "-" + String.Format("{0:000}", (maxcode + 1));


            var articledetail = await _db.articleDetails.ToListAsync();
            purchaseOrderDetailVM.PurchaseOrderDetail.ArticleDescription = articledetail.Where(x => x.Id == purchaseOrderDetailVM.PurchaseOrderDetail.FKArticle).FirstOrDefault().Description;
            purchaseOrderDetailVM.PurchaseOrderDetail.OrderStatus = lookUpMaster.Where(x => x.Id == purchaseOrderDetailVM.PurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;

            _db.purchaseOrderDetails.Add(purchaseOrderDetailVM.PurchaseOrderDetail);
            await _db.SaveChangesAsync();

            var pOMainfromDb = await _db.purchaseOrderMains.FindAsync(purchaseOrderDetailVM.PurchaseOrderDetail.FKPurchaseOrderMain);
            pOMainfromDb.EnteredQuantity = _db.purchaseOrderDetails.Where(x => x.FKPurchaseOrderMain == purchaseOrderDetailVM.PurchaseOrderDetail.FKPurchaseOrderMain).Sum(x => x.TotalOrderQuantity);
            await _db.SaveChangesAsync();



            if (SaveDtl == "Save & Continue")
            {
                if (pOMainfromDb.EnteredQuantity < pOMainfromDb.TotalOrderQuantity)
                {
                    return RedirectToAction("PODtlCreate", "PurchaseOrder", new { Id = purchaseOrderDetailVM.PurchaseOrderDetail.FKPurchaseOrderMain });
                }
                else
                {
                    return RedirectToAction("PODetailIndex", "PurchaseOrder", new { Id = purchaseOrderDetailVM.PurchaseOrderDetail.FKPurchaseOrderMain });
                }
            }
            else
            {
                return RedirectToAction("PODetailIndex", "PurchaseOrder", new { Id = purchaseOrderDetailVM.PurchaseOrderDetail.FKPurchaseOrderMain });
            }

        }

        //GET - EDIT
        public async Task<IActionResult> PODtlEdit(int? Id)
        {
            var poDetail = await _db.purchaseOrderDetails.FindAsync(Id);
            TempData["PurchaseOrderDetail"] = poDetail;
            var poMain = await _db.purchaseOrderMains.FindAsync(poDetail.FKPurchaseOrderMain);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            var sizemaster = await _db.sizeMasters.FindAsync(poMain.FKSizeMaster);
            TempData["SizeMaster"] = sizemaster;


            purchaseOrderDetailVM.PurchaseOrderDetail = await _db.purchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);
            purchaseOrderDetailVM.GetArticle = await _db.articleDetails.OrderBy(c => c.Description).Where(c => c.FKArticleGroup == poMain.FKArticleGroup && c.IsActive).ToListAsync();
            purchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();

            //purchaseOrderMainVM.PurchaseOrderMain = await _db.purchaseOrderMains.SingleOrDefaultAsync(m => m.Id == Id);
            //purchaseOrderMainVM.FKDeliveryTo = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 50 && c.IsActive == true).ToListAsync();
            //purchaseOrderMainVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();
            //purchaseOrderMainVM.FKSizeMaster = await _db.sizeMasters.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            //purchaseOrderMainVM.GetArticleGroup = await _db.articleGroups.OrderBy(c => c.Description).Where(c => c.IsActive).ToListAsync();
            //purchaseOrderMainVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.IsActive == true).ToListAsync();
            //purchaseOrderMainVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();

            //return View(purchaseOrderMainVM);

            return View(purchaseOrderDetailVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PODtlEdit(int id, PurchaseOrderDetailViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var articledetail = await _db.articleDetails.ToListAsync();

            var pODtlfromDb = await _db.purchaseOrderDetails.FindAsync(id);

            pODtlfromDb.FKOrderStatus = model.PurchaseOrderDetail.FKOrderStatus;
            pODtlfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.PurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;
            pODtlfromDb.FKArticle = model.PurchaseOrderDetail.FKArticle;
            pODtlfromDb.ArticleDescription = articledetail.Where(x => x.Id == model.PurchaseOrderDetail.FKArticle).FirstOrDefault().Description;
            pODtlfromDb.TotalOrderQuantity = model.PurchaseOrderDetail.TotalOrderQuantity;
            pODtlfromDb.Rate = model.PurchaseOrderDetail.Rate;
            pODtlfromDb.Value = model.PurchaseOrderDetail.Value;
            pODtlfromDb.Quantity01 = model.PurchaseOrderDetail.Quantity01;
            pODtlfromDb.Quantity02 = model.PurchaseOrderDetail.Quantity02;
            pODtlfromDb.Quantity03 = model.PurchaseOrderDetail.Quantity03;
            pODtlfromDb.Quantity04 = model.PurchaseOrderDetail.Quantity04;
            pODtlfromDb.Quantity05 = model.PurchaseOrderDetail.Quantity05;
            pODtlfromDb.Quantity06 = model.PurchaseOrderDetail.Quantity06;
            pODtlfromDb.Quantity07 = model.PurchaseOrderDetail.Quantity07;
            pODtlfromDb.Quantity08 = model.PurchaseOrderDetail.Quantity08;
            pODtlfromDb.Quantity09 = model.PurchaseOrderDetail.Quantity09;
            pODtlfromDb.Quantity10 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity11 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity12 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity13 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity14 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity15 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity16 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity17 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.Quantity18 = model.PurchaseOrderDetail.Quantity10;
            pODtlfromDb.DeliveryDate = model.PurchaseOrderDetail.DeliveryDate;
            pODtlfromDb.IsPartDeliveryAllowed = model.PurchaseOrderDetail.IsPartDeliveryAllowed;
            pODtlfromDb.ModifiedBy = model.PurchaseOrderDetail.ModifiedBy;
            pODtlfromDb.ModifiedDate = model.PurchaseOrderDetail.ModifiedDate;
            await _db.SaveChangesAsync();

            var pOMainfromDb = await _db.purchaseOrderMains.FindAsync(pODtlfromDb.FKPurchaseOrderMain);
            pOMainfromDb.EnteredQuantity = _db.purchaseOrderDetails.Where(x => x.FKPurchaseOrderMain == pODtlfromDb.FKPurchaseOrderMain).Sum(x => x.TotalOrderQuantity);
            await _db.SaveChangesAsync();

            return RedirectToAction("PODetailIndex", "PurchaseOrder", new { Id = pODtlfromDb.FKPurchaseOrderMain });
        }

        //GET - CANCEL
        public async Task<IActionResult> PODtlCancel(int? Id)
        {
            var poDetail = await _db.purchaseOrderDetails.FindAsync(Id);
            TempData["PurchaseOrderDetail"] = poDetail;
            var poMain = await _db.purchaseOrderMains.FindAsync(poDetail.FKPurchaseOrderMain);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            var sizemaster = await _db.sizeMasters.FindAsync(poMain.FKSizeMaster);
            TempData["SizeMaster"] = sizemaster;


            purchaseOrderDetailVM.PurchaseOrderDetail = await _db.purchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);
            purchaseOrderDetailVM.FKOrderStatus = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 36 && c.Description == "Cancel" && c.IsActive == true).ToListAsync();

            return View(purchaseOrderDetailVM);
        }

        //POST - CANCEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PODtlCancel(int id, PurchaseOrderDetailViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var pODtlfromDb = await _db.purchaseOrderDetails.FindAsync(id);

            pODtlfromDb.FKOrderStatus = model.PurchaseOrderDetail.FKOrderStatus;
            pODtlfromDb.OrderStatus = lookUpMaster.Where(x => x.Id == model.PurchaseOrderDetail.FKOrderStatus).FirstOrDefault().Description;
            pODtlfromDb.DeleteBy = model.PurchaseOrderDetail.DeleteBy;
            pODtlfromDb.DeletedDate = model.PurchaseOrderDetail.DeletedDate;
            await _db.SaveChangesAsync();

            return RedirectToAction("PODetailIndex", "PurchaseOrder", new { Id = pODtlfromDb.FKPurchaseOrderMain });
        }

        //GET - DETAIL
        public async Task<IActionResult> PODtlDetail(int? Id)
        {
            var poDetail = await _db.purchaseOrderDetails.FindAsync(Id);
            TempData["PurchaseOrderDetail"] = poDetail;
            var poMain = await _db.purchaseOrderMains.FindAsync(poDetail.FKPurchaseOrderMain);
            TempData["PurchaseOrderMain"] = poMain;
            var po = await _db.purchaseOrders.FindAsync(poMain.FKPurchaseOrder);
            TempData["PurchaseOrder"] = po;
            var sizemaster = await _db.sizeMasters.FindAsync(poMain.FKSizeMaster);
            TempData["SizeMaster"] = sizemaster;


            purchaseOrderDetailVM.PurchaseOrderDetail = await _db.purchaseOrderDetails.SingleOrDefaultAsync(m => m.Id == Id);

            return View(purchaseOrderDetailVM);
        }
        #endregion
    }
}
