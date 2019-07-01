using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishMvc.Models
{
    public class Site
    {
        public Site()
        {
            this.Reports = new List<Report>();
        }
        public virtual int SiteId { get; set; }
        [System.ComponentModel.DisplayName("سایت")]
        public virtual string Name { get; set; }
        [System.ComponentModel.DisplayName("گزارشها")]
        public virtual IList<Report> Reports { get; set; }
    }
}
