using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using FileManager.BLL.DTO;
using FileManager.DAL.Entities;
using FileManager.DAL.UnitsOfWork;

namespace FileManager.BLL.Services
{
    public class FileService
    {
        
        public List<FileDto> GetAllFiles()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<FileDto> filesDto = new List<FileDto>();
                IEnumerable<File> files = unitOfWork.FileRepository.Get();
                foreach (var file in files)
                {
                    filesDto.Add(new FileDto
                    {
                        FileId = file.FileId,
                        FilePath = file.FilePath,
                        UserId = file.UserId,
                        FileAccess = file.FileAccess
                    });
                }
                return filesDto;
            }
        }
        public List<FileDto> GetAllFiles(string userId)
        {
            return GetAllFiles().Where(f => f.UserId == userId).ToList();
        }
        public List<FilePathUserNameEmailDto> GetAllFilePathsEmails()
        {
            List<FileDto> filesDto = GetAllFiles();
            List<UserDto> usersDto = new UserService().GetAllUsers();

            List<FilePathUserNameEmailDto> filePathUserNameEmailDto = new List<FilePathUserNameEmailDto>();

            foreach (var fileDto in filesDto)
            {
                filePathUserNameEmailDto.Add(new FilePathUserNameEmailDto
                {
                    FileId = fileDto.FileId,
                    FilePath = fileDto.FilePath,
                    UserName = usersDto.Where(i => i.Id == fileDto.UserId).Select(u => u.UserName).First(),
                    UserEmail = usersDto.Where(i => i.Id == fileDto.UserId).Select(e => e.Email).First(),
                });
            }

            return filePathUserNameEmailDto;
        }
        public List<FileDto> GetAllPublicFiles()
        {
            return GetAllFiles().Where(a => !a.FileAccess).ToList();
        }
        public List<FileDto> GetAllUserFiles(string userId)
        {
            return GetAllFiles().Where(f => f.UserId == userId).ToList();
        }
        public List<FileDto> SearchFiles(string namePart, IPrincipal user)
        {
            if (namePart != "")
            {
                if (user.IsInRole("Administrator"))
                    return GetAllFiles().Where(f => f.FilePath.Contains(namePart)).ToList();
                return GetAllPublicFiles().Where(f => f.FilePath.Contains(namePart)).ToList();
            }
            return new List<FileDto>();
        }
        public void InsertFile(FileDto file)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.FileRepository.Insert(new File
                {
                    FilePath = file.FilePath,
                    UserId = file.UserId,
                    FileAccess = file.FileAccess
                });
                unitOfWork.SaveChanges();
            }
        }
        public void DeleteFile(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                System.IO.File.Delete(unitOfWork.FileRepository.GetById(id).FilePath);
                unitOfWork.FileRepository.Delete(id);
                unitOfWork.SaveChanges();
            }
        }
    }
}
