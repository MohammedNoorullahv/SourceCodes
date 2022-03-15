using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class AreaMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Country", Description = "Country")]
        [ForeignKey("FKCountryId")]
        public int FKCountry{ get; set; }
        public virtual AreaLookUpMaster FKCountryId { get; set; }

        
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Country { get; set; }

        [Display(Name = "State", Description = "State")]
        [ForeignKey("FKStateId")]
        public int FKState { get; set; }
        public virtual AreaLookUpMaster FKStateId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string State { get; set; }

        [Display(Name = "City", Description = "City")]
        [ForeignKey("FKCityId")]
        public int FKCity { get; set; }
        public virtual AreaLookUpMaster FKCityId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }


        [Display(Name = "Area", Description = "Area")]
        [ForeignKey("FKAreaId")]
        public int FKArea { get; set; }
        public virtual AreaLookUpMaster FKAreaId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Area { get; set; }

        [Display(Name = "Pincode", Description = "Pincode")]
        [ForeignKey("FKPincodeId")]
        public int FKPincode { get; set; }
        public virtual AreaLookUpMaster FKPincodeId { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Pincode { get; set; }

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
