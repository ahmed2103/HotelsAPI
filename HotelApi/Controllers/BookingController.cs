using HotelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Booking;
using Models.Entities;

[Authorize] 
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    
    private string UserId => User.FindFirst("uid")?.Value;

    //  User Actions 

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingService.GetAllBookingsAsync(UserId);
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id, UserId);
        return booking == null ? NotFound("Booking Not Found") : Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDTO bookingDto)
    {
        
        var createdBooking = await _bookingService.CreateBookingAsync(bookingDto, UserId);
        return CreatedAtAction(nameof(GetById), new { id = createdBooking.BookingId }, createdBooking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookingDto updatedBooking)
    {
        var result = await _bookingService.UpdateBookingAsync(id, updatedBooking, UserId);
        return result ? Ok("Booking Updated Successfully") : NotFound("Booking Not Found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _bookingService.DeleteBookingAsync(id, UserId);
        return result ? Ok("Booking Deleted Successfully") : NotFound("Booking Not Found");
    }


    [HttpDelete("AllForUser")]
    public async Task<IActionResult> DeleteAllForUser()
    {
        var result = await _bookingService.DeleteAllBookingsForUserAsync(UserId);

        if (!result)
        {
            return NotFound(new { message = "No bookings found for this user." });
        }

        
        return Ok(new { message = "All bookings deleted successfully." });
    }

    //  Room Actions 

    [HttpGet("room/{id}")]
    public async Task<IActionResult> GetByRoomId(int id)
    {
        var booking = await _bookingService.GetBookingByRoomIdAsync(id);
        return booking == null ? NotFound("No Booking Found for this Room") : Ok(booking);
    }
    // Admin Actions 

    [Authorize(Roles ="Admin")]
    [HttpGet("admin/all")]
    public async Task<IActionResult> GetAllBookingsForAdmin() => Ok(await _bookingService.GetAllBookingAsync());

    [Authorize(Roles ="Admin")]
    [HttpGet("admin/stats")]
    public async Task<IActionResult> GetStats() => Ok(await _bookingService.GetStats());

    [Authorize(Roles = "Admin")]
    [HttpGet("admin/AllLatestBooking")]
    public async Task<IActionResult> GetAdminDashboard() => Ok(await _bookingService.GetLatestBookings());

    [Authorize(Roles = "Admin")]
    [HttpPut("admin/status/{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] BookingStatus status)
    {
        var result = await _bookingService.UpdateStatusForAdmin(id, status);
        return result ? Ok("Status Updated") : NotFound();
    }
}