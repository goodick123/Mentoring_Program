using DAL.Models.Base;

namespace DAL.Models
{
    public class Country : BaseFileModel
    {
        public string Name { get; set; } = string.Empty;
    }
}
