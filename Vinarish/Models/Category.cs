using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    public partial class Category
    {
        public Category()
        {
            StatCode = new HashSet<StatCode>();
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Display(Name = "نام")]
        public string CategoryName { get; set; }
        [Display(Name = "بخش")]
        public int DepartmentId { get; set; }

        [Display(Name = "بخش")]
        public virtual Department Department { get; set; }

        public virtual ICollection<StatCode> StatCode { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
