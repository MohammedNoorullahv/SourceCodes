using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class DeliveryChallanDetailViewModel
    {
        public DeliveryChallanDetail DeliveryChallanDetail { get; set; }
        public DeliveryChallan DeliveryChallan { get; set; }
        public IEnumerable<LookUpMaster> FKUOM { get; set; }
        public IEnumerable<HSNCodeMaster> FKHSNCode { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
