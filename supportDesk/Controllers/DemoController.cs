using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using supportDesk.Helper;
using supportDesk.Service;
using System.Diagnostics;
using System.Security.Principal;

namespace supportDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IEmailService emailService;

        public DemoController( IEmailService service) {
            this.emailService =service;
            
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail() {

            try { 
                var mailrequest = new Mailrequest();
                mailrequest.ToEmail = "prematal2828@gmail";
                mailrequest.Subject = "Test Mail 05";
                mailrequest.Body = "Test Mail 01 body from dotnet";

                Console.WriteLine(mailrequest);
                if (emailService != null)
                {
                    await emailService.SendEmailAsync(mailrequest);
                    Console.WriteLine(emailService);
                    return Ok();
                }
                else
                {
                    Console.WriteLine(emailService);
                    return BadRequest("Email service is not initialized.");
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
