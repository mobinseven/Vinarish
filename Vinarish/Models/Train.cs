using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    public partial class Train
    {
        public Train()
        {
            Wagon = new HashSet<Wagon>();
        }
        [Display(Name = "شماره رام")]
        public int TrainId { get; set; }

        public virtual ICollection<Wagon> Wagon { get; set; }
    }
}
