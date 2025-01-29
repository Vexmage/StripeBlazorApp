using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using StripeBlazorApp.Models; // Ensure correct namespace for Product model

namespace StripeBlazorApp.Services
{
    public class StripeService
    {
        private readonly string _secretKey;

        public StripeService(IConfiguration configuration)
        {
            _secretKey = configuration["Stripe:SecretKey"]
                ?? throw new InvalidOperationException("Stripe Secret Key is missing.");
            StripeConfiguration.ApiKey = _secretKey;
        }

        public Session CreateCheckoutSession(StripeBlazorApp.Models.Product product) // Fully qualify Product
        {
            Console.WriteLine($"✔️ Creating Stripe session for Product: {product.Name}, Price: {product.Price} USD");

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
                                UnitAmount = Convert.ToInt64(product.Price * 100), // Convert dollars to cents
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

                Console.WriteLine($"✔️ Stripe session created successfully. Session ID: {session.Id}, URL: {session.Url}");
                return session;
            }
            catch (StripeException stripeEx)
            {
                Console.WriteLine($"❌ Stripe API Error: {stripeEx.Message}");
                throw new Exception("Stripe API error: " + stripeEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ General Error: {ex.Message}");
                throw new Exception("General error: " + ex.Message);
            }
        }
    }
}
