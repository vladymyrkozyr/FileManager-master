using System.ComponentModel.DataAnnotations.Schema;

namespace FileManager.DAL.Entities
{
    public class File
    {
        public int FileId { get; set; }
        
        public string FilePath { get; set; }
        public bool FileAccess { get; set; }
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
    }
}