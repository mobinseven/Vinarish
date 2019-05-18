using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Place")]
    public partial class Place
    {
        public Place()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Required]
        [Column("Code")]
        [Display(Name = "کد")]
        public string Code { get; set; }
        [Required]
        [Column("Text")]
        [Display(Name = "موقعیت")]
        public string Text { get; set; }

        [InverseProperty("Place")]
        public virtual ICollection<Report> Report { get; set; }
    }
}
