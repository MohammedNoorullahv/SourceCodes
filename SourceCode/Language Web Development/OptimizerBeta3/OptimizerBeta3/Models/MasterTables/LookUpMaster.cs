﻿using Microsoft.AspNetCore.Mvc;
using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class LookUpMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Look Up Category", Description = "Look Up Category")]
        [ForeignKey("LookUpCategory")]
        //[Remote("IsDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public int FKLookUpCategory { get; set; }
        public virtual LookUpCategory LookUpCategory { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        //[Remote("IsLMDescriptionExists", "LookUpMaster", ErrorMessage = "Description is already exist")]
        public string Description { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string? ShortCode { get; set; }
        public Boolean SetAsDefault { get; set; }

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