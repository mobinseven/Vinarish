using System;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class Place
    {
        public Place()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Report> Report { get; set; }
    }
}
