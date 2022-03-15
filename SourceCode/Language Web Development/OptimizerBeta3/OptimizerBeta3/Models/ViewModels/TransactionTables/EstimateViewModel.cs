using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class EstimateViewModel
    {
        public Estimate Estimate { get; set; }
        public IEnumerable<UnitMaster> FKStore{ get; set; }
        public IEnumerable<Locations> FKLocation { get; set; }
        public IEnumerable<Offers> FKOffer { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
