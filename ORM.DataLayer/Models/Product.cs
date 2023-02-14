namespace ORM.DataLayer.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public float Length { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
