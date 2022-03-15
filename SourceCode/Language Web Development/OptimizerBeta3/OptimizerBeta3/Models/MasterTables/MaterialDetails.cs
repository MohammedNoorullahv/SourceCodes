using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class MaterialDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Material", Description = "Material")]
        [ForeignKey("Materials")]
        public int FKMaterial { get; set; }
        public virtual Materials Materials{ get; set; }

        [Display(Name = "Supplier", Description = "Supplier")]
        [ForeignKey("PartyInfo")]
        public int FKParty { get; set; }
        public virtual PartyInfo PartyInfo { get; set; }

        [Display(Name = "Currency", Description = "Currency")]
        [ForeignKey("LookUpMasterCurrency")]
        public int FKCurrency { get; set; }
        public virtual LookUpMaster LookUpMasterCurrency { get; set; }

        [Column(TypeName = "Decimal(18,4)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal MinimumOrdQty { get; set; }

        public int MinimumTransitDays { get; set; }

        public bool IsPrimeSupplier { get; set; }
        public DateTime ApplicableFrom { get; set; }
        public DateTime ApplicableTo { get; set; }

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
