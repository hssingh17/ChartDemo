using Newtonsoft.Json;

namespace ChartDemo.Models.Entity
{
    public class Customer
    {
        public int DataObjectID { get; set; }
        public int TableID { get; set; }
        public bool Status { get; set; }
        public bool Inactive { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Address 1")]
        public string Address_1 { get; set; }
        public int Country { get; set; }

        [JsonProperty(PropertyName = "Payment Type")]
        public int Payment_Type { get; set; }
    }
}
