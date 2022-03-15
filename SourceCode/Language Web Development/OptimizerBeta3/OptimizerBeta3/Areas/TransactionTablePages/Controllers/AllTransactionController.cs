using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class AllTransactionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AllTransactionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string? EANCode)
        {
            var effectStartDate = fromDate ?? DateTime.Now.AddMonths(-12);
            var effectEndDate = toDate ?? DateTime.Now;
            ViewBag.FromDate = effectStartDate;
            ViewBag.ToDate = effectEndDate;

            return View(await _db.AllTransactions.OrderBy(x => x.Id).Where(x => x.TranDate >= effectStartDate && x.TranDate <= effectEndDate && x.EANCode == EANCode).ToListAsync());
        }
    }
}
