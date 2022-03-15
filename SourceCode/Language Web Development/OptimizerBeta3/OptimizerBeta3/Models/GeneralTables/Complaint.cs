using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class Complaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime ComplaintDt { get; set; }

        [Display(Name = "Menu Category", Description = "Menu Category")]
        [ForeignKey("LookUpMasterMenuCategory")]
        public int FKMenuCategory { get; set; }
        public virtual ComplaintLookUpMaster LookUpMasterMenuCategory { get; set; }

        [Display(Name = "Menu Name", Description = "Menu Name")]
        [ForeignKey("LookUpMasterMenuName")]
        public int FKMenuName { get; set; }
        public virtual ComplaintLookUpMaster LookUpMasterMenuName { get; set; }

        [Display(Name = "Location", Description = "Location")]
        [ForeignKey("LookUpMasterLocation")]
        public int FKLocation { get; set; }
        public virtual ComplaintLookUpMaster LookUpMasterLocation { get; set; }

        [Display(Name = "AdminName", Description = "AdminName")]
        [ForeignKey("LookUpMasterAdminName")]
        public int FKAdminName { get; set; }
        public virtual ComplaintLookUpMaster LookUpMasterAdminName { get; set; }

        [Display(Name = "UserName", Description = "UserName")]
        [ForeignKey("LookUpMasterUserName")]
        public int FKUserName { get; set; }
        public virtual ComplaintLookUpMaster LookUpMasterUserName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Comments { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string AddressLink { get; set; }

        [Display(Name = "Status", Description = "Status")]
        [ForeignKey("LookUpMasterStatus")]
        public int FKStatus { get; set; }
        public virtual ComplaintLookUpMaster LookUpMasterStatus { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CommentBySupportTeam { get; set; }

        public DateTime PlannedDt { get; set; }
        public DateTime CompletedDt { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
