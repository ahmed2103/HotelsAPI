using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace HotelServices.Implementation
{
    public class PaymentService
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {
            _config = config;
            Stripe.StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        public Session CreateCheckoutSession(
            int bookingId,
            string hotelName,
            decimal price,
            string successUrl,
            string cancelUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = price * 100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = hotelName
                            },
                        },
                        Quantity = 1,
                    },
                },

                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,

                // ⭐ ربط Stripe بالحجز
                Metadata = new Dictionary<string, string>
                {
                    { "BookingId", bookingId.ToString() }
                }
            };

            var service = new SessionService();
            var session = service.Create(options);
            return session;
        }
    }
}
