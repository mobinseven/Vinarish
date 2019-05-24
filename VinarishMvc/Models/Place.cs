using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class Place
    {
        public Place()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Display(Name = "کد دستگاه")]
        public string Code { get; set; }
        [Display(Name = "دستگاه")]
        public string Text { get; set; }
        public string FullName => Code + " " + Text;

        public virtual ICollection<Report> Report { get; set; }
    }
}
