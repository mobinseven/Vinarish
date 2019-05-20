using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    public partial class Place
    {
        public Place()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Display(Name = "کد")]
        public string Code { get; set; }
        [Display(Name = "موقعیت")]
        public string Text { get; set; }

        public virtual ICollection<Report> Report { get; set; }
    }
}
