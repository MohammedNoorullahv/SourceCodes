using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class SizeAssortment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Size Master", Description = "Size Master")]
        [ForeignKey("SizeMaster")]
        public int FKSizeMaster { get; set; }
        public virtual SizeMaster SizeMaster { get; set; }

        [Required]
        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string Code { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        //[Remote("IsLMDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size01 { get; set; }

        public int? Quantity01 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size02 { get; set; }
        public int? Quantity02 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size03 { get; set; }
        public int? Quantity03 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size04 { get; set; }
        public int? Quantity04 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size05 { get; set; }
        public int? Quantity05 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size06 { get; set; }
        public int? Quantity06 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size07 { get; set; }
        public int? Quantity07 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size08 { get; set; }
        public int? Quantity08 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size09 { get; set; }
        public int? Quantity09 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size10 { get; set; }
        public int? Quantity10 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size11 { get; set; }
        public int? Quantity11 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size12 { get; set; }
        public int? Quantity12 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size13 { get; set; }
        public int? Quantity13 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size14 { get; set; }
        public int? Quantity14 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size15 { get; set; }
        public int? Quantity15 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size16 { get; set; }
        public int? Quantity16 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size17 { get; set; }
        public int? Quantity17 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size18 { get; set; }
        public int? Quantity18 { get; set; }
        public int TotalQuantity { get; set; }

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
