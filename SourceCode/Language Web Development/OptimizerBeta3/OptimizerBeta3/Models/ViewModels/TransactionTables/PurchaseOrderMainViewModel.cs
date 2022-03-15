using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class PurchaseOrderMainViewModel
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public PurchaseOrderMain PurchaseOrderMain { get; set; }
        public IEnumerable<ArticleGroup> GetArticleGroup { get; set; }
        public IEnumerable<LookUpMaster> FKDestination { get; set; }
        public IEnumerable<LookUpMaster> FKDeliveryTo { get; set; }
        public IEnumerable<LookUpMaster> FKOrderStatus { get; set; }
        public IEnumerable<LookUpMaster> FKModeofTransport { get; set; }
        public IEnumerable<SizeMaster> FKSizeMaster { get; set; }

        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
