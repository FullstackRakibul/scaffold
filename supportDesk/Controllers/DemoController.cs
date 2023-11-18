using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace supportDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {

        [HttpGet]
        public string Index() {
            return "This is test api controller"; 
        }
    }
}
