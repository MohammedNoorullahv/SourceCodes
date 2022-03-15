using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class ColorMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Colour", Description = "Colour")]
        [ForeignKey("LookUpMasterColour")]
        public int FKColour { get; set; }
        public virtual LookUpMaster LookUpMasterColour { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        //[Remote("IsLMDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string ColourName { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string ColourCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string CustomerColourName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Combination { get; set; }

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
