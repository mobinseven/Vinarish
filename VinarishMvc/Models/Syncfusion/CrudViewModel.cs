using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishMvc.Models.Syncfusion
{
    public class CrudViewModel<T> where T : class
    {
        public string Action { get; set; }
        public object Key { get; set; }
        public string AntiForgery { get; set; }
        public T Value { get; set; }
    }
}
