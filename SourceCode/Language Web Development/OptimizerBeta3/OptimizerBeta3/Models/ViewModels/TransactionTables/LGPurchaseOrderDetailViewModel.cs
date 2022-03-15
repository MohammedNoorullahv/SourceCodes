using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class LGPurchaseOrderDetailViewModel
    {
        public LGPurchaseOrderDetails LGPurchaseOrderDetail { get; set; }
        public LGPurchaseOrder LGPurchaseOrder { get; set; }

        public IEnumerable<LeatherGoodsDetail> GetLeatherGoodsDetail { get; set; }
        public IEnumerable<LookUpMaster> FKOrderStatus { get; set; }
        public IEnumerable<LookUpMaster> FKUom { get; set; }
        public IEnumerable<SizeMasterforLeatherGoods> FKLGSize { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
