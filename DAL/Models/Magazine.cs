using DAL.Models.Base;

namespace DAL.Models
{
    public class Magazine : BaseFileModel
    {
        public string Title { get; set; } = string.Empty;

        public Publisher Publisher { get; set; } = new();

        public int ReleaseNumber { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
