using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class StockWithArticleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StockWithArticleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(string? EANCode)
        {
            int nLen = 0;
            if (EANCode != null)
            {
                nLen = EANCode.Length;
            }
            
            if (nLen == 13)
            {
                return View(await _db.stockWithArticles.OrderBy(x => x.Id).Where(x => x.EANCode == EANCode).ToListAsync());
            }
            else if (nLen == 16)
            {
                var po = await _db.purchaseOrders.Where(x => x.PurchaseOrderNo == EANCode).FirstOrDefaultAsync();

                //#region EXPORT TO EXCEL
                //Excel.Application application = new Excel.Application();
                //Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                //var export = await _db.stockWithArticles.OrderBy(x => x.FKOrderId).ThenBy(x => x.FKOrderMainId).ThenBy(x => x.FKOrderDetailId).ThenBy(X => X.Size).Where(x => x.FKOrderId == po.Id).ToListAsync();

                ////worksheet.Cells[1, 1] = "Name";
                ////worksheet.Cells[1, 2] = "Age";
                ////worksheet.Cells[1, 3] = "Position";
                ////worksheet.Cells[1, 4] = "Address";
                ////worksheet.Cells[1, 5] = "Contact";
                //worksheet.Cells[1, 1] = "Id";
                //worksheet.Cells[1, 2] = "StockNo";
                //worksheet.Cells[1, 3] = "Brand";
                //worksheet.Cells[1, 4] = "Product";
                //worksheet.Cells[1, 5] = "ArticleNo";
                //worksheet.Cells[1, 6] = "ArticleDescription";
                //worksheet.Cells[1, 7] = "Variant";
                //worksheet.Cells[1, 8] = "ColorDescription";
                //worksheet.Cells[1, 9] = "Size";
                //worksheet.Cells[1, 10] = "ItemDescription";
                //worksheet.Cells[1, 11] = "ArticleName";
                //worksheet.Cells[1, 12] = "LeatherType";
                //worksheet.Cells[1, 13] = "Group";
                //worksheet.Cells[1, 14] = "Dept";
                //worksheet.Cells[1, 15] = "EANCode";
                //worksheet.Cells[1, 16] = "Type";
                //worksheet.Cells[1, 17] = "Category";
                //worksheet.Cells[1, 18] = "Vendor";
                //worksheet.Cells[1, 19] = "DOM";
                //worksheet.Cells[1, 20] = "SICM";
                //worksheet.Cells[1, 21] = "MRP";
                //worksheet.Cells[1, 22] = "DealerPrice";
                //worksheet.Cells[1, 23] = "CostPrice";
                //worksheet.Cells[1, 24] = "ProductTax";
                //worksheet.Cells[1, 25] = "Quantity";
                //worksheet.Cells[1, 26] = "FKArticleDetailId";
                //worksheet.Cells[1, 27] = "FKOrderDetailId";
                //worksheet.Cells[1, 28] = "ArrivedQty";
                //worksheet.Cells[1, 29] = "BalQty";
                //worksheet.Cells[1, 30] = "SoldQty";
                //worksheet.Cells[1, 31] = "LastTranDate";
                //worksheet.Cells[1, 32] = "StockInitiatedDate";
                //worksheet.Cells[1, 33] = "FKOffer";
                //worksheet.Cells[1, 34] = "OfferType";
                //worksheet.Cells[1, 35] = "FKCategory";
                //worksheet.Cells[1, 36] = "FKOrderId";
                //worksheet.Cells[1, 37] = "FKOrderMainId";
                //worksheet.Cells[1, 38] = "FLAM";
                //worksheet.Cells[1, 39] = "SizeinString";
                //worksheet.Cells[1, 40] = "FKSupplier";

                //int row = 2;
                //foreach (var e in export)
                //{
                //    //worksheet.Cells[row, 1] = e.Name;
                //    //worksheet.Cells[row, 2] = e.Age;
                //    //worksheet.Cells[row, 3] = e.Position;
                //    //worksheet.Cells[row, 4] = e.Address;
                //    //worksheet.Cells[row, 5] = e.Contact;
                //    worksheet.Cells[row, 1] = e.Id;
                //    worksheet.Cells[row, 2] = e.StockNo;
                //    worksheet.Cells[row, 3] = e.Brand;
                //    worksheet.Cells[row, 4] = e.Product;
                //    worksheet.Cells[row, 5] = e.ArticleNo;
                //    worksheet.Cells[row, 6] = e.ArticleDescription;
                //    worksheet.Cells[row, 7] = e.Variant;
                //    worksheet.Cells[row, 8] = e.ColorDescription;
                //    worksheet.Cells[row, 9] = e.Size;
                //    worksheet.Cells[row, 10] = e.ItemDescription;
                //    worksheet.Cells[row, 11] = e.ArticleName;
                //    worksheet.Cells[row, 12] = e.LeatherType;
                //    worksheet.Cells[row, 13] = e.Group;
                //    worksheet.Cells[row, 14] = e.Dept;
                //    worksheet.Cells[row, 15] = e.EANCode;
                //    worksheet.Cells[row, 16] = e.Type;
                //    worksheet.Cells[row, 17] = e.Category;
                //    worksheet.Cells[row, 18] = e.Vendor;
                //    worksheet.Cells[row, 19] = e.DOM;
                //    worksheet.Cells[row, 20] = e.SICM;
                //    worksheet.Cells[row, 21] = e.MRP;
                //    worksheet.Cells[row, 22] = e.DealerPrice;
                //    worksheet.Cells[row, 23] = e.CostPrice;
                //    worksheet.Cells[row, 24] = e.ProductTax;
                //    worksheet.Cells[row, 25] = e.Quantity;
                //    worksheet.Cells[row, 26] = e.FKArticleDetailId;
                //    worksheet.Cells[row, 27] = e.FKOrderDetailId;
                //    worksheet.Cells[row, 28] = e.ArrivedQty;
                //    worksheet.Cells[row, 29] = e.BalQty;
                //    worksheet.Cells[row, 30] = e.SoldQty;
                //    worksheet.Cells[row, 31] = e.LastTranDate;
                //    worksheet.Cells[row, 32] = e.StockInitiatedDate;
                //    worksheet.Cells[row, 33] = e.FKOffer;
                //    worksheet.Cells[row, 34] = e.OfferType;
                //    worksheet.Cells[row, 35] = e.FKCategory;
                //    worksheet.Cells[row, 36] = e.FKOrderId;
                //    worksheet.Cells[row, 37] = e.FKOrderMainId;
                //    worksheet.Cells[row, 38] = e.FLAM;
                //    worksheet.Cells[row, 39] = e.SizeinString;
                //    worksheet.Cells[row, 40] = e.FKSupplier;

                //    row++;
                //}

                //workbook.SaveAs(@"C:\Excel\sample.xls");
                //workbook.Close();
                //Marshal.ReleaseComObject(workbook);

                //application.Quit();
                //Marshal.FinalReleaseComObject(application);
                //#endregion
                return View(await _db.stockWithArticles.OrderBy(x => x.FKOrderId).ThenBy(x => x.FKOrderMainId).ThenBy(x => x.FKOrderDetailId).ThenBy(X => X.Size).Where(x => x.FKOrderId == po.Id).ToListAsync());
            }
            else
            {
                return View(await _db.stockWithArticles.OrderBy(x => x.FKOrderDetailId).ThenBy(x => x.Size).Where(x => x.ArticleName == EANCode).ToListAsync());
            }
            
        }
    }
}
