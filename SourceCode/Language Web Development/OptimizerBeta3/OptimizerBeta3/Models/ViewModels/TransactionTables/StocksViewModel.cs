using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class StocksViewModel
    {
        public Stock Stock { get; set; }
        //public IEnumerable<Season> FKSeason { get; set; }
        //public IEnumerable<LookUpMaster> FKSource { get; set; }
        //public IEnumerable<UnitMaster> FKUnit { get; set; }
        //public IEnumerable<PartyInfo> FKParty { get; set; }
        //public IEnumerable<LookUpMaster> FKDepartment { get; set; }
        //public IEnumerable<LookUpMaster> FKPOType { get; set; }
        //public IEnumerable<LookUpMaster> FKDocumentType { get; set; }
        //public IEnumerable<LookUpMaster> FKCurrency { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
