using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HanDesign.ApiGateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string GetDemo()
        {
            return "afafafaf";
        }
    }
}
