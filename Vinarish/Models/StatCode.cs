using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
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
        public int CatId { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
