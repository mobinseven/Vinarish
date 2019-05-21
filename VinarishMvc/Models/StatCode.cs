using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class StatCode
    {
        public StatCode()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Display(Name = "کد")]
        public string Code { get; set; }
        [Display(Name = "پیام")]
        public string Text { get; set; }
        [Display(Name = "افزار")]
        public int CatId { get; set; }

        [Display(Name = "افزار")]
        public virtual Category Cat { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
