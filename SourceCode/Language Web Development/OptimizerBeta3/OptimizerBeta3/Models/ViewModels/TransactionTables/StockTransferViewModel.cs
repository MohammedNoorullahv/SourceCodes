using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class StockTransferViewModel
    {
        public StockTransfer StockTransfer { get; set; }
        public IEnumerable<Season> FKSeason { get; set; }
        public IEnumerable<LookUpMaster> FKOutwardType { get; set; }
        public IEnumerable<UnitMaster> FKFromUnit { get; set; }
        //public IEnumerable<LookUpMaster> FKFromDepartment { get; set; }
        public IEnumerable<Locations> FKFromLocation { get; set; }
        public IEnumerable<UnitMaster> FKToUnit { get; set; }
        //public IEnumerable<LookUpMaster> FKToDepartment { get; set; }
        public IEnumerable<Locations> FKToLocation { get; set; }
        public IEnumerable<LookUpMaster> FKQuality { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
