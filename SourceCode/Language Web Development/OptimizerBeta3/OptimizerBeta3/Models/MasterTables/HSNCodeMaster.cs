using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class HSNCodeMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int HSNCode { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public String Category { get; set; }

        [Display(Name = "Percentage Type", Description = "Percentage Type")]
        [ForeignKey("LookUpMasterPercentageType")]
        public int FKPercentageType { get; set; }
        public virtual LookUpMaster LookUpMasterPercentageType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(10)")]
        public string? PercentageType { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GSTPercentage { get; set; }

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
