using Stripe.Checkout;
using StripeBlazorApp.Models;

namespace StripeBlazorApp.Services
{
    public class StripeService
    {
        public Session CreateCheckoutSession(Product product)
        {
            Console.WriteLine($"Creating Stripe session for Product: {product.Name}, Price: {product.Price}");

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
                    UnitAmount = (long)(product.Price * 100), // Ensure this calculation is correct
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

            Console.WriteLine($"Stripe session created: {session.Id}, URL: {session.Url}");
            return session;
        }
    }
}
