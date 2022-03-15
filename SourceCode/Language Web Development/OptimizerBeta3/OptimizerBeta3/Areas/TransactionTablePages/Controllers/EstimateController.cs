using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class EstimateController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public EstimateViewModel EstimateVM { get; set; }
        public EstimateDetailViewModel EstimateDetailVM { get; set; }

        public EstimateController(ApplicationDbContext db)
        {
            _db = db;
            EstimateVM = new EstimateViewModel()
            {
                FKStore = _db.unitMasters,
                FKLocation = _db.locations,
                Estimate = new Models.TransactionTables.Estimate()

            };

            EstimateDetailVM = new EstimateDetailViewModel()
            {
                Estimate = new Models.TransactionTables.Estimate(),
                FKArticle = _db.articleDetails,
                FKUom = _db.lookUpMasters,
                EstimateDetail = new Models.TransactionTables.EstimateDetail()
            };
        }

        #region "INVOICE"
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.Estimates.OrderByDescending(x => x.Id).Where(x => x.EstimateDt >= effectStartDate && x.EstimateDt <= effectEndDate).ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "Estimate", new { fromDate = fromDate, toDate = toDate });
        }

        public IActionResult GotoInvoice()
        {
            var effectStartDate = DateTime.Now.AddMonths(-1);
            var effectEndDate = DateTime.Now;

            return RedirectToAction("Index", "CounterInvoice", new { fromDate = effectStartDate, toDate = effectEndDate });
        }
        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            EstimateVM.FKStore = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();

            return View(EstimateVM);
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
            var unitmaster = await _db.unitMasters.ToListAsync();
            var location = await _db.locations.ToListAsync();

            string sStoreCode = unitmaster.Where(x => x.Id == EstimateVM.Estimate.FKStore).FirstOrDefault().ShortName;
            string sYearAndMonth = EstimateVM.Estimate.EstimateDt.Year.ToString().Substring(2, 2) + 
                EstimateVM.Estimate.EstimateDt.Month.ToString().PadLeft(2, '0');

            string codechar = (sStoreCode.Substring(0, 4) + sYearAndMonth.Substring(0, 4)).ToUpper();
            var maxcode = 0;

            if (_db.Estimates.Where(x => x.EstimateNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.Estimates.Where(x => x.EstimateNo.Contains(codechar)).Select(x => int.Parse(x.EstimateNo.Substring(10, 4))).ToList().Max();
            }

            EstimateVM.Estimate.EstimateNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            EstimateVM.Estimate.StoreName = unitmaster.Where(x => x.Id == EstimateVM.Estimate.FKStore).FirstOrDefault().CompanyName;
            EstimateVM.Estimate.FKFromState = unitmaster.Where(x => x.Id == EstimateVM.Estimate.FKStore).FirstOrDefault().FKState;
            EstimateVM.Estimate.LocationName = location.Where(x => x.Id == EstimateVM.Estimate.FKLocation).FirstOrDefault().LocationName;
            EstimateVM.Estimate.QuantityForOC = 0;
            _db.Estimates.Add(EstimateVM.Estimate);
            await _db.SaveChangesAsync();
            var estimate = await _db.Estimates.Where(x => x.EstimateNo == EstimateVM.Estimate.EstimateNo).FirstOrDefaultAsync();
            return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = estimate.Id });
        }

        //GET - CREATE
        public async Task<IActionResult> CreateEstimateDtl(int Id)
        {
            if (Id == 0)
            {
                Id = Convert.ToInt32(TempData["FKEstimateId"]);
            }
            var est = await _db.Estimates.FindAsync(Id);
            TempData["Estimate"] = est;
            int nQuantityforOF = est.QuantityForOC;

            int nRowCount = _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).ToList().Count;
            if (nRowCount == 0)
            {
                var offrDtl = await _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id).ToListAsync();
                _db.TempOffersBillWises.RemoveRange(offrDtl);
                await _db.SaveChangesAsync();
            }
            if (nRowCount > 0)
            {
                var estdtls = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).ToListAsync();

                int nTotalQuantity = 0;
                decimal dInvoiceValue = 0;
                decimal dShoesValue = 0;
                decimal dDiscountValue = 0;
                int nTotalShoePairs = 0;
                foreach (var item in estdtls)
                {
                    nTotalQuantity += Convert.ToInt32(item.Quantity);
                    dInvoiceValue += Convert.ToDecimal(item.ItemNettValue);
                    dDiscountValue += Convert.ToDecimal(item.DiscountValuePWise);
                    if (item.FLAM == "F")
                    {
                        dShoesValue += Convert.ToDecimal(item.ItemNettValue);
                        nTotalShoePairs += Convert.ToInt32(item.Quantity);
                    }
                }



                if (dDiscountValue == 0)
                {
                    if (dShoesValue > 0 && nQuantityforOF == 0)
                    {
                        if (nTotalShoePairs == 1)
                        {
                            List<TempOffersBillWise> tempOFR = new List<TempOffersBillWise>();

                            var Offr1 = await _db.offers.OrderBy(x => x.FKOffer).Where(x => x.BuyPair == 0 & x.ApplicableForInvoice == true && x.IsActive == true).ToListAsync();
                            foreach (var item in Offr1)
                            {
                                int nRowCount1 = _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.OfferId == item.Id).ToList().Count;
                                if (nRowCount1 == 0)
                                {
                                    var swa = new TempOffersBillWise
                                    {
                                        OfferId = item.Id,
                                        OfferCode = item.OfferName,
                                        OfferCategory = item.OfferCategory,
                                        OfferPair = item.OfferPair,
                                        BuyPair = item.BuyPair,
                                        IsProductCompliment = item.IsProductCompliment,
                                        DiscountPercentage = item.DiscountPercentage,
                                        DiscountValue = item.DiscountValue,
                                        ApplicableForItem = item.ApplicableForItem,
                                        ApplicableForInvoice = item.ApplicableForInvoice,
                                        MinimumBillValue = item.MinimumBillValue,
                                        MaximumDiscountValue = item.MaximumDiscountValue,
                                        IsVenueBased = item.IsVenueBased,
                                        EstimateId = est.Id,
                                        FKLocation = est.FKLocation,
                                        FKOffer = item.FKOffer,
                                        CouponCode = item.CouponCode,
                                        LineNo = 189
                                    };
                                    tempOFR.Add(swa);
                                }


                            }
                            _db.TempOffersBillWises.AddRange(tempOFR);
                            await _db.SaveChangesAsync();

                            nRowCount = _db.offers.Where(x => x.BuyPair == 0 & x.ApplicableForInvoice == true && x.MinimumBillValue >= dShoesValue && x.IsActive == true).ToList().Count;
                            if (nRowCount > 0)
                            {
                                var Offr2 = await _db.offers.OrderBy(x => x.FKOffer).Where(x => x.BuyPair == 0 & x.ApplicableForInvoice == true && x.MinimumBillValue >= dShoesValue && x.IsActive == true).FirstOrDefaultAsync();

                                int nRowCount1 = _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == Offr2.Id).ToList().Count;
                                if (nRowCount1 == 0)
                                {
                                    var swa = new TempOffersBillWise();
                                    swa.OfferId = Offr2.Id;
                                    swa.OfferCode = Offr2.OfferName;
                                    swa.OfferCategory = Offr2.OfferCategory;
                                    swa.OfferPair = Offr2.OfferPair;
                                    swa.BuyPair = Offr2.BuyPair;
                                    swa.IsProductCompliment = Offr2.IsProductCompliment;
                                    swa.DiscountPercentage = Offr2.DiscountPercentage;
                                    swa.DiscountValue = Offr2.DiscountValue;
                                    swa.ApplicableForItem = Offr2.ApplicableForItem;
                                    swa.ApplicableForInvoice = Offr2.ApplicableForInvoice;
                                    swa.MinimumBillValue = Offr2.MinimumBillValue;
                                    swa.MaximumDiscountValue = Offr2.MaximumDiscountValue;
                                    swa.IsVenueBased = Offr2.IsVenueBased;
                                    swa.EstimateId = est.Id;
                                    swa.FKLocation = est.FKLocation;
                                    swa.CouponCode = Offr2.CouponCode;
                                    swa.LineNo = 221;
                                    _db.TempOffersBillWises.Add(swa);
                                    await _db.SaveChangesAsync();
                                }
                            }

                        }

                        int nOfferTypeId = 0;
                        var Offr3 = await _db.offers.OrderBy(x => x.FKOffer).ThenBy(x => x.BuyPair).Where(x => x.BuyPair == nTotalShoePairs && x.IsProductCompliment == false && x.ApplicableForInvoice == true && x.IsActive == true).ToListAsync();
                        foreach (var item in Offr3)
                        {

                            //if (nOfferTypeId == 0)
                            //{
                            //    nOfferTypeId = item.FKOffer;
                            //}
                            if (nOfferTypeId != item.FKOffer)
                            {

                                if (nTotalQuantity == item.BuyPair)
                                {
                                    int nRowCount1 = _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == item.FKOffer && x.BuyPair != nTotalQuantity && x.IsProductCompliment == false).ToList().Count;
                                    if (nRowCount1 > 0)
                                    {
                                        var offrDtl = await _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == item.FKOffer && x.BuyPair != nTotalQuantity && x.IsProductCompliment == false).ToListAsync();
                                        _db.TempOffersBillWises.RemoveRange(offrDtl);
                                        await _db.SaveChangesAsync();
                                    }


                                    int nRowCount2 = _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == item.Id).ToList().Count;
                                    if (nRowCount2 == 0)
                                    {
                                        var swa = new TempOffersBillWise
                                        {
                                            OfferId = item.Id,
                                            OfferCode = item.OfferName,
                                            OfferCategory = item.OfferCategory,
                                            OfferPair = item.OfferPair,
                                            BuyPair = item.BuyPair,
                                            IsProductCompliment = item.IsProductCompliment,
                                            DiscountPercentage = item.DiscountPercentage,
                                            DiscountValue = item.DiscountValue,
                                            ApplicableForItem = item.ApplicableForItem,
                                            ApplicableForInvoice = item.ApplicableForInvoice,
                                            MinimumBillValue = item.MinimumBillValue,
                                            MaximumDiscountValue = item.MaximumDiscountValue,
                                            IsVenueBased = item.IsVenueBased,
                                            EstimateId = est.Id,
                                            FKLocation = est.FKLocation,
                                            FKOffer = item.FKOffer,
                                            CouponCode = item.CouponCode,
                                            LineNo = 274
                                        };
                                        _db.TempOffersBillWises.Add(swa);
                                        await _db.SaveChangesAsync();

                                        nOfferTypeId = item.FKOfferType;
                                    }

                                }
                            }

                        }

                        var Offr4 = await _db.offers.OrderBy(x => x.BuyPair).ThenBy(x => x.FKOffer).Where(x => x.IsProductCompliment == true && x.ApplicableForInvoice == true && x.IsActive == true).ToListAsync();
                        foreach (var item in Offr4)
                        {
                            if (nTotalShoePairs >= item.BuyPair)
                            {
                                int nRowCount1 = _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == item.FKOffer).ToList().Count;
                                if (nRowCount1 == 0)
                                {
                                    var swa = new TempOffersBillWise
                                    {
                                        OfferId = item.Id,
                                        OfferCode = item.OfferName,
                                        OfferCategory = item.OfferCategory,
                                        OfferPair = item.OfferPair,
                                        BuyPair = item.BuyPair,
                                        IsProductCompliment = item.IsProductCompliment,
                                        DiscountPercentage = item.DiscountPercentage,
                                        DiscountValue = item.DiscountValue,
                                        ApplicableForItem = item.ApplicableForItem,
                                        ApplicableForInvoice = item.ApplicableForInvoice,
                                        MinimumBillValue = item.MinimumBillValue,
                                        MaximumDiscountValue = item.MaximumDiscountValue,
                                        IsVenueBased = item.IsVenueBased,
                                        EstimateId = est.Id,
                                        FKLocation = est.FKLocation,
                                        FKOffer = item.FKOffer,
                                        CouponCode = item.CouponCode,
                                        LineNo = 314
                                    };
                                    _db.TempOffersBillWises.Add(swa);
                                    await _db.SaveChangesAsync();

                                    nOfferTypeId = item.FKOfferType;
                                }
                            }
                            else
                            {
                                int nRowCount1 = _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == item.FKOffer).ToList().Count;
                                if (nRowCount1 > 0)
                                {
                                    var offrDtl = await _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id && x.FKOffer == item.FKOffer && x.BuyPair < nTotalQuantity).ToListAsync();
                                    _db.TempOffersBillWises.RemoveRange(offrDtl);
                                    await _db.SaveChangesAsync();
                                }
                            }
                        }
                    }

                    else
                    {
                        if (nQuantityforOF > 0)
                        {
                            var offrDtl = await _db.TempOffersBillWises.Where(x => x.EstimateId == est.Id).ToListAsync();
                            _db.TempOffersBillWises.RemoveRange(offrDtl);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
            }
            //EstimateVM.FKOffer = await _db.offers.OrderBy(x => x.OfferName).Where(x => x.ApplicableForInvoice == true && x.IsActive == true).ToListAsync();

            var result = _db.EstimateDetails.Where(x => x.FKEstimateNo == Id).ToList();
            ViewBag.EstDtls = _db.EstimateDetails.Where(x => x.FKEstimateNo == Id).ToList();
            //ViewBag.Offer = await _db.offers.OrderBy(x => x.OfferName).Where(x => x.ApplicableForInvoice == true && x.IsActive == true).ToListAsync();
            ViewBag.Offer = await _db.TempOffersBillWises.OrderBy(x => x.OfferCode).Where(x => x.ApplicableForInvoice == true && x.EstimateId == Id).ToListAsync();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEstimateDtl()
        {

            int nFKEstimate = Convert.ToInt32(Request.Form["FKEstimateId"]);
            var est = await _db.Estimates.FindAsync(nFKEstimate);
            string sEANCode = Request.Form["EANCode"];
            int nNFKHSNCode;
            decimal nGSTPercentage;
            decimal nValue;
            decimal nSGSTPercentage, nCGSTPercentage, nIGSTPercentage;
            decimal nSGSTValue, nCGSTValue, nIGSTValue, nGSTValue;

            int nRowCount = _db.stocks.Where(x => (x.EANCode == sEANCode || x.StockNo == sEANCode) && x.Quantity > 0 && x.FKUnit == est.FKStore && x.FKLocation == est.FKLocation).ToList().Count;
            if (nRowCount == 0)
            {
                TempData["ErrorMessage"] = "Invalid EANCode/StockNo Scanned!";
                return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
            }
            else
            {
                var stock = await _db.stocks.OrderBy(x => x.Id).Where(x => (x.EANCode == sEANCode || x.StockNo == sEANCode) && x.Quantity > 0 && x.FKUnit == est.FKStore && x.FKLocation == est.FKLocation).FirstOrDefaultAsync();
                decimal nStockQuantity = stock.Quantity;

                int nRowCount1 = _db.EstimateDetails.Where(x => x.FKEstimateNo == nFKEstimate && x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count;
                if (nRowCount1 == 0)
                {
                    //var tmpofrdlt = _db.TempOfferforInvoices.Where(x => x.FKLocation == est.FKLocation && x.EstimateId != est.Id);
                    var tmpofrdlt = _db.TempOfferforInvoices.Where(x => x.FKLocation == est.FKLocation);
                    _db.TempOfferforInvoices.RemoveRange(tmpofrdlt);
                    await _db.SaveChangesAsync();

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

                    EstimateDetailVM.EstimateDetail.Id = 0;
                    EstimateDetailVM.EstimateDetail.FLAM = stock.FLAM;
                    EstimateDetailVM.EstimateDetail.FKEstimateNo = est.Id;
                    EstimateDetailVM.EstimateDetail.FKArticle = stock.FKArticleDetail;
                    EstimateDetailVM.EstimateDetail.Description = stock.Description;
                    EstimateDetailVM.EstimateDetail.Colour = stock.Colour;
                    EstimateDetailVM.EstimateDetail.Size = stock.Size;
                    EstimateDetailVM.EstimateDetail.HSNCode = 0;
                    EstimateDetailVM.EstimateDetail.Quantity = 1;
                    EstimateDetailVM.EstimateDetail.FKUOM = stock.FKUOM;
                    EstimateDetailVM.EstimateDetail.MRP = stock.MRP;
                    decimal nRate = stock.MRP - ((stock.MRP * nGSTPercentage) / (100 + nGSTPercentage));
                    EstimateDetailVM.EstimateDetail.Rate = nRate;
                    nValue = nRate;
                    EstimateDetailVM.EstimateDetail.Value = nValue;
                    EstimateDetailVM.EstimateDetail.ValueinINR = nValue;
                    EstimateDetailVM.EstimateDetail.DiscountPercentagePWise = 0;
                    EstimateDetailVM.EstimateDetail.DiscountValuePWise = 0;

                    #region OFFER AVAILABILITY
                    int nOfferCount = 0;
                    //var Tmpofr = await _db.TempOfferforInvoices.Where(x => x.EstimateId == nFKEstimate && )

                    nRowCount = _db.TempOfferforInvoices.Where(x => x.FKLocation == est.FKLocation && x.EstimateId != est.Id && x.FKArticleDtl == stock.FKArticleDetail).ToList().Count;
                    if (nRowCount == 0)
                    {
                        List<TempOfferforInvoice> tempOFR = new List<TempOfferforInvoice>();
                        DbDataReader result;

                        string sqlQuery = $"EXEC SLI_Offer @mAction='ARTOFFER', @mFKArticleDetail='{stock.FKArticleDetail}'";
                        var cmd = _db.Database.GetDbConnection().CreateCommand();
                        cmd.CommandText = sqlQuery;
                        _db.Database.OpenConnection();

                        result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                        while (result.Read())
                        {
                            nOfferCount += 1;
                            var swa = new TempOfferforInvoice
                            {
                                OfferId = result.GetInt32(0),
                                OfferCode = result.GetString(1),
                                OfferCategory = result.GetString(2),
                                OfferPair = result.GetInt32(3),
                                BuyPair = result.GetInt32(4),
                                IsProductCompliment = result.GetBoolean(5),
                                DiscountPercentage = result.GetDecimal(6),
                                DiscountValue = result.GetDecimal(7),
                                ApplicableForItem = result.GetBoolean(8),
                                ApplicableForInvoice = result.GetBoolean(9),
                                MinimumBillValue = result.GetDecimal(10),
                                MaximumDiscountValue = result.GetDecimal(11),
                                IsVenueBased = result.GetBoolean(12),
                                EstimateId = est.Id,
                                FKArticleDtl = stock.FKArticleDetail,
                                FKLocation = est.FKLocation
                            };
                            tempOFR.Add(swa);
                        }
                        result.Close();

                        _db.TempOfferforInvoices.AddRange(tempOFR);
                        await _db.SaveChangesAsync();
                    }


                    #endregion
                    decimal dDiscountPercentagePWise = 0;
                    decimal dDiscountValuePWise = 0;
                    Decimal nRevisedMRP = 0;
                    if (nOfferCount == 1)
                    {
                        var ofr = await _db.TempOfferforInvoices.Where(x => x.EstimateId == est.Id && x.FKArticleDtl == stock.FKArticleDetail).FirstOrDefaultAsync();

                        if (ofr.ApplicableForItem == true)
                        {
                            if (ofr.OfferCategory == "Percentage")
                            {
                                dDiscountPercentagePWise = ofr.DiscountPercentage;
                                dDiscountValuePWise = (stock.MRP * dDiscountPercentagePWise) / 100;

                            }
                            else if (ofr.OfferCategory == "Value")
                            {
                                dDiscountPercentagePWise = 0;
                                dDiscountValuePWise = ofr.DiscountValue;
                            }
                        }
                        nRevisedMRP = stock.MRP - dDiscountValuePWise;
                        decimal nRevisedRate = nRevisedMRP - ((nRevisedMRP * nGSTPercentage) / (100 + nGSTPercentage));
                        EstimateDetailVM.EstimateDetail.Rate = nRevisedRate;
                        nValue = nRevisedRate;
                        EstimateDetailVM.EstimateDetail.Value = nValue;
                        EstimateDetailVM.EstimateDetail.ValueinINR = nValue;
                        EstimateDetailVM.EstimateDetail.DiscountPercentagePWise = dDiscountPercentagePWise;
                        EstimateDetailVM.EstimateDetail.DiscountValuePWise = dDiscountValuePWise;
                        EstimateDetailVM.EstimateDetail.DiscountPercentageBWise = 0;
                        EstimateDetailVM.EstimateDetail.DiscountValueBWise = 0;
                        //nValue = nValue - dDiscountValue;
                    }
                    else
                    {
                        //TODO: if Single Product has Multiple Offer
                    }
                    nSGSTPercentage = nGSTPercentage / 2;
                    nCGSTPercentage = nGSTPercentage / 2;
                    nIGSTPercentage = 0;

                    nSGSTValue = nValue * nSGSTPercentage / 100;
                    nCGSTValue = nValue * nCGSTPercentage / 100;
                    nIGSTValue = nValue * nIGSTPercentage / 100;
                    nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                    EstimateDetailVM.EstimateDetail.GrossValue = nValue;
                    EstimateDetailVM.EstimateDetail.SGSTPercentage = nSGSTPercentage;
                    EstimateDetailVM.EstimateDetail.SGSTValue = nSGSTValue;
                    EstimateDetailVM.EstimateDetail.CGSTPercentage = nCGSTPercentage;
                    EstimateDetailVM.EstimateDetail.CGSTValue = nCGSTValue;
                    EstimateDetailVM.EstimateDetail.IGSTPercentage = nIGSTPercentage;
                    EstimateDetailVM.EstimateDetail.IGSTValue = nIGSTValue;
                    EstimateDetailVM.EstimateDetail.GSTTotalValue = nGSTValue;
                    EstimateDetailVM.EstimateDetail.OthersValuePlus = 0;
                    EstimateDetailVM.EstimateDetail.OthersValueMinus = 0;
                    EstimateDetailVM.EstimateDetail.ItemNettValue = nValue + nGSTValue;
                    EstimateDetailVM.EstimateDetail.IsActive = true;
                    EstimateDetailVM.EstimateDetail.CreatedBy = 1;
                    EstimateDetailVM.EstimateDetail.CreatedDate = DateTime.Now;
                    EstimateDetailVM.EstimateDetail.EANCode = sEANCode;
                    EstimateDetailVM.EstimateDetail.StockNo = stock.StockNo;
                    EstimateDetailVM.EstimateDetail.AvailableQty = Convert.ToInt32(nStockQuantity);

                    _db.EstimateDetails.Add(EstimateDetailVM.EstimateDetail);
                    _db.SaveChanges();

                }
                else
                {
                    decimal nQuantity = _db.EstimateDetails.Where(x => x.FKEstimateNo == nFKEstimate && x.EANCode == sEANCode || x.StockNo == sEANCode).Select(x => x.Quantity).ToList().Sum();
                    if (nQuantity + 1 > nStockQuantity)
                    {
                        TempData["ErrorMessage"] = "Selected EANCode/StockNo Scanned Quantity Exceeds the Stock Quantity!";
                        return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                    }
                    else
                    {
                        var TempFromdb = await _db.EstimateDetails.Where(x => x.FKEstimateNo == nFKEstimate && x.EANCode == sEANCode || x.StockNo == sEANCode).FirstOrDefaultAsync();
                        return RedirectToAction("AddItem", "Estimate", new { Id = TempFromdb.Id });
                        //TempFromdb.Quantity = TempFromdb.Quantity + 1;
                        //TempFromdb.Value = TempFromdb.Quantity * TempFromdb.Rate;

                        //if (stock.FKMaterial > 0)
                        //{
                        //    nNFKHSNCode = 0;
                        //    nGSTPercentage = 0;
                        //}
                        //else if (stock.FKArticleDetail > 0)
                        //{
                        //    nNFKHSNCode = _db.articleDetails.Where(x => x.Id == stock.FKArticleDetail).FirstOrDefault().FKHSNCode;
                        //    nGSTPercentage = _db.HSNCodeMasters.Where(x => x.Id == nNFKHSNCode).FirstOrDefault().GSTPercentage;
                        //}
                        //else
                        //{
                        //    nNFKHSNCode = 0;
                        //    nGSTPercentage = 0;
                        //}

                        //nSGSTPercentage = nGSTPercentage / 2;
                        //nCGSTPercentage = nGSTPercentage / 2;
                        //nIGSTPercentage = 0;

                        //nSGSTValue = TempFromdb.Value * nSGSTPercentage / 100;
                        //nCGSTValue = TempFromdb.Value * nCGSTPercentage / 100;
                        //nIGSTValue = TempFromdb.Value * nIGSTPercentage / 100;
                        //nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                        //TempFromdb.SGSTValue = nSGSTValue;
                        //TempFromdb.CGSTValue = nCGSTValue;
                        //TempFromdb.IGSTValue = nIGSTValue;
                        //TempFromdb.GSTTotalValue = nGSTValue;
                        //TempFromdb.ItemNettValue = TempFromdb.Value + nGSTValue;
                        //_db.SaveChanges();
                    }
                }
            }

            var estdtls = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).ToListAsync();
            int nestItemTotal = 0;
            int nTotalQuantity = 0;
            decimal destItemGrossValue = 0;
            decimal destItemNettValue = 0;
            decimal destDiscountValue = 0;
            decimal destOfferPercentage = 0;
            decimal destOfferValue = 0;
            decimal destNettValue = 0;
            decimal destInvoiceValue = 0;
            decimal destRoundOff = 0;

            foreach (var item in estdtls)
            {
                nestItemTotal += 1;
                nTotalQuantity += Convert.ToInt32(item.Quantity);
                destItemGrossValue += (item.Quantity) * (item.MRP);
                destDiscountValue += item.DiscountValuePWise;
                destItemNettValue += item.ItemNettValue;
            }
            destNettValue = destItemNettValue + destOfferValue;
            destRoundOff = Convert.ToDecimal(Convert.ToInt32(destNettValue) - destNettValue);
            destInvoiceValue = destNettValue + destRoundOff;

            var estfromdb = await _db.Estimates.FindAsync(est.Id);
            estfromdb.ItemsCount = nestItemTotal;
            estfromdb.Quantity = nTotalQuantity;
            estfromdb.ItemsGrossValue = destItemGrossValue;
            estfromdb.ItemsDiscountValuePWise = destDiscountValue;
            estfromdb.ItemsNettValue = destItemNettValue;
            estfromdb.OfferPercentagePWise = destOfferPercentage;
            estfromdb.OfferValuePWise = destOfferValue;
            estfromdb.RoundOff = destRoundOff;
            estfromdb.NettValue = destInvoiceValue;
            _db.Estimates.Update(estfromdb);
            await _db.SaveChangesAsync();

            return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
        }

        public async Task<IActionResult> AddItem(int Id)
        {
            var estdtl = await _db.EstimateDetails.FindAsync(Id);
            int nQuantity = Convert.ToInt32(estdtl.Quantity) + 1;
            decimal dGSTPercentage = estdtl.CGSTPercentage + estdtl.SGSTPercentage + estdtl.IGSTPercentage;
            decimal dMRP = estdtl.MRP;
            decimal dRate = estdtl.Rate;
            decimal dDiscountPercentage = estdtl.DiscountPercentagePWise;
            decimal dQuantityIntoRate = nQuantity * dRate;
            decimal dDiscountValue = (dQuantityIntoRate * dDiscountPercentage) / 100;
            decimal dRateWithGST = dQuantityIntoRate - dDiscountValue;
            //decimal nRevisedRate = dRateWithGST - ((dRateWithGST * dGSTPercentage) / (100 + dGSTPercentage));
            decimal nRevisedRate = dRateWithGST;

            decimal nSGSTPercentage = dGSTPercentage / 2;
            decimal nCGSTPercentage = dGSTPercentage / 2;
            decimal nIGSTPercentage = 0;

            decimal nSGSTValue = nRevisedRate * nSGSTPercentage / 100;
            decimal nCGSTValue = nRevisedRate * nCGSTPercentage / 100;
            decimal nIGSTValue = nRevisedRate * nIGSTPercentage / 100;
            decimal nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

            estdtl.Quantity = nQuantity;
            estdtl.Value = dQuantityIntoRate;
            estdtl.ValueinINR = dQuantityIntoRate;
            estdtl.DiscountValuePWise = dDiscountValue;

            estdtl.GrossValue = nRevisedRate;
            estdtl.SGSTPercentage = nSGSTPercentage;
            estdtl.SGSTValue = nSGSTValue;
            estdtl.CGSTPercentage = nCGSTPercentage;
            estdtl.CGSTValue = nCGSTValue;
            estdtl.IGSTPercentage = nIGSTPercentage;
            estdtl.IGSTValue = nIGSTValue;
            estdtl.GSTTotalValue = nGSTValue;
            estdtl.OthersValuePlus = 0;
            estdtl.OthersValueMinus = 0;
            estdtl.ItemNettValue = nRevisedRate + nGSTValue;

            int nFKEstimate = estdtl.FKEstimateNo;

            _db.EstimateDetails.Update(estdtl);
            await _db.SaveChangesAsync();
            return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
        }

        public async Task<IActionResult> RemoveItem(int Id)
        {
            var estdtl = await _db.EstimateDetails.FindAsync(Id);
            int nFKEstimate = estdtl.FKEstimateNo;
            int nQuantity = Convert.ToInt32(estdtl.Quantity);
            if (nQuantity > 1)
            {
                nQuantity = Convert.ToInt32(estdtl.Quantity) - 1;
                decimal dGSTPercentage = estdtl.CGSTPercentage + estdtl.SGSTPercentage + estdtl.IGSTPercentage;
                decimal dMRP = estdtl.MRP;
                decimal dDiscountPercentage = estdtl.DiscountPercentagePWise;
                decimal dQuantityIntoRate = nQuantity * dMRP;
                decimal dDiscountValue = (dQuantityIntoRate * dDiscountPercentage) / 100;
                decimal dRateWithGST = dQuantityIntoRate - dDiscountValue;
                decimal nRevisedRate = dRateWithGST - ((dRateWithGST * dGSTPercentage) / (100 + dGSTPercentage));

                decimal nSGSTPercentage = dGSTPercentage / 2;
                decimal nCGSTPercentage = dGSTPercentage / 2;
                decimal nIGSTPercentage = 0;

                decimal nSGSTValue = nRevisedRate * nSGSTPercentage / 100;
                decimal nCGSTValue = nRevisedRate * nCGSTPercentage / 100;
                decimal nIGSTValue = nRevisedRate * nIGSTPercentage / 100;
                decimal nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                estdtl.Quantity = nQuantity;
                estdtl.Value = dQuantityIntoRate;
                estdtl.ValueinINR = dQuantityIntoRate;
                estdtl.DiscountValuePWise = dDiscountValue;

                estdtl.GrossValue = nRevisedRate;
                estdtl.SGSTPercentage = nSGSTPercentage;
                estdtl.SGSTValue = nSGSTValue;
                estdtl.CGSTPercentage = nCGSTPercentage;
                estdtl.CGSTValue = nCGSTValue;
                estdtl.IGSTPercentage = nIGSTPercentage;
                estdtl.IGSTValue = nIGSTValue;
                estdtl.GSTTotalValue = nGSTValue;
                estdtl.OthersValuePlus = 0;
                estdtl.OthersValueMinus = 0;
                estdtl.ItemNettValue = nRevisedRate + nGSTValue;

                _db.EstimateDetails.Update(estdtl);
                await _db.SaveChangesAsync();
            }
            else
            {
                _db.EstimateDetails.Remove(estdtl);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEstimate(int Id, string EstimateUpdate)
        {
            int nFKEstimate = Convert.ToInt32(Request.Form["FKEstimateId"]);
            var est = await _db.Estimates.FindAsync(nFKEstimate);
            int nRowCount;
            if (EstimateUpdate == "Include Offer")
            {
                string sOfferCode = Request.Form["OfferCode"].ToString();

                nRowCount = _db.offers.Where(x => x.CouponCode == sOfferCode && x.IsActive == true).ToList().Count;
                if (nRowCount == 0)
                {
                    TempData["ErrorMessage"] = "Invalid Offer Code!";
                    return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                }
                else
                {
                    var offer = await _db.offers.Where(x => x.CouponCode == sOfferCode && x.IsActive == true).FirstOrDefaultAsync();

                    nRowCount = _db.TempOffersBillWises.Where(x => x.OfferId == offer.Id && x.EstimateId == est.Id).ToList().Count;
                    if (nRowCount == 0)
                    {
                        TempData["ErrorMessage"] = "This Offer Code does Not Applicable for this Estimate!";
                        return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                    }

                    int nBuyPair = offer.BuyPair;
                    int nOfferPair = offer.OfferPair;
                    decimal dMinimumBillValue = offer.MinimumBillValue;
                    decimal dOfferPercentage = offer.DiscountPercentage;
                    decimal dOfferValue = offer.DiscountValue;
                    bool bCompliment = offer.IsProductCompliment;
                    decimal dRevisedMRP = 0;

                    #region UPDATING BILL WISE OFFER IN ESIMATE DETAILS
                    if (dMinimumBillValue == 0)
                    {
                        var estdtl = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id && x.FLAM == "F").OrderBy(x => x.Id).ToListAsync();
                        if (nBuyPair == 0)
                        {
                            foreach (var item in estdtl)
                            {
                                int nestDtlId = item.Id;

                                if (dOfferPercentage > 0)
                                {
                                    dOfferValue = (item.MRP * dOfferPercentage) / 100;
                                    dRevisedMRP = item.MRP - dOfferValue;
                                }
                                else
                                {
                                    if (dOfferValue > 0 && item.MRP >= dOfferValue)
                                    {
                                        dRevisedMRP = item.MRP - dOfferValue;
                                        dOfferValue -= 0;
                                    }
                                    else if (dOfferValue > 0 && item.MRP < dOfferValue)
                                    {
                                        dRevisedMRP = 0;
                                        dOfferValue = dOfferValue - item.MRP;
                                    }
                                }


                                decimal dGSTPercentage = item.SGSTPercentage + item.CGSTPercentage + item.IGSTPercentage;
                                decimal dRevisedRate = dRevisedMRP - ((dRevisedMRP * dGSTPercentage) / (100 + dGSTPercentage));
                                decimal dRate = dRevisedRate;
                                decimal dValue = dRevisedRate * item.Quantity;

                                var estdtlfromdb4Update = await _db.EstimateDetails.FindAsync(nestDtlId);
                                estdtlfromdb4Update.Rate = dRevisedRate;
                                estdtlfromdb4Update.Value = dValue;
                                estdtlfromdb4Update.ValueinINR = dValue;
                                estdtlfromdb4Update.DiscountPercentagePWise = 0;
                                estdtlfromdb4Update.DiscountValuePWise = 0;
                                estdtlfromdb4Update.DiscountPercentageBWise = dOfferPercentage;
                                estdtlfromdb4Update.DiscountValueBWise = dOfferValue;
                                estdtlfromdb4Update.SGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                estdtlfromdb4Update.CGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                estdtlfromdb4Update.GSTTotalValue = estdtlfromdb4Update.SGSTValue + estdtlfromdb4Update.CGSTValue;

                                estdtlfromdb4Update.GrossValue = dValue;
                                estdtlfromdb4Update.ItemNettValue = dValue + estdtlfromdb4Update.GSTTotalValue;
                                _db.EstimateDetails.Update(estdtlfromdb4Update);
                                _db.SaveChanges();
                            }
                        }
                        else
                        {
                            decimal nTotalPairs = _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id && x.FLAM == "F").Select(x => x.Quantity).ToList().Sum();

                            if (nTotalPairs >= nBuyPair && nOfferPair == 0)
                            {
                                foreach (var item in estdtl)
                                {
                                    int nestDtlId = item.Id;

                                    if (dOfferPercentage > 0)
                                    {
                                        dOfferValue = (item.MRP * dOfferPercentage) / 100;
                                        dRevisedMRP = item.MRP - dOfferValue;
                                    }
                                    else
                                    {
                                        if (dOfferValue > 0 && item.MRP >= dOfferValue)
                                        {
                                            dRevisedMRP = item.MRP - (dOfferValue / nTotalPairs);
                                            dOfferValue -= 0;
                                        }
                                        else if (dOfferValue > 0 && item.MRP < dOfferValue)
                                        {
                                            dRevisedMRP = 0;
                                            dOfferValue = dOfferValue - item.MRP;
                                        }
                                    }

                                    decimal dGSTPercentage = item.SGSTPercentage + item.CGSTPercentage + item.IGSTPercentage;
                                    decimal dRevisedRate = dRevisedMRP - ((dRevisedMRP * dGSTPercentage) / (100 + dGSTPercentage));
                                    decimal dRate = dRevisedRate;
                                    decimal dValue = dRevisedRate * item.Quantity;

                                    var estdtlfromdb4Update = await _db.EstimateDetails.FindAsync(nestDtlId);
                                    estdtlfromdb4Update.Rate = dRevisedRate;
                                    estdtlfromdb4Update.Value = dValue;
                                    estdtlfromdb4Update.ValueinINR = dValue;
                                    estdtlfromdb4Update.DiscountPercentagePWise = 0;
                                    estdtlfromdb4Update.DiscountValuePWise = 0;
                                    estdtlfromdb4Update.DiscountPercentageBWise = dOfferPercentage;
                                    estdtlfromdb4Update.DiscountValueBWise = dOfferValue;
                                    estdtlfromdb4Update.SGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                    estdtlfromdb4Update.CGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                    estdtlfromdb4Update.GSTTotalValue = estdtlfromdb4Update.SGSTValue + estdtlfromdb4Update.CGSTValue;

                                    estdtlfromdb4Update.GrossValue = dValue;
                                    estdtlfromdb4Update.ItemNettValue = dValue + estdtlfromdb4Update.GSTTotalValue;
                                    _db.EstimateDetails.Update(estdtlfromdb4Update);
                                    _db.SaveChanges();
                                }
                            }
                            else if (nTotalPairs >= nBuyPair && nOfferPair > 0)
                            {
                                if (nTotalPairs < (nBuyPair + nOfferPair))
                                {
                                    TempData["ErrorMessage"] = "This Offer Code does Not Applicable for this Estimate! As Invoice Quantity is less than Offer Pair + Buy Pair";
                                    return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                                }
                                else
                                {
                                    var estdtl1 = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id && x.FLAM == "F").OrderBy(x => x.MRP).ThenByDescending(x => x.Id).ToListAsync();
                                    foreach (var item in estdtl1)
                                    {
                                        if (nOfferPair > 0)
                                        {
                                            int nestDtlId = item.Id;
                                            int nLessPair = 0;

                                            if (item.Quantity >= nOfferPair)
                                            {
                                                dOfferValue = item.MRP * nOfferPair;
                                                nLessPair = nOfferPair;
                                                nOfferPair = 0;
                                            }
                                            else if (item.Quantity < nOfferPair)
                                            {
                                                dOfferValue += item.MRP * item.Quantity;
                                                nLessPair = Convert.ToInt32(item.Quantity);
                                                nOfferPair -= Convert.ToInt32(item.Quantity);
                                            }


                                            decimal dGSTPercentage = item.SGSTPercentage + item.CGSTPercentage + item.IGSTPercentage;
                                            decimal dRevisedRate = item.MRP - ((item.MRP * dGSTPercentage) / (100 + dGSTPercentage));
                                            decimal dRate = dRevisedRate;
                                            decimal dValue = dRevisedRate * (item.Quantity - nLessPair);

                                            var estdtlfromdb4Update = await _db.EstimateDetails.FindAsync(nestDtlId);
                                            estdtlfromdb4Update.Rate = dRevisedRate;
                                            estdtlfromdb4Update.Value = dValue;
                                            estdtlfromdb4Update.ValueinINR = dValue;
                                            estdtlfromdb4Update.DiscountPercentagePWise = 0;
                                            estdtlfromdb4Update.DiscountValuePWise = 0;
                                            estdtlfromdb4Update.DiscountPercentageBWise = dOfferPercentage;
                                            estdtlfromdb4Update.DiscountValueBWise = dOfferValue;
                                            estdtlfromdb4Update.SGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                            estdtlfromdb4Update.CGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                            estdtlfromdb4Update.GSTTotalValue = estdtlfromdb4Update.SGSTValue + estdtlfromdb4Update.CGSTValue;

                                            estdtlfromdb4Update.GrossValue = dValue;
                                            estdtlfromdb4Update.ItemNettValue = dValue + estdtlfromdb4Update.GSTTotalValue;
                                            _db.EstimateDetails.Update(estdtlfromdb4Update);
                                            _db.SaveChanges();
                                        }
                                    }
                                }


                            }
                        }
                    }
                    else if (dMinimumBillValue > 0)
                    {
                        if (bCompliment == true)
                        {
                            decimal nBillValue = _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).Select(x => x.ItemNettValue).ToList().Sum();
                            if (nBillValue <= dMinimumBillValue)
                            {
                                TempData["ErrorMessage"] = "This Offer Code does Not Applicable for this Estimate! As Invoice Value is less than Minimum Bill Value";
                                return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                            }
                            else
                            {
                                nRowCount = _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id && x.FLAM == "L").ToList().Count();
                                if (nRowCount == 0)
                                {
                                    TempData["ErrorMessage"] = "This Offer Code does Not Applicable for this Estimate! As Compliment Product is Not Added";
                                    return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                                }
                                else
                                {
                                    var estdtlsLG = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id && x.FLAM == "L").ToListAsync();
                                    decimal dLGSum = 0;
                                    decimal dLGBillValue = 0;
                                    foreach (var item in estdtlsLG)
                                    {
                                        dLGSum += item.Quantity;
                                        dLGBillValue += item.ItemNettValue;
                                    }
                                    int nComplimentCount = Convert.ToInt32(nBillValue - dLGBillValue) / Convert.ToInt32(dMinimumBillValue);
                                    if ((nBillValue - dLGBillValue) < dMinimumBillValue)
                                    {
                                        TempData["ErrorMessage"] = "This Offer Code does Not Applicable for this Estimate! As Invoice Value excluding compliment is less than Minimum Bill Value";
                                        return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                                    }
                                    if (dLGSum < nComplimentCount)
                                    {
                                        TempData["ErrorMessage"] = "This Offer Code does Not Applicable for this Estimate! As Compliment Product Added is less";
                                        return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                                    }
                                    var estdtl1 = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id && x.FLAM == "L").OrderBy(x => x.MRP).ThenByDescending(x => x.Id).ToListAsync();
                                    foreach (var item in estdtl1)
                                    {
                                        if (nComplimentCount > 0)
                                        {
                                            int nestDtlId = item.Id;
                                            int nLessPair = 0;

                                            if (item.Quantity >= nComplimentCount)
                                            {
                                                dOfferValue = item.MRP * nComplimentCount;
                                                nLessPair = nComplimentCount;
                                                nComplimentCount = 0;
                                            }
                                            else if (item.Quantity < nComplimentCount)
                                            {
                                                dOfferValue += item.MRP * item.Quantity;
                                                nLessPair = Convert.ToInt32(item.Quantity);
                                                nComplimentCount -= Convert.ToInt32(item.Quantity);
                                            }


                                            decimal dGSTPercentage = item.SGSTPercentage + item.CGSTPercentage + item.IGSTPercentage;
                                            decimal dRevisedRate = item.MRP - ((item.MRP * dGSTPercentage) / (100 + dGSTPercentage));
                                            decimal dRate = dRevisedRate;
                                            decimal dValue = dRevisedRate * (item.Quantity - nLessPair);

                                            var estdtlfromdb4Update = await _db.EstimateDetails.FindAsync(nestDtlId);
                                            estdtlfromdb4Update.Rate = dRevisedRate;
                                            estdtlfromdb4Update.Value = dValue;
                                            estdtlfromdb4Update.ValueinINR = dValue;
                                            estdtlfromdb4Update.DiscountPercentagePWise = 0;
                                            estdtlfromdb4Update.DiscountValuePWise = 0;
                                            estdtlfromdb4Update.DiscountPercentageBWise = dOfferPercentage;
                                            estdtlfromdb4Update.DiscountValueBWise = dOfferValue;
                                            estdtlfromdb4Update.SGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                            estdtlfromdb4Update.CGSTValue = dValue * (dGSTPercentage / 2) / 100;
                                            estdtlfromdb4Update.GSTTotalValue = estdtlfromdb4Update.SGSTValue + estdtlfromdb4Update.CGSTValue;

                                            estdtlfromdb4Update.GrossValue = dValue;
                                            estdtlfromdb4Update.ItemNettValue = dValue + estdtlfromdb4Update.GSTTotalValue;
                                            _db.EstimateDetails.Update(estdtlfromdb4Update);
                                            _db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }

                    #endregion
                    //var estfromdb4Ofr = await _db.Estimates.FindAsync(est.Id);
                    //decimal dItemsNettValue = estfromdb4Ofr.ItemsNettValue;

                    //decimal dNettValueAfterDiscount;
                    //decimal dRoundOff;
                    //decimal dNettValueFinal;
                    //if (offer.BuyPair == 0)
                    //{
                    //    if (dOfferPercentage > 0)
                    //    {
                    //        dOfferValue = (dItemsNettValue * dOfferPercentage) / 100;
                    //    }
                    //}
                    //else
                    //{

                    //}

                    //dNettValueAfterDiscount = dItemsNettValue - dOfferValue;
                    //dRoundOff = Convert.ToInt32(dNettValueAfterDiscount) - dNettValueAfterDiscount;
                    //dNettValueFinal = dNettValueAfterDiscount + dRoundOff;
                    //estfromdb4Ofr.OfferPercentagePWise = 0;
                    //estfromdb4Ofr.OfferValuePWise = 0;
                    //estfromdb4Ofr.OfferPercentageBWise = dOfferPercentage;
                    //estfromdb4Ofr.OfferValueBWise = dOfferValue;
                    //estfromdb4Ofr.RoundOff = dRoundOff;
                    //estfromdb4Ofr.NettValue = dNettValueFinal;
                    //_db.Estimates.Update(estfromdb4Ofr);
                    //await _db.SaveChangesAsync();

                    var estdtls1 = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).ToListAsync();
                    int nestItemTotal1 = 0;
                    int nTotalQuantity1 = 0;
                    decimal destItemGrossValue1 = 0;
                    decimal destItemNettValue1 = 0;
                    decimal destDiscountValuePWise1 = 0;
                    decimal destDiscountValueBWise1 = 0;
                    decimal destOfferPercentage1 = 0;
                    decimal destOfferValue1 = 0;
                    decimal destNettValue1 = 0;
                    decimal destInvoiceValue1 = 0;
                    decimal destRoundOff1 = 0;

                    foreach (var item in estdtls1)
                    {
                        nestItemTotal1 += 1;
                        nTotalQuantity1 += Convert.ToInt32(item.Quantity);
                        destItemGrossValue1 += (item.Quantity) * (item.MRP);
                        destDiscountValuePWise1 += item.DiscountValuePWise;
                        destDiscountValueBWise1 += item.DiscountValueBWise;
                        destItemNettValue1 += item.ItemNettValue;
                    }
                    destNettValue1 = destItemNettValue1 + destOfferValue1;
                    destRoundOff1 = Convert.ToDecimal(Convert.ToInt32(destNettValue1) - destNettValue1);
                    destInvoiceValue1 = destNettValue1 + destRoundOff1;

                    var estfromdb1 = await _db.Estimates.FindAsync(est.Id);
                    estfromdb1.ItemsCount = nestItemTotal1;
                    estfromdb1.Quantity = nTotalQuantity1;
                    estfromdb1.ItemsGrossValue = destItemGrossValue1;
                    estfromdb1.ItemsDiscountValuePWise = destDiscountValuePWise1;
                    estfromdb1.ItemsDiscountValueBWise = destDiscountValueBWise1;
                    estfromdb1.ItemsNettValue = destItemNettValue1;
                    estfromdb1.OfferPercentagePWise = destOfferPercentage1;
                    estfromdb1.OfferValuePWise = destOfferValue1;
                    estfromdb1.RoundOff = destRoundOff1;
                    estfromdb1.NettValue = destInvoiceValue1;
                    estfromdb1.FKOffer = offer.Id;
                    _db.Estimates.Update(estfromdb1);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });

                }
            }
            else if (EstimateUpdate == "Exclude Offer")
            {
                var estdtl = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).OrderBy(x => x.Id).ToListAsync();
                decimal dItemsNettValue = 0;
                foreach (var item in estdtl)
                {
                    int nestDtlId = item.Id;

                    decimal dGSTPercentage = item.SGSTPercentage + item.CGSTPercentage + item.IGSTPercentage;
                    decimal dRevisedRate = item.MRP - ((item.MRP * dGSTPercentage) / (100 + dGSTPercentage));
                    decimal dRate = dRevisedRate;
                    decimal dValue = dRevisedRate * item.Quantity;

                    var estdtlfromdb4Update = await _db.EstimateDetails.FindAsync(nestDtlId);
                    estdtlfromdb4Update.Rate = dRevisedRate;
                    estdtlfromdb4Update.Value = dValue;
                    estdtlfromdb4Update.ValueinINR = dValue;
                    estdtlfromdb4Update.DiscountPercentagePWise = 0;
                    estdtlfromdb4Update.DiscountValuePWise = 0;
                    estdtlfromdb4Update.DiscountPercentageBWise = 0;
                    estdtlfromdb4Update.DiscountValueBWise = 0;
                    estdtlfromdb4Update.SGSTValue = dValue * (dGSTPercentage / 2) / 100;
                    estdtlfromdb4Update.CGSTValue = dValue * (dGSTPercentage / 2) / 100;
                    estdtlfromdb4Update.GSTTotalValue = estdtlfromdb4Update.SGSTValue + estdtlfromdb4Update.CGSTValue;

                    estdtlfromdb4Update.GrossValue = dValue;
                    estdtlfromdb4Update.ItemNettValue = dValue + estdtlfromdb4Update.GSTTotalValue;
                    _db.EstimateDetails.Update(estdtlfromdb4Update);
                    _db.SaveChanges();
                    dItemsNettValue += dValue + estdtlfromdb4Update.GSTTotalValue;
                }


                var estfromdb4Ofr = await _db.Estimates.FindAsync(est.Id);
                //dItemsNettValue = dItemsNettValue;
                decimal dNettValueAfterDiscount;
                decimal dRoundOff;
                decimal dNettValueFinal;
                dNettValueAfterDiscount = dItemsNettValue;
                dRoundOff = Convert.ToInt32(dNettValueAfterDiscount) - dNettValueAfterDiscount;
                dNettValueFinal = dNettValueAfterDiscount + dRoundOff;
                estfromdb4Ofr.OfferPercentagePWise = 0;
                estfromdb4Ofr.OfferValuePWise = 0;
                estfromdb4Ofr.RoundOff = dRoundOff;
                estfromdb4Ofr.NettValue = dNettValueFinal;
                estfromdb4Ofr.FKOffer = 0;
                _db.Estimates.Update(estfromdb4Ofr);
                await _db.SaveChangesAsync();

                return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
            }
            else if (EstimateUpdate == "Convert 2 Invoice")
            {
                TempData["FKEstimateId"] = nFKEstimate;
                return RedirectToAction("GenerateInvoicefromEstimate", "CounterInvoice", new { Id = nFKEstimate });
            }
            else
            {
                return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
            }


            string sEANCode = Request.Form["EANCode"];
            int nNFKHSNCode;
            decimal estdtlfromdb;
            decimal nValue;
            decimal nSGSTPercentage, nCGSTPercentage, nIGSTPercentage;
            decimal nSGSTValue, nCGSTValue, nIGSTValue, nGSTValue;

            nRowCount = _db.stocks.Where(x => (x.EANCode == sEANCode || x.StockNo == sEANCode) && x.Quantity > 0 && x.FKUnit == est.FKStore && x.FKLocation == est.FKLocation).ToList().Count;
            if (nRowCount == 0)
            {
                TempData["ErrorMessage"] = "Invalid EANCode/StockNo Scanned!";
                return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
            }
            else
            {
                var stock = await _db.stocks.OrderBy(x => x.Id).Where(x => (x.EANCode == sEANCode || x.StockNo == sEANCode) && x.Quantity > 0 && x.FKUnit == est.FKStore && x.FKLocation == est.FKLocation).FirstOrDefaultAsync();
                decimal nStockQuantity = stock.Quantity;

                int nRowCount1 = _db.EstimateDetails.Where(x => x.FKEstimateNo == nFKEstimate && x.EANCode == sEANCode || x.StockNo == sEANCode).ToList().Count;
                if (nRowCount1 == 0)
                {
                    var tmpofrdlt = _db.TempOfferforInvoices.Where(x => x.FKLocation == est.FKLocation && x.EstimateId != est.Id);
                    _db.TempOfferforInvoices.RemoveRange(tmpofrdlt);
                    await _db.SaveChangesAsync();
                    decimal nGSTPercentage = 0;
                    if (stock.FKMaterial > 0)
                    {
                        nNFKHSNCode = 0;
                        estdtlfromdb = 0;
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

                    EstimateDetailVM.EstimateDetail.Id = 0;
                    EstimateDetailVM.EstimateDetail.FKEstimateNo = est.Id;
                    EstimateDetailVM.EstimateDetail.FKArticle = stock.FKArticleDetail;
                    EstimateDetailVM.EstimateDetail.Description = stock.Description;
                    EstimateDetailVM.EstimateDetail.Colour = stock.Colour;
                    EstimateDetailVM.EstimateDetail.Size = stock.Size;
                    EstimateDetailVM.EstimateDetail.HSNCode = 0;
                    EstimateDetailVM.EstimateDetail.Quantity = 1;
                    EstimateDetailVM.EstimateDetail.FKUOM = stock.FKUOM;
                    EstimateDetailVM.EstimateDetail.MRP = stock.MRP;
                    decimal nRate = stock.MRP - ((stock.MRP * nGSTPercentage) / (100 + nGSTPercentage));
                    EstimateDetailVM.EstimateDetail.Rate = nRate;
                    nValue = nRate;
                    EstimateDetailVM.EstimateDetail.Value = nValue;
                    EstimateDetailVM.EstimateDetail.ValueinINR = nValue;
                    EstimateDetailVM.EstimateDetail.DiscountPercentagePWise = 0;
                    EstimateDetailVM.EstimateDetail.DiscountValuePWise = 0;

                    #region OFFER AVAILABILITY
                    int nOfferCount = 0;
                    //var Tmpofr = await _db.TempOfferforInvoices.Where(x => x.EstimateId == nFKEstimate && )

                    nRowCount = _db.TempOfferforInvoices.Where(x => x.FKLocation == est.FKLocation && x.EstimateId != est.Id && x.FKArticleDtl == stock.FKArticleDetail).ToList().Count;
                    if (nRowCount == 0)
                    {
                        List<TempOfferforInvoice> tempOFR = new List<TempOfferforInvoice>();
                        DbDataReader result;

                        string sqlQuery = $"EXEC SLI_Offer @mAction='ARTOFFER', @mFKArticleDetail='{stock.FKArticleDetail}'";
                        var cmd = _db.Database.GetDbConnection().CreateCommand();
                        cmd.CommandText = sqlQuery;
                        _db.Database.OpenConnection();

                        result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                        while (result.Read())
                        {
                            nOfferCount += 1;
                            var swa = new TempOfferforInvoice
                            {
                                OfferId = result.GetInt32(0),
                                OfferCode = result.GetString(1),
                                OfferCategory = result.GetString(2),
                                OfferPair = result.GetInt32(3),
                                BuyPair = result.GetInt32(4),
                                IsProductCompliment = result.GetBoolean(5),
                                DiscountPercentage = result.GetDecimal(6),
                                DiscountValue = result.GetDecimal(7),
                                ApplicableForItem = result.GetBoolean(8),
                                ApplicableForInvoice = result.GetBoolean(9),
                                MinimumBillValue = result.GetDecimal(10),
                                MaximumDiscountValue = result.GetDecimal(11),
                                IsVenueBased = result.GetBoolean(12),
                                EstimateId = est.Id,
                                FKArticleDtl = stock.FKArticleDetail,
                                FKLocation = est.FKLocation
                            };
                            tempOFR.Add(swa);
                        }
                        result.Close();

                        _db.TempOfferforInvoices.AddRange(tempOFR);
                        await _db.SaveChangesAsync();
                    }


                    #endregion
                    decimal dDiscountPercentage = 0;
                    decimal dDiscountValue = 0;
                    Decimal nRevisedMRP = 0;
                    if (nOfferCount == 1)
                    {
                        var ofr = await _db.TempOfferforInvoices.Where(x => x.EstimateId == est.Id && x.FKArticleDtl == stock.FKArticleDetail).FirstOrDefaultAsync();

                        if (ofr.ApplicableForItem == true)
                        {
                            if (ofr.OfferCategory == "Percentage")
                            {
                                dDiscountPercentage = ofr.DiscountPercentage;
                                dDiscountValue = (stock.MRP * dDiscountPercentage) / 100;

                            }
                            else if (ofr.OfferCategory == "Value")
                            {
                                dDiscountPercentage = 0;
                                dDiscountValue = ofr.DiscountValue;
                            }
                        }
                        nRevisedMRP = stock.MRP - dDiscountValue;
                        decimal nRevisedRate = nRevisedMRP - ((nRevisedMRP * nGSTPercentage) / (100 + nGSTPercentage));
                        EstimateDetailVM.EstimateDetail.Rate = nRevisedRate;
                        nValue = nRevisedRate;
                        EstimateDetailVM.EstimateDetail.Value = nValue;
                        EstimateDetailVM.EstimateDetail.ValueinINR = nValue;
                        EstimateDetailVM.EstimateDetail.DiscountPercentagePWise = dDiscountPercentage;
                        EstimateDetailVM.EstimateDetail.DiscountValuePWise = dDiscountValue;
                        //nValue = nValue - dDiscountValue;
                    }
                    else
                    {
                        //TODO: if Single Product has Multiple Offer
                    }
                    nSGSTPercentage = nGSTPercentage / 2;
                    nCGSTPercentage = nGSTPercentage / 2;
                    nIGSTPercentage = 0;

                    nSGSTValue = nValue * nSGSTPercentage / 100;
                    nCGSTValue = nValue * nCGSTPercentage / 100;
                    nIGSTValue = nValue * nIGSTPercentage / 100;
                    nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                    EstimateDetailVM.EstimateDetail.GrossValue = nValue;
                    EstimateDetailVM.EstimateDetail.SGSTPercentage = nSGSTPercentage;
                    EstimateDetailVM.EstimateDetail.SGSTValue = nSGSTValue;
                    EstimateDetailVM.EstimateDetail.CGSTPercentage = nCGSTPercentage;
                    EstimateDetailVM.EstimateDetail.CGSTValue = nCGSTValue;
                    EstimateDetailVM.EstimateDetail.IGSTPercentage = nIGSTPercentage;
                    EstimateDetailVM.EstimateDetail.IGSTValue = nIGSTValue;
                    EstimateDetailVM.EstimateDetail.GSTTotalValue = nGSTValue;
                    EstimateDetailVM.EstimateDetail.OthersValuePlus = 0;
                    EstimateDetailVM.EstimateDetail.OthersValueMinus = 0;
                    EstimateDetailVM.EstimateDetail.ItemNettValue = nValue + nGSTValue;
                    EstimateDetailVM.EstimateDetail.IsActive = true;
                    EstimateDetailVM.EstimateDetail.CreatedBy = 1;
                    EstimateDetailVM.EstimateDetail.CreatedDate = DateTime.Now;
                    EstimateDetailVM.EstimateDetail.EANCode = sEANCode;
                    EstimateDetailVM.EstimateDetail.StockNo = stock.StockNo;
                    EstimateDetailVM.EstimateDetail.AvailableQty = Convert.ToInt32(nStockQuantity);

                    _db.EstimateDetails.Add(EstimateDetailVM.EstimateDetail);
                    _db.SaveChanges();

                }
                else
                {
                    decimal nQuantity = _db.EstimateDetails.Where(x => x.FKEstimateNo == nFKEstimate && x.EANCode == sEANCode || x.StockNo == sEANCode).Select(x => x.Quantity).ToList().Sum();
                    if (nQuantity + 1 > nStockQuantity)
                    {
                        TempData["ErrorMessage"] = "Selected EANCode/StockNo Scanned Quantity Exceeds the Stock Quantity!";
                        return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
                    }
                    else
                    {
                        var TempFromdb = await _db.EstimateDetails.Where(x => x.FKEstimateNo == nFKEstimate && x.EANCode == sEANCode || x.StockNo == sEANCode).FirstOrDefaultAsync();
                        return RedirectToAction("AddItem", "Estimate", new { Id = TempFromdb.Id });
                        //TempFromdb.Quantity = TempFromdb.Quantity + 1;
                        //TempFromdb.Value = TempFromdb.Quantity * TempFromdb.Rate;

                        //if (stock.FKMaterial > 0)
                        //{
                        //    nNFKHSNCode = 0;
                        //    nGSTPercentage = 0;
                        //}
                        //else if (stock.FKArticleDetail > 0)
                        //{
                        //    nNFKHSNCode = _db.articleDetails.Where(x => x.Id == stock.FKArticleDetail).FirstOrDefault().FKHSNCode;
                        //    nGSTPercentage = _db.HSNCodeMasters.Where(x => x.Id == nNFKHSNCode).FirstOrDefault().GSTPercentage;
                        //}
                        //else
                        //{
                        //    nNFKHSNCode = 0;
                        //    nGSTPercentage = 0;
                        //}

                        //nSGSTPercentage = nGSTPercentage / 2;
                        //nCGSTPercentage = nGSTPercentage / 2;
                        //nIGSTPercentage = 0;

                        //nSGSTValue = TempFromdb.Value * nSGSTPercentage / 100;
                        //nCGSTValue = TempFromdb.Value * nCGSTPercentage / 100;
                        //nIGSTValue = TempFromdb.Value * nIGSTPercentage / 100;
                        //nGSTValue = nSGSTValue + nCGSTValue + nIGSTValue;

                        //TempFromdb.SGSTValue = nSGSTValue;
                        //TempFromdb.CGSTValue = nCGSTValue;
                        //TempFromdb.IGSTValue = nIGSTValue;
                        //TempFromdb.GSTTotalValue = nGSTValue;
                        //TempFromdb.ItemNettValue = TempFromdb.Value + nGSTValue;
                        //_db.SaveChanges();
                    }
                }
            }

            var estdtls = await _db.EstimateDetails.Where(x => x.FKEstimateNo == est.Id).ToListAsync();
            int nestItemTotal = 0;
            int nTotalQuantity = 0;
            decimal destItemGrossValue = 0;
            decimal destItemNettValue = 0;
            decimal destDiscountValue = 0;
            decimal destOfferPercentage = 0;
            decimal destOfferValue = 0;
            decimal destNettValue = 0;
            decimal destInvoiceValue = 0;
            decimal destRoundOff = 0;

            foreach (var item in estdtls)
            {
                nestItemTotal += 1;
                nTotalQuantity += Convert.ToInt32(item.Quantity);
                destItemGrossValue += (item.Quantity) * (item.MRP);
                destDiscountValue += item.DiscountValuePWise;
                destItemNettValue += item.ItemNettValue;
            }
            destNettValue = destItemNettValue + destOfferValue;
            destRoundOff = Convert.ToDecimal(Convert.ToInt32(destNettValue) - destNettValue);
            destInvoiceValue = destNettValue + destRoundOff;

            var estfromdb = await _db.Estimates.FindAsync(est.Id);
            estfromdb.ItemsCount = nestItemTotal;
            estfromdb.Quantity = nTotalQuantity;
            estfromdb.ItemsGrossValue = destItemGrossValue;
            estfromdb.ItemsDiscountValuePWise = destDiscountValue;
            estfromdb.ItemsNettValue = destItemNettValue;
            estfromdb.OfferPercentagePWise = destOfferPercentage;
            estfromdb.OfferValuePWise = destOfferValue;
            estfromdb.RoundOff = destRoundOff;
            estfromdb.NettValue = destInvoiceValue;
            _db.Estimates.Update(estfromdb);
            await _db.SaveChangesAsync();

            return RedirectToAction("CreateEstimateDtl", "Estimate", new { Id = nFKEstimate });
        }
        #endregion
    }
}
