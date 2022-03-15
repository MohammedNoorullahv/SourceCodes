using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class ArticleGroupViewModel
    {
        public ArticleGroup ArticleGroup { get; set; }
        public IEnumerable<Season> FKSeason { get; set; }
        public IEnumerable<LookUpMaster> FKBrand { get; set; }
        public IEnumerable<LookUpMaster> FKGroup { get; set; }
        public IEnumerable<LookUpMaster> FKSizeFor { get; set; }
        public IEnumerable<LookUpMaster> FKCategory { get; set; }
        public IEnumerable<LookUpMaster> FKProduct { get; set; }
        public IEnumerable<LookUpMaster> FKAssortmentGroup { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
