using Models.DTOS.User;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUser();
        Task<UserResponseDto> GetUserById(string id);
        Task AddUser(AddUserDto user);
        Task<bool> UpdateUser(string id, UpdateUserDto user);
        Task<bool> DeleteUser(string id);


    }
}
