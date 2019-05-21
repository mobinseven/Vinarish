using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class Category
    {
        public Category()
        {
            Report = new HashSet<Report>();
            StatCode = new HashSet<StatCode>();
        }

        public int Id { get; set; }
        [Display(Name = "افزار")]
        public string CategoryName { get; set; }
        [Display(Name = "بخش")]
        public int DepartmentId { get; set; }

        [Display(Name = "بخش")]
        public virtual Department Department { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<StatCode> StatCode { get; set; }
    }
}
