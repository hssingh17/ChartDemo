using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class DropdownModel
    {
        public bool Status { get; set; }
        public long RecordsAffected { get; set; }
        public string Message { get; set; }
        public string OperationId { get; set; }
        public List<DropdownValues> Result { get; set; }
    }
}
