using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    public partial class Department
    {
        public Department()
        {
            Person = new HashSet<Person>();
            Category = new HashSet<Category>();
        }

        public int Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}
