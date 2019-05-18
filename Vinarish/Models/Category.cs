using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            StatCode = new HashSet<StatCode>();
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Required]
        [Column("CategoryName")]
        [Display(Name = "نام")]
        public string CategoryName { get; set; }
        [Column("OrderNumber")]
        [Display(Name = "ترتیب")]
        public uint? OrderNumber { get; set; }
        [Required]
        [Column("DepartmentId")]
        [Display(Name = "بخش")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Category")]
        public virtual Department Department { get; set; }
        [InverseProperty("Cat")]
        public virtual ICollection<StatCode> StatCode { get; set; }
        [InverseProperty("Cat")]
        public virtual ICollection<Report> Report { get; set; }
    }
}
