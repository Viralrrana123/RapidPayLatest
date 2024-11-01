using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Data.Dto;
using RapidPay.Data.Model;
using RapidPay.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RapidPayController : ControllerBase
    {        
        private readonly ICardService cardService;
        private readonly IPaymentService paymentService;
        public RapidPayController(ICardService cardService, IPaymentService paymentService)
        {            
            this.cardService = cardService;
            this.paymentService = paymentService;
        }
        [Authorize]
        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateCard(CardRequest request)
        {
            try
            {
                CardDetails cardDetails = await cardService.CreateCardAsync(request.CustomerId);

                return Ok(cardDetails);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Pay")]
        public async Task<IActionResult> Pay(PaymentRequest request)
        {
            try
            {
                AuthResult result= await paymentService.PayAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetCurrentBalance")]
        public async Task<IActionResult> GetCurrentBalance(string cardNumber)
        {
            try
            {
                BalanceResponse currentBalance = await paymentService.GetCardBalanceAsync(cardNumber);                
                return Ok(currentBalance);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        [Authorize]
        [HttpPost("ProcessEndOfDay")]
        public async Task<IActionResult> ProcessEndOfDay(string cardNumber)
        {
            try
            {
                await paymentService.EndOfDayProcessAsync(cardNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //** No Authentication for the sack of quick transations verification. can be added later**
        //[Authorize]
        [HttpGet("GetCurrentActivities")]
        public async Task<IActionResult> GetCurrentActivities(string cardNumber)
        {
            try
            {
                IEnumerable<Transaction> currentTransactions=await paymentService.GetCurrentTransactionsAsync(cardNumber);                
                return Ok(currentTransactions);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
