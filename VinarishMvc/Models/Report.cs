using System;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class Report
    {
        public Report()
        {
            InverseAppendixReport = new HashSet<Report>();
        }

        public int Id { get; set; }
        public int WagonId { get; set; }
        public int CatId { get; set; }
        public int CodeId { get; set; }
        public int PlaceId { get; set; }
        public DateTime DateTime { get; set; }
        public int ReporterId { get; set; }
        public int? AppendixReportId { get; set; }
        public bool? IsValid { get; set; }

        public virtual Report AppendixReport { get; set; }
        public virtual Category Cat { get; set; }
        public virtual StatCode Code { get; set; }
        public virtual Place Place { get; set; }
        public virtual Person Reporter { get; set; }
        public virtual Wagon Wagon { get; set; }
        public virtual ICollection<Report> InverseAppendixReport { get; set; }
    }
}
