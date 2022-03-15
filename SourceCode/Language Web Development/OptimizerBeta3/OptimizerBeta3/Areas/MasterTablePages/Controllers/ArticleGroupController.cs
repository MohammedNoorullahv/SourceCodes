using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.ViewModels.MasterTables;
using OptimizerBeta3.Utility;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.MasterTablePages.Controllers
{
    [Area("MasterTablePages")]
    public class ArticleGroupController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public ArticleGroupViewModel ArticleGroupVM { get; set; }
        public ArticleDetailViewModel ArticleDetailVM { get; set; }
        public ColorMasterViewModel ColorMasterVM { get; set; }
        public MaterialsViewModel materialsVM { get; set; }

        public static int NFKArticleGroup;
        public static string SAGArticleName, SAGArticleNo, SAGDescription;

        public ArticleGroupController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            ArticleGroupVM = new ArticleGroupViewModel()
            {
                FKSeason = _db.seasons,
                FKBrand = _db.lookUpMasters,
                FKGroup = _db.lookUpMasters,
                FKSizeFor = _db.lookUpMasters,
                FKCategory = _db.lookUpMasters,
                FKProduct = _db.lookUpMasters,
                FKAssortmentGroup = _db.lookUpMasters,
                ArticleGroup = new Models.MasterTables.ArticleGroup()
    };
            ArticleDetailVM = new ArticleDetailViewModel()
            {
                FKLeather = _db.materials,
                FKColour = _db.colorMasters,
                FKEntryType = _db.lookUpMasters,
                FKCategory = _db.lookUpMasters,
                FKHSNCode = _db.HSNCodeMasters,                
                ArticleGroup = new Models.MasterTables.ArticleGroup(),
                ArticleDetail = new Models.MasterTables.ArticleDetail()
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
            return View(await _db.articleGroups.ToListAsync());
        }

        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            ArticleGroupVM.FKSeason = await _db.seasons.ToListAsync();
            //ArticleGroupVM.FKArticleType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 31).ToListAsync();
            ArticleGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKSizeFor = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 23 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKProduct = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 61 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKAssortmentGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 68 && s.IsActive == true).ToListAsync();

            return View(ArticleGroupVM);
        }

        //POST - CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ArticleGroupVM);
            }

           var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var season = await _db.seasons.FindAsync(ArticleGroupVM.ArticleGroup.FKSeason);
            ArticleGroupVM.ArticleGroup.Season = season.Code;
            ArticleGroupVM.ArticleGroup.ArticleGroupName = lookUpMaster.Where(x => x.Id == ArticleGroupVM.ArticleGroup.FKGroup).FirstOrDefault().Description;
            ArticleGroupVM.ArticleGroup.Brand = lookUpMaster.Where(x => x.Id == ArticleGroupVM.ArticleGroup.FKBrand).FirstOrDefault().Description;
            ArticleGroupVM.ArticleGroup.SizeFor = lookUpMaster.Where(x => x.Id == ArticleGroupVM.ArticleGroup.FKSizeFor).FirstOrDefault().Description;
            ArticleGroupVM.ArticleGroup.Product = lookUpMaster.Where(x => x.Id == ArticleGroupVM.ArticleGroup.FKProduct).FirstOrDefault().Description;
            ArticleGroupVM.ArticleGroup.AssortmentGroup = lookUpMaster.Where(x => x.Id == ArticleGroupVM.ArticleGroup.FKAssortmentGroup).FirstOrDefault().Description;

            _db.articleGroups.Add(ArticleGroupVM.ArticleGroup);
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

            var articlegroup = await _db.articleGroups.SingleOrDefaultAsync(m => m.Id == id);

            if (articlegroup == null)
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

            //ArticleGroupVM.ArticleGroup = await _db.articleGroups.Include(m => m.Season).Include(m => m.LookUpMasterArticleType).
            //    Include(m => m.LookUpMasterBrand).Include(m => m.LookUpMasterGroup).Include(m => m.LookUpMasterSizeFor).SingleOrDefaultAsync(m => m.Id == id);
            ArticleGroupVM.ArticleGroup = await _db.articleGroups.SingleOrDefaultAsync(m => m.Id == id);
            ArticleGroupVM.FKSeason = await _db.seasons.ToListAsync();
            //ArticleGroupVM.FKArticleType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 31).ToListAsync();
            ArticleGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKSizeFor = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 23 && s.IsActive == true).ToListAsync();
            ArticleGroupVM.FKAssortmentGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 68 && s.IsActive == true).ToListAsync();


            if (ArticleGroupVM.ArticleGroup == null)
            {
                return NotFound();
            }
            return View(ArticleGroupVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticleGroupViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var articleGroupExist = _db.articleGroups.Include(s => s.Description).Where(s => s.Description == model.ArticleGroup.Description);

                //if (doesLookUpMstExist.Count() > 0 )
                //{
                //    //ERROR.    
                //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
                //}
                //else
                //{
                var ArticleGroupfromDb = await _db.articleGroups.FindAsync(id);

                ArticleGroupfromDb.FKSeason = model.ArticleGroup.FKSeason;
                ArticleGroupfromDb.Description = model.ArticleGroup.Description;
                ArticleGroupfromDb.FKBrand = model.ArticleGroup.FKBrand;
                ArticleGroupfromDb.Product = model.ArticleGroup.Product;
                ArticleGroupfromDb.ArticleNo = model.ArticleGroup.ArticleNo;
                ArticleGroupfromDb.ArticleName = model.ArticleGroup.ArticleName;
                ArticleGroupfromDb.FKGroup = model.ArticleGroup.FKGroup;
                ArticleGroupfromDb.FKSizeFor = model.ArticleGroup.FKSizeFor;
                ArticleGroupfromDb.VersionNo = model.ArticleGroup.VersionNo;
                ArticleGroupfromDb.ModifiedBy = model.ArticleGroup.ModifiedBy;
                ArticleGroupfromDb.ModifiedDate = model.ArticleGroup.ModifiedDate;

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();

            var season = await _db.seasons.FindAsync(model.ArticleGroup.FKSeason);
            ArticleGroupfromDb.Season = season.Code;
            ArticleGroupfromDb.ArticleGroupName = lookUpMaster.Where(x => x.Id == model.ArticleGroup.FKGroup).FirstOrDefault().Description;
            
            ArticleGroupfromDb.Brand = lookUpMaster.Where(x => x.Id == model.ArticleGroup.FKBrand).FirstOrDefault().Description;
            ArticleGroupfromDb.SizeFor = lookUpMaster.Where(x => x.Id == model.ArticleGroup.FKSizeFor).FirstOrDefault().Description;
            ArticleGroupfromDb.AssortmentGroup = lookUpMaster.Where(x => x.Id == model.ArticleGroup.FKAssortmentGroup).FirstOrDefault().Description;
            ArticleGroupfromDb.Product = lookUpMaster.Where(x => x.Id == model.ArticleGroup.FKProduct).FirstOrDefault().Description;
            

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

                //}
            //}
            //PartyInfoDtlsViewModel modelVM = new PartyInfoDtlsViewModel()
            //{
            //    //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    //LookUpMasters = model.LookUpMasters,
            //    ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
            //    //StatusMessage = StatusMessage
            //};
            //return View(modelVM);
        }

        //GET - DETAIL
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articlegroup = await _db.articleGroups.SingleOrDefaultAsync(m => m.Id == id);

            if (articlegroup == null)
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

            ArticleGroupVM.ArticleGroup = await _db.articleGroups.Include(m => m.Season).
                Include(m => m.LookUpMasterBrand).Include(m => m.LookUpMasterGroup).Include(m => m.LookUpMasterSizeFor).SingleOrDefaultAsync(m => m.Id == id);
            ArticleGroupVM.FKSeason = await _db.seasons.ToListAsync();
            ArticleGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            ArticleGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32).ToListAsync();
            ArticleGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33).ToListAsync();
            ArticleGroupVM.FKSizeFor = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 23).ToListAsync();

            if (ArticleGroupVM.ArticleGroup == null)
            {
                return NotFound();
            }
            return View(ArticleGroupVM);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articlegroup = await _db.articleGroups.SingleOrDefaultAsync(m => m.Id == id);

            if (articlegroup == null)
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


            ArticleGroupVM.ArticleGroup = await _db.articleGroups.Include(m => m.Season).
                Include(m => m.LookUpMasterBrand).Include(m => m.LookUpMasterGroup).Include(m => m.LookUpMasterSizeFor).SingleOrDefaultAsync(m => m.Id == id);
            ArticleGroupVM.FKSeason = await _db.seasons.ToListAsync();
            ArticleGroupVM.FKCategory = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 24).ToListAsync();
            ArticleGroupVM.FKBrand = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 32).ToListAsync();
            ArticleGroupVM.FKGroup = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 33).ToListAsync();
            ArticleGroupVM.FKSizeFor = await _db.lookUpMasters.OrderByDescending(c => c.SetAsDefault).ThenBy(c => c.Description).Where(s => s.FKLookUpCategory == 23).ToListAsync();

            if (ArticleGroupVM.ArticleGroup == null)
            {
                return NotFound();
            }
            return View(ArticleGroupVM);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var ArticleGroup = await _db.articleGroups.FindAsync(id);

            if (ArticleGroup == null)
            {
                return View();
            }
            _db.articleGroups.Remove(ArticleGroup);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ArticleDetailIndex(int Id)
        {
            var Article = await _db.articleGroups.FindAsync(Id);

            TempData["AGArticleNo"] = Article.ArticleNo;
            TempData["AGArticleName"] = Article.ArticleName;
            TempData["AGDescription"] = Article.Description;
            TempData["AGID"] = Article.Id;
            
            NFKArticleGroup = Article.Id;
            SAGArticleNo = Article.ArticleNo;
            SAGArticleName = Article.ArticleName;
            SAGDescription = Article.Description;

            return View(await _db.articleDetails.Where(a => a.FKArticleGroup == NFKArticleGroup).ToListAsync());

        }

        //GET - CREATE
        public async Task<IActionResult> ArticleDetailCreate()
        {
            ArticleDetailVM.FKLeather = await _db.materials.ToListAsync();
            ArticleDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKLiningColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKSocksColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKSoleColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            ArticleDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            ArticleDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();
            ArticleDetailVM.FKHSNCode = await _db.HSNCodeMasters.ToListAsync();

            TempData["AGArticleNo"] = SAGArticleNo;
            TempData["AGArticleName"] = SAGArticleName;
            TempData["AGDescription"] = SAGDescription;
            TempData["AGID"] = NFKArticleGroup;
            
            return View(ArticleDetailVM);
        }

        //POST - CREATE
        //[HttpPost, ActionName("ArticleDetailCreate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArticleDetailCreate(ArticleDetailViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(ArticleGroupVM);
            //}

            //_db.articleDetails.Add(ArticleDetailVM.ArticleDetail);
            //await _db.SaveChangesAsync();

            //return RedirectToAction(nameof(ArticleDetailCreate));

            if (ModelState.IsValid)
            {
                //var doesLookUpMstExist = _db.lookUpMasters.Include(s => s.LookUpCategory).Where(s => s.Description == model.LookUpMasters.Description && s.LookUpCategory.Id == model.LookUpMasters.FKLookUpCategory);

                //if (doesLookUpMstExist.Count() > 0)
                //{
                //    ////ERROR.
                //    //StatusMessage = "Error : Look Up Master Exists under " + doesLookUpMstExist.First().LookUpCategory.Description + " Category. Please use anothe name ";
                //}
                //else
                //{

                var lookUpMaster = await _db.lookUpMasters.ToListAsync();
                model.ArticleDetail.EntryType = lookUpMaster.Where(x => x.Id == model.ArticleDetail.FKEntryType).FirstOrDefault().Description;
                model.ArticleDetail.Category = lookUpMaster.Where(x => x.Id == model.ArticleDetail.FKCategory).FirstOrDefault().Description;
                model.ArticleDetail.Features = lookUpMaster.Where(x => x.Id == model.ArticleDetail.FKFeatures).FirstOrDefault().Description;
                model.ArticleDetail.Leather = _db.materials.Where(x => x.Id == model.ArticleDetail.FKLeather).FirstOrDefault().Description;

                var colorMaster = await _db.colorMasters.ToListAsync();
                model.ArticleDetail.LiningColour = colorMaster.Where(x => x.Id == model.ArticleDetail.FKLiningColour).FirstOrDefault().ColourName;
                model.ArticleDetail.SocksColour = colorMaster.Where(x => x.Id == model.ArticleDetail.FKSocksColour).FirstOrDefault().ColourName;
                model.ArticleDetail.SoleColour = colorMaster.Where(x => x.Id == model.ArticleDetail.FKSoleColour).FirstOrDefault().ColourName;
                model.ArticleDetail.ColorDescription = colorMaster.Where(x => x.Id == model.ArticleDetail.FKColour).FirstOrDefault().ColourName;

                var HSNCode = await _db.HSNCodeMasters.ToListAsync();
                model.ArticleDetail.HSNCode = HSNCode.Where(x => x.Id == model.ArticleDetail.FKHSNCode).FirstOrDefault().HSNCode;

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
                    model.ArticleDetail.Picture = p1;
                }

                int nLengthofArticleNo = model.ArticleDetail.ArticleNo.ToString().Length;
                int nLengthofLeather = model.ArticleDetail.Leather.ToString().Length;
                
                string sStockNo = model.ArticleDetail.Type.ToString() + model.ArticleDetail.ArticleNo.ToString().Substring(nLengthofArticleNo - 5, 5) + 
                    model.ArticleDetail.Leather.ToString().Substring(nLengthofLeather - 2, 2) + model.ArticleDetail.Variant.ToString();

                model.ArticleDetail.StockNo = sStockNo.ToString().ToUpper();

                //Size,Brand,Product,ArticleNo,Leather Type, Variant, Color Description,Group

                var ArticleGroup = await _db.articleGroups.FindAsync(NFKArticleGroup);

                string sItemDescription = ArticleGroup.Brand.ToString() + " " + ArticleGroup.Product.ToString() + " " + ArticleGroup.ArticleNo +
                    " " + model.ArticleDetail.Leather.ToString() + " " + model.ArticleDetail.Variant.ToString() + " " +
                    model.ArticleDetail.ColorDescription + " " + ArticleGroup.ArticleGroupName.ToString();

                if (sItemDescription.ToString().Length < 100)
                    model.ArticleDetail.Description = sItemDescription.ToString().ToUpper();
                else
                    model.ArticleDetail.Description = sItemDescription.ToString().Substring(0, 100).ToUpper();

                model.ArticleDetail.ArtGrp = ArticleGroup.ArticleGroupName;
                string sArticleCode = "";

                string sArtNo = Regex.Replace((ArticleGroup.ArticleNo.ToString()), "\\s+", "");
                sArtNo = Regex.Replace(sArtNo, "-", "");

                if (sArtNo.ToString().Length == 1) sArtNo = sArtNo + "####";
                else if (sArtNo.ToString().Length == 2) sArtNo = sArtNo + "###";
                else if (sArtNo.ToString().Length == 3) sArtNo = sArtNo + "##";
                else if (sArtNo.ToString().Length == 4) sArtNo = sArtNo + "#";
                else if (sArtNo.ToString().Length > 4) sArtNo = sArtNo.Substring(0, 5);

                string sColor = Regex.Replace((model.ArticleDetail.ColorDescription.ToString()), "\\s+", "");
                sColor = Regex.Replace(sColor, "-", "");

                if (sColor.ToString().Length == 1) sColor = sColor + "####";
                else if (sColor.ToString().Length == 2) sColor = sColor + "###";
                else if (sColor.ToString().Length == 3) sColor = sColor + "##";
                else if (sColor.ToString().Length == 4) sColor = sColor + "#";
                else if (sColor.ToString().Length > 4) sColor = sColor.Substring(0, 5);

                //replace(/\s +/ g, '')
                string sArtGrp = Regex.Replace((ArticleGroup.ArticleGroupName.ToString()), "\\s+", "");
                sArtGrp = Regex.Replace(sArtGrp, "-", "");
                string sArt = Regex.Replace((ArticleGroup.ArticleName.ToString()), "\\s+", "");
                sArt = Regex.Replace(sArt, "-", "");
                string sColorCode = colorMaster.Where(x => x.Id == model.ArticleDetail.FKColour).FirstOrDefault().ColourCode;

                if (sArtGrp.ToString().Length == 1) sArtGrp = sArtGrp + "###";
                else if (sArtGrp.ToString().Length == 2) sArtGrp = sArtGrp + "##";
                else if (sArtGrp.ToString().Length == 3) sArtGrp = sArtGrp + "#";
                else if (sArtGrp.ToString().Length > 4) sArtGrp = sArtGrp.Substring(0, 4);

                if (sArt.ToString().Length == 1) sArt = sArt + "#####";
                else if (sArt.ToString().Length == 2) sArt = sArt + "####";
                else if (sArt.ToString().Length == 3) sArt = sArt + "###";
                else if (sArt.ToString().Length == 4) sArt = sArt + "##";
                else if (sArt.ToString().Length == 5) sArt = sArt + "#";
                else if (sArt.ToString().Length > 6) sArt = sArt.Substring(0, 6);

                if (sColorCode.ToString().Length == 1) sColorCode = sColorCode + "###";
                else if (sColorCode.ToString().Length == 2) sColorCode = sColorCode + "##";
                else if (sColorCode.ToString().Length == 3) sColorCode = sColorCode + "#";
                else if (sColorCode.ToString().Length > 4) sColorCode = sColorCode.Substring(0, 4);


                string codechar = (sArtGrp + sArt + sColorCode).ToString().ToUpper();
                var maxcode = 0;

                if (_db.articleDetails.Where(x => x.ArticleCode.Contains(codechar)).Select(x => int.Parse(x.ArticleCode.Substring(16, 2))).ToList().Count > 0)
                {
                    maxcode = _db.articleDetails.Where(x => x.ArticleCode.Contains(codechar)).Select(x => int.Parse(x.ArticleCode.Substring(16, 2))).ToList().Max();
                }

                model.ArticleDetail.ArticleCode = codechar + "-" + String.Format("{0:00}", (maxcode + 1));


                string codechar1 = (sArtNo + sColor).ToString().ToUpper();
                var maxcode1 = 0;

                if (_db.articleDetails.Where(x => x.NewStockNo.Contains(codechar1)).Select(x => int.Parse(x.NewStockNo.Substring(12, 2))).ToList().Count > 0)
                {
                    maxcode1 = _db.articleDetails.Where(x => x.NewStockNo.Contains(codechar1)).Select(x => int.Parse(x.NewStockNo.Substring(12, 2))).ToList().Max();
                }

                model.ArticleDetail.NewStockNo = codechar1 + "-" + String.Format("{0:00}", (maxcode1 + 1));

                _db.articleDetails.Add(model.ArticleDetail);
                await _db.SaveChangesAsync();

                


                //Work on the image saving section

                //string webRootPath = _hostingEnvironment.WebRootPath;
                //var files = HttpContext.Request.Form.Files;

                //var articledetailFromDb = await _db.articleDetails.FindAsync(model.ArticleDetail.Id);

                //if (files.Count > 0)
                //{
                //    //files has been uploaded
                //    var uploads = Path.Combine(webRootPath, "images");
                //    var extension = Path.GetExtension(files[0].FileName);

                //    using (var filesStream = new FileStream(Path.Combine(uploads, model.ArticleDetail.Id + extension), FileMode.Create))
                //    {
                //        files[0].CopyTo(filesStream);
                //    }
                //    articledetailFromDb.ArticleImage = @"\images\" + model.ArticleDetail.Id + extension;
                //}
                //else
                //{
                //    //no file was uploaded, so use default
                //    var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultImage);
                //    System.IO.File.Copy(uploads, webRootPath + @"\images\" + model.ArticleDetail.Id + ".png");
                //    articledetailFromDb.ArticleImage = @"\images\" + model.ArticleDetail.Id + ".png";
                //}

                //await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ArticleDetailCreate));
                //    }
                //}
                //LookUpCategoryAndLookUpMstViewModel modelVM = new LookUpCategoryAndLookUpMstViewModel()
                //{
                //    lookUpCategorieslist = await _db.lookUpCategories.ToListAsync(),
                //    LookUpMasters = new Models.MasterTables.LookUpMaster()
                //    //LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
                //    //StatusMessage = StatusMessage
                //};
                //return View(modelVM);
            }
            return View(ArticleGroupVM);
        }

        //GET - EDIT
        public async Task<IActionResult> ArticleDetailEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articledetail = await _db.articleDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (articledetail == null)
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

            TempData["AGArticleNo"] = SAGArticleNo;
            TempData["AGArticleName"] = SAGArticleName;
            TempData["AGDescription"] = SAGDescription;
            TempData["AGID"] = NFKArticleGroup;

            ArticleDetailVM.ArticleDetail= await _db.articleDetails.SingleOrDefaultAsync(m => m.Id == id);
            ArticleDetailVM.FKLeather = await _db.materials.ToListAsync();
            //ArticleDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKLiningColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKSocksColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKSoleColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            ArticleDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            ArticleDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();
            ArticleDetailVM.FKHSNCode = await _db.HSNCodeMasters.ToListAsync();

            if (ArticleDetailVM.ArticleDetail == null)
            {
                return NotFound();
            }
            return View(ArticleDetailVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArticleDetailEdit(int id, ArticleDetailViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            var articleDetailExist = _db.articleDetails.Include(s => s.Description).Where(s => s.Description == model.ArticleDetail.Description);

            //if (doesLookUpMstExist.Count() > 0 )
            //{
            //    //ERROR.    
            //    StatusMessage = "Error : LookUpMst Exists under " + doesLookUpMstExist.First().LkUpHdr.Description + " LookUpHdr. Please use anothe name ";
            //}
            //else
            //{
            var ArticleDetailfromDb = await _db.articleDetails.FindAsync(id);

            //Work on the image saving section

            //string webRootPath = _hostingEnvironment.WebRootPath;
            //var files = HttpContext.Request.Form.Files;

            //if (files.Count > 0)
            //{
            //    //New Image has been uploaded
            //    var uploads = Path.Combine(webRootPath, "images");
            //    var extension_new = Path.GetExtension(files[0].FileName);

            //    //Delete the original file
            //    if (ArticleDetailfromDb.ArticleImage != null)
            //    {
            //        var imagePath = Path.Combine(webRootPath, ArticleDetailfromDb.ArticleImage.TrimStart('\\'));

            //        if (System.IO.File.Exists(imagePath))
            //        {
            //            System.IO.File.Delete(imagePath);
            //        }
            //    }

            //    //we will upload the new file
            //    using (var filesStream = new FileStream(Path.Combine(uploads, id + extension_new), FileMode.Create))
            //    {
            //        files[0].CopyTo(filesStream);
            //    }
            //    ArticleDetailfromDb.ArticleImage = @"\images\" + id + extension_new;
            //}

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
                ArticleDetailfromDb.Picture = p1;
            }

            ArticleDetailfromDb.FKArticleGroup = model.ArticleDetail.FKArticleGroup;
            ArticleDetailfromDb.StockNo = model.ArticleDetail.StockNo;
            ArticleDetailfromDb.Description = model.ArticleDetail.Description;
            ArticleDetailfromDb.FKLeather = model.ArticleDetail.FKLeather;
            ArticleDetailfromDb.FKColour = model.ArticleDetail.FKColour;
            ArticleDetailfromDb.FKLiningColour = model.ArticleDetail.FKLiningColour;
            ArticleDetailfromDb.FKSocksColour = model.ArticleDetail.FKSocksColour;
            ArticleDetailfromDb.FKSoleColour = model.ArticleDetail.FKSoleColour;
            ArticleDetailfromDb.VersionNo = model.ArticleDetail.VersionNo;
            ArticleDetailfromDb.AdditionalInfo = model.ArticleDetail.AdditionalInfo;
            ArticleDetailfromDb.ModifiedBy = model.ArticleDetail.ModifiedBy;
            ArticleDetailfromDb.ModifiedDate = model.ArticleDetail.ModifiedDate;
            ArticleDetailfromDb.Type = model.ArticleDetail.Type;
            ArticleDetailfromDb.Variant = model.ArticleDetail.Variant;
            ArticleDetailfromDb.FKHSNCode = model.ArticleDetail.FKHSNCode;
            ArticleDetailfromDb.CostPrice = model.ArticleDetail.CostPrice;
            ArticleDetailfromDb.MRP = model.ArticleDetail.MRP;
            ArticleDetailfromDb.DealerPrice = model.ArticleDetail.DealerPrice;
            ArticleDetailfromDb.ProductTax = model.ArticleDetail.ProductTax;

            var colorMaster = await _db.colorMasters.ToListAsync();
            ArticleDetailfromDb.LiningColour = colorMaster.Where(x => x.Id == model.ArticleDetail.FKLiningColour).FirstOrDefault().ColourName;
            ArticleDetailfromDb.SocksColour = colorMaster.Where(x => x.Id == model.ArticleDetail.FKSocksColour).FirstOrDefault().ColourName;
            ArticleDetailfromDb.SoleColour = colorMaster.Where(x => x.Id == model.ArticleDetail.FKSoleColour).FirstOrDefault().ColourName;
            ArticleDetailfromDb.ColorDescription = colorMaster.Where(x => x.Id == model.ArticleDetail.FKColour).FirstOrDefault().ColourName;

            var lookUpMaster = await _db.lookUpMasters.ToListAsync();
            ArticleDetailfromDb.EntryType = lookUpMaster.Where(x => x.Id == model.ArticleDetail.FKEntryType).FirstOrDefault().Description;
            ArticleDetailfromDb.Category = lookUpMaster.Where(x => x.Id == model.ArticleDetail.FKCategory).FirstOrDefault().Description;
            ArticleDetailfromDb.Features = lookUpMaster.Where(x => x.Id == model.ArticleDetail.FKFeatures).FirstOrDefault().Description;
            ArticleDetailfromDb.Leather = _db.materials.Where(x => x.Id == model.ArticleDetail.FKLeather).FirstOrDefault().Description;
            
            int nLengthofArticleNo = model.ArticleDetail.ArticleNo.ToString().Length;
            int nLengthofLeather = ArticleDetailfromDb.Leather.ToString().Length;
            string sStockNo;
            if (ArticleDetailfromDb.Type == null)
            {
                sStockNo = model.ArticleDetail.ArticleNo.ToString().Substring(nLengthofArticleNo - 5, 5) +
                ArticleDetailfromDb.Leather.ToString().Substring(nLengthofLeather - 2, 2) + model.ArticleDetail.Variant.ToString();
            }
            else
            {
                sStockNo = model.ArticleDetail.Type.ToString() + model.ArticleDetail.ArticleNo.ToString().Substring(nLengthofArticleNo - 5, 5) +
                ArticleDetailfromDb.Leather.ToString().Substring(nLengthofLeather - 2, 2) + model.ArticleDetail.Variant.ToString();
            }
            

            ArticleDetailfromDb.StockNo = sStockNo.ToString().ToUpper();

            var HSNCode = await _db.HSNCodeMasters.ToListAsync();
            ArticleDetailfromDb.HSNCode = HSNCode.Where(x => x.Id == model.ArticleDetail.FKHSNCode).FirstOrDefault().HSNCode;

            await _db.SaveChangesAsync();
            return RedirectToAction("ArticleDetailIndex", "ArticleGroup", new { Id = NFKArticleGroup });
            //}
            //}
            //PartyInfoDtlsViewModel modelVM = new PartyInfoDtlsViewModel()
            //{
            //    //lookUpCategorieslist = await _db.lookUpCatergory.ToListAsync(),
            //    //LookUpMasters = model.LookUpMasters,
            //    ////LookUpMstList = await _db.lookupMst.OrderBy(p => p.Description).Select(p => p.Description).ToListAsync(),
            //    //StatusMessage = StatusMessage
            //};
            //return View(modelVM);
        }

        //GET - DETAIL
        public async Task<IActionResult> ArticleDetailDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articledetail = await _db.articleDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (articledetail == null)
            {
                return NotFound();
            }

            TempData["AGArticleNo"] = SAGArticleNo;
            TempData["AGArticleName"] = SAGArticleName;
            TempData["AGDescription"] = SAGDescription;
            TempData["AGID"] = NFKArticleGroup;

            ArticleDetailVM.ArticleDetail = await _db.articleDetails.SingleOrDefaultAsync(m => m.Id == id);
            ArticleDetailVM.FKLeather = await _db.materials.ToListAsync();
            ArticleDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            ArticleDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            ArticleDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();
            if (ArticleDetailVM.ArticleDetail == null)
            {
                return NotFound();
            }
            return View(ArticleDetailVM);
        }

        //GET - DELETE
        public async Task<IActionResult> ArticleDetailDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            
            var articledetail = await _db.articleDetails.SingleOrDefaultAsync(m => m.Id == id);

            if (articledetail == null)
            {
                return NotFound();
            }

            TempData["AGArticleNo"] = SAGArticleNo;
            TempData["AGArticleName"] = SAGArticleName;
            TempData["AGDescription"] = SAGDescription;
            TempData["AGID"] = NFKArticleGroup;

            ArticleDetailVM.ArticleDetail = await _db.articleDetails.SingleOrDefaultAsync(m => m.Id == id);
            ArticleDetailVM.FKLeather = await _db.materials.ToListAsync();
            ArticleDetailVM.FKColour = await _db.colorMasters.ToListAsync();
            ArticleDetailVM.FKCategory = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 53).ToListAsync();
            ArticleDetailVM.FKEntryType = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 52).ToListAsync();
            ArticleDetailVM.FKFeatures = await _db.lookUpMasters.Where(s => s.FKLookUpCategory == 54).ToListAsync();

            if (ArticleDetailVM.ArticleDetail == null)
            {
                return NotFound();
            }
            return View(ArticleDetailVM);
        }

        //POST - Delete
        [HttpPost, ActionName("ArticleDetailDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArticleDetailDeleteConfirmed(int? id)
        {
            var ArticleDetail = await _db.articleDetails.FindAsync(id);

            if (ArticleDetail == null)
            {
                return View();
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            //var ArticleDetailfromDb = await _db.articleDetails.FindAsync(id);
            if (ArticleDetail.ArticleImage != null)
            {
                var imagePath = Path.Combine(webRootPath, ArticleDetail.ArticleImage.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _db.articleDetails.Remove(ArticleDetail);
            await _db.SaveChangesAsync();
            return RedirectToAction("ArticleDetailIndex", "ArticleGroup", new { Id = NFKArticleGroup });
        }


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
            TempData["AGID"] = NFKArticleGroup;
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
            TempData["AGID"] = NFKArticleGroup;
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
    }
}
