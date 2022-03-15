using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class PaymentOptionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentOptionController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.paymentOptions.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentOption paymentOption)
        {
            if (ModelState.IsValid)
            {
                _db.paymentOptions.Add(paymentOption);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(paymentOption);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var PaymentOption = await _db.paymentOptions.FindAsync(id);
            if (PaymentOption == null)
            {
                return NotFound();
            }
            return View(PaymentOption);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentOption paymentOption)
        {
            if (ModelState.IsValid)
            {
                var PaymentOption = await _db.paymentOptions.FindAsync(paymentOption.Id);
                PaymentOption.Code = paymentOption.Code;
                PaymentOption.Description = paymentOption.Description;
                PaymentOption.CardNoMinLength = paymentOption.CardNoMinLength;
                PaymentOption.IsNameCompulsory = paymentOption.IsNameCompulsory;
                PaymentOption.IsExpiryDateCompulsory = paymentOption.IsExpiryDateCompulsory;
                PaymentOption.ModifiedBy = paymentOption.ModifiedBy;
                PaymentOption.ModifiedDate = DateTime.Now;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(paymentOption);
        }

        //GET - Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var PaymentOption = await _db.paymentOptions.FindAsync(id);
            if (PaymentOption == null)
            {
                return NotFound();
            }
            return View(PaymentOption);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var PaymentOption = await _db.paymentOptions.FindAsync(id);
            if (PaymentOption == null)
            {
                return NotFound();
            }
            return View(PaymentOption);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var PaymentOption = await _db.paymentOptions.FindAsync(id);

            if (PaymentOption == null)
            {
                return View();
            }
            _db.paymentOptions.Remove(PaymentOption);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
