using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// This controller handles the paymentGateway Integration.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPayUPaymentService _payUPaymentService;


        public PaymentController(IPayUPaymentService payUPaymentService)
        {
            _payUPaymentService = payUPaymentService;
        }

        [HttpGet("get-user-payment-details")]
        public IActionResult GetUserPaymentDetails(int accountId)
        {
            var response = _payUPaymentService.getUserPaymentDetails(accountId);
            return Ok(response);
        }




    }
}
