using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class PageDataModel
    {
        public List<string> Date { get; set; }
        public List<DropdownValues> Country { get; set; }
        public List<DropdownValues> PaymentMethods { get; set; }
    }
}
