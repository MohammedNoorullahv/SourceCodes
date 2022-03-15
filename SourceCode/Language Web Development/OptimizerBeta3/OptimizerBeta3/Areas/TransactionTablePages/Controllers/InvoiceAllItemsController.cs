using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class InvoiceAllItemsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public InvoiceViewModel InvoiceVM { get; set; }
        public InvoiceDetailViewModel InvoiceDetailVM { get; set; }
        public StocksViewModel StocksVM { get; set; }
        public AllTransactionViewModel AllTransactionVM { get; set; }

        public static int nFKInvoice;
        public static string sScanningMode, sInvoiceNo;
        public static string sIpAddress;
        public static string ipaddress = string.Empty;

        public InvoiceAllItemsController(ApplicationDbContext db)
        {
            _db = db;
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

        public async Task<IActionResult> Index()
        {
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
            return View(await _db.Invoices.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            InvoiceVM.FKCurrency = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 30 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKDepartment = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 9 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKParty = await _db.partyInfos.OrderBy(c => c.CompanyName).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKPaymentTerms = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 37 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKSeason = await _db.seasons.OrderBy(c => c.Description).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKUnit = await _db.unitMasters.OrderBy(c => c.CompanyInfo).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKBillTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKNotifyTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKDeliveryTo = await _db.partyInfoDtls.OrderBy(c => c.CompanyName).Where(c => c.IsActive).ToListAsync();
            InvoiceVM.FKLocation = await _db.locations.OrderBy(c => c.LocationName).Where(c => c.IsActive == true).ToListAsync();
            InvoiceVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 56 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKDestination = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 51 && c.IsActive == true).ToListAsync();


            InvoiceVM.FKTypeOfInvoice = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 45 && c.IsActive == true).ToListAsync();
            InvoiceVM.FKModeofTransport = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(c => c.FKLookUpCategory == 38 && c.IsActive == true).ToListAsync();

            return View(InvoiceVM);

        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
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
            var companyInfo = await _db.companyInfos.ToListAsync();
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

            _db.Invoices.Add(InvoiceVM.Invoice);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
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
            nFKInvoice = inv.Id;
            sInvoiceNo = inv.InvoiceNo;

            return View(await _db.InvoiceDetails.Where(x => x.FKInvoiceNo == Id).ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> InvoiceDetailCreate()
        {
            var inv = await _db.Invoices.FindAsync(nFKInvoice);
            TempData["Invoice"] = inv;
            return View();

        }

        //POST - CREATE
        [HttpPost, ActionName("InvoiceDetailCreate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvoiceDetailCreatePost()
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
            var companyInfo = await _db.companyInfos.ToListAsync();
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

            _db.Invoices.Add(InvoiceVM.Invoice);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
        #endregion
    }
}
