namespace FileManager.Models
{
    public class File
    {
        public int FileId { get; set; }
        
        public string FilePath { get; set; }
        public bool FileAccess { get; set; }
        
        public string UserId { get; set; }
      
        
    }
}