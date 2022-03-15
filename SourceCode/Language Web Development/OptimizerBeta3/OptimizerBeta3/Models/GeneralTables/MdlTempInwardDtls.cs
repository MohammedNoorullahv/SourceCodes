using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class MdlTempInwardDtls
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string IPAddress { get; set; }
        public int FKInwardNo { get; set; }
        
        [Column(TypeName = "varchar(20)")]
        public string InwardNo { get; set; }
        public DateTime InwardDt { get; set; }
        public int FKMaterial { get; set; }

        public int FKArticle { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Colour { get; set; }

        [Column(TypeName = "Decimal(18,1)")]
        public decimal Size { get; set; }
        public int HSNCode { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IIQuantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ValueinINR { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GrossValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]

        public decimal GSTTotalValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OthersValuePlus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemNettValue { get; set; }
        
        [StringLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string EANCode { get; set; }

        public Boolean ReadyforImport { get; set; }
        
        [Column(TypeName = "Varchar(20)")]
        public string Reason { get; set; }

        [Column(TypeName = "Varchar(50)")]
        public string OrderReferenceNo { get; set; }
        public int FKUnit { get; set; }
        public int FKLocation { get; set; }
        public int FKUOM { get; set; }
        public int FKCurrency { get; set; }
        public int FKSource { get; set; }
        public int FKQuality { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }
        public int FKSupplier { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string MaterialorFinishedProduct { get; set; }

        public int FKHSNCode { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentageforSales { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal MRP { get; set; }
        public DateTime LastTranDate { get; set; }
        public int FKOffer { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? OfferType { get; set; }
        public int FKCategory { get; set; }
        public int FKPurchaseOrder { get; set; }
        public int FKPurchaseOrderMain { get; set; }
        public int FKPurchaseOrderDtl { get; set; }

    }
}
