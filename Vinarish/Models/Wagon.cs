using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    public partial class Wagon
    {
        public Wagon()
        {
            Report = new HashSet<Report>();
        }
        [Display(Name = "شماره واگن")]
        public int WagonId { get; set; }
        [Display(Name = "شماره رام")]
        public int TrainId { get; set; }
        [Display(Name = "شماره رام")]
        public virtual Train Train { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
