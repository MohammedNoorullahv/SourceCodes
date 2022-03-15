using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class LGPurchaseOrderMainViewModel
    {
        public LGPurchaseOrder LGPurchaseOrder { get; set; }
        public LGPurchaseOrderMain LGPurchaseOrderMain { get; set; }
        public IEnumerable<LeatherGoodsGroup> GetLeatherGoodsGroup { get; set; }
        public IEnumerable<LookUpMaster> FKDestination { get; set; }
        public IEnumerable<LookUpMaster> FKDeliveryTo { get; set; }
        public IEnumerable<LookUpMaster> FKOrderStatus { get; set; }
        public IEnumerable<LookUpMaster> FKModeofTransport { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
