using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    public partial class Person
    {
        public Person()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Display(Name = "نام")]
        public string FullName { get { return FirstName + " " + LastName; } }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = " نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "بخش")]
        public int? DepartmentId { get; set; }

        [Display(Name = "بخش")]
        public virtual Department Department { get; set; }

        public virtual ICollection<Report> Report { get; set; }
    }
}
