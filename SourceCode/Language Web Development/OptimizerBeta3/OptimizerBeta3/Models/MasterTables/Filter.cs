using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class Filter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string ActionMethod { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string TableName { get; set; }

        [Display(Name = "Look Up Category", Description = "Look Up Category")]
        [ForeignKey("FKLookUPCategoryId")]
        public int FKLookUpCategory { get; set; }
        public virtual LookUpCategory FKLookUPCategoryId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string LookUPCategory { get; set; }

        [Display(Name = "Look Up Master", Description = "Look Up Master")]
        [ForeignKey("FKLookUPMasterId")]
        public int FKLookUpMaster { get; set; }
        public virtual LookUpMaster FKLookUPMasterId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string LookUPMaster { get; set; }

        public bool ConditionIn { get; set; }
        public bool ConditionNotIn { get; set; }
        
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
