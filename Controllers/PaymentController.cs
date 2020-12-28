using Microsoft.AspNetCore.Mvc;
using PaypalExpressCheckout.BusinessLogic.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private IPaypalServices _PaypalServices;

        public PaymentController(IPaypalServices paypalServices)
        {
            _PaypalServices = paypalServices;
        }

        //public IActionResult Index()
        //{
        //    return Ok();
        //}

        [HttpPost("create-payment")]
        public IActionResult CreatePayment()
        {
            var payment = _PaypalServices.CreatePayment(1, "https://localhost:44359/Payment/ExecutePayment", "https://localhost:44359/Payment/Cancel", "sale");

            return new JsonResult(payment);
        }

        [HttpGet("execute-payment")]
        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            var payment = _PaypalServices.ExecutePayment(paymentId, PayerID);

            // Hint: You can save the transaction details to your database using payment/buyer info

            return Ok();
        }

        [HttpGet("execute-sucess")]
        public IActionResult Success()
        {
            return Ok();
        }

        [HttpGet("execute-cancel")]
        public IActionResult Cancel()
        {
            return Cancel();
        }
    }
}
