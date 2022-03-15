using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class ArticleGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Season", Description = "Season")]
        [ForeignKey("FKSeasonCode")]
        public int FKSeason { get; set; }
        public virtual Season FKSeasonCode { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Season { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        //[Remote("IsDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string Description { get; set; }

        [Display(Name = "Brand", Description = "Brand")]
        [ForeignKey("LookUpMasterBrand")]
        public int FKBrand { get; set; }
        public virtual LookUpMaster LookUpMasterBrand { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Brand { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Product { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ArticleNo { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ArticleName { get; set; }

        [Display(Name = "Group", Description = "Group")]
        [ForeignKey("LookUpMasterGroup")]
        public int FKGroup { get; set; }
        public virtual LookUpMaster LookUpMasterGroup { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ArticleGroupName { get; set; }

        [Display(Name = "SizeFor", Description = "SizeFor")]
        [ForeignKey("LookUpMasterSizeFor")]
        public int FKSizeFor { get; set; }
        public virtual LookUpMaster LookUpMasterSizeFor { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? SizeFor { get; set; }
        public int VersionNo { get; set; }
        
        public Boolean IsSeasonSpecific { get; set; } = true;

        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }

        [Display(Name = "Product", Description = "Product")]
        [ForeignKey("LookUpMasterProduct")]
        public int FKProduct { get; set; }
        public virtual LookUpMaster LookUpMasterProduct { get; set; }

        [Display(Name = "Assortment Group", Description = "Assortment Group")]
        [ForeignKey("LookUpAssortmentGroup")]
        public int FKAssortmentGroup { get; set; }
        public virtual LookUpMaster LookUpAssortmentGroup { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? AssortmentGroup { get; set; }

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
