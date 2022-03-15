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
    public class PaymentOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string Code { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Description { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Card No Minumum Length must be a natural number only")]
        public int CardNoMinLength { get; set; }
        public bool IsNameCompulsory { get; set; }
        public bool IsExpiryDateCompulsory { get; set; }

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
