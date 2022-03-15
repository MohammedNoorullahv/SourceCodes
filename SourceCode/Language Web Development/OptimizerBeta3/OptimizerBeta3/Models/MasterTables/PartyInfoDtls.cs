﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class PartyInfoDtls
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Party", Description = "Party")]
        [ForeignKey("PartyInfo")]
        public int FKPartyInfo { get; set; }
        public virtual PartyInfo PartyInfo { get; set; }

        [Display(Name = "Unit Type", Description = "Unit Type")]
        [ForeignKey("LookUpMasterUnitType")]
        public int FKUnitType { get; set; }
        public virtual LookUpMaster LookUpMasterUnitType { get; set; }

        [Required]
        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        //[Remote("IsDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string CompanyName { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ShortName { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Address1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Address2 { get; set; }

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

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ContactPersonName { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ContactNo { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? MailId { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? PANNumber { get; set; }

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