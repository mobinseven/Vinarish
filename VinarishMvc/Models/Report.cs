using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    [DisplayName("گزارش")]
    public partial class Report
    {
        public Report()
        {
            DateTime = DateTime.Now;
            InverseAppendixReport = new HashSet<Report>();
        }

        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "واگن")]
        public int WagonId { get; set; }
        [Display(Name = "دستگاه")]
        public int PlaceId { get; set; }
        [Display(Name = "افزار")]
        public int CatId { get; set; }
        [Display(Name = "وضعیت")]
        public int CodeId { get; set; }
        [Display(Name = "زمان")]
        public DateTime DateTime { get; set; }
        [Display(Name = "گزارشگر")]
        public int ReporterId { get; set; }
        [Display(Name = "گزارش پیوست")]
        public int? AppendixReportId { get; set; }
        [Display(Name = "تایید شده؟")]
        [UIHint("IsValid")]
        public bool? IsValid { get; set; }

        [Display(Name = "گزارش پیوست")]
        public virtual Report AppendixReport { get; set; }
        [Display(Name = "افزار")]
        public virtual Category Cat { get; set; }
        [Display(Name = "وضعیت")]
        public virtual StatCode Code { get; set; }
        [Display(Name = "دستگاه")]
        public virtual Place Place { get; set; }
        [Display(Name = "گزارشگر")]
        public virtual Person Reporter { get; set; }
        [Display(Name = "واگن")]
        public virtual Wagon Wagon { get; set; }
        public virtual ICollection<Report> InverseAppendixReport { get; set; }
    }
}
