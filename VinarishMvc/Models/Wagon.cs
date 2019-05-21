using System;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class Wagon
    {
        public Wagon()
        {
            Report = new HashSet<Report>();
        }

        public int WagonId { get; set; }
        public int TrainId { get; set; }

        public virtual Train Train { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
