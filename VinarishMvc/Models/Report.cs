using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Models
{
    public partial class Report
    {
        public Report()
        {
            InverseAppendixReport = new HashSet<Report>();
        }

        public int Id { get; set; }
        [Display(Name = "واگن")]
        public int WagonId { get; set; }
        [Display(Name = "کد موقعیت")]
        public int PlaceId { get; set; }
        [Display(Name = "افزار")]
        public int CatId { get; set; }
        [Display(Name = "کد وضعیت")]
        public int CodeId { get; set; }
        [Display(Name = "زمان")]
        public DateTime DateTime { get; set; }
        [Display(Name = "گزارشگر")]
        public int ReporterId { get; set; }
        [Display(Name = "گزارش پیوست")]
        public int? AppendixReportId { get; set; }
        [Display(Name = "درستی گزارش")]
        public bool? IsValid { get; set; }

        [Display(Name = "گزارش پیوست")]
        public virtual Report AppendixReport { get; set; }
        [Display(Name = "افزار")]
        public virtual Category Cat { get; set; }
        [Display(Name = "کد وضعیت")]
        public virtual StatCode Code { get; set; }
        [Display(Name = "کد موقعیت")]
        public virtual Place Place { get; set; }
        [Display(Name = "گزارشگر")]
        public virtual Person Reporter { get; set; }
        [Display(Name = "واگن")]
        public virtual Wagon Wagon { get; set; }
        public virtual ICollection<Report> InverseAppendixReport { get; set; }
    }
}
