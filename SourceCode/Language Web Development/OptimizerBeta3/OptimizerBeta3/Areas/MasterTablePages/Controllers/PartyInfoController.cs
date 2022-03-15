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
    public class PartyInfoController : Controller
    {
        private readonly ApplicationDbContext _db;
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public PartyInfoViewModel PartyInfoVM { get; set; }
        public SalesPromotionViewModel SalesPromotionVM { get; set; }
        public PartyInfoController(ApplicationDbContext db)
        {
            _db = db;
            PartyInfoVM = new PartyInfoViewModel()
            {
                FKCategory = _db.lookUpMasters,
                FKArea = _db.lookUpMasters,
                FKCity = _db.lookUpMasters,
                FKPincode = _db.lookUpMasters,
                FKState = _db.StateMasters,
                FKCountry = _db.lookUpMasters,
                partyInfo = new Models.MasterTables.PartyInfo()
            };

            SalesPromotionVM = new SalesPromotionViewModel()
            {
                FKOffer = _db.lookUpMasters,
                SalesPromotionOffer = new Models.MasterTables.SalesPromotionOffer()
            };
        }
        public async Task<IActionResult> Index()
        {
            //await _db.partyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            return View(await _db.partyInfos.Include(m => m.LookUpMasterCategory).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            PartyInfoVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 20).ToListAsync();
            PartyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            PartyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            PartyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            PartyInfoVM.FKState = await _db.StateMasters.ToListAsync();
            PartyInfoVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();

            return View(PartyInfoVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(PartyInfoVM);
            }

            string codechar = PartyInfoVM.partyInfo.CompanyName.Substring(0, 2).ToUpper();
            var maxcode = 0;

            if (_db.partyInfos.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Count > 0)
            {
                maxcode = _db.partyInfos.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Max();
            }

            PartyInfoVM.partyInfo.Code = codechar + String.Format("{0:0000}", (maxcode + 1));

            _db.partyInfos.Add(PartyInfoVM.partyInfo);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PartyInfo = await _db.partyInfos.SingleOrDefaultAsync(m => m.Id == id);

            if (PartyInfo == null)
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

            //PartyInfoVM.partyInfo = await _db.partyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            PartyInfoVM.partyInfo = await _db.partyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            PartyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            PartyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            PartyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            PartyInfoVM.FKState = await _db.StateMasters.ToListAsync();
            PartyInfoVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 20).ToListAsync();
            PartyInfoVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();

            if (PartyInfoVM.partyInfo == null)
            {
                return NotFound();
            }
            return View(PartyInfoVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartyInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesPatyInfoExist = _db.partyInfos.Include(s => s.CompanyName).Where(s => s.CompanyName == model.partyInfo.CompanyName && s.Address1 == model.partyInfo.Address1);

                //if (doesPatyInfoExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var PartyInfofromDb = await _db.partyInfos.FindAsync(id);

                PartyInfofromDb.FKCategory = model.partyInfo.FKCategory;
                PartyInfofromDb.Code = model.partyInfo.Code;
                PartyInfofromDb.CompanyName = model.partyInfo.CompanyName;
                PartyInfofromDb.ShortName = model.partyInfo.ShortName;
                PartyInfofromDb.Address1 = model.partyInfo.Address1;
                PartyInfofromDb.Address2 = model.partyInfo.Address2;
                PartyInfofromDb.FKArea = model.partyInfo.FKArea;
                PartyInfofromDb.FKCity = model.partyInfo.FKCity;
                PartyInfofromDb.FKPincode = model.partyInfo.FKPincode;
                PartyInfofromDb.FKState = model.partyInfo.FKState;
                PartyInfofromDb.FKCountry = model.partyInfo.FKCountry;
                PartyInfofromDb.ContactPersonName = model.partyInfo.ContactPersonName;
                PartyInfofromDb.ContactNo = model.partyInfo.ContactNo;
                PartyInfofromDb.MailId = model.partyInfo.MailId;
                PartyInfofromDb.PANNumber = model.partyInfo.PANNumber;
                PartyInfofromDb.GSTNumber = model.partyInfo.GSTNumber;
                PartyInfofromDb.ModifiedBy = model.partyInfo.ModifiedBy;
                PartyInfofromDb.ModifiedDate = model.partyInfo.ModifiedDate;
                PartyInfofromDb.NewOrderDeliveryDays = model.partyInfo.NewOrderDeliveryDays;
                PartyInfofromDb.ReplinishmentOrderDeliveryDays = model.partyInfo.ReplinishmentOrderDeliveryDays;
                PartyInfofromDb.GeneralDeliveryDays = model.partyInfo.GeneralDeliveryDays;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
            }
            PartyInfoViewModel modelVM = new PartyInfoViewModel()
            {
                //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
                //LookUpMasters = model.LookUpMasters,
                ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET - DETAILS
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PartyInfo = await _db.partyInfos.SingleOrDefaultAsync(m => m.Id == id);

            if (PartyInfo == null)
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

            PartyInfoVM.partyInfo = await _db.partyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            PartyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            PartyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            PartyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            PartyInfoVM.FKState = await _db.StateMasters.ToListAsync();
            PartyInfoVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 20).ToListAsync();
            PartyInfoVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();

            if (PartyInfoVM.partyInfo == null)
            {
                return NotFound();
            }
            return View(PartyInfoVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PartyInfo = await _db.partyInfos.SingleOrDefaultAsync(m => m.Id == id);

            if (PartyInfo == null)
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

            PartyInfoVM.partyInfo = await _db.partyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            PartyInfoVM.FKArea = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 1).ToListAsync();
            PartyInfoVM.FKCity = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 2).ToListAsync();
            PartyInfoVM.FKPincode = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 3).ToListAsync();
            PartyInfoVM.FKState = await _db.StateMasters.ToListAsync();
            PartyInfoVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 20).ToListAsync();
            PartyInfoVM.FKCountry = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 21).ToListAsync();

            if (PartyInfoVM.partyInfo == null)
            {
                return NotFound();
            }
            return View(PartyInfoVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var partyInfo = await _db.partyInfos.FindAsync(id);

            if (partyInfo == null)
            {
                return View();
            }
            _db.partyInfos.Remove(partyInfo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SalesPromotionIndex(int? Id)
        {
            var party = await _db.partyInfos.FindAsync(Id);
            TempData["party"] = party;

            return View(await _db.SalesPromotionOffers.Where(x => x.FKParty == Id).ToListAsync());
        }


        //GET - CREATE
        public async Task<IActionResult> SalesPromotionCreate(int Id)
        {
            var party = await _db.partyInfos.FindAsync(Id);
            TempData["party"] = party;
            //sPurchaseOrderNo = po.PurchaseOrderNo;

            SalesPromotionVM.FKOffer = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 59 && c.IsActive == true).ToListAsync();

            return View(SalesPromotionVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalesPromotionCreate(SalesPromotionViewModel SalesPromotionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(SalesPromotionVM);
            }

            if (SalesPromotionVM.SalesPromotionOffer.IsActive == true )
            {
                var SPFromDb = await _db.SalesPromotionOffers.Where(x => x.FKParty == SalesPromotionVM.SalesPromotionOffer.FKParty && x.IsActive == true).FirstAsync();
                SPFromDb.IsActive = false;
                await _db.SaveChangesAsync();
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            SalesPromotionVM.SalesPromotionOffer.OfferName = lookUpMaster.Where(x => x.Id == SalesPromotionVM.SalesPromotionOffer.FKOffer).FirstOrDefault().Description;
            
            _db.SalesPromotionOffers.Add(SalesPromotionVM.SalesPromotionOffer);
            await _db.SaveChangesAsync();

            return RedirectToAction("SalesPromotionIndex", "PartyInfo", new { Id = SalesPromotionVM.SalesPromotionOffer.FKParty });

        }

        public async Task<IActionResult> SalesPromotionEdit(int Id)
        {
            var spo = await _db.SalesPromotionOffers.SingleOrDefaultAsync(m => m.Id == Id);

            if (spo == null)
            {
                return NotFound();
            }


            //PartyInfoVM.partyInfo = await _db.partyInfos.Include(m => m.LookUpMasterArea).Include(m => m.LookUpMasterCity).Include(m => m.LookUpMasterPincode).Include(m => m.StateMaster).Include(m => m.LookUpMasterCategory).Include(m => m.LookUpMasterCountry).SingleOrDefaultAsync(m => m.Id == id);
            SalesPromotionVM.SalesPromotionOffer = await _db.SalesPromotionOffers.SingleOrDefaultAsync(m => m.Id == Id);
            SalesPromotionVM.FKOffer = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 59 && c.IsActive == true).ToListAsync();
            
            var party = await _db.partyInfos.FindAsync(SalesPromotionVM.SalesPromotionOffer.FKParty);
            TempData["party"] = party;
            

            if (SalesPromotionVM.SalesPromotionOffer == null)
            {
                return NotFound();
            }
            return View(SalesPromotionVM);

            
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalesPromotionEdit(int Id, SalesPromotionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(SalesPromotionVM);
            }

            if (model.SalesPromotionOffer.IsActive == true)
            {
                var SPFromDbActive = await _db.SalesPromotionOffers.Where(x => x.FKParty == model.SalesPromotionOffer.FKParty && x.IsActive == true).FirstAsync();
                SPFromDbActive.IsActive = false;
                await _db.SaveChangesAsync();
            }

            var SPFromDb = await _db.SalesPromotionOffers.Where(x => x.Id == Id).FirstAsync();
            SPFromDb.FromDate = model.SalesPromotionOffer.FromDate;
            SPFromDb.ToDate = model.SalesPromotionOffer.ToDate;
            SPFromDb.IsExtendable = model.SalesPromotionOffer.IsExtendable;
            SPFromDb.DiscountPercentage = model.SalesPromotionOffer.DiscountPercentage;
            SPFromDb.DiscountValue = model.SalesPromotionOffer.DiscountValue;
            SPFromDb.MaximumDiscountValue = model.SalesPromotionOffer.MaximumDiscountValue;
            SPFromDb.IsActive = model.SalesPromotionOffer.IsActive;
            SPFromDb.FKOffer = model.SalesPromotionOffer.FKOffer;
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            SPFromDb.OfferName = lookUpMaster.Where(x => x.Id == model.SalesPromotionOffer.FKOffer).FirstOrDefault().Description;

            await _db.SaveChangesAsync();
            return RedirectToAction("SalesPromotionIndex", "PartyInfo", new { Id = model.SalesPromotionOffer.FKParty });

        }

        //GET - CREATE
        public async Task<IActionResult> LookUpMasterCreate()
        {
            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderBy(c => c.Description).ToListAsync(),
                LookUpMasters = new Models.MasterTables.LookUpMaster(),
                LookUpMstList = await _db.lookUpMasters.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
            };
            return View(model);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LookUpMasterCreate(LookUpCategoryAndLookUpMstViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesLookUpMstExist = _db.lookUpMasters.Include(s => s.LookUpCategory).Where(s => s.Description == model.LookUpMasters.Description && s.LookUpCategory.Id == model.LookUpMasters.FKLookUpCategory);

                if (doesLookUpMstExist.Count() > 0)
                {
                    ////ERROR.
                    //StatusMessage = "Error : Look Up Master Exists under " + doesLookUpMstExist.First().LookUpCategory.Description + " Category. Please use anothe name ";
                }
                else
                {
                    if (model.LookUpMasters.SetAsDefault == true)
                    {
                        var defaultId = _db.lookUpMasters.Where(s => s.FKLookUpCategory == model.LookUpMasters.FKLookUpCategory && s.SetAsDefault == true);
                        if (defaultId.Count() > 0)
                        {
                            var lookUpMasterFromDb = await _db.lookUpMasters.FindAsync(defaultId.First().Id);

                            lookUpMasterFromDb.SetAsDefault = false;
                            await _db.SaveChangesAsync();

                        }
                    }
                    _db.lookUpMasters.Add(model.LookUpMasters);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(LookUpMasterCreate));
                }
            }
            LookUpCategoryAndLookUpMstViewModel modelVM = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.ToListAsync(),
                LookUpMasters = new Models.MasterTables.LookUpMaster()
                //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

    }
}
