using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileManager.BLL.DTO;
using FileManager.BLL.Services;
using Microsoft.AspNet.Identity;
using File = FileManager.Models.File;

namespace FileManager.Controllers
{
    public class GetAllUserFilesController : Controller
    {
        // GET: GetAllUserFiles
        private FileService _service = new FileService();
        [Authorize(Roles = "Administrator, User")]
        public ActionResult Index()
        {
            List<File> files = new List<File>();
            IEnumerable<FileDto> filesDto = new List<FileDto>();
            
            filesDto = _service.GetAllFiles(User.Identity.GetUserId());
           
            files.AddRange(filesDto.Select(fileDto => new File
            {
                FileId = fileDto.FileId,
                FilePath = fileDto.FilePath,
                FileAccess = fileDto.FileAccess
            }));
            return View(files);
        }
        public FileResult Download(string filePath)
        {
            return File(filePath, PublicFunctions.GetFileExtension(Path.GetExtension(filePath)));
        }
        public ActionResult DeleteFile(int id)
        {
            _service.DeleteFile(id);
            return View();
        }
    }
}