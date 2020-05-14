namespace ChartDemo.Models
{
    public class CustomerModel : ResultModel<Customer>
    {
    }

    public class Customer
    {
        public long DataObjectID { get; set; }
        public long TableID { get; set; }
        public bool Status { get; set; }
        public bool Inactive { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public long Country { get; set; }
        public double SqFt { get; set; }
        public double Country_calc { get; set; }
        public string TextCalc { get; set; }
        public long IntCalc { get; set; }
        public double DecimalCalc { get; set; }
        public bool BitCalc { get; set; }
    }
}
