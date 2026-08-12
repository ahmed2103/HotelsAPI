using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOS.User;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Implementation
{
    public class UserService : IUserService
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork ) {
            

            _unitOfWork = unitOfWork;

        }
        public async Task AddUser(AddUserDto user)
        {
            var u = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
            };
            await _unitOfWork.Users.AddAsync(u);
        }

        public async Task<bool> DeleteUser(string id)
        {
            
            var u = await _unitOfWork.Users.GetUserById(id);
            if (u == null)
            {
                return false;
            }
            else
            {
                _unitOfWork.Users.Delete(u);
               await _unitOfWork.CompleteAsync();
                return true;
            }
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUser()
        {
          var us=  await _unitOfWork.Users.GetAllAsync();
            return us.Select(u => new UserResponseDto
            {
                UserId=u.Id,
                Email = u.Email,
                FullName = u.FullName,
                PhoneNumber = u.PhoneNumber,
            });
        }

        public async Task<UserResponseDto> GetUserById(string id)
        {
            
                var u = await _unitOfWork.Users.GetUserById(id);
            if (u == null)
            {
                return null;
            }
            else
            {
                return new UserResponseDto
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                };
            }


        }

        public async Task<bool> UpdateUser(string id, UpdateUserDto user)
        {

            var existingUser = await _unitOfWork.Users.GetUserById(id);
            if (existingUser == null)
            {
                return false;
            }
            else
            {
                existingUser.FullName = user.FullName;
                existingUser.PhoneNumber = user.PhoneNumber;
                
                return true;
            }
        }
    }
}
