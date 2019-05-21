using System;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class Train
    {
        public Train()
        {
            Wagon = new HashSet<Wagon>();
        }

        public int TrainId { get; set; }

        public virtual ICollection<Wagon> Wagon { get; set; }
    }
}
