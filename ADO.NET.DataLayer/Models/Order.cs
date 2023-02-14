using ADO.NET.DataLayer.Enums;

namespace ADO.NET.DataLayer.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int ProductId { get; set; }
    }
}
