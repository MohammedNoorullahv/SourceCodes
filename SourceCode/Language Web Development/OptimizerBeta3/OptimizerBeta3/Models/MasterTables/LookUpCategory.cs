using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class LookUpCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Serial No. must be a natural number only")]
        [Display(Name = "SerialNo", Description = "Serial No.")]
        //[Remote("IsSlNoExists", "lookUpCatergory", ErrorMessage = "Serial No. is already exist")]
        public int SlNo { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        //[Remote("IsDescriptionExists", "lookUpCatergory", ErrorMessage = "Description is already exist")]
        public string Description { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        //[Remote("IsShortCodeExists", "lookUpCatergory", ErrorMessage = "Short Code is already exist")]
        public string ShortCode { get; set; }

        public Boolean LastEntryCategory { get; set; } = true;

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
