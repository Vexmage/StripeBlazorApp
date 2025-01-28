using Stripe;
using Stripe.Checkout;
using StripeBlazorApp.Models;

namespace StripeBlazorApp.Services
{
    public class StripeService
    {
        private readonly string _secretKey;

        public StripeService(IConfiguration configuration)
        {
            var secretKey = configuration["Stripe:SecretKey"];
            Console.WriteLine($"Loaded Stripe Secret Key: {secretKey}");

            if (string.IsNullOrEmpty(secretKey) || !secretKey.StartsWith("sk_test_"))
            {
                throw new InvalidOperationException("Stripe test secret key is missing or invalid.");
            }

            StripeConfiguration.ApiKey = secretKey;
        }


        public Session CreateCheckoutSession(StripeBlazorApp.Models.Product product)
        {
            Console.WriteLine($"Creating Stripe session for Product: {product.Name}, Price: {product.Price}");

            try
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
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                        },
                        UnitAmount = (long)(product.Price * 100), // Convert dollars to cents
                    },
                    Quantity = 1,
                },
            },
                    Mode = "payment",
                    SuccessUrl = "https://localhost:44386/success",
                    CancelUrl = "https://localhost:44386/cancel",
                };

                var service = new SessionService();
                var session = service.Create(options);

                Console.WriteLine($"Stripe session created successfully. Session ID: {session.Id}, URL: {session.Url}");
                return session;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Stripe session creation: {ex.Message}");
                throw;
            }
        }
    }
}
