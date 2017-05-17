using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileManager.BLL.DTO;
using FileManager.BLL.Services;
using FileManager.Models;

namespace FileManager.Controllers
{
    public class GetAllFilesController : Controller
    {
        // GET: GetAllFiles
        private FileService _service = new FileService();
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<FilePathUserNameEmail> filePathUserNameEmails = new List<FilePathUserNameEmail>();
            IEnumerable<FilePathUserNameEmailDto> filePathUserNameEmailsDto = new List<FilePathUserNameEmailDto>();

            filePathUserNameEmailsDto = _service.GetAllFilePathsEmails();

            filePathUserNameEmails.AddRange(filePathUserNameEmailsDto.Select(filePathUserNameEmail => new FilePathUserNameEmail
            {
                FileId = filePathUserNameEmail.FileId,
                UserName = filePathUserNameEmail.UserName,
                UserEmail = filePathUserNameEmail.UserEmail,
                FilePath = filePathUserNameEmail.FilePath
            }));
            return View(filePathUserNameEmails);
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