using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Person = new HashSet<Person>();
            Category = new HashSet<Category>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Person> Person { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Category> Category { get; set; }
    }
}
