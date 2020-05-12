using ChartDemo.Controllers;
using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class TableNames
    {
        public long TableID { get; set; }
        public string TableName { get; set; }
    }

    public class RootObject
    {
        public bool Status { get; set; }
        public long RecordsAffected { get; set; }
        public string Message { get; set; }
        public string OperationId { get; set; }
        public List<ChartObject> Result { get; set; }
    }
}
