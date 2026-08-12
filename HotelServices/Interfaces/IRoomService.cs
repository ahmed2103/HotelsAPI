using Models.DTOS.Booking;
using Models.DTOS.Room;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Interfaces
{
    public interface IRoomService
    {
        public Task<IEnumerable<RoomResponseDto>> GetAvailableRooms();
        Task<RoomResponseDto?> GetRoomById(int id);
        Task<RoomResponseDto> CreateRoom(CreateRoomDto room);
        Task<bool> UpdateRoom(int id, UpdateRoomDto updatedRoom);
        Task<bool> DeleteRoom(int id);
        Task<IEnumerable<RoomResponseDto>> GetAllRooms();
        //Task<IEnumerable<Booking>> GetAllBookingsAsync(string userId);
        //Task<Booking?> GetBookingByIdAsync(int id, string userId);
        //Task<Booking> CreateBookingAsync(CreateBookingDTO Dto, string userId);
        //Task<bool> UpdateBookingAsync(int id, Booking updatedBooking, string userId);
        //Task<bool> DeleteBookingAsync(int id, string userId);
    }
}
