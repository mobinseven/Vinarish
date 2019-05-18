using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Train")]
    public partial class Train
    {
        public Train()
        {
            Wagon = new HashSet<Wagon>();
        }
        [Display(Name = "شماره قطار")]
        public int TrainId { get; set; }
        [Display(Name = "سرپرست")]
        public int? HeadId { get; set; }
        [Display(Name = "مامور")]
        public int? OfficerId { get; set; }

        [ForeignKey("HeadId")]
        [InverseProperty("TrainHead")]
        [Display(Name = "سرپرست")]
        public virtual Person Head { get; set; }
        [ForeignKey("OfficerId")]
        [Display(Name = "مامور")]
        [InverseProperty("TrainOfficer")]
        public virtual Person Officer { get; set; }
        [InverseProperty("Train")]
        public virtual ICollection<Wagon> Wagon { get; set; }
    }
}
