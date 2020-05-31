using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class ResultModel<T>
    {
        public bool Status { get; set; }
        public int RecordsAffected { get; set; }
        public object Message { get; set; }
        public object OperationId { get; set; }
        public List<T> Result { get; set; }
        public int ErrorCode { get; set; }
        public object ExceptionMessage { get; set; }
        public object ExceptionStackTrace { get; set; }
        public object ExceptionInnerMessage { get; set; }
        public object ExceptionInnerStackTrace { get; set; }
    }
}
