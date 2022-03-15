using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class InvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<LookUpMaster> FKTypeOfInvoice { get; set; }
        public IEnumerable<Season> FKSeason { get; set; }
        public IEnumerable<UnitMaster> FKUnit { get; set; }
        public IEnumerable<UnitMaster> FKToUnit { get; set; }
        public IEnumerable<PartyInfo> FKParty { get; set; }
        public IEnumerable<LookUpMaster> FKDepartment { get; set; }
        public IEnumerable<PartyInfoDtls> FKBillTo { get; set; }
        public IEnumerable<PartyInfoDtls> FKNotifyTo { get; set; }
        public IEnumerable<PartyInfoDtls> FKDeliveryTo { get; set; }
        public IEnumerable<LookUpMaster> FKPaymentTerms { get; set; }
        public IEnumerable<LookUpMaster> FKCurrency { get; set; }
        public IEnumerable<LookUpMaster> FKModeofTransport { get; set; }
        public IEnumerable<Locations> FKLocation { get; set; }
        public IEnumerable<LookUpMaster> FKDestination { get; set; }
        public IEnumerable<LookUpMaster> FKCategory { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
