using System.Collections.Generic;
using FileManager.BLL.DTO;
using FileManager.DAL.Entities;
using FileManager.DAL.UnitsOfWork;

namespace FileManager.BLL.Services
{
    public class UserService
    {
        public List<UserDto> GetAllUsers()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<UserDto> usersDto = new List<UserDto>();
                IEnumerable<User> users = unitOfWork.UserRepository.Get();
                foreach (var user in users)
                {
                    usersDto.Add(new UserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
                }

                return usersDto;

            }
        }
        public User GetUserById(string id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.UserRepository.GetById(id);
            }
        }
        public void DeleteUser(string id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.UserRepository.Delete(id);
                unitOfWork.SaveChanges();
            }
        }
    }
}
