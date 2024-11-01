using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Services;
using RapidPay.Data.Dto;
using RapidPay.Data.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        IAuthenticationService authenticationService;
        public AuthenticateController(IAuthenticationService authenticationService) { 
            this.authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            AuthTicket ticket= await authenticationService.LoginAsync(request.Username, request.Password);
            if(ticket.Authenticated)
                return Ok(new { Token = ticket.Token });            

            return Unauthorized("Invalid username or password");
        }
    }
}
