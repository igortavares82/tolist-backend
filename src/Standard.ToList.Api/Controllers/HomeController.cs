using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Standard.ToList.Api.Controllers
{
    //[Authorize]
    [Route("home")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Welcome");
        }
    }
}

