using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class LeatherGoodsGroupController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public LeatherGoodsGroupViewModel LeatherGoodsGroupVM { get; set; }
        public LeatherGoodsDetailViewModel LeatherGoodsDetailVM { get; set; }
        public ColorMasterViewModel ColorMasterVM { get; set; }
        public MaterialsViewModel materialsVM { get; set; }

        public LeatherGoodsGroupController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            LeatherGoodsGroupVM = new LeatherGoodsGroupViewModel()
            {
                FKBrand = _db.lookUpMasters,
                FKGroup = _db.lookUpMasters,
                FKCategory = _db.lookUpMasters,
                FKProduct = _db.lookUpMasters,
                LeatherGoodsGroup = new Models.MasterTables.LeatherGoodsGroup()
            };
            LeatherGoodsDetailVM = new LeatherGoodsDetailViewModel()
            {
                FKLeather = _db.materials,
                FKColour = _db.colorMasters,
                FKEntryType = _db.lookUpMasters,
                FKCategory = _db.lookUpMasters,
                FKHSNCode = _db.HSNCodeMasters,
                FKFeatures = _db.lookUpMasters,
                FKSizeorDimension = _db.lookUpMasters,
                LeatherGoodsGroup = new Models.MasterTables.LeatherGoodsGroup(),
                LeatherGoodsDetail = new Models.MasterTables.LeatherGoodsDetail()
            };
            ColorMasterVM = new ColorMasterViewModel()
            {
                FKColour = _db.lookUpMasters,
                colorMaster = new Models.MasterTables.ColorMaster()
            };
            materialsVM = new MaterialsViewModel()
            {
                FKCategory = _db.lookUpMasters,
                FKType = _db.lookUpMasters,
                FKSubType = _db.lookUpMasters,
                FKBrand = _db.lookUpMasters,
                FKSource = _db.lookUpMasters,
                FKUom = _db.lookUpMasters,
                FKColour = _db.lookUpMasters,
                materials = new Models.MasterTables.Materials()
            };

        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.LeatherGoodsGroups.ToListAsync());
        }

        #region "LEATHER GOODS GROUP"
        public async Task<IActionResult> Create()
        {
            LeatherGoodsGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            LeatherGoodsGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32).ToListAsync();
            LeatherGoodsGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33).ToListAsync();
            LeatherGoodsGroupVM.FKProduct = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 61).ToListAsync();

            return View(LeatherGoodsGroupVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(LeatherGoodsGroupVM);
            }

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            LeatherGoodsGroupVM.LeatherGoodsGroup.Brand = lookUpMaster.Where(x => x.Id == LeatherGoodsGroupVM.LeatherGoodsGroup.FKBrand).FirstOrDefault().Description;
            LeatherGoodsGroupVM.LeatherGoodsGroup.LGGroupName = lookUpMaster.Where(x => x.Id == LeatherGoodsGroupVM.LeatherGoodsGroup.FKGroup).FirstOrDefault().Description;
            LeatherGoodsGroupVM.LeatherGoodsGroup.Product = lookUpMaster.Where(x => x.Id == LeatherGoodsGroupVM.LeatherGoodsGroup.FKProduct).FirstOrDefault().Description;

            _db.LeatherGoodsGroups.Add(LeatherGoodsGroupVM.LeatherGoodsGroup);
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

            var LeatherGoodsgroup = await _db.LeatherGoodsGroups.SingleOrDefaultAsync(m => m.Id == id);

            if (LeatherGoodsgroup == null)
            {
                return NotFound();
            }

            LeatherGoodsGroupVM.LeatherGoodsGroup = await _db.LeatherGoodsGroups.SingleOrDefaultAsync(m => m.Id == id);
            LeatherGoodsGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            LeatherGoodsGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32).ToListAsync();
            LeatherGoodsGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33).ToListAsync();
            LeatherGoodsGroupVM.FKProduct = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 61).ToListAsync();

            if (LeatherGoodsGroupVM.LeatherGoodsGroup == null)
            {
                return NotFound();
            }
            return View(LeatherGoodsGroupVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeatherGoodsGroupViewModel model)
        {
            var LeatherGoodsGroupExist = _db.LeatherGoodsGroups.Include(s => s.Description).Where(s => s.Description == model.LeatherGoodsGroup.Description);
            
            var LeatherGoodsGroupfromDb = await _db.LeatherGoodsGroups.FindAsync(id);

            LeatherGoodsGroupfromDb.Description = model.LeatherGoodsGroup.Description;
            LeatherGoodsGroupfromDb.FKGroup = model.LeatherGoodsGroup.FKGroup;
            LeatherGoodsGroupfromDb.FKBrand = model.LeatherGoodsGroup.FKBrand;
            LeatherGoodsGroupfromDb.FKProduct = model.LeatherGoodsGroup.FKProduct;
            LeatherGoodsGroupfromDb.ArticleNo = model.LeatherGoodsGroup.ArticleNo;
            LeatherGoodsGroupfromDb.ArticleName = model.LeatherGoodsGroup.ArticleName;
            LeatherGoodsGroupfromDb.FKGroup = model.LeatherGoodsGroup.FKGroup;
            LeatherGoodsGroupfromDb.VersionNo = model.LeatherGoodsGroup.VersionNo;
            LeatherGoodsGroupfromDb.ModifiedBy = model.LeatherGoodsGroup.ModifiedBy;
            LeatherGoodsGroupfromDb.ModifiedDate = model.LeatherGoodsGroup.ModifiedDate;

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            LeatherGoodsGroupfromDb.Brand = lookUpMaster.Where(x => x.Id == model.LeatherGoodsGroup.FKBrand).FirstOrDefault().Description;
            LeatherGoodsGroupfromDb.LGGroupName = lookUpMaster.Where(x => x.Id == model.LeatherGoodsGroup.FKGroup).FirstOrDefault().Description;
            LeatherGoodsGroupfromDb.Product = lookUpMaster.Where(x => x.Id == model.LeatherGoodsGroup.FKProduct).FirstOrDefault().Description;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LeatherGoodsgroup = await _db.LeatherGoodsGroups.SingleOrDefaultAsync(m => m.Id == id);

            if (LeatherGoodsgroup == null)
            {
                return NotFound();
            }

            LeatherGoodsGroupVM.LeatherGoodsGroup = await _db.LeatherGoodsGroups.SingleOrDefaultAsync(m => m.Id == id);
            LeatherGoodsGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            LeatherGoodsGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32).ToListAsync();
            LeatherGoodsGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33).ToListAsync();
            LeatherGoodsGroupVM.FKProduct = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 61).ToListAsync();

            if (LeatherGoodsGroupVM.LeatherGoodsGroup == null)
            {
                return NotFound();
            }
            return View(LeatherGoodsGroupVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LeatherGoodsgroup = await _db.LeatherGoodsGroups.SingleOrDefaultAsync(m => m.Id == id);

            if (LeatherGoodsgroup == null)
            {
                return NotFound();
            }

            LeatherGoodsGroupVM.LeatherGoodsGroup = await _db.LeatherGoodsGroups.SingleOrDefaultAsync(m => m.Id == id);
            LeatherGoodsGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            LeatherGoodsGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32).ToListAsync();
            LeatherGoodsGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33).ToListAsync();
            LeatherGoodsGroupVM.FKProduct = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 61).ToListAsync();

            if (LeatherGoodsGroupVM.LeatherGoodsGroup == null)
            {
                return NotFound();
            }
            return View(LeatherGoodsGroupVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var LeatherGoodsGroup = await _db.LeatherGoodsGroups.FindAsync(id);

            if (LeatherGoodsGroup == null)
            {
                return View();
            }
            _db.LeatherGoodsGroups.Remove(LeatherGoodsGroup);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region "LEATHER GOODS DETAIL"
        public async Task<IActionResult> LeatherGoodsDetailIndex(int Id)
        {
            var LeatherGoods = await _db.LeatherGoodsGroups.FindAsync(Id);
            TempData["LeatherGoods"] = LeatherGoods;

            return View(await _db.leatherGoodsDetails.Where(a => a.FKArticleGroup == Id).ToListAsync());

        }

        //GET - CREATE
        public async Task<IActionResult> LeatherGoodsDetailCreate(int Id)
        {
            var LeatherGoods = await _db.LeatherGoodsGroups.FindAsync(Id);
            TempData["LeatherGoods"] = LeatherGoods;

            LeatherGoodsDetailVM.FKLeather = await _db.materials.ToListAsync();
            LeatherGoodsDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            LeatherGoodsDetailVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 53 && s.IsActive == true).ToListAsync();
            LeatherGoodsDetailVM.FKEntryType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 52 && s.IsActive == true).ToListAsync();
            LeatherGoodsDetailVM.FKFeatures = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 54 && s.IsActive == true).ToListAsync();
            LeatherGoodsDetailVM.FKHSNCode = await _db.HSNCodeMasters.ToListAsync();
            LeatherGoodsDetailVM.FKSizeorDimension = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 63 && s.IsActive == true).ToListAsync();

            return View(LeatherGoodsDetailVM);
        }

        //POST - CREATE
        //[HttpPost, ActionName("LeatherGoodsDetailCreate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeatherGoodsDetailCreate(LeatherGoodsDetailViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                
                var lookUpMaster = await _db.lookUpMasters.ToListAsync();
                model.LeatherGoodsDetail.EntryType = lookUpMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKEntryType).FirstOrDefault().Description;
                model.LeatherGoodsDetail.Category = lookUpMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKCategory).FirstOrDefault().Description;
                model.LeatherGoodsDetail.Features = lookUpMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKFeatures).FirstOrDefault().Description;
                model.LeatherGoodsDetail.Leather = _db.materials.Where(x => x.Id == model.LeatherGoodsDetail.FKLeather).FirstOrDefault().Description;

                var colorMaster = await _db.colorMasters.ToListAsync();
                model.LeatherGoodsDetail.ColorDescription = colorMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKColour).FirstOrDefault().ColourName;

                var HSNCode = await _db.HSNCodeMasters.ToListAsync();
                model.LeatherGoodsDetail.HSNCode = HSNCode.Where(x => x.Id == model.LeatherGoodsDetail.FKHSNCode).FirstOrDefault().HSNCode;

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    model.LeatherGoodsDetail.Picture = p1;
                }

                int nLengthofLeatherGoodsNo = model.LeatherGoodsDetail.ArticleNo.ToString().Length;
                int nLengthofLeather = model.LeatherGoodsDetail.Leather.ToString().Length;

                string sStockNo = model.LeatherGoodsDetail.Type.ToString() + model.LeatherGoodsDetail.ArticleNo.ToString().Substring(nLengthofLeatherGoodsNo - 5, 5) +
                    model.LeatherGoodsDetail.Leather.ToString().Substring(nLengthofLeather - 2, 2) + model.LeatherGoodsDetail.Variant.ToString();

                model.LeatherGoodsDetail.StockNo = sStockNo.ToString().ToUpper();

                //Size,Brand,Product,LeatherGoodsNo,Leather Type, Variant, Color Description,Group

                var LeatherGoods = await _db.LeatherGoodsGroups.FindAsync(model.LeatherGoodsDetail.FKArticleGroup);

                string sItemDescription = LeatherGoods.Brand.ToString() + " " + LeatherGoods.Product.ToString() + " " + LeatherGoods.ArticleNo +
                    " " + model.LeatherGoodsDetail.Leather.ToString() + " " + model.LeatherGoodsDetail.Variant.ToString() + " " +
                    model.LeatherGoodsDetail.ColorDescription + " " + LeatherGoods.ArticleName.ToString();

                if (sItemDescription.ToString().Length < 100)
                    model.LeatherGoodsDetail.Description = sItemDescription.ToString().ToUpper();
                else
                    model.LeatherGoodsDetail.Description = sItemDescription.ToString().Substring(0, 100).ToUpper();

                _db.leatherGoodsDetails.Add(model.LeatherGoodsDetail);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(LeatherGoodsDetailCreate));
            }
            return View(LeatherGoodsGroupVM);
        }

        //GET - EDIT
        public async Task<IActionResult> LeatherGoodsDetailEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LeatherGoodsdetail = await _db.leatherGoodsDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (LeatherGoodsdetail == null)
            {
                return NotFound();
            }

            var LeatherGoods = await _db.LeatherGoodsGroups.FindAsync(LeatherGoodsdetail.FKArticleGroup);
            TempData["LeatherGoods"] = LeatherGoods;

            LeatherGoodsDetailVM.LeatherGoodsDetail = await _db.leatherGoodsDetails.SingleOrDefaultAsync(m => m.Id == id);
            LeatherGoodsDetailVM.FKLeather = await _db.materials.ToListAsync();
            LeatherGoodsDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            LeatherGoodsDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            LeatherGoodsDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            LeatherGoodsDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();
            LeatherGoodsDetailVM.FKHSNCode = await _db.HSNCodeMasters.ToListAsync();

            if (LeatherGoodsDetailVM.LeatherGoodsDetail == null)
            {
                return NotFound();
            }
            return View(LeatherGoodsDetailVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeatherGoodsDetailEdit(int id, LeatherGoodsDetailViewModel model)
        {
            var LeatherGoodsDetailfromDb = await _db.leatherGoodsDetails.FindAsync(id);
            
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                byte[] p1 = null;
                using (var fs1 = files[0].OpenReadStream())
                {
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                LeatherGoodsDetailfromDb.Picture = p1;
            }

            LeatherGoodsDetailfromDb.FKArticleGroup = model.LeatherGoodsDetail.FKArticleGroup;
            LeatherGoodsDetailfromDb.StockNo = model.LeatherGoodsDetail.StockNo;
            LeatherGoodsDetailfromDb.Description = model.LeatherGoodsDetail.Description;
            LeatherGoodsDetailfromDb.FKLeather = model.LeatherGoodsDetail.FKLeather;
            LeatherGoodsDetailfromDb.FKColour = model.LeatherGoodsDetail.FKColour;
            LeatherGoodsDetailfromDb.VersionNo = model.LeatherGoodsDetail.VersionNo;
            LeatherGoodsDetailfromDb.AdditionalInfo = model.LeatherGoodsDetail.AdditionalInfo;
            LeatherGoodsDetailfromDb.ModifiedBy = model.LeatherGoodsDetail.ModifiedBy;
            LeatherGoodsDetailfromDb.ModifiedDate = model.LeatherGoodsDetail.ModifiedDate;
            LeatherGoodsDetailfromDb.Type = model.LeatherGoodsDetail.Type;
            LeatherGoodsDetailfromDb.Variant = model.LeatherGoodsDetail.Variant;
            LeatherGoodsDetailfromDb.FKHSNCode = model.LeatherGoodsDetail.FKHSNCode;

            var colorMaster = await _db.colorMasters.ToListAsync();
            LeatherGoodsDetailfromDb.ColorDescription = colorMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKColour).FirstOrDefault().ColourName;

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            LeatherGoodsDetailfromDb.EntryType = lookUpMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKEntryType).FirstOrDefault().Description;
            LeatherGoodsDetailfromDb.Category = lookUpMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKCategory).FirstOrDefault().Description;
            LeatherGoodsDetailfromDb.Features = lookUpMaster.Where(x => x.Id == model.LeatherGoodsDetail.FKFeatures).FirstOrDefault().Description;
            LeatherGoodsDetailfromDb.Leather = _db.materials.Where(x => x.Id == model.LeatherGoodsDetail.FKLeather).FirstOrDefault().Description;

            int nLengthofLeatherGoodsNo = model.LeatherGoodsDetail.ArticleNo.ToString().Length;
            int nLengthofLeather = LeatherGoodsDetailfromDb.Leather.ToString().Length;
            string sStockNo;
            if (LeatherGoodsDetailfromDb.Type == null)
            {
                sStockNo = model.LeatherGoodsDetail.ArticleNo.ToString().Substring(nLengthofLeatherGoodsNo - 5, 5) +
                LeatherGoodsDetailfromDb.Leather.ToString().Substring(nLengthofLeather - 2, 2) + model.LeatherGoodsDetail.Variant.ToString();
            }
            else
            {
                sStockNo = model.LeatherGoodsDetail.Type.ToString() + model.LeatherGoodsDetail.ArticleNo.ToString().Substring(nLengthofLeatherGoodsNo - 5, 5) +
                LeatherGoodsDetailfromDb.Leather.ToString().Substring(nLengthofLeather - 2, 2) + model.LeatherGoodsDetail.Variant.ToString();
            }


            LeatherGoodsDetailfromDb.StockNo = sStockNo.ToString().ToUpper();

            var HSNCode = await _db.HSNCodeMasters.ToListAsync();
            LeatherGoodsDetailfromDb.HSNCode = HSNCode.Where(x => x.Id == model.LeatherGoodsDetail.FKHSNCode).FirstOrDefault().HSNCode;

            await _db.SaveChangesAsync();
            return RedirectToAction("LeatherGoodsDetailIndex", "LeatherGoodsGroup", new { Id = model.LeatherGoodsDetail.FKArticleGroup });
        }

        //GET - DETAIL
        public async Task<IActionResult> LeatherGoodsDetailDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LeatherGoodsdetail = await _db.leatherGoodsDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (LeatherGoodsdetail == null)
            {
                return NotFound();
            }

            var LeatherGoods = await _db.LeatherGoodsGroups.FindAsync(LeatherGoodsdetail.FKArticleGroup);
            TempData["LeatherGoods"] = LeatherGoods;

            LeatherGoodsDetailVM.LeatherGoodsDetail = await _db.leatherGoodsDetails.SingleOrDefaultAsync(m => m.Id == id);
            LeatherGoodsDetailVM.FKLeather = await _db.materials.ToListAsync();
            LeatherGoodsDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            LeatherGoodsDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            LeatherGoodsDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            LeatherGoodsDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();
            if (LeatherGoodsDetailVM.LeatherGoodsDetail == null)
            {
                return NotFound();
            }
            return View(LeatherGoodsDetailVM);
        }

        //GET - DELETE
        public async Task<IActionResult> LeatherGoodsDetailDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var LeatherGoodsdetail = await _db.leatherGoodsDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (LeatherGoodsdetail == null)
            {
                return NotFound();
            }

            var LeatherGoods = await _db.LeatherGoodsGroups.FindAsync(LeatherGoodsdetail.FKArticleGroup);
            TempData["LeatherGoods"] = LeatherGoods;

            LeatherGoodsDetailVM.LeatherGoodsDetail = await _db.leatherGoodsDetails.SingleOrDefaultAsync(m => m.Id == id);
            LeatherGoodsDetailVM.FKLeather = await _db.materials.ToListAsync();
            LeatherGoodsDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            LeatherGoodsDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            LeatherGoodsDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            LeatherGoodsDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();

            if (LeatherGoodsDetailVM.LeatherGoodsDetail == null)
            {
                return NotFound();
            }
            return View(LeatherGoodsDetailVM);
        }

        //POST - Delete
        [HttpPost, ActionName("LeatherGoodsDetailDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeatherGoodsDetailDeleteConfirmed(int? id)
        {
            var LeatherGoodsDetail = await _db.leatherGoodsDetails.FindAsync(id);

            if (LeatherGoodsDetail == null)
            {
                return View();
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            //var LeatherGoodsDetailfromDb = await _db.LeatherGoodsDetails.FindAsync(id);
            if (LeatherGoodsDetail.ArticleImage != null)
            {
                var imagePath = Path.Combine(webRootPath, LeatherGoodsDetail.ArticleImage.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _db.leatherGoodsDetails.Remove(LeatherGoodsDetail);
            await _db.SaveChangesAsync();
            return RedirectToAction("LeatherGoodsDetailIndex", "LeatherGoodsGroup", new { Id = LeatherGoodsDetail.Id });
        }
        #endregion

        #region "MASTERS"
        //GET - CREATE
        public async Task<IActionResult> LookUpMasterCreate()
        {
            LookUpCategoryAndLookUpMstViewModel model = new LookUpCategoryAndLookUpMstViewModel()
            {
                lookUpCategorieslist = await _db.lookUpCategories.OrderBy(c => c.Description).ToListAsync(),
                LookUpMasters = new Models.MasterTables.LookUpMaster()
                //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).Distinct().ToListAsync()
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

        //GET - CREATE
        public async Task<IActionResult> ColorMasterCreate()
        {
            //TempData["AGID"] = NFKArticleGroup;
            ColorMasterVM.FKColour = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 34).ToListAsync();

            return View(ColorMasterVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ColorMasterCreate(ColorMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ColorMasterVM);
            }

            _db.colorMasters.Add(model.colorMaster);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ColorMasterCreate));
        }

        //GET - CREATE
        public async Task<IActionResult> MaterialMasterCreate()
        {
            //TempData["AGID"] = NFKArticleGroup;
            materialsVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            materialsVM.FKType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 25).ToListAsync();
            materialsVM.FKSubType = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 26).ToListAsync();
            materialsVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 27).ToListAsync();
            materialsVM.FKSource = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 28).ToListAsync();
            materialsVM.FKUom = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 29).ToListAsync();
            materialsVM.FKColour = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 34).ToListAsync();

            return View(materialsVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MaterialMasterCreate(MaterialsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(materialsVM);
            }

            //var lookUpMaster = await _db.lookUpMasters.FindAsync(materialsVM.materials.FKCategory);
            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            string sCategory = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKCategory).FirstOrDefault().Description;
            string sType = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKType).FirstOrDefault().Description;
            string sSubType = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKSubType).FirstOrDefault().Description;
            string sBrand = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKBrand).FirstOrDefault().Description;
            //lookUpMaster = await _db.lookUpMasters.FindAsync(materialsVM.materials.FKSource);
            string sSource = lookUpMaster.Where(x => x.Id == materialsVM.materials.FKSource).FirstOrDefault().Description;

            string codechar = (sCategory.Substring(0, 2) + sType.Substring(0, 2) + sSubType.Substring(0, 2) + sBrand.Substring(0, 2) + sSource.Substring(0, 1)).ToUpper();
            var maxcode = 0;

            if (_db.companyInfos.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Count > 0)
            {
                maxcode = _db.companyInfos.Where(x => x.Code.Contains(codechar)).Select(x => int.Parse(x.Code.Substring(2, 4))).ToList().Max();
            }

            materialsVM.materials.Code = codechar + "-" + String.Format("{0:0000}", (maxcode + 1));

            _db.materials.Add(model.materials);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(MaterialMasterCreate));
        }
        #endregion
    }
}
