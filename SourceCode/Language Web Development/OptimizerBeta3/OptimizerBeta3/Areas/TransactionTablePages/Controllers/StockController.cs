using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Data;
using OptimizerBeta3.Models.GeneralTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OptimizerBeta3.Areas.TransactionTablePages.Controllers
{
    [Area("TransactionTablePages")]
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _db;

        public static string sIpAddress;
        public static string ipaddress = string.Empty;

        public StockController(ApplicationDbContext db)
        {
            _db = db;
        }
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

            var delTmpStock = _db.TempStockViews.Where(x => x.IPAddress == ipaddress);
            _db.TempStockViews.RemoveRange(delTmpStock);
            await _db.SaveChangesAsync();

            List<TempStockView> stock = new List<TempStockView>();
            DbDataReader result;

            string sqlQuery = $"EXEC SLI_Stocks @mAction='STOCK'";
            var cmd = _db.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            _db.Database.OpenConnection();

            result = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (result.Read())
            {
                var swa = new TempStockView
                {
                    Id = 0,
                    FLAM = result.GetString(0),
                    FKUnit = result.GetInt32(1),
                    UnitName = result.GetString(2),
                    FKLocation = result.GetInt32(3),
                    LocationName = result.GetString(4),
                    FKStage = result.GetInt32(5),
                    Stage = result.GetString(6),
                    FKUOM = result.GetInt32(7),
                    UOM = result.GetString(8),
                    FKSource = result.GetInt32(9),
                    Source = result.GetString(10),
                    FKQuality = result.GetInt32(11),
                    Quality = result.GetString(12),
                    FKStatus = result.GetInt32(13),
                    Status = result.GetString(14),
                    StockNo = result.GetString(15),
                    EANCode = result.GetString(16),
                    FKMaterial = result.GetInt32(17),
                    FKArticleDetail = result.GetInt32(18),
                    Description = result.GetString(19),
                    Colour = result.GetString(20),
                    Size = result.GetString(21),
                    OrderReferenceNo = result.GetString(22),
                    Quantity = result.GetDecimal(23),
                    Rate = result.GetDecimal(24),
                    //Value = result.GetDecimal(25),
                    IPAddress = ipaddress
                };
                stock.Add(swa);
            }
            result.Close();

            _db.TempStockViews.AddRange(stock);
            await _db.SaveChangesAsync();

            return View(await _db.TempStockViews.ToListAsync());
        }
    }
}
