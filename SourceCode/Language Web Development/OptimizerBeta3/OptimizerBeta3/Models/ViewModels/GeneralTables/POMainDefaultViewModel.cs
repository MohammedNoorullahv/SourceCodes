using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.GeneralTables
{
    public class POMainDefaultViewModel
    {
        public POMainDefault POMainDefault { get; set; }
        public IEnumerable<LookUpMaster> FKDestination { get; set; }
        public IEnumerable<LookUpMaster> FKDeliveryTo { get; set; }
        public IEnumerable<LookUpMaster> FKOrderStatus { get; set; }
        public IEnumerable<LookUpMaster> FKModeofTransport { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
