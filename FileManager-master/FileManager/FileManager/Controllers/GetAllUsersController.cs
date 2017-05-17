using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileManager.BLL.DTO;
using FileManager.BLL.Services;
using FileManager.Models;
using File = FileManager.Models.File;

namespace FileManager.Controllers
{
    public class GetAllUsersController : Controller
    {
        // GET: GetAllUsers
        private UserService _userService = new UserService();
        private FileService _fileService = new FileService();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            IEnumerable<UserDto> users = new List<UserDto>();
            List<UserNameEmail> userNameEmails = new List<UserNameEmail>();
            users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                userNameEmails.Add(new UserNameEmail
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                });
            }
            return View(userNameEmails);
        }

        public ActionResult DeleteUser(string id)
        {
            _userService.DeleteUser(id);
            return View();
        }

        public ActionResult GetUserFiles(string id)
        {
            List<File> files = new List<File>();
            IEnumerable<FileDto> filesDto = new List<FileDto>();

            filesDto = _fileService.GetAllUserFiles(id);
           
            files.AddRange(filesDto.Select(fileDto => new File
            {
                FileId = fileDto.FileId,
                FilePath = fileDto.FilePath,
                FileAccess = fileDto.FileAccess
            }));
            return View("GetUserFiles", files);
        }
        public FileResult Download(string filePath)
        {
            return File(filePath, PublicFunctions.GetFileExtension(Path.GetExtension(filePath)));
        }
        public ActionResult DeleteFile(int id)
        {
            _fileService.DeleteFile(id);
            return View();
        }
    }
}