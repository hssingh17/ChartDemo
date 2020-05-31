using System.Collections.Generic;

namespace ChartDemo.Models
{
    public class ContactModel
    {
        public bool Status { get; set; }
        public long RecordsAffected { get; set; }
        public string Message { get; set; }
        public string OperationId { get; set; }
        public List<Contact> Result { get; set; }
    }
    public class Contact
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public long PaymentMethod { get; set; }
        public long Country { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
    }
}
