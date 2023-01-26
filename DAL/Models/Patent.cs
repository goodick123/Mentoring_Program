using System.Runtime.Serialization;
using DAL.Models.Base;

namespace DAL.Models
{
    [DataContract]
    public class Patent : BaseFileModel
    {
        public string Title { get; set; } = string.Empty;

        public List<Author> Authors { get; set; } = new();

        public DateTime DatePublished { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
