using Microsoft.AspNetCore.Mvc;
using EventApi.DTOs;
using EventApi.Services;
using System.Threading.Tasks;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _service;
        public ParticipantsController(IParticipantService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParticipantDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var p = await _service.CreateParticipantAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = p.Id }, p);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllParticipantsAsync();
            return Ok(list);
        }
    }
}