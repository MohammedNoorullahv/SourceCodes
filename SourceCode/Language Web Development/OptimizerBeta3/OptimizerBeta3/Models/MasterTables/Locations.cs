using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class Locations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Unit", Description = "Unit")]
        [ForeignKey("UnitMaster")]
        public int FKUnitDtls { get; set; }
        public virtual UnitMaster UnitMaster { get; set; }

        [Display(Name = "Location Type", Description = "Location Type")]
        [ForeignKey("LookUpMasterLocationType")]
        public int FKLocationType { get; set; }
        public virtual LookUpMaster LookUpMasterLocationType { get; set; }

        [Required]
        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        //[Remote("IsDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string LocationName { get; set; }

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
