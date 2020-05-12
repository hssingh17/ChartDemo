using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class CustomerModel
    {
        public bool Status { get; set; }
        public int RecordsAffected { get; set; }
        public string Message { get; set; }
        public string OperationId { get; set; }
        public List<DropdownValues> Result { get; set; }
    }
}
