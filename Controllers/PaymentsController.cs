using Microsoft.AspNetCore.Mvc;
using StripeBlazorApp.Models;
using StripeBlazorApp.Services;
using System;

namespace StripeBlazorApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly StripeService _stripeService;

        public PaymentsController(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] Product product)
        {
            Console.WriteLine($"Received API request - Product: {product?.Name}, Price: {product?.Price}");

            if (product == null)
            {
                Console.WriteLine("Error: Product object is null.");
                return BadRequest(new { error = "Invalid product data. Product object is null." });
            }

            if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                Console.WriteLine($"Error: Invalid product data received. Name: {product?.Name}, Price: {product?.Price}");
                return BadRequest(new { error = "Invalid product data. Ensure the name and price are correct." });
            }

            // Convert Price to cents
            long priceInCents = (long)(product.Price * 100);
            Console.WriteLine($"Formatted Price for Stripe: {priceInCents} cents");


            try
            {
                var session = _stripeService.CreateCheckoutSession(new Product
                {
                    Name = product.Name,
                    Price = priceInCents // Use cents instead of decimal
                });

                return Ok(new { sessionUrl = session.Url });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating Stripe session: {ex.Message}");
                return BadRequest(new { error = "Failed to create Stripe session.", details = ex.Message });
            }
        }
    }
}
