using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vinarish.Models
{
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
        [Display(Name = "زمان")]
        public DateTime DateTime { get; set; }
        [Display(Name = "گزارشگر")]
        public int ReporterId { get; set; }
        [Display(Name = "گزارش پیوست")]
        public int? AppendixReportId { get; set; }
        [Display(Name = "تایید گزارش")]
        public bool? IsValid { get; set; }

        [Display(Name = "دسته")]
        public virtual Category Cat { get; set; }
        [Display(Name = "وضعیت")]
        public virtual StatCode Code { get; set; }
        [Display(Name = "موقعیت")]
        public virtual Place Place { get; set; }
        [Display(Name = "گزارشگر")]
        public virtual Person Reporter { get; set; }
        [Display(Name = "واگن")]
        public virtual Wagon Wagon { get; set; }
        [Display(Name = "گزارش پیوست")]
        public virtual Report AppendixReport { get; set; }
        public virtual ICollection<Report> AppendixReports { get; set; }
    }
}
