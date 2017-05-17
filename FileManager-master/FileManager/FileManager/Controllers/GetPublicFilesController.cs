using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileManager.BLL.DTO;
using FileManager.BLL.Services;
using File = FileManager.Models.File;

namespace FileManager.Controllers
{
    public class GetPublicFilesController : Controller
    {
        // GET: GetPublicFiles
        private FileService _service = new FileService();

        public ActionResult Index()
        {
            List<File> files = new List<File>();
            List<FileDto> filesDto = new List<FileDto>();

            filesDto = _service.GetAllPublicFiles();

            files.AddRange(filesDto.Select(fileDto => new File
            {
                FileId = fileDto.FileId,
                FilePath = fileDto.FilePath,
                UserId = fileDto.UserId,
                FileAccess = fileDto.FileAccess
            }));
            return View(files);
        }

        public FileResult Download(string filePath)
        {
            return File(filePath, PublicFunctions.GetFileExtension(Path.GetExtension(filePath)));
        }

    }
}