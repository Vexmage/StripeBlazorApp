using Stripe.Checkout;
using StripeBlazorApp.Models;

namespace StripeBlazorApp.Services
{
    public class StripeService
    {
        public Session CreateCheckoutSession(StripeBlazorApp.Models.Product product)
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
                            UnitAmount = (long)(product.Price * 100),
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:5001/success",
                CancelUrl = "https://localhost:5001/cancel",
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}
