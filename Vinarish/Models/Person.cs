using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Person")]
    public partial class Person
    {
        public Person()
        {
            Report = new HashSet<Report>();
            TrainHead = new HashSet<Train>();
            TrainOfficer = new HashSet<Train>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = " نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "نام")]
        public string FullName { get { return FirstName + " " + LastName; } }
        [Display(Name = "بخش")]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Person")]
        [Display(Name = "بخش")]
        public virtual Department Department { get; set; }
        [InverseProperty("Reporter")]
        public virtual ICollection<Report> Report { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<Train> TrainHead { get; set; }
        [InverseProperty("Officer")]
        public virtual ICollection<Train> TrainOfficer { get; set; }
    }
}
