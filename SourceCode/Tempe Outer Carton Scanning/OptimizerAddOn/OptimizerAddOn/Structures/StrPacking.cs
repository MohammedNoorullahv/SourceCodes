using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerAddOn.Structures
{
    public class StrPacking
    {
        public string ID { get; set; }
        public string JobCardNo { get; set; }
        public string Shipper { get; set; }
        public DateTime PackingDate { get; set; }
        public string BuyerGroupCode { get; set; }
        public string BuyerCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetWt { get; set; }
        public decimal GrossWt { get; set; }
        public decimal FromCarton { get; set; }
        public decimal ToCarton { get; set; }
        public decimal PerBoxPackingQty { get; set; }
        public string CartonMark { get; set; }
        public string CartonMark2 { get; set; }
        public string CartonMark3 { get; set; }
        public string CartonMark4 { get; set; }
        public string CartonMark5 { get; set; }
        public string CartonMark6 { get; set; }
        public string CartonMark7 { get; set; }
        public string CartonMark8 { get; set; }
        public string CartonMark9 { get; set; }
        public decimal TotalCarton { get; set; }
        public string InvoiceNo { get; set; }
        public string EnteredOnMachineID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ExeVersionNo { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string ModuleName { get; set; }
        public string SalesOrderNo { get; set; }
        public string JobCardDetailID { get; set; }
        public string ArticleName { get; set; }
        public string ArticleColor { get; set; }
        public string Status { get; set; }
        public string PackingListNo { get; set; }
        public int FinalPerBoxQty { get; set; }
        public string OrderNo { get; set; }
        public string TypeOfPacking { get; set; }
        public string CustWorkorderNo { get; set; }
        public string SalesOrderDetailID { get; set; }
        public string ModeOfPacking { get; set; }

    }
}
