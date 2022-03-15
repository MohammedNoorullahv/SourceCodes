using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class MdlStateMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StateCode { get; set; }

        [Required]
        [StringLength(40)]
        [Column(TypeName = "varchar(30)")]
        public string StateName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? ShortName { get; set; }
    }
}
