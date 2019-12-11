using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishApi.Models
{
    public class Site
    {
        public Site()
        {
            this.Reports = new List<Report>();
        }

        public virtual int SiteId { get; set; }

        [System.ComponentModel.DisplayName(Expressions.Site)]
        public virtual string Name { get; set; }

        [System.ComponentModel.DisplayName(Expressions.Reports)]
        public virtual IList<Report> Reports { get; set; }
    }
}