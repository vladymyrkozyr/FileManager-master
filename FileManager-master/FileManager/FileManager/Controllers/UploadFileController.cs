using System.IO;
using System.Web;
using System.Web.Mvc;
using FileManager.BLL.DTO;
using FileManager.BLL.Services;
using Microsoft.AspNet.Identity;

namespace FileManager.Controllers
{
    public class UploadFileController : Controller
    {
        // GET: UploadFile
        private FileService _service = new FileService();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, User")]
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, bool fileAccess)
        {
                if (file != null && file.ContentLength > 0)
                {
                    string userPath = Server.MapPath("~/App_Data/DataStorage/"+ User.Identity.GetUserName());
                    Directory.CreateDirectory(userPath);
                    string filepath = userPath +"\\"+ Path.GetFileName(file.FileName);

                    file.SaveAs(filepath);

                    _service.InsertFile(new FileDto
                    {
                        FilePath = filepath,
                        UserId = User.Identity.GetUserId(),
                        FileAccess = fileAccess
                    });
            }
            return View("Uploaded");
        }
    }
}