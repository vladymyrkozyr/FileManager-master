namespace FileManager.BLL.DTO
{
    public class FileDto
    {
        public int FileId { get; set; }
        public string FilePath { get; set; }
        public string UserId { get; set; }
        public bool FileAccess { get; set; }
    }
}
