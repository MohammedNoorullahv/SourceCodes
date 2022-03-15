using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.HRTables
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Unit", Description = "Unit")]
        [ForeignKey("UnitMaster")]
        public int FKUnit { get; set; }
        public virtual UnitMaster UnitMaster { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? UnitName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Code { get; set; }

        [Display(Name = "Salutation", Description = "Salutation")]
        [ForeignKey("LookUpMasterSalutation")]
        public int FKSalutation { get; set; }
        public virtual LookUpMaster LookUpMasterSalutation { get; set; }

        [Required]
        [StringLength(160)]
        [Column(TypeName = "varchar(160)")]
        public string FullName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string MiddleName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Initials { get; set; }

        [Display(Name = "MaritalStatus", Description = "MaritalStatus")]
        [ForeignKey("LookUpMasterMaritalStatus")]
        public int FKMaritalStatus { get; set; }
        public virtual LookUpMaster LookUpMasterMaritalStatus { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string HorFName { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Address1 { get; set; }
        
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Address2 { get; set; }

        [Display(Name = "Area", Description = "Area")]
        [ForeignKey("LookUpMasterArea")]
        public int FKArea { get; set; }
        public virtual LookUpMaster LookUpMasterArea { get; set; }

        [Display(Name = "Gender", Description = "Gender")]
        [ForeignKey("LookUpMasterGender")]
        public int FKGender { get; set; }
        public virtual LookUpMaster LookUpMasterGender { get; set; }

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

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? MobileNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? EMailId { get; set; }

        public DateTime DOB { get; set; }
        
        [Display(Name = "DOBProofType", Description = "DOBProofType")]
        [ForeignKey("LookUpMasterDOBProofType")]
        public int FKDOBProofType { get; set; }
        public virtual LookUpMaster LookUpMasterDOBProofType { get; set; }
        public DateTime DOJ { get; set; }
        
        [Display(Name = "Department", Description = "Department")]
        [ForeignKey("LookUpMasterDepartment")]
        public int FKDepartment { get; set; }
        public virtual LookUpMaster LookUpMasterDepartment { get; set; }

        [Display(Name = "Designation", Description = "Designation")]
        [ForeignKey("LookUpMasterDesignation")]
        public int FKDesignation { get; set; }
        public virtual LookUpMaster LookUpMasterDesignation { get; set; }

        [Display(Name = "EmpCategory", Description = "EmpCategory")]
        [ForeignKey("LookUpMasterEmpCategory")]
        public int FKEmpCategory { get; set; }
        public virtual LookUpMaster LookUpMasterEmpCategory { get; set; }

        [Display(Name = "Religion", Description = "Religion")]
        [ForeignKey("LookUpMasterReligion")]
        public int FKReligion { get; set; }
        public virtual LookUpMaster LookUpMasterReligion { get; set; }

        [Display(Name = "Qualification", Description = "Qualification")]
        [ForeignKey("LookUpMasterQualification")]
        public int FKQualification { get; set; }
        public virtual LookUpMaster LookUpMasterQualification { get; set; }

        public int NoofDependants { get; set; }


        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? PFNo { get; set; }


        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? ESINo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? PANNo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? BankAccountNo { get; set; }


        public DateTime? ResignDate { get; set; }


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
