using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
    [Table("Report")]
    public partial class Report
    {
        public Report()
        {
            AppendixReports = new HashSet<Report>();
        }
        public int Id { get; set; }
        [Display(Name = "واگن")]
        public int WagonId { get; set; }
        [Display(Name = "دسته")]
        public int CatId { get; set; }
        [Display(Name = "وضعیت")]
        public int CodeId { get; set; }
        [Display(Name = "موقعیت")]
        public int PlaceId { get; set; }
        [Required]
        [Column("DateTime")]
        [Display(Name = "زمان")]
        public DateTime DateTime { get; set; }
        [Display(Name = "گزارشگر")]
        public int ReporterId { get; set; }
        [Display(Name = "گزارش پیوست")]
        public int? AppendixReportId { get; set; }

        [ForeignKey("CatId")]
        [InverseProperty("Report")]
        [Display(Name = "دسته")]
        public virtual Category Cat { get; set; }
        [ForeignKey("CodeId")]
        [InverseProperty("Report")]
        [Display(Name = "وضعیت")]
        public virtual StatCode Code { get; set; }
        [ForeignKey("PlaceId")]
        [InverseProperty("Report")]
        [Display(Name = "موقعیت")]
        public virtual Place Place { get; set; }
        [ForeignKey("ReporterId")]
        [InverseProperty("Report")]
        [Display(Name = "گزارشگر")]
        public virtual Person Reporter { get; set; }
        [ForeignKey("WagonId")]
        [InverseProperty("Report")]
        [Display(Name = "واگن")]
        public virtual Wagon Wagon { get; set; }
        public virtual Report AppendixReport { get; set; }
        public virtual ICollection<Report> AppendixReports { get; set; }
    }
}
