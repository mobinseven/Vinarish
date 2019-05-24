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
        [Display(Name = "واگن")]
        public int WagonId { get; set; }
        [Display(Name = "رام")]
        public int TrainId { get; set; }
        public string Full => Train.TrainId.ToString() + "-" + WagonId.ToString();
        [Display(Name = "رام")]
        public virtual Train Train { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
