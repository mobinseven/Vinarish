using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class Department
    {
        public Department()
        {
            Category = new HashSet<Category>();
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        [Display(Name="بخش")]
        public string Name { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
