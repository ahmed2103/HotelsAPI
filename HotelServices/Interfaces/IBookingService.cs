using Models.DTOS.Booking;
using Models.DTOS.DashboardStats;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(string userId);
        Task<BookingResponseDto?> GetBookingByIdAsync(int id, string userId);
        Task<BookingResponseDto> CreateBookingAsync(CreateBookingDTO Dto, string userId);
        Task<bool> UpdateBookingAsync(int id, UpdateBookingDto updatedBooking, string userId);
        Task<bool> DeleteBookingAsync(int id , string userId);
        Task<bool> DeleteAllBookingsForUserAsync(string userId);
        Task<IEnumerable<BookingResponseDto>> GetBookingsByGuestId(string guestId);
        Task<IEnumerable<BookingResponseDto>> GetActiveBookings();
        // Fro Room
        Task <IEnumerable<BookingResponseByRoomIDDto?>?> GetBookingByRoomIdAsync(int id);

        //For Admin
        Task<DashboardStatsDto> GetStats();
        Task<IEnumerable<BookingDashboardDto>>GetAllBookingAsync();

        Task<BookingResponseDto?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDashboardDto>> GetLatestBookings();
        Task<bool> UpdateStatusForAdmin(int id, BookingStatus status);
        Task<bool> UpdateBookingForAdminAsync(int id, UpdateBookingDto updatedBooking);
        Task<bool> DeleteBookingForAdminAsync(int id);
        public  Task ConfirmBookingAfterPaymentAsync(int bookingId);
    }
}
