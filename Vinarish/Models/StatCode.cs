using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("StatCode")]
    public partial class StatCode
    {
        public StatCode()
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
        [Display(Name = "پیام")]
        public string Text { get; set; }
        public int CatId { get; set; }

        [ForeignKey("CatId")]
        [InverseProperty("StatCode")]
        public virtual Category Cat { get; set; }
        [InverseProperty("Code")]
        public virtual ICollection<Report> Report { get; set; }
    }
}
