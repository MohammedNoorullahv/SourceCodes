using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class CustomerPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        //[Remote("IsDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string PersonName { get; set; }

        [Display(Name = "Gender", Description = "Gender")]
        [ForeignKey("LookUpMasterGender")]
        public int FKGender { get; set; }
        public virtual LookUpMaster LookUpMasterGender { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Address { get; set; }

        [Display(Name = "Area", Description = "Area")]
        [ForeignKey("LookUpMasterArea")]
        public int FKArea { get; set; }
        public virtual LookUpMaster LookUpMasterArea { get; set; }

        [Display(Name = "City", Description = "City")]
        [ForeignKey("LookUpMasterCity")]
        public int FKCity { get; set; }
        public virtual LookUpMaster LookUpMasterCity { get; set; }

        [Display(Name = "Pincode", Description = "Pincode")]
        [ForeignKey("LookUpMasterPincode")]
        public int FKPincode { get; set; }
        public virtual LookUpMaster LookUpMasterPincode { get; set; }

        [Display(Name = "State", Description = "State")]
        [ForeignKey("StateMaster")]
        public int FKState { get; set; }
        public virtual MdlStateMaster StateMaster { get; set; }

        [Display(Name = "Country", Description = "Country")]
        [ForeignKey("LookUpMasterCountry")]
        public int FKCountry { get; set; }
        public virtual LookUpMaster LookUpMasterCountry { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OfficePhoneNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? HomePhoneNo { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string MobileNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? EMailId { get; set; }

        public DateTime? BirthDate { get; set; }

        [Display(Name = "MaritalStatus", Description = "MaritalStatus")]
        [ForeignKey("LookUpMasterMaritalStatus")]
        public int FKMaritalStatus { get; set; }
        public virtual LookUpMaster LookUpMasterMaritalStatus { get; set; }

        public DateTime? WeddingDate { get; set; }

        [Display(Name = "Customer Of", Description = "Customer Of")]
        [ForeignKey("LookUpMasterCustomerOf")]
        public int FKCustomerOf { get; set; }
        public virtual LookUpMaster LookUpMasterCustomerOf { get; set; }

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
