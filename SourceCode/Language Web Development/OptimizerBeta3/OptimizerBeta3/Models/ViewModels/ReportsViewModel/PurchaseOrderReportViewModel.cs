using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.ReportsViewModel
{
    public class PurchaseOrderReportViewModel
    {
        #region PURHCASE ORDER
        public string POPurchaseOrderNo { get; set; }
        public DateTime? POPurchaseOrderDt { get; set; }
        public string POSeason { get; set; }
        public string POSource { get; set; }
        public string POUnitName { get; set; }
        public string POSupplierName { get; set; }
        public string POOrderStatus { get; set; }
        public string POPaymentTerms { get; set; }
        #endregion

        #region PURCHASE ORDER MAIN
        public string POMArticleGroup { get; set; }
        public string POMArticle { get; set; }
        #endregion

        #region PURCHASE ORDER DETAIL
        public string PODArticleDescription { get; set; }
        public string PODArticleColor { get; set; }
        public string PODOrderReferenceNo { get; set; }
        public int PODTotalOrderQuantity { get; set; }

        public decimal? PODSize01 { get; set; }
        public int? PODQuantity01 { get; set; }
        public decimal? PODSize02 { get; set; }
        public int? PODQuantity02 { get; set; }
        public decimal? PODSize03 { get; set; }
        public int? PODQuantity03 { get; set; }
        public decimal? PODSize04 { get; set; }
        public int? PODQuantity04 { get; set; }
        public decimal? PODSize05 { get; set; }
        public int? PODQuantity05 { get; set; }
        public decimal? PODSize06 { get; set; }
        public int? PODQuantity06 { get; set; }
        public decimal? PODSize07 { get; set; }
        public int? PODQuantity07 { get; set; }
        public decimal? PODSize08 { get; set; }
        public int? PODQuantity08 { get; set; }
        public decimal? PODSize09 { get; set; }
        public int? PODQuantity09 { get; set; }
        public decimal? PODSize10 { get; set; }
        public int? PODQuantity10 { get; set; }
        public decimal? PODSize11 { get; set; }
        public int? PODQuantity11 { get; set; }
        public decimal? PODSize12 { get; set; }
        public int? PODQuantity12 { get; set; }
        public decimal? PODSize13 { get; set; }
        public int? PODQuantity13 { get; set; }
        public decimal? PODSize14 { get; set; }
        public int? PODQuantity14 { get; set; }
        public decimal? PODSize15 { get; set; }
        public int? PODQuantity15 { get; set; }
        public decimal? PODSize16 { get; set; }
        public int? PODQuantity16 { get; set; }
        public decimal? PODSize17 { get; set; }
        public int? PODQuantity17 { get; set; }
        public decimal? PODSize18 { get; set; }
        public int? PODQuantity18 { get; set; }
        public string PODPurchaseOrderDtlNo { get; set; }

        #endregion

        #region ARTICLE DETAIL
        public string ARTDescription { get; set; }
        #endregion

        #region COLOUR MASTER
        public string CMLiningColourName { get; set; }
        public string CMSocksColourName { get; set; }
        public string CMSoleColourName { get; set; }
        public string CMShoeColourName { get; set; }
        #endregion
    }
}
