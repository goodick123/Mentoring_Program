namespace BLL.LibraryFileSystem.DTOs
{
    public class PatentDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<AuthorDTO> Authors { get; set; } = new();

        public DateTime DatePublished { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
