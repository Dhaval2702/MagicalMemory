using Microsoft.AspNetCore.Mvc;
using System;
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


        /// <summary>
        /// Payment Service controller
        /// </summary>
        /// <param name="payUPaymentService">payUPaymentService</param>
        public PaymentController(IPayUPaymentService payUPaymentService)
        {
            _payUPaymentService = payUPaymentService;
        }

        /// <summary>
        /// Get User Payment Details
        /// </summary>
        /// <param name="accountId">account Id</param>
        /// <param name="paymentYear">payment Year</param>
        /// <returns></returns>
        [HttpGet("get-user-payment-details")]
        public IActionResult GetUserPaymentDetails(int accountId, string paymentYear)
        {
            var response = _payUPaymentService.getUserPaymentDetails(accountId, paymentYear);
            return Ok(response);
        }


        /// <summary>
        /// Update Child Payment Detais
        /// </summary>
        /// <param name="accountId">account Id</param>
        /// <param name="childId">child Id</param>
        /// <param name="paymentYear">payment Year</param>
        /// <returns></returns>
        [HttpPost("update-user-payment-details")]
        public IActionResult UpdateChildPaymentDetais(int accountId, Guid childId, string paymentYear)
        {
            var response = _payUPaymentService.UpdateChildPaymentDetaisForSucess(accountId, childId, paymentYear);
            return Ok(response);

        }





    }
}
