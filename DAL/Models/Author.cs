using DAL.Models.Base;

namespace DAL.Models
{
    public class Author : BaseFileModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
