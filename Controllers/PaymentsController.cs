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
            var session = _stripeService.CreateCheckoutSession(product);
            return Ok(new { sessionUrl = session.Url });
        }
    }
}
