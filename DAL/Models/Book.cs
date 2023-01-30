using System.Runtime.Serialization;
using DAL.Models.Base;

namespace DAL.Models
{
    [DataContract]
    public class Book : BaseFileModel
    {
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public List<Author> Authors { get; set; } = new();

        public virtual Publisher Publisher { get; set; } = new();

        public int NumberOfPages { get; set; }

        public DateTime DatePublished { get; set; }
    }
}
