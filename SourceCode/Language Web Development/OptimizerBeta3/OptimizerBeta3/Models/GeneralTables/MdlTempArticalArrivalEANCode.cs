using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class MdlTempArticalArrivalEANCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string IPAddress { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string InwardNo { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string EANCode { get; set; }
        public int Quantity { get; set; }
        public Boolean IsMatching { get; set; }
        
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Reason { get; set; }
    }
}
