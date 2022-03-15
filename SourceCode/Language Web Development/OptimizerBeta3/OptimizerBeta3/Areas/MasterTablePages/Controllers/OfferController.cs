using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class OfferController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OfferViewModel OfferVM { get; set; }
        public OfferController(ApplicationDbContext db)
        {
            _db = db;
            OfferVM = new OfferViewModel()
            {
                FKOffer = _db.lookUpMasters,
                FKOfferCategory = _db.lookUpMasters,
                FKOfferType = _db.lookUpMasters,
                Offers = new Models.MasterTables.Offers()
            };
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _db.offers.ToListAsync());
        //}

        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.Date.AddDays(-7);
            var effectEndDate = toDate ?? DateTime.Now.AddMonths(3);
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.offers.OrderByDescending(s => s.Id).Where(x => x.FromDate >= effectStartDate && x.FromDate <= effectEndDate).ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "Offer", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            var offer = await _db.offers.Where(o => o.IsRangeOffer == true).ToListAsync();
            int maxRangeNo = 0;

            if (_db.offers.ToList().Count > 0)
            {
                maxRangeNo = Convert.ToInt32(_db.offers.Select(x => x.RangeSlNo).ToList().Max());
            }

            TempData["RangeSlNo"] = maxRangeNo + 1;

            List<LookUpMaster> lmOfferTypes = new List<LookUpMaster>();
            string sqlQuery1 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='65'";
            var cmd1 = _db.Database.GetDbConnection().CreateCommand();
            cmd1.CommandText = sqlQuery1;
            _db.Database.OpenConnection();

            var result1 = cmd1.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result1.Read())
            {
                LookUpMaster lmOfferType = new LookUpMaster
                {
                    Id = (int)result1["Id"],
                    Description = result1["Description"].ToString()
                };
                lmOfferTypes.Add(lmOfferType);
            }
            OfferVM.FKOfferType = lmOfferTypes.ToList();

            OfferVM.FKOffer = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 59 && c.IsActive == true).ToListAsync();
            OfferVM.FKOfferCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 60 && c.IsActive == true).ToListAsync();

            return View(OfferVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferViewModel OfferVM)
        {
            if (ModelState.IsValid)
            {
                if  (OfferVM.Offers.DiscountPercentage + OfferVM.Offers.DiscountValue + OfferVM.Offers.OfferPair <= 0)
                {
                    if (OfferVM.Offers.IsProductCompliment == false)
                    {
                        TempData["ErrorMessage"] = "Offer of Should contain either Offer Percentage, Offer Value, Offer Pair or Is Product Compliment. This offer does not have any!";
                        return RedirectToAction(nameof(Create));
                    }
                }

                int nRowCount = _db.offers.Where(x => x.FKOffer == OfferVM.Offers.FKOffer && x.FromDate == OfferVM.Offers.FromDate && x.ToDate == OfferVM.Offers.ToDate).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Offer of Same Name & Same Period Already Exists";
                    return RedirectToAction(nameof(Create));
                }

                nRowCount = _db.offers.Where(x => x.CouponCode == OfferVM.Offers.CouponCode && x.IsActive == true).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Offer of Same Coupon Code Already Exists";
                    return RedirectToAction(nameof(Create));
                }

                var lookUpMaster = await _db.lookUpMasters.ToListAsync();
                OfferVM.Offers.OfferType = lookUpMaster.Where(x => x.Id == OfferVM.Offers.FKOfferType).FirstOrDefault().Description;
                OfferVM.Offers.OfferName = lookUpMaster.Where(x => x.Id == OfferVM.Offers.FKOffer).FirstOrDefault().Description;
                string sOfferNameCode = lookUpMaster.Where(x => x.Id == OfferVM.Offers.FKOffer).FirstOrDefault().ShortCode;
                string sFromDate = OfferVM.Offers.FromDate.ToString("MM") + OfferVM.Offers.FromDate.ToString("yy");
                string sToDate = OfferVM.Offers.ToDate.ToString("MM") + OfferVM.Offers.ToDate.ToString("yy");

                string codechar = (OfferVM.Offers.OfferType.Substring(0, 1) + sOfferNameCode.Substring(0, 4) + sFromDate + sToDate).ToUpper();
                var maxcode = 0;

                if (_db.offers.Where(x => x.OfferId.Contains(codechar)).ToList().Count > 0)
                {
                    maxcode = _db.offers.Where(x => x.OfferId.Contains(codechar)).Select(x => int.Parse(x.OfferId.Substring(14, 1))).ToList().Max();
                }

                OfferVM.Offers.OfferId = codechar + "-" + (maxcode + 1).ToString();

                OfferVM.Offers.OfferCategory = lookUpMaster.Where(x => x.Id == OfferVM.Offers.FKOfferCategory).FirstOrDefault().Description;

                _db.offers.Add(OfferVM.Offers);
                await _db.SaveChangesAsync();

                //_db.offers.Add(offers);
                //await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(OfferVM);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Offer = await _db.offers.FindAsync(id);
            if (Offer == null)
            {
                return NotFound();
            }

            OfferVM.Offers = await _db.offers.Include(m => m.LookUpMasterType).Include(m => m.LookUpMasterOffer).Include(m => m.LookUpMasterCategory).SingleOrDefaultAsync(m => m.Id == id);
            OfferVM.FKOfferType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 65 && c.IsActive == true).ToListAsync();
            OfferVM.FKOffer = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 59 && c.IsActive == true).ToListAsync();
            OfferVM.FKOfferCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 60 && c.IsActive == true).ToListAsync();

            if (OfferVM.Offers == null)
            {
                return NotFound();
            }
            return View(OfferVM);

        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,OfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lookUpMaster = await _db.lookUpMasters.ToListAsync();
                //OfferVM.Offers.OfferType = lookUpMaster.Where(x => x.Id == OfferVM.Offers.FKOfferType).FirstOrDefault().Description;
                //OfferVM.Offers.OfferName = lookUpMaster.Where(x => x.Id == OfferVM.Offers.FKOffer).FirstOrDefault().Description;

                var Offer = await _db.offers.FindAsync(Id);
                Offer.FKOffer = model.Offers.FKOffer;
                Offer.OfferName = lookUpMaster.Where(x => x.Id == model.Offers.FKOffer).FirstOrDefault().Description;
                Offer.FromDate = model.Offers.FromDate;
                Offer.ToDate = model.Offers.ToDate;
                Offer.IsExtendable = model.Offers.IsExtendable;
                Offer.IsAnniverseryBased = model.Offers.IsAnniverseryBased;
                Offer.DiscountPercentage = model.Offers.DiscountPercentage;
                Offer.IsRangeOffer = model.Offers.IsRangeOffer;
                Offer.RangeSlNo = model.Offers.RangeSlNo;
                Offer.ApplicableForItem = model.Offers.ApplicableForItem;
                Offer.ApplicableForInvoice = model.Offers.ApplicableForInvoice;
                Offer.MinimumBillValue = model.Offers.MinimumBillValue;
                Offer.MaximumDiscountValue = model.Offers.MaximumDiscountValue;
                Offer.IsActive = model.Offers.IsActive;
                Offer.ModifiedBy = model.Offers.ModifiedBy;
                Offer.ModifiedDate = DateTime.Now;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //GET - Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Offer = await _db.offers.FindAsync(id);
            if (Offer == null)
            {
                return NotFound();
            }
            return View(Offer);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Offer = await _db.offers.FindAsync(id);
            if (Offer == null)
            {
                return NotFound();
            }
            return View(Offer);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int? id)
        public async Task<IActionResult> Delete(Offers offers)
        {
            //var Offer = await _db.offers.FindAsync(id);

            //if (Offer == null)
            //{
            //    return View();
            //}
            //_db.offers.Remove(Offer);
            //await _db.SaveChangesAsync();
            if (ModelState.IsValid)
            {
                int nOfferId = offers.Id;

                string sqlQuery = $"EXEC SLI_Offer @mAction='RELEASEOFFER', @mFKOffer='{nOfferId}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();
                var result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                //var stock = _db.stocks.Where(x => x.Quantity > 0 && x.FKOffer == nOfferId).AsQueryable();
                //foreach (var item in stock)
                //{
                //    item.FKOffer = 0;
                //    item.OfferType = "";
                //}
                //_db.UpdateRange(stock);
                //_db.SaveChanges();

                //var stockwithArticle = _db.stockWithArticles.Where(x => x.Quantity > 0 && x.FKOffer == nOfferId).AsQueryable();
                //foreach (var item in stockwithArticle)
                //{
                //    item.FKOffer = 0;
                //    item.OfferType = "";
                //}
                //_db.UpdateRange(stockwithArticle);
                //_db.SaveChanges();

                var Offer = await _db.offers.FindAsync(offers.Id);
                Offer.IsActive = offers.IsActive;
                Offer.ModifiedBy = offers.ModifiedBy;
                Offer.ModifiedDate = DateTime.Now;
                Offer.DeleteBy = offers.ModifiedBy;
                Offer.DeletedDate = DateTime.Now;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> OfferMapping(int Id)
        {
            #region "STOCK DETAILS FOR ARTICLE GROUP OFFER MAPPING"

            var tempstk = _db.TempStockForOfferMappings;
            if (tempstk != null)
            {
                _db.TempStockForOfferMappings.RemoveRange(tempstk);
                await _db.SaveChangesAsync();

            }

            List<TempStockForOfferMapping> tempstock = new List<TempStockForOfferMapping>();
            DbDataReader result;

            string sqlQuery = $"EXEC SLI_Stocks @mAction='SELSTK'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {

                var swa = new TempStockForOfferMapping
                {
                    FKCategory = result.GetInt32(0),
                    Category = result.GetString(1),
                    FKUnit = result.GetInt32(2),
                    UnitName = result.GetString(3),
                    Description = result.GetString(4),
                    ArticleGroupName = result.GetString(5),
                    FKLocation = result.GetInt32(6),
                    LocationName = result.GetString(7),
                    FKArticleDetail = result.GetInt32(8),
                    ArticleDescription = result.GetString(9),
                    Colour = result.GetString(10),
                    OrderReferenceNo = result.GetString(11),
                    Size = result.GetString(12),
                    Quantity = result.GetDecimal(13),
                    MRP = result.GetDecimal(14),
                    FKStage = result.GetInt32(15),
                    Status = result.GetString(16)
                };
                tempstock.Add(swa);
            }
            result.Close();

            _db.TempStockForOfferMappings.AddRange(tempstock);
            await _db.SaveChangesAsync();
            #endregion
            var offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ViewBag.UnAssignedStock = await _db.stocks.Where(x => x.FKOffer == 0 && x.Quantity > 0).ToListAsync();
            ViewBag.StockWithOffer = await _db.stocks.Where(x => x.FKOffer == Id && x.Quantity > 0).ToListAsync();
            ViewBag.UnAssignedStockGroupdBy = await _db.TempStockForOfferMappings.Where(x => x.Quantity > 0).ToListAsync();
            TempData["OfferId"] = Id;
            return View(offer);
        }

        //GET - OfferDtlVenueMapping
        public async Task<IActionResult> OfferDtlVenueMapping(int Id)
        {
            var tempstk = _db.TempOfferVenueMappings;
            _db.TempOfferVenueMappings.RemoveRange(tempstk);
            await _db.SaveChangesAsync();

            List<TempOfferVenueMapping> tempOM = new List<TempOfferVenueMapping>();
            DbDataReader result;

            string sqlQuery = $"EXEC SLI_Offer @mAction='OFFERVM'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {

                var swa = new TempOfferVenueMapping
                {
                    FKUnitId = result.GetInt32(0),
                    StrCode = result.GetString(1),
                    CompanyName = result.GetString(2),
                    Address = result.GetString(3),
                    City = result.GetString(4),
                    FKOffer = result.GetInt32(5)
                };
                tempOM.Add(swa);
            }
            result.Close();

            _db.TempOfferVenueMappings.AddRange(tempOM);
            await _db.SaveChangesAsync();

            var offervm = await _db.OfferDtlVenueMappings.Where(x => x.FKOffer == Id).FirstOrDefaultAsync();
            ViewBag.UnAssignedVenue = await _db.TempOfferVenueMappings.Where(x => x.FKOffer == 0).ToListAsync();
            ViewBag.AssignedVenue = await _db.TempOfferVenueMappings.Where(x => x.FKOffer == Id).ToListAsync();
            ViewBag.Offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(offervm);
        }

        //POST - OfferDtlVenueMapping
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfferDtlVenueMappingAssign(string FKCompany, string OfferId)
        {
            if (ModelState.IsValid)
            {
                var offerdtlvm = new OfferDtlVenueMapping();
                offerdtlvm.FKOffer = Convert.ToInt32(OfferId);
                offerdtlvm.FKUnitName = Convert.ToInt32(FKCompany);
                _db.OfferDtlVenueMappings.Add(offerdtlvm);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("OfferDtlVenueMapping", "Offer", new { Id = Convert.ToInt32(OfferId) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfferDtlVenueMappingUnAssign(string FKCompany, string OfferId)
        {
            if (ModelState.IsValid)
            {
                var offId = _db.OfferDtlVenueMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKUnitName == Convert.ToInt32(FKCompany));
                _db.OfferDtlVenueMappings.RemoveRange(offId);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("OfferDtlVenueMapping", "Offer", new { Id = Convert.ToInt32(OfferId) });
        }

        //GET - OfferDtlStockMappingArticleGroupWise
        public async Task<IActionResult> OfferSMAGWise(int Id)
        {
            var tempstk = _db.TempOfferStockMappings;
            _db.TempOfferStockMappings.RemoveRange(tempstk);
            await _db.SaveChangesAsync();

            List<TempOfferStockMapping> tempOM = new List<TempOfferStockMapping>();
            DbDataReader result;

            string sqlQuery = $"EXEC SLI_Offer @mAction='OFFERSMARG'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {

                var swa = new TempOfferStockMapping
                {
                    ArtId = result.GetInt32(0),
                    ArticleGroup = result.GetString(1),
                    ArticleName = "",//result.GetString(2),
                    ArticleColor = "",//result.GetString(3),
                    Stock = result.GetInt32(2),
                    OfferId = result.GetInt32(3),
                    OfferAddMode = result.GetString(4)
                };
                tempOM.Add(swa);
            }
            result.Close();

            _db.TempOfferStockMappings.AddRange(tempOM);
            await _db.SaveChangesAsync();

            var offervm = await _db.OfferDtlStockMappings.Where(x => x.FKOffer == Id).ToListAsync();
            //ViewBag.TempStock = await _db.TempOfferStockMappings.OrderByDescending(x => x.OfferId).ThenBy(x => x.ArticleGroup).ThenBy(x => x.ArtId).ToListAsync();
            ViewBag.TempStock = await _db.TempOfferStockMappings.OrderByDescending(x => x.OfferId).ThenByDescending(x => x.Stock).ThenBy(x => x.ArticleGroup).ThenBy(x => x.ArticleName).ThenBy(x => x.ArtId).ToListAsync();
            //ViewBag.UnAssignedStock = await _db.TempOfferStockMappings.Where(x => x.OfferId != Id).ToListAsync();
            //ViewBag.AssignedStock = await _db.TempOfferStockMappings.Where(x => x.OfferId == Id).ToListAsync();
            ViewBag.Offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(offervm);
        }

        //POST - OfferDtlStockMappingArticleGroupWise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfferSMAGWise(int Id, string OfferId, string Save)
        {
            if (ModelState.IsValid)
            {
                if (Save == "Assign")
                {
                    var lkma = await _db.lookUpMasters.Where(x => x.Id == Id).FirstOrDefaultAsync();

                    var offrDtl = new OfferDtlStockMapping();
                    offrDtl.FKOffer = Convert.ToInt32(OfferId);
                    offrDtl.FKArticleGroup = Id;
                    offrDtl.FKArticle = 0;
                    offrDtl.StockNo = "";
                    offrDtl.EANCode = "";
                    offrDtl.ArticleColor = "";
                    offrDtl.ArticleGroup = lkma.Description;
                    offrDtl.ArticleName = "";
                    offrDtl.OfferAddMode = "AG";
                    offrDtl.IsActive = true;
                    _db.OfferDtlStockMappings.Add(offrDtl);
                    await _db.SaveChangesAsync();

                    //List<TempStockWithArticle> stkWA = new List<TempStockWithArticle>();
                    DbDataReader result;

                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFAGADD', @mFKArticleGroup='{Id}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    //while (result.Read())
                    //{
                    //    var swa = new TempStockWithArticle
                    //    {
                    //        StockId = result.GetInt32(0),
                    //        OfferCount = result.GetInt32(1)
                    //    };
                    //    stkWA.Add(swa);
                    //}
                    //result.Close();

                    //_db.TempStockWithArticles.AddRange(stkWA);
                    //await _db.SaveChangesAsync();

                    //var tempSWA = _db.TempStockWithArticles.OrderBy(x => x.Id).ToList();
                    //foreach (var item in tempSWA)
                    //{
                    //    int nId = item.StockId;
                    //    int nOfferCount = item.OfferCount;

                    //    var swafromdb = await _db.stockWithArticles.Where(x => x.Id == nId).FirstOrDefaultAsync();
                    //    swafromdb.OfferCount = swafromdb.OfferCount + 1;
                    //    _db.SaveChanges();
                    //}
                    result.Close();

                }
                else
                {
                    var offrDtl = await _db.OfferDtlStockMappings.Where(x => x.FKArticleGroup == Id && x.FKOffer == Convert.ToInt32(OfferId)).ToListAsync();
                    _db.OfferDtlStockMappings.RemoveRange(offrDtl);
                    await _db.SaveChangesAsync();

                    //List<TempStockWithArticle> stkWA = new List<TempStockWithArticle>();
                    DbDataReader result;

                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFAGLESS', @mFKArticleGroup='{Id}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    //while (result.Read())
                    //{
                    //    var swa = new TempStockWithArticle
                    //    {
                    //        StockId = result.GetInt32(0),
                    //        OfferCount = result.GetInt32(1)
                    //    };
                    //    stkWA.Add(swa);
                    //}
                    //result.Close();

                    //_db.TempStockWithArticles.AddRange(stkWA);
                    //await _db.SaveChangesAsync();

                    //var tempSWA = _db.TempStockWithArticles.OrderBy(x => x.Id).ToList();
                    //foreach (var item in tempSWA)
                    //{
                    //    int nId = item.StockId;
                    //    int nOfferCount = item.OfferCount;

                    //    var swafromdb = await _db.stockWithArticles.Where(x => x.Id == nId).FirstOrDefaultAsync();
                    //    swafromdb.OfferCount = swafromdb.OfferCount - 1;
                    //    _db.SaveChanges();
                    //}
                    result.Close();
                }
            }
            return RedirectToAction("OfferSMAGWise", "Offer", new { Id = Convert.ToInt32(OfferId) });
        }

        //GET - OfferDtlStockMappingArticleNameWise
        public async Task<IActionResult> OfferSMARTWise(int Id)
        {
            var tempstk = _db.TempOfferStockMappings;
            _db.TempOfferStockMappings.RemoveRange(tempstk);
            await _db.SaveChangesAsync();

            List<TempOfferStockMapping> tempOM = new List<TempOfferStockMapping>();
            DbDataReader result;

            string sqlQuery = $"EXEC SLI_Offer @mAction='OFFERSMART', @mFKOffer='{Id}'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {

                var swa = new TempOfferStockMapping
                {
                    ArtId = result.GetInt32(0),
                    ArticleGroup = result.GetString(1),
                    ArticleName = result.GetString(2),
                    ArticleColor = "",//result.GetString(3),
                    Stock = result.GetInt32(3),
                    OfferId = result.GetInt32(4),
                    OfferAddMode = result.GetString(5)
                };
                tempOM.Add(swa);
            }
            result.Close();

            _db.TempOfferStockMappings.AddRange(tempOM);
            await _db.SaveChangesAsync();

            var offervm = await _db.OfferDtlStockMappings.Where(x => x.FKOffer == Id).ToListAsync();
            ViewBag.TempStock = await _db.TempOfferStockMappings.OrderByDescending(x => x.OfferId).ThenByDescending(x => x.Stock).ThenBy(x => x.ArticleGroup).ThenBy(x => x.ArticleName).ThenBy(x => x.ArtId).ToListAsync();
            ViewBag.Offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(offervm);
        }

        //POST - OfferDtlStockMappingArticleNameWise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfferSMARTWise(int Id, string OfferId, string Save)
        {
            if (ModelState.IsValid)
            {
                if (Save == "Assign")
                {
                    var Art = await _db.articleGroups.Where(x => x.Id == Id).FirstOrDefaultAsync();
                    int FKArticleGroup = Art.FKGroup;

                    var offrDtl = new OfferDtlStockMapping();
                    offrDtl.FKOffer = Convert.ToInt32(OfferId);
                    offrDtl.FKArticleGroup = FKArticleGroup;
                    offrDtl.FKArticle = Id;
                    offrDtl.FKArticleDtl = 0;
                    offrDtl.StockNo = "";
                    offrDtl.EANCode = "";
                    offrDtl.ArticleColor = "";
                    offrDtl.ArticleGroup = Art.ArticleGroupName;
                    offrDtl.ArticleName = Art.ArticleName;
                    offrDtl.OfferAddMode = "AD";
                    offrDtl.IsActive = true;
                    _db.OfferDtlStockMappings.Add(offrDtl);
                    await _db.SaveChangesAsync();

                    DbDataReader result;

                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFARTADD', @mFKArticleGroup='{Id}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    result.Close();

                }
                else
                {
                    var offrDtl = await _db.OfferDtlStockMappings.Where(x => x.FKArticle == Id && x.FKOffer == Convert.ToInt32(OfferId)).ToListAsync();
                    _db.OfferDtlStockMappings.RemoveRange(offrDtl);
                    await _db.SaveChangesAsync();

                    DbDataReader result;
                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFARTLESS', @mFKArticleGroup='{Id}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    result.Close();
                }
            }
            return RedirectToAction("OfferSMARTWise", "Offer", new { Id = Convert.ToInt32(OfferId) });
        }

        //GET - OfferDtlStockMappingArticleColorWise
        public async Task<IActionResult> OfferSMARTCOLORWise(int Id)
        {
            var tempstk = _db.TempOfferStockMappings;
            _db.TempOfferStockMappings.RemoveRange(tempstk);
            await _db.SaveChangesAsync();

            List<TempOfferStockMapping> tempOM = new List<TempOfferStockMapping>();
            DbDataReader result;

            string sqlQuery = $"EXEC SLI_Offer @mAction='OFFERSMARTWCOLOR', @mFKOffer='{Id}'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {

                var swa = new TempOfferStockMapping
                {
                    ArtId = result.GetInt32(0),
                    ArticleGroup = result.GetString(1),
                    ArticleName = result.GetString(2),
                    ArticleColor = result.GetString(3),
                    Stock = result.GetInt32(4),
                    OfferId = result.GetInt32(5),
                    OfferAddMode = result.GetString(6)
                };
                tempOM.Add(swa);
            }
            result.Close();

            _db.TempOfferStockMappings.AddRange(tempOM);
            await _db.SaveChangesAsync();

            var offervm = await _db.OfferDtlStockMappings.Where(x => x.FKOffer == Id).ToListAsync();
            ViewBag.TempStock = await _db.TempOfferStockMappings.OrderByDescending(x => x.OfferId).ThenByDescending(x => x.Stock).ThenBy(x => x.ArticleGroup).ThenBy(x => x.ArticleName).ThenBy(x => x.ArtId).ToListAsync();
            ViewBag.Offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(offervm);
        }

        //POST - OfferDtlStockMappingArticleColorWise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfferSMARTCOLORWise(int Id, string OfferId, string Save)
        {
            if (ModelState.IsValid)
            {
                if (Save == "Assign")
                {
                    var ArtDtl = await _db.articleDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();
                    int FKArticle = ArtDtl.FKArticleGroup;

                    var Art = await _db.articleGroups.Where(x => x.Id == FKArticle).FirstOrDefaultAsync();
                    int FKArticleGroup = Art.FKGroup;

                    var offrDtl = new OfferDtlStockMapping();
                    offrDtl.FKOffer = Convert.ToInt32(OfferId);
                    offrDtl.FKArticleGroup = FKArticleGroup;
                    offrDtl.FKArticle = FKArticle;
                    offrDtl.FKArticleDtl = Id;
                    offrDtl.StockNo = "";
                    offrDtl.EANCode = "";
                    offrDtl.ArticleColor = ArtDtl.ColorDescription;
                    offrDtl.ArticleGroup = Art.ArticleGroupName;
                    offrDtl.ArticleName = Art.ArticleName;
                    offrDtl.OfferAddMode = "AC";
                    offrDtl.IsActive = true;
                    _db.OfferDtlStockMappings.Add(offrDtl);
                    await _db.SaveChangesAsync();

                    DbDataReader result;

                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFARTDTLADD', @mFKArticleGroup='{Id}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    result.Close();

                }
                else
                {
                    var offrDtl = await _db.OfferDtlStockMappings.Where(x => x.FKArticleDtl == Id && x.FKOffer == Convert.ToInt32(OfferId)).ToListAsync();
                    _db.OfferDtlStockMappings.RemoveRange(offrDtl);
                    await _db.SaveChangesAsync();

                    DbDataReader result;
                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFARTDTLLESS', @mFKArticleGroup='{Id}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    result.Close();
                }
            }
            return RedirectToAction("OfferSMARTCOLORWise", "Offer", new { Id = Convert.ToInt32(OfferId) });
        }

        //GET - OfferDtlStockMappingStockNoAndEANCode
        public async Task<IActionResult> OfferSMStockAndEANCode(int Id)
        {
            var offervm = await _db.OfferDtlStockMappings.Where(x => x.FKOffer == Id).ToListAsync();
            ViewBag.Offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(offervm);
        }

        //POST - OfferDtlStockMappingStockNoAndEANCode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfferSMStockAndEANCode(int Id, string OfferId, string Save, string StockNo, string EANCode)
        {

            if (Save == "Stock No")
            {
                if (StockNo == "" || StockNo == null)
                {
                    TempData["ErrorMessage"] = "Stock No Not Entered!";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                int nRowCount = _db.stockWithArticles.Where(x => x.StockNo == StockNo).ToList().Count();

                if ( nRowCount == 0)
                {
                    TempData["ErrorMessage"] = "Invalid Stock No!";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                nRowCount = _db.stockWithArticles.Where(x => x.StockNo == StockNo && x.FKOffer > 0 ).ToList().Count();

                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "This Stock No is already mapped with Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.StockNo == StockNo && x.FKOffer == Convert.ToInt32(OfferId)).ToList().Count();
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "This Stock No Already Included in this Offer!";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var SWA = await _db.stockWithArticles.Where(x => x.StockNo == StockNo).FirstOrDefaultAsync();
                int nFKArticleDetail = SWA.FKArticleDetailId;

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKArticleDtl == nFKArticleDetail).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Article Color Of this Stock No. Already Included in this Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var ArtDtl = await _db.articleDetails.Where(x => x.Id == nFKArticleDetail).FirstOrDefaultAsync();
                int nFKArticle = ArtDtl.FKArticleGroup;

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKArticle == nFKArticle).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Article Of this Stock No. Already Included in this Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var Art = await _db.articleGroups.Where(x => x.Id == nFKArticle).FirstOrDefaultAsync();
                int nFKArticleGroup = Art.FKGroup;

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKArticleGroup == nFKArticleGroup).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Article Group Of this Stock No. Already Included in this Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var OfrDtlStkMap = new OfferDtlStockMapping();
                OfrDtlStkMap.FKOffer = Convert.ToInt32(OfferId);
                OfrDtlStkMap.FKArticleGroup = nFKArticleGroup;
                OfrDtlStkMap.FKArticle = nFKArticle;
                OfrDtlStkMap.StockNo = StockNo;
                OfrDtlStkMap.EANCode = SWA.EANCode;
                OfrDtlStkMap.IsActive = true;
                OfrDtlStkMap.FKArticleDtl = nFKArticleDetail;
                OfrDtlStkMap.ArticleColor = ArtDtl.ColorDescription;
                OfrDtlStkMap.ArticleGroup = Art.ArticleGroupName;
                OfrDtlStkMap.ArticleName = Art.ArticleName;
                OfrDtlStkMap.OfferAddMode = "STKNO";
                _db.OfferDtlStockMappings.Add(OfrDtlStkMap);
                await _db.SaveChangesAsync();

                DbDataReader result;

                string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFSTKNOADD', @mStockNo='{StockNo.Substring(0,10)}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                result.Close();

            }
            if (Save == "All EAN Code" || Save == "Single Order" || Save == "Single EAN Code")
            {
                if (EANCode == "" || EANCode == null)
                {
                    TempData["ErrorMessage"] = "EANCode Not Entered!";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                int nRowCount = _db.stockWithArticles.Where(x => x.EANCode == EANCode).ToList().Count();

                if (nRowCount == 0)
                {
                    TempData["ErrorMessage"] = "Invalid EANCode!";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.EANCode == EANCode && x.FKOffer == Convert.ToInt32(OfferId)).ToList().Count();
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "This EANCode Already Included in this Offer!";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var SWA = await _db.stockWithArticles.Where(x => x.EANCode == EANCode).FirstOrDefaultAsync();
                int nFKArticleDetail = SWA.FKArticleDetailId;

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKArticleDtl == nFKArticleDetail).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Article Color Of this EANCode Already Included in this Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var ArtDtl = await _db.articleDetails.Where(x => x.Id == nFKArticleDetail).FirstOrDefaultAsync();
                int nFKArticle = ArtDtl.FKArticleGroup;

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKArticle == nFKArticle).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Article Of this EANCode Already Included in this Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var Art = await _db.articleGroups.Where(x => x.Id == nFKArticle).FirstOrDefaultAsync();
                int nFKArticleGroup = Art.FKGroup;

                nRowCount = _db.OfferDtlStockMappings.Where(x => x.FKOffer == Convert.ToInt32(OfferId) && x.FKArticleGroup == nFKArticleGroup).ToList().Count;
                if (nRowCount > 0)
                {
                    TempData["ErrorMessage"] = "Article Group Of this EANCode Already Included in this Offer";
                    return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
                }

                var OfrDtlStkMap = new OfferDtlStockMapping();
                OfrDtlStkMap.FKOffer = Convert.ToInt32(OfferId);
                OfrDtlStkMap.FKArticleGroup = nFKArticleGroup;
                OfrDtlStkMap.FKArticle = nFKArticle;
                OfrDtlStkMap.StockNo = SWA.StockNo;
                OfrDtlStkMap.EANCode = EANCode;
                OfrDtlStkMap.IsActive = true;
                OfrDtlStkMap.FKArticleDtl = nFKArticleDetail;
                OfrDtlStkMap.ArticleColor = ArtDtl.ColorDescription;
                OfrDtlStkMap.ArticleGroup = Art.ArticleGroupName;
                OfrDtlStkMap.ArticleName = Art.ArticleName;
                if (Save == "All EAN Code")
                    OfrDtlStkMap.OfferAddMode = "EAN";
                else if (Save == "Single Order")
                    OfrDtlStkMap.OfferAddMode = "EANSO";
                else if (Save == "Single EAN Code")
                    OfrDtlStkMap.OfferAddMode = "SIEAN";
                _db.OfferDtlStockMappings.Add(OfrDtlStkMap);
                await _db.SaveChangesAsync();

                DbDataReader result;
                string sqlQuery = "";
                if (Save == "All EAN Code")
                {
                    sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFARTDTLADD', @mFKArticleGroup='{Convert.ToInt32(EANCode.Substring(1, 4))}'";
                }
                else if (Save == "Single Order")
                {
                    sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFORDDTLADD', @mFKOrderDetailId='{Convert.ToInt32(EANCode.Substring(5, 5))}'";
                }
                else if (Save == "Single EAN Code")
                {
                    sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFEANADD', @mEANCode='{EANCode}'";
                }
                var cmd = _db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                _db.Database.OpenConnection();

                result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                result.Close();

            }

            if (Save == "Release")
            {
                var ofrstkDtl = await _db.OfferDtlStockMappings.Where(x => x.Id == Id && x.FKOffer == Convert.ToInt32(OfferId)).FirstOrDefaultAsync();
                string sStockNo = ofrstkDtl.StockNo;
                string sEANCode = ofrstkDtl.EANCode;
                string sOfferAddMode = ofrstkDtl.OfferAddMode;

                var offrDtl = await _db.OfferDtlStockMappings.Where(x => x.Id == Id && x.FKOffer == Convert.ToInt32(OfferId)).ToListAsync();
                _db.OfferDtlStockMappings.RemoveRange(offrDtl);
                await _db.SaveChangesAsync();
                
                if (sStockNo != "")
                {
                    DbDataReader result;
                    string sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFSTKNOLESS', @mStockNo='{sStockNo.Substring(0, 10)}', @mOfferId='{Convert.ToInt32(OfferId)}'";
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    result.Close();
                }
                else if (sEANCode != "")
                {
                    DbDataReader result;
                    string sqlQuery = "";
                    if (sOfferAddMode == "EAN")
                    {
                        sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFARTDTLLESS', @mFKArticleGroup='{Convert.ToInt32(sEANCode.Substring(1, 4))}'";
                    }
                    else if (sOfferAddMode == "EANSO")
                    {
                        sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFORDDTLLESS', @mFKOrderDetailId='{Convert.ToInt32(sEANCode.Substring(5, 5))}'";
                    }
                    else if (sOfferAddMode == "SIEAN")
                    {
                        sqlQuery = $"EXEC SLI_StockWithArticle @mAction='UPDSTKWOFEANLESS', @mEANCode='{sEANCode}'";
                    }
                    var cmd = _db.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = sqlQuery;
                    _db.Database.OpenConnection();

                    result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    result.Close();
                }
                
            }
            return RedirectToAction("OfferSMStockAndEANCode", "Offer", new { Id = Convert.ToInt32(OfferId) });
        }

        public async Task<IActionResult> OfferDtlStockMapping(int Id)
        {
            var offersm = await _db.OfferDtlStockMappings.Where(x => x.FKOffer == Id).ToListAsync();
            ViewBag.Offer = await _db.offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(offersm);
        }

        public async Task<IActionResult> AssignOfferSingle(string EANCode)
        {
            int nOfferId = Convert.ToInt32(Request.Form["OfferId"]);
            var offer = await _db.offers.Where(x => x.Id == Convert.ToInt32(TempData["OfferId"])).FirstOrDefaultAsync();
            //int nOfferId = offer.Id;
            //TempData["OfferId"] = Id;

            string sOfferType, sEanCode;
            if (offer.ApplicableForItem == true)
            {
                sOfferType = "I";
            }
            else
            {
                sOfferType = "B";
            }
            //var stock = await _db.stocks.Where(x => x.Id == Id).FirstOrDefaultAsync();
            //stock.FKOffer = nOfferId;
            //stock.OfferType = sOfferType;
            //await _db.SaveChangesAsync();
            //sEanCode = stock.EANCode;

            //var stockwithARticle = await _db.stockWithArticles.Where(x => x.EANCode == sEanCode).FirstOrDefaultAsync();
            //stockwithARticle.FKOffer = nOfferId;
            //stockwithARticle.OfferType = sOfferType;
            //await _db.SaveChangesAsync();
            return RedirectToAction("OfferMapping", "Offer", new { Id = nOfferId });

        }

        //public async Task<IActionResult> AssignOffer(int Id)
        public async Task<IActionResult> AssignOffer(int Id)
        {
            //int nOfferId = Convert.ToInt32(Request.Form["OfferId1"]);
            var offer = await _db.offers.Where(x => x.Id == Convert.ToInt32(TempData["OfferId"])).FirstOrDefaultAsync();
            int nOfferId = offer.Id;
            TempData["OfferId"] = Id;
            string sOfferType, sEanCode;
            if (offer.ApplicableForItem == true)
            {
                sOfferType = "I";
            }
            else
            {
                sOfferType = "B";
            }
            var stock = await _db.stocks.Where(x => x.Id == Id).FirstOrDefaultAsync();
            stock.FKOffer = nOfferId;
            stock.OfferType = sOfferType;
            await _db.SaveChangesAsync();
            sEanCode = stock.EANCode;

            var stockwithARticle = await _db.stockWithArticles.Where(x => x.EANCode == sEanCode).FirstOrDefaultAsync();
            stockwithARticle.FKOffer = nOfferId;
            stockwithARticle.OfferType = sOfferType;
            await _db.SaveChangesAsync();
            return RedirectToAction("OfferMapping", "Offer", new { Id = nOfferId });

        }

        public async Task<IActionResult> UnAssignOffer(int Id)
        {
            int nOfferId = Convert.ToInt32(TempData["OfferId"]);
            string sEanCode;
            var stock = await _db.stocks.Where(x => x.Id == Id).FirstOrDefaultAsync();
            stock.FKOffer = 0;
            stock.OfferType = "";
            await _db.SaveChangesAsync();
            sEanCode = stock.EANCode;

            var stockwithARticle = await _db.stockWithArticles.Where(x => x.EANCode == sEanCode).FirstOrDefaultAsync();
            stockwithARticle.FKOffer = 0;
            stockwithARticle.OfferType = "";
            await _db.SaveChangesAsync();
            return RedirectToAction("OfferMapping", "Offer", new { Id = nOfferId });

        }
    }
}
