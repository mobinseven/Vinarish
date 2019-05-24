using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class Train
    {
        public Train()
        {
            Wagon = new HashSet<Wagon>();
        }
        public int Id { get; set; }
        [Display(Name = "رام")]
        public int TrainId { get; set; }

        public virtual ICollection<Wagon> Wagon { get; set; }
    }
}
