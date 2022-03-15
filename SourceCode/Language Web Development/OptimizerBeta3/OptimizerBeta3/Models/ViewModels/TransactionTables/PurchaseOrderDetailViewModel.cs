using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class PurchaseOrderDetailViewModel
    {
        public PurchaseOrderDetails PurchaseOrderDetail { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        
        public IEnumerable<ArticleDetail> GetArticle { get; set; }
        public IEnumerable<LookUpMaster> FKOrderStatus { get; set; }
        public IEnumerable<LookUpMaster> FKUOM { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
