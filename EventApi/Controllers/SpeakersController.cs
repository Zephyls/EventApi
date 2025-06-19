using Microsoft.AspNetCore.Mvc;
using EventApi.DTOs;
using EventApi.Services;
using System.Threading.Tasks;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerService _service;
        public SpeakersController(ISpeakerService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var sp = await _service.CreateSpeakerAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = sp.Id }, sp);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllSpeakersAsync();
            return Ok(list);
        }
    }
}