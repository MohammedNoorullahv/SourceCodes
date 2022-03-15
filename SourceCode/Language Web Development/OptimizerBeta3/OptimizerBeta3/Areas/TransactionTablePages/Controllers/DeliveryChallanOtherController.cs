using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class DeliveryChallanOtherController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public DeliveryChallanViewModel deliveryChallanVM { get; set; }

        public DeliveryChallanDetailViewModel deliveryChallanDetailVM { get; set; }

        public DeliveryChallanOtherController(ApplicationDbContext db)
        {
            _db = db;
            deliveryChallanVM = new DeliveryChallanViewModel()
            {
                FKDCType = _db.lookUpMasters,
                FKSeason = _db.seasons,
                FKFromUnit = _db.unitMasters,
                FKFromLocation = _db.locations,
                FKToUnit = _db.unitMasters,
                FKToParty = _db.partyInfos,
                FKToLocation = _db.locations,
                FKModeofTransport = _db.lookUpMasters,
                FKToState = _db.StateMasters,
                DeliveryChallan = new Models.TransactionTables.DeliveryChallan()
            };

            deliveryChallanDetailVM = new DeliveryChallanDetailViewModel()
            {
                FKUOM = _db.lookUpMasters,
                FKHSNCode = _db.HSNCodeMasters,
                DeliveryChallan = new Models.TransactionTables.DeliveryChallan(),
                DeliveryChallanDetail = new Models.TransactionTables.DeliveryChallanDetail()
            };
        }

        #region DELIVERY CHALLAN
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-1);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.DeliveryChallans.OrderByDescending(x => x.Id).Where(x => x.DCDt >= effectStartDate && x.DCDt <= effectEndDate && x.InvoiceTo == "OT").ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexFilter(DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return RedirectToAction("Index", "DeliveryChallan", new { fromDate = fromDate, toDate = toDate });
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

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

            deliveryChallanVM.FKDCType = lkInvTypes.ToList();
            //deliveryChallanVM.FKDCType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();

            List<LookUpMaster> lkCategorys = new List<LookUpMaster>();
            string sqlQuery2 = $"EXEC SLI_Filters @mAction='SELLkpUpCategory', @mControllerName='{controllerName}', @mActionMethod='{actionName}', @mFKLookUpCategory='56'";
            var cmd2 = _db.Database.GetDbConnection().CreateCommand();
            cmd2.CommandText = sqlQuery2;
            _db.Database.OpenConnection();

            var result2 = cmd2.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result2.Read())
            {
                LookUpMaster lkCategory = new LookUpMaster
                {
                    Id = (int)result2["Id"],
                    Description = result2["Description"].ToString()
                };
                lkCategorys.Add(lkCategory);
            }

            deliveryChallanVM.FKCategory = lkCategorys.ToList();
            //deliveryChallanVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();

            deliveryChallanVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            deliveryChallanVM.FKFromUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();

            List<PartyInfo> suppliers = new List<PartyInfo>();
            string sqlQuery3 = $"EXEC SLI_Filters @mAction='SELSUPPINPOFW', @mControllerName='{controllerName}', @mActionMethod='{actionName}'";
            var cmd3 = _db.Database.GetDbConnection().CreateCommand();
            cmd3.CommandText = sqlQuery3;
            _db.Database.OpenConnection();

            var result3 = cmd3.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result3.Read())
            {
                PartyInfo supplier = new PartyInfo
                {
                    Id = (int)result3["Id"],
                    CompanyName = result3["CompanyName"].ToString()
                };
                suppliers.Add(supplier);
            }

            deliveryChallanVM.FKToParty = suppliers.ToList();
            //deliveryChallanVM.FKToUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            deliveryChallanVM.FKToState = await _db.StateMasters.OrderBy(c => c.StateName).ToListAsync();
            deliveryChallanVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();

            return View(deliveryChallanVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string save)
        {
            if (!ModelState.IsValid)
            {
                deliveryChallanVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
                deliveryChallanVM.FKDCType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
                deliveryChallanVM.FKFromUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKFromLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKToUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKToLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();

                return View(deliveryChallanVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            var unitmaster = await _db.unitMasters.ToListAsync();
            var location = await _db.locations.ToListAsync();
            var party = await _db.partyInfos.ToListAsync();

            string sDCType = lookUpMaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKDCType).FirstOrDefault().Description;
            deliveryChallanVM.DeliveryChallan.DCType = sDCType;
            var season = await _db.seasons.FindAsync(deliveryChallanVM.DeliveryChallan.FKSeason);
            string sSeason = season.Code;
            string sSourceCode = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().ShortName;

            string codechar = (sSourceCode.Substring(0, 4) + sDCType.Substring(0, 2) + sSeason.Substring(0, 4));
            var maxcode = 0;

            if (_db.DeliveryChallans.Where(x => x.DCNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.DeliveryChallans.Where(x => x.DCNo.Contains(codechar)).Select(x => int.Parse(x.DCNo.Substring(11, 4))).ToList().Max();
            }

            deliveryChallanVM.DeliveryChallan.DCNo = codechar.ToUpper() + "-" + String.Format("{0:0000}", (maxcode + 1));

            deliveryChallanVM.DeliveryChallan.FromUnitName = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().CompanyName;
            deliveryChallanVM.DeliveryChallan.FromLocation = location.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromLocation).FirstOrDefault().LocationName;
            deliveryChallanVM.DeliveryChallan.FKFromState = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().FKState;

            //deliveryChallanVM.DeliveryChallan.ToUnitName = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().CompanyName;
            //deliveryChallanVM.DeliveryChallan.ToLocation = location.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToLocation).FirstOrDefault().LocationName;
            //deliveryChallanVM.DeliveryChallan.FKToState = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().FKState;


            deliveryChallanVM.DeliveryChallan.Season = season.Code;
            deliveryChallanVM.DeliveryChallan.ModeofTranspoft = lookUpMaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKModeofTransport).FirstOrDefault().Description;
            deliveryChallanVM.DeliveryChallan.Category = lookUpMaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKCategory).FirstOrDefault().Description;
            deliveryChallanVM.DeliveryChallan.InvoiceTo = "OT";

            if (deliveryChallanVM.DeliveryChallan.ToUnitName == null)
            {
                deliveryChallanVM.DeliveryChallan.ToUnitName = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().CompanyName;
                deliveryChallanVM.DeliveryChallan.ToLocation = "";
                deliveryChallanVM.DeliveryChallan.FKToState = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().FKState;


                int nCity = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().FKCity;
                int nArea = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().FKArea;
                int nState = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().FKState;
                int nPincode = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().FKPincode;
                //int nFKCompany = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().Id;

                deliveryChallanVM.DeliveryChallan.City = lookUpMaster.Where(x => x.Id == nCity).FirstOrDefault().Description;
                deliveryChallanVM.DeliveryChallan.GSTNo = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().GSTNumber;
                deliveryChallanVM.DeliveryChallan.Address = party.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().Address1;
                deliveryChallanVM.DeliveryChallan.Area = lookUpMaster.Where(x => x.Id == nArea).FirstOrDefault().Description;
                deliveryChallanVM.DeliveryChallan.ToState = _db.StateMasters.Where(x => x.Id == nState).FirstOrDefault().StateName;
                deliveryChallanVM.DeliveryChallan.Pincode = Convert.ToInt32(lookUpMaster.Where(x => x.Id == nPincode).FirstOrDefault().Description);
            }
            else
            {
                int nState = deliveryChallanVM.DeliveryChallan.FKToState;
                deliveryChallanVM.DeliveryChallan.ToState = _db.StateMasters.Where(x => x.Id == nState).FirstOrDefault().StateName;
            }    
            
            _db.DeliveryChallans.Add(deliveryChallanVM.DeliveryChallan);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }


        //POST - CREATE
        //[HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAndInsertDtlPost()
        {
            if (!ModelState.IsValid)
            {
                deliveryChallanVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
                deliveryChallanVM.FKDCType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 49 && c.IsActive == true).ToListAsync();
                deliveryChallanVM.FKFromUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKFromLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKToUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKToLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive).ToListAsync();
                deliveryChallanVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();

                return View(deliveryChallanVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sDCType = lookUpMaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKDCType).FirstOrDefault().Description;
            var season = await _db.seasons.FindAsync(deliveryChallanVM.DeliveryChallan.FKSeason);
            string sSeason = season.Code;

            string codechar = (sDCType.Substring(0, 2) + sSeason.Substring(0, 4));
            var maxcode = 0;

            if (_db.DeliveryChallans.Where(x => x.DCNo.Contains(codechar)).ToList().Count > 0)
            {
                maxcode = _db.DeliveryChallans.Where(x => x.DCNo.Contains(codechar)).Select(x => int.Parse(x.DCNo.Substring(7, 4))).ToList().Max();
            }

            deliveryChallanVM.DeliveryChallan.DCNo = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            var unitmaster = await _db.unitMasters.ToListAsync();
            var location = await _db.locations.ToListAsync();
            deliveryChallanVM.DeliveryChallan.FromUnitName = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().CompanyName;
            deliveryChallanVM.DeliveryChallan.FromLocation = location.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromLocation).FirstOrDefault().LocationName;
            deliveryChallanVM.DeliveryChallan.FKFromState = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().FKState;

            deliveryChallanVM.DeliveryChallan.ToUnitName = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToUnit).FirstOrDefault().CompanyName;
            deliveryChallanVM.DeliveryChallan.ToLocation = location.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKToLocation).FirstOrDefault().LocationName;
            deliveryChallanVM.DeliveryChallan.FKToState = unitmaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKFromUnit).FirstOrDefault().FKState;

            deliveryChallanVM.DeliveryChallan.Season = season.Code;
            deliveryChallanVM.DeliveryChallan.ModeofTranspoft = lookUpMaster.Where(x => x.Id == deliveryChallanVM.DeliveryChallan.FKModeofTransport).FirstOrDefault().Description;

            _db.DeliveryChallans.Add(deliveryChallanVM.DeliveryChallan);
            await _db.SaveChangesAsync();



            int nFKDCNo = Convert.ToInt32(_db.DeliveryChallans.Where(x => x.DCNo == deliveryChallanVM.DeliveryChallan.DCNo).FirstOrDefault().Id);
            TempData["DCId"] = nFKDCNo;
            return RedirectToAction(nameof(DCDetailsCreate));
        }
        #endregion

        #region DELIVERY CHALLAN DETAILS
        public async Task<IActionResult> DCDetailsIndex(int Id)
        {

            var dc = await _db.DeliveryChallans.FindAsync(Id);
            TempData["DeliveryChallan"] = dc;

            return View(await _db.DeliveryChallanDetails.Where(x => x.FKDcNo == dc.Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> DCDetailsCreate(int Id)
        {
            //var dc = await _db.DeliveryChallans.FindAsync(Convert.ToInt32(TempData["DCId"]));
            var dc = await _db.DeliveryChallans.FindAsync(Id);
            TempData["DeliveryChallan"] = dc;
            ViewBag.DCDtls = _db.DeliveryChallanDetails.Where(x => x.FKDcNo == dc.Id).ToList();

            //materialsVM.FKUom = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 29).ToListAsync();
            deliveryChallanDetailVM.FKUOM = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 29 && c.IsActive == true).ToListAsync();
            deliveryChallanDetailVM.FKHSNCode = await _db.HSNCodeMasters.ToListAsync();

            return View(deliveryChallanDetailVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DCDetailsCreate(DeliveryChallanDetailViewModel deliveryChallanDetailVM, string Save)
        {

            TempData["DCId"] = deliveryChallanDetailVM.DeliveryChallanDetail.FKDcNo;

            if (!ModelState.IsValid)
            {
                deliveryChallanDetailVM.FKUOM = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 29 && c.IsActive == true).ToListAsync();

                return View(deliveryChallanDetailVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();


            deliveryChallanDetailVM.DeliveryChallanDetail.UOM = lookUpMaster.Where(x => x.Id == deliveryChallanDetailVM.DeliveryChallanDetail.FKUOM).FirstOrDefault().Description;

            _db.DeliveryChallanDetails.Add(deliveryChallanDetailVM.DeliveryChallanDetail);
            await _db.SaveChangesAsync();
            if (Save == "Save & Continue")
            {
                return RedirectToAction("DCDetailsCreate", "DeliveryChallanOther", new { Id = deliveryChallanDetailVM.DeliveryChallanDetail.FKDcNo });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }
        #endregion

    }
}
