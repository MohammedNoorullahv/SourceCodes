using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerAddOn.Structures
{
    public class StrTransferData
    {
        public int Id { get; set; }
        public string StockId { get; set; }
        public decimal ExistingStock { get; set; }
        public decimal IssueQuantity { get; set; }
        public decimal RevisedStock { get; set; }
        public decimal RevisedAssignStock { get; set; }
        public string JobcardNo { get; set; }
        public string SalesOrderNo { get; set; }
        public string FromSalesOrderNo { get; set; }
        public string FromJobcardNo { get; set; }

        public string MaterialCode { get; set; }
        public string ComponentGroup { get; set; }
        public string PlaceofUse { get; set; }
        public string MaterialSize { get; set; }
        public string ToLocation { get; set; }
    }
}
