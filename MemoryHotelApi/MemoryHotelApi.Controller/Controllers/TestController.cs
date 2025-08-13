using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("TestCICD")]
        public IActionResult TestCICD()
        {
            // This endpoint is used to test the CI/CD pipeline
            return Ok("CI/CD pipeline is working!");
        }
    }
}
