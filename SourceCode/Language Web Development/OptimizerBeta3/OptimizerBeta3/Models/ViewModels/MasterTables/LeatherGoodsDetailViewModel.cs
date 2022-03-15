using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class LeatherGoodsDetailViewModel
    {
        public LeatherGoodsDetail LeatherGoodsDetail { get; set; }
        public LeatherGoodsGroup LeatherGoodsGroup { get; set; }
        public IEnumerable<Materials> FKLeather { get; set; }
        public IEnumerable<ColorMaster> FKColour { get; set; }
        public IEnumerable<LookUpMaster> FKCategory { get; set; }
        public IEnumerable<LookUpMaster> FKEntryType { get; set; }
        public IEnumerable<LookUpMaster> FKFeatures { get; set; }
        public IEnumerable<HSNCodeMaster> FKHSNCode { get; set; }
        public IEnumerable<LookUpMaster> FKSizeorDimension { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
