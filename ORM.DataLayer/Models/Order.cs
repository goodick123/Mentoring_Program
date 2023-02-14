using ORM.DataLayer.Enums;

namespace ORM.DataLayer.Models
{
    public class Order : BaseModel
    {
        public Status Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
