using HotelServices.Implementation;
using HotelServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Stripe;
using Stripe.Checkout;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly IBookingService _bookingService;
    private readonly IConfiguration _config;

    public PaymentController(
        PaymentService paymentService,
        IBookingService bookingService,
        IConfiguration config)
    {
        _paymentService = paymentService;
        _bookingService = bookingService;
        _config = config;
    }

    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentRequest request)
    {
        // ⭐ تأكد إن الحجز لسه صالح
        var booking = await _bookingService.GetBookingByIdAsync(request.BookingId);

        if (booking == null)
            return NotFound("Booking not found");

        if (booking.Status != "Pending" ||
            booking.ExpiresAt == null ||
            booking.ExpiresAt < DateTime.Now)
        {
            return BadRequest("Booking expired or invalid");
        }

        var session = _paymentService.CreateCheckoutSession(
            request.BookingId,
            request.HotelName,
            request.Price,
            request.SuccessUrl,
            request.CancelUrl
        );

        return Ok(new { url = session.Url });
    }

    // ================================
    // Stripe Webhook (أهم حتة 🔥)
    // ================================
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var endpointSecret = _config["Stripe:WebhookSecret"];

        Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                endpointSecret
            );
        }
        catch
        {
            return BadRequest();
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

            if (session?.Metadata?.ContainsKey("BookingId") == true)
            {
                var bookingId = int.Parse(session.Metadata["BookingId"]);

                await _bookingService.ConfirmBookingAfterPaymentAsync(bookingId);
            }
        }

        return Ok();
    }


}

public class PaymentRequest
{
    public int BookingId { get; set; }   // ⭐ ربط الدفع بالحجز
    public string HotelName { get; set; }
    public decimal Price { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}
