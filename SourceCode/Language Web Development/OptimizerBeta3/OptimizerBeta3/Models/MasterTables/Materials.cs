using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class Materials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }

        [Display(Name = "Type", Description = "Type")]
        [ForeignKey("LookUpMasterType")]
        public int FKType { get; set; }
        public virtual LookUpMaster LookUpMasterType { get; set; }

        [Display(Name = "Sub Type", Description = "Sub Type")]
        [ForeignKey("LookUpMasterSubType")]
        public int FKSubType { get; set; }
        public virtual LookUpMaster LookUpMasterSubType { get; set; }

        [Display(Name = "Brand", Description = "Brand")]
        [ForeignKey("LookUpMasterBrand")]
        public int FKBrand { get; set; }
        public virtual LookUpMaster LookUpMasterBrand { get; set; }

        [Display(Name = "Source", Description = "Source")]
        [ForeignKey("LookUpMasterSource")]
        public int FKSource { get; set; }
        public virtual LookUpMaster LookUpMasterSource { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? ShortDescription { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string PrintDescription { get; set; }

        [Display(Name = "UOM", Description = "UOM")]
        [ForeignKey("LookUpMasterUOM")]
        public int FKUom { get; set; }
        public virtual LookUpMaster LookUpMasterUOM { get; set; }

        [Display(Name = "Colour", Description = "Colour")]
        [ForeignKey("LookUpMasterColour")]
        public int FKColour { get; set; }
        public virtual LookUpMaster LookUpMasterColour { get; set; }

        public bool IsExpirable { get; set; }

        [Display(Name = "HSN Code", Description = "HSN Code")]
        [ForeignKey("HSNCodeMaster")]
        public int FKHSNCode { get; set; }
        public virtual HSNCodeMaster HSNCodeMaster { get; set; }

        public int HSNCode { get; set; }

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
