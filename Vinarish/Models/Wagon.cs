using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Wagon")]
    public partial class Wagon
    {
        public Wagon()
        {
            Report = new HashSet<Report>();
        }
        [Display(Name = "شماره واگن")]
        public int WagonId { get; set; }
        [Display(Name = "شماره قطار")]
        public int TrainId { get; set; }
        [ForeignKey("TrainId")]
        [InverseProperty("Wagon")]
        [Display(Name = "شماره قطار")]
        public virtual Train Train { get; set; }
        [InverseProperty("Wagon")]
        public virtual ICollection<Report> Report { get; set; }
    }
}
