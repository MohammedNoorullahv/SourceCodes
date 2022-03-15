using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class SizeMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Display(Name = "Size Category", Description = "Size Category")]
		[ForeignKey("LookUpSizeCategory")]
		public int FKSizeCategory { get; set; }
		public virtual LookUpMaster LookUpSizeCategory { get; set; }

		[Display(Name = "Size For", Description = "Size For")]
		[ForeignKey("LookUpSizeFor")]
		public int FKSizeFor { get; set; }
		public virtual LookUpMaster LookUpSizeFor { get; set; }

        [Required]
        [StringLength(6)]
		[Column(TypeName = "varchar(6)")]
		public string Code { get; set; }

		[Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
		//[Remote("IsLMDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
		public string Description { get; set; }

        public bool IsHalfSize { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Size01 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size02 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size03 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size04 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size05 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size06 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size07 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size08 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size09 { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size10 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size11 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size12 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size13 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size14 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size15 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size16 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size17 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size18 { get; set; }

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
