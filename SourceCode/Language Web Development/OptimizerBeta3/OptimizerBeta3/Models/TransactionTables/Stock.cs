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
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string MaterialorFinishedProduct { get; set; }

        [Display(Name = "Material", Description = "Material")]
        public int FKMaterial { get; set; }
        

        [Display(Name = "Article Detail", Description = "Article Detail")]
        public int FKArticleDetail { get; set; }
        

        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Colour { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string Size { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string OrderReferenceNo { get; set; }
        public DateTime StockInitiatedDate { get; set; }
        public DateTime LastTranDate { get; set; }

        [Display(Name = "Unit", Description = "Unit")]
        [ForeignKey("Unit")]
        public int FKUnit { get; set; }
        public virtual UnitMaster Unit { get; set; }

        [Display(Name = "Location", Description = "Location")]
        [ForeignKey("LookUpMasterLocation")]
        public int FKLocation { get; set; }
        public virtual LookUpMaster LookUpMasterLocation { get; set; }

        [Display(Name = "Stage", Description = "Stage")]
        [ForeignKey("LookUpMasterStage")]
        public int FKStage { get; set; }
        public virtual LookUpMaster LookUpMasterStage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Rate { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal MRP { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentageforSales { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Display(Name = "Currency", Description = "Currency")]
        [ForeignKey("LookUpMasterCurrency")]
        public int FKCurrency { get; set; }
        public virtual LookUpMaster LookUpMasterCurrency { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ExchangeRate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ValueInINR { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal LandedCost { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal LandedRate { get; set; }

        [Display(Name = "UOM", Description = "UOM")]
        [ForeignKey("LookUpMasterUOM")]
        public int FKUOM { get; set; }
        public virtual LookUpMaster LookUpMasterUOM { get; set; }

        [Display(Name = "IIUOM", Description = "IIUOM")]
        //[ForeignKey("LookUpMasterIIUOM")]
        public int FKIIUOM { get; set; }
        //public virtual LookUpMaster LookUpMasterIIUOM { get; set; }

        [Display(Name = "Source", Description = "Source")]
        [ForeignKey("LookUpMasterSource")]
        public int FKSource { get; set; }
        public virtual LookUpMaster LookUpMasterSource { get; set; }

        [Display(Name = "Quality", Description = "Quality")]
        [ForeignKey("LookUpMasterQuality")]
        public int FKQuality { get; set; }
        public virtual LookUpMaster LookUpMasterQuality { get; set; }

        [Display(Name = "Status", Description = "Status")]
        [ForeignKey("LookUpMasterStatus")]
        public int FKStatus { get; set; }
        public virtual LookUpMaster LookUpMasterStatus { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }

        [Required]
        [StringLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string EANCode { get; set; }

        public int FKSupplier { get; set; }
        public int FKHSNCode { get; set; }
        public int FKOffer { get; set; }

        [Column(TypeName = "varchar(1)")]
        public string? OfferType { get; set; }

        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string FLAM { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }
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
