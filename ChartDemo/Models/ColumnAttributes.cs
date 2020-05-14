namespace ChartDemo.Models
{

    public class ColumnAttributesModel : ResultModel<ColumnAttributes>
    {

    }

    public class ColumnAttributes
    {
        public int TableID { get; set; }
        public string Field { get; set; }
        public string HeaderName { get; set; }
        public bool Sortable { get; set; }
        public bool Filter { get; set; }
        public bool RowGroup { get; set; }
        public bool Resizable { get; set; }
        public bool Pinned { get; set; }
        public bool Colspan { get; set; }
        public bool Colmove { get; set; }
    }
}
