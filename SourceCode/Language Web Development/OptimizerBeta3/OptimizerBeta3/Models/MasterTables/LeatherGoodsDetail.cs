using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class LeatherGoodsDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Leather Goods Group", Description = "Leather Goods Group")]
        [ForeignKey("LGGroup")]
        public int FKArticleGroup { get; set; }
        public virtual LeatherGoodsGroup LGGroup { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string StockNo { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [Display(Name = "Leather", Description = "Leather")]
        [ForeignKey("LookUpMasterLeather")]
        public int FKLeather { get; set; }
        public virtual LookUpMaster LookUpMasterLeather { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Leather { get; set; }

        [Display(Name = "Colour", Description = "Colour")]
        [ForeignKey("ColorMaster")]
        public int FKColour { get; set; }
        public virtual ColorMaster ColorMaster { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string ColorDescription { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal VersionNo { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? AdditionalInfo { get; set; }

        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string Variant { get; set; }

        public string? ArticleImage { get; set; }
        public byte[] Picture { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal MRP { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DealerPrice { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ProductTax { get; set; }

        [Display(Name = "Entry Type", Description = "Entry Type")]
        [ForeignKey("LookUpMasterEntryType")]
        public int FKEntryType { get; set; }
        public virtual LookUpMaster LookUpMasterEntryType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? EntryType { get; set; }


        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Category { get; set; }

        [Display(Name = "Features", Description = "Features")]
        [ForeignKey("LookUpMasterFeatures")]
        public int FKFeatures { get; set; }
        public virtual LookUpMaster LookUpMasterFeatures { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Features { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ArticleNo { get; set; }

        [Required]
        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Type { get; set; }

        [Display(Name = "HSN Code", Description = "HSN Code")]
        [ForeignKey("HSNCodeMaster")]
        public int FKHSNCode { get; set; }
        public virtual HSNCodeMaster HSNCodeMaster { get; set; }

        public int HSNCode { get; set; }

        [Display(Name = "Size / Dimension", Description = "Size / Dimension")]
        [ForeignKey("SizeorDimensionId")]
        public int FKSizeorDimension { get; set; }
        public virtual LookUpMaster SizeorDimensionId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string SizeorDimension { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
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
