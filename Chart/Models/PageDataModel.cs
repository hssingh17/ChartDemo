using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class PageDataModel
    {
        public List<string> Date { get; set; }
        public List<DropdownValues> Tables { get; set; }
        public List<DropdownValues> HeaderValues { get; set; }
    }
}
