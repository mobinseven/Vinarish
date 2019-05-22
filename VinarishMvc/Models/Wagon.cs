using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class Wagon
    {
        [DisplayName("واگن")]
        public Wagon()
        {
            Report = new HashSet<Report>();
        }
        public int Id { get; set; }
        [Display(Name = "شماره واگن")]
        public int WagonId { get; set; }
        [Display(Name = "شماره رام")]
        public int TrainId { get; set; }

        [Display(Name = "شماره رام")]
        public virtual Train Train { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
