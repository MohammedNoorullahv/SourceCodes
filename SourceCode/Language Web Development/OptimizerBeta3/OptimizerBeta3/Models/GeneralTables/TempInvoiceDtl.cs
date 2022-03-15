using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempInvoiceDtl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int FKInvoiceNo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDt { get; set; }
        public int FKMaterial { get; set; }

        public int FKArticle { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Colour { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string Size { get; set; }
        public int HSNCode { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        public int FKUOM { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IIQuantity { get; set; }
        public int FKIIUom { get; set; }

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
        public decimal OthersValueMinus { get; set; }

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
        public int FKCurrency { get; set; }
        public int FKSource { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }
        public int FKCustomer { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string MaterialorFinishedProduct { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string IPAddress { get; set; }
        public int FKHSNCode { get; set; }
        
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string UOM { get; set; }

    }
}
