using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.Receptionist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistController : ControllerBase
    {
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] RequestCreateOrderDto request)
        {
            // Logic to create an order
            // This is a placeholder; actual implementation will depend on your business logic
            return Ok();
        }
    }
}
