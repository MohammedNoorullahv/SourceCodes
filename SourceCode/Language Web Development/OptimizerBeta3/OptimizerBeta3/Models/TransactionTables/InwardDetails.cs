using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.TransactionTables
{
    public class InwardDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Inward", Description = "Inward")]
        [ForeignKey("Inward")]
        public int FKInwardNo { get; set; }
        public virtual Inward Inward { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string InwardNo { get; set; }
        public DateTime InwardDt { get; set; }
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
        
        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }
        public int FKSupplier { get; set; }
       
        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string FLAM { get; set; }
        public Boolean IsEntryCompleted { get; set; }

        public int FKPurchaseOrder { get; set; }
        public int FKPurchaseOrderMain { get; set; }
        public int FKPurchaseOrderDetail { get; set; }

        [Display(Name = "Quality", Description = "Quality")]
        [ForeignKey("LookUpMasterQuality")]
        public int FKQuality { get; set; }
        public virtual LookUpMaster LookUpMasterQuality { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Quality { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        [Column(TypeName = "varchar(30)")]
        public string? EnteredSystemId { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
