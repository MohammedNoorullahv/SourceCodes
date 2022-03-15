using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class OfferDetailsViewModel
    {
        public Offers Offers { get; set; }
        public OfferDetails offerDetails { get; set; }
        public IEnumerable<LookUpMaster> FKAnniverseryInfo { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
