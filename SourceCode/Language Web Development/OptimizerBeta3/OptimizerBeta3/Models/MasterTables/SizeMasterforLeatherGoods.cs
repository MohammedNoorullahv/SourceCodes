using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class SizeMasterforLeatherGoods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string Code { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(30)")]
        public string Description { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string ShortDescription { get; set; }

        [Display(Name = "Measurement", Description = "Measurement")]
        [ForeignKey("LookUpMeasurement")]
        public int FKMeasurement { get; set; }
        public virtual LookUpMaster LookUpMeasurement { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? Measurement { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        [StringLength(30)]
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
