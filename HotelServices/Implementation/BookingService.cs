using AutoMapper;
using DataAcess;
using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOS.Booking;
using Models.DTOS.DashboardStats;
using Models.Entities;

namespace HotelServices.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _dbcontext;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper , HotelDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dbcontext= dbContext;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDTO dto, string userId)
        {
            if (dto.CheckInDate >= dto.CheckOutDate)
                throw new ArgumentException("Check-out date must be after check-in date.");

            if (dto.CheckInDate < DateTime.Now.Date)
                throw new ArgumentException("Check-in date cannot be in the past.");

            var existingBookings = await _unitOfWork.Bookings.GetBookingsByRoomIdAsyn(dto.RoomId);

            bool isConflict = existingBookings.Any(b =>
                b.Status != BookingStatus.Cancelled && // مهم
                (dto.CheckInDate < b.CheckOutDate) &&
                (dto.CheckOutDate > b.CheckInDate)
            );

            if (isConflict)
                throw new InvalidOperationException("The room is already booked for the selected dates.");

            var booking = _mapper.Map<Booking>(dto);
            booking.UserId = userId;

            booking.Status = BookingStatus.Pending;
            booking.CreatedAt = DateTime.Now;
            booking.ExpiresAt = DateTime.Now.AddMinutes(15); // ⭐ هنا بقى

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            var room = await _unitOfWork.Rooms.GetByIdAsync(booking.RoomId);
            booking.Room = room;

            return _mapper.Map<BookingResponseDto>(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id, string userId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null || booking.UserId != userId)
                return false;

            _unitOfWork.Bookings.Delete(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(string userId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            var userBookings = bookings.Where(b => b.UserId == userId);

            return _mapper.Map<IEnumerable<BookingResponseDto>>(userBookings);
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(int id, string userId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null || booking.UserId != userId)
                return null;

            return _mapper.Map<BookingResponseDto>(booking);
        }

        public async Task<bool> UpdateBookingAsync(int id, UpdateBookingDto updatedBooking, string userId)
        {
            var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (existingBooking == null || existingBooking.UserId != userId)
                return false;

            _mapper.Map(updatedBooking, existingBooking);

            _unitOfWork.Bookings.Update(existingBooking);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetActiveBookings()
        {
            var bookings = await _unitOfWork.Bookings.GetActiveBookingsAsync();
            return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByGuestId(string guestId)
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsByGuestIdAsync(guestId);
            return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
        }

        public async Task<DashboardStatsDto> GetStats()
        {
              return new DashboardStatsDto
            {
                Rooms = await _dbcontext.Rooms.CountAsync(),
                TotalBookings = await _dbcontext.Bookings.CountAsync(),
                Users = await _dbcontext.Users.CountAsync(),
                Revenue = await _dbcontext.Bookings.Include(b => b.Room).SumAsync(b => EF.Functions.DateDiffDay(b.CheckInDate, b.CheckOutDate) * b.Room.PricePerNight)


            };
        }

        public async Task<IEnumerable<BookingDashboardDto>> GetLatestBookings()
        {
           var bookings = await  _unitOfWork.Bookings.GetLatestBookings();
            return bookings.Select(b=> new BookingDashboardDto
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                GuestName = b.User.FullName,
                Room = b.Room.Type,
                CheckIn = b.CheckInDate,
                CheckOut = b.CheckOutDate,
                Status= b.Status.ToString()



            }).OrderByDescending(b=>b.BookingId).ToList();
        }

        public async Task<IEnumerable<BookingDashboardDto>> GetAllBookingAsync()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            return bookings.Select(b => new BookingDashboardDto
            {
                BookingId = b.BookingId,
                UserId =b.User.Id,
                GuestName = b.User.FullName,
                Room = b.Room.Type,
                CheckIn = b.CheckInDate,
                CheckOut = b.CheckOutDate,
                RoomNumber=b.Room.Number,
                Status = b.Status.ToString()
            }).OrderByDescending(b=>b.BookingId).ToList();


        }

        public async Task<bool> UpdateStatusForAdmin(int id, BookingStatus status)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                return false;
            else
            {
                booking.Status = status;
               await _unitOfWork.CompleteAsync();
                return true;
            }

        }

        public async Task<bool> UpdateBookingForAdminAsync(int id, UpdateBookingDto updatedBooking)
        {
            var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (existingBooking == null)
                return false;
            else
            {
                existingBooking.CheckOutDate = updatedBooking.CheckOutDate;
                existingBooking.CheckInDate = updatedBooking.CheckInDate;
                existingBooking.RoomId = updatedBooking.RoomId;
                await _unitOfWork.CompleteAsync();
                return true;


            }


        }

        public async Task<bool> DeleteBookingForAdminAsync(int id)
        {
            var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (existingBooking == null)
                return false;
            else
            {
                _unitOfWork.Bookings.Delete(existingBooking);
                await _unitOfWork.CompleteAsync();
                return true;
            }
        }

        public async Task<IEnumerable<BookingResponseByRoomIDDto?>> GetBookingByRoomIdAsync(int id)
        {
          var b= await _unitOfWork.Bookings.GetBookingsByRoomIdAsyn(id);
            return  _mapper.Map<IEnumerable<BookingResponseByRoomIDDto?>>(b);
        }

        public async Task ConfirmBookingAfterPaymentAsync(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.Status == BookingStatus.Cancelled)
                throw new Exception("Booking already cancelled");

            booking.Status = BookingStatus.Confirmed;
            booking.ExpiresAt = null;

            await _unitOfWork.CompleteAsync();
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(int id)
        {
            return await _dbcontext.Bookings.Include(b => b.Room).Include(b => b.User)
                .Where(b => b.BookingId == id)
                .Select(b => _mapper.Map<BookingResponseDto>(b))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAllBookingsForUserAsync( string userId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            var userBookings = bookings.Where(b => b.UserId == userId).ToList();
            if (!userBookings.Any())
                return false;
            foreach (var booking in userBookings)
            {
                _unitOfWork.Bookings.Delete(booking);
            }
            await _unitOfWork.CompleteAsync();
            return true;


        }
    }
}
