using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class MaterialPurchaseOrderDetailViewModel
    {
        public MaterialPurchaseOrderDetails MaterialPurchaseOrderDetail { get; set; }
        public MaterialPurchaseOrder MaterialPurchaseOrder { get; set; }
        public IEnumerable<Materials> MaterialId { get; set; }
        public IEnumerable<LookUpMaster> FKOrderStatus { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
