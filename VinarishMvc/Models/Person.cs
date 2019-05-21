using System;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class Person
    {
        public Person()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
