using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileManager.BLL.DTO;
using FileManager.BLL.Services;
using File = FileManager.Models.File;

namespace FileManager.Controllers
{
    public class SearchFileController : Controller
    {
        private FileService _service = new FileService();
        // GET: SearchFile
        
        public ActionResult Index(string namePart)
        {
            List<File> files = new List<File>();
            IEnumerable<FileDto> filesDto = new List<FileDto>();
            filesDto = _service.SearchFiles(namePart, User);
            files.AddRange(filesDto.Select(fileDto => new File
            {
                FileId = fileDto.FileId,
                FilePath = fileDto.FilePath,
                FileAccess = fileDto.FileAccess
            }));
            return View("SearchFile", files);
        }
        public FileResult Download(string filePath)
        {
            return File(filePath, PublicFunctions.GetFileExtension(Path.GetExtension(filePath)));
        }
    }
}