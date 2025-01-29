using Microsoft.AspNetCore.Mvc;
using StripeBlazorApp.Models;
using StripeBlazorApp.Services;

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
        public IActionResult CreateCheckoutSession([FromBody] StripeBlazorApp.Models.Product product)
        {
            Console.WriteLine($"Received Product: {product?.Name}, Price: {product?.Price}");

            if (string.IsNullOrEmpty(product?.Name) || product.Price <= 0)
            {
                Console.WriteLine("Invalid product data received.");
                return BadRequest("Invalid product data.");
            }

            try
            {
                var session = _stripeService.CreateCheckoutSession(product);
                return Ok(new { sessionUrl = session.Url });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating Stripe session: {ex.Message}");
                return BadRequest("Failed to create Stripe session.");
            }
        }
    }
}
