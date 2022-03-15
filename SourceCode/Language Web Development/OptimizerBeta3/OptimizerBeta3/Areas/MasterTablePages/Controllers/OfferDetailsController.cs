using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class OfferDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OfferDetailsViewModel offerDetailsVM { get; set; }

        public static int NFKOffer, NLastRecord;
        public static DateTime DFromDate, DToDate;
        public static String SOfferName;
        public static Decimal DecRangeSlNo, DecValueFrom, DecValueTo;

        public OfferDetailsController(ApplicationDbContext db)
        {
            _db = db;
            offerDetailsVM = new OfferDetailsViewModel()
            {
                FKAnniverseryInfo = _db.lookUpMasters,
                offerDetails = new Models.MasterTables.OfferDetails()
            };
        }
        public async Task<IActionResult> Index()
        {

            return View(await _db.offers.Where(s => s.IsRangeOffer == true && s.IsActive == true).ToListAsync());
        }

        public async Task<IActionResult> OfferDtlsIndex(int Id)
        {
            var Offer = await _db.offers.FindAsync(Id);

            TempData["OfferName"] = Offer.OfferName;
            TempData["FromDate"] = Offer.FromDate;
            TempData["ToDate"] = Offer.ToDate;
            TempData["OfferId"] = Offer.Id;

            NFKOffer = Offer.Id;
            DFromDate = Offer.FromDate;
            DToDate = Offer.ToDate;
            SOfferName = Offer.OfferName;
            //DecRangeSlNo = (decimal)Offer.RangeSlNo;

            return View(await _db.offerDetails.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            offerDetailsVM.FKAnniverseryInfo = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();

            TempData["OfferName"] = SOfferName;
            TempData["FromDate"] = DFromDate;
            TempData["ToDate"] = DToDate;
            TempData["OfferId"] = NFKOffer;

            var offerDetails = await _db.offerDetails.OrderByDescending(p => p.RangeSubSlNo).Where(m => m.FKOffer == NFKOffer).ToListAsync();
            var offer = await _db.offers.OrderByDescending(p => p.RangeSlNo).Where(m => m.Id == NFKOffer).ToListAsync();

            if (offerDetails.Count == 0)
            {
                DecRangeSlNo = (decimal)offer[0].RangeSlNo + (decimal).01;

                TempData["RangeSubSlNo"] = DecRangeSlNo;
                DecValueFrom = 0;
                DecValueTo = 0;
                NLastRecord = 0;

                ViewData["ValueFrom"] = DecValueFrom;
                ViewData["ValueTo"] = DecValueTo;
            }
            else
            {
                DecRangeSlNo = (decimal)offerDetails[0].RangeSubSlNo + (decimal).01;
                DecValueFrom = (decimal)offerDetails[0].ValueFrom;
                DecValueTo = (decimal)offerDetails[0].ValueTo;
                NLastRecord = (int)offerDetails[0].Id;

                TempData["RangeSubSlNo"] = DecRangeSlNo;
                ViewData["ValueFrom"] = DecValueFrom;
                ViewData["ValueTo"] = DecValueTo;
            }




            return View(offerDetailsVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(offerDetailsVM);
            }

            if (offerDetailsVM.offerDetails.ValueFrom < DecValueTo)
            {
                TempData["Message"] = "ALERT : From Value is Lesser than the last To Value";
                return RedirectToAction(nameof(Create));
                //return View();
            }

            if (offerDetailsVM.offerDetails.ValueTo < offerDetailsVM.offerDetails.ValueFrom)
            {
                TempData["Message"] = "ALERT : To Value is Less than the From Value";
                return View(offerDetailsVM);
            }

            if (NLastRecord > 0)
            {
                var offerDetailsfromDb = await _db.offerDetails.FindAsync(NLastRecord);

                offerDetailsfromDb.IsLastRange = false;
                await _db.SaveChangesAsync();
            }
            
            _db.offerDetails.Add(offerDetailsVM.offerDetails);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Create));


        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerDetails = await _db.offerDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (offerDetails == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            TempData["OfferName"] = SOfferName;
            TempData["FromDate"] = DFromDate;
            TempData["ToDate"] = DToDate;
            TempData["OfferId"] = NFKOffer;

            //partyInfoDtlsVM.PartyInfoDtls = await _db.partyInfoDtls.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterState).Include(m => m.LookUpMasterUnitType).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            offerDetailsVM.offerDetails = await _db.offerDetails.SingleOrDefaultAsync(m => m.Id == id);
            offerDetailsVM.FKAnniverseryInfo = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();

            if (offerDetailsVM.offerDetails == null)
            {
                return NotFound();
            }
            return View(offerDetailsVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfferDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesofferDetailsExist = _db.offerDetails.Include(s => s.FKAnniverseryInfo).Where(s => s.FKAnniverseryInfo == model.offerDetails.FKAnniverseryInfo);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var offerDetailsfromDb = await _db.offerDetails.FindAsync(id);

                offerDetailsfromDb.FKAnniverseryInfo = model.offerDetails.FKAnniverseryInfo;
                offerDetailsfromDb.RangeSubSlNo = model.offerDetails.RangeSubSlNo;
                offerDetailsfromDb.DiscountPercentage = model.offerDetails.DiscountPercentage;
                offerDetailsfromDb.ValueFrom = model.offerDetails.ValueFrom;
                offerDetailsfromDb.ValueTo = model.offerDetails.ValueTo;
                offerDetailsfromDb.ModifiedBy = model.offerDetails.ModifiedBy;
                offerDetailsfromDb.ModifiedDate = model.offerDetails.ModifiedDate;

                await _db.SaveChangesAsync();
                return RedirectToAction("OfferDtlsIndex", "OfferDetails", new { Id = NFKOffer });
                //}
            }
            OfferDetailsViewModel modelVM = new OfferDetailsViewModel()
            {
                //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
                //LookUpMasters = model.LookUpMasters,
                ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OfferDtl = await _db.offerDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (OfferDtl == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            TempData["OfferName"] = SOfferName;
            TempData["FromDate"] = DFromDate;
            TempData["ToDate"] = DToDate;
            TempData["OfferId"] = NFKOffer;

            offerDetailsVM.offerDetails = await _db.offerDetails.SingleOrDefaultAsync(m => m.Id == id);
            offerDetailsVM.FKAnniverseryInfo = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();

            if (offerDetailsVM.offerDetails == null)
            {
                return NotFound();
            }
            return View(offerDetailsVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OfferDtl = await _db.offerDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (OfferDtl == null)
            {
                return NotFound();
            }

            //LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            //{
            //    lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    LookUpMasters = lookUpMaster
            //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            //};
            //return View(model);

            TempData["OfferName"] = SOfferName;
            TempData["FromDate"] = DFromDate;
            TempData["ToDate"] = DToDate;
            TempData["OfferId"] = NFKOffer;

            offerDetailsVM.offerDetails = await _db.offerDetails.SingleOrDefaultAsync(m => m.Id == id);
            offerDetailsVM.FKAnniverseryInfo = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();

            if (offerDetailsVM.offerDetails == null)
            {
                return NotFound();
            }
            return View(offerDetailsVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var offerDtls = await _db.offerDetails.FindAsync(id);

            if (offerDtls == null)
            {
                return View();
            }
            _db.offerDetails.Remove(offerDtls);
            await _db.SaveChangesAsync();

            var offerDetails = await _db.offerDetails.OrderByDescending(p => p.RangeSubSlNo).Where(m => m.FKOffer == NFKOffer).ToListAsync();

            if (offerDetails.Count == 0)
            {
                NLastRecord = 0;
            }
            else
            {
                NLastRecord = (int)offerDetails[0].Id;
            }

            if (NLastRecord > 0)
            {
                var offerDetailsfromDb = await _db.offerDetails.FindAsync(NLastRecord);

                offerDetailsfromDb.IsLastRange = true;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("OfferDtlsIndex", "OfferDetails", new { Id = NFKOffer });
        }
    }
}
