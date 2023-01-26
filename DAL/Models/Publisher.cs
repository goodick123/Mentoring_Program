using DAL.Models.Base;

namespace DAL.Models
{
    public class Publisher : BaseFileModel
    {
        public string Name { get; set; } = string.Empty;
    }
}
