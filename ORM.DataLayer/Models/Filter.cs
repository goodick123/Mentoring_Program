using ORM.DataLayer.Enums;

namespace ORM.DataLayer.Models
{
    public class Filter
    {
        public int? Month { get; set; }

        public int? Year { get; set; }

        public Status? Status { get; set; }

        public int? ProductId { get; set; }
    }
}
