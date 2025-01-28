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
                        UnitAmount = (long)(product.Price * 100), // Ensure dollars are converted to cents
                    },
                    Quantity = 1,
                },
            },
                    Mode = "payment",
                    SuccessUrl = "https://localhost:44386/success",
                    CancelUrl = "https://localhost:44386/cancel",
                };

                Console.WriteLine("Session options created successfully.");

                var service = new SessionService();
                var session = service.Create(options);

                Console.WriteLine($"Stripe session created successfully: ID={session.Id}, URL={session.Url}");
                return session;
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe error: {ex.StripeError?.Message}, Type: {ex.StripeError?.Type}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
