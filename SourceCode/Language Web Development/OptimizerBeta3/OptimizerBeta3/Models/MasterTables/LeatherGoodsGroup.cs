using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class LeatherGoodsGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
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
        public string? LGGroupName { get; set; }

        public int VersionNo { get; set; }

        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }

        [Display(Name = "Product", Description = "Product")]
        [ForeignKey("LookUpMasterProduct")]
        public int FKProduct { get; set; }
        public virtual LookUpMaster LookUpMasterProduct { get; set; }

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
