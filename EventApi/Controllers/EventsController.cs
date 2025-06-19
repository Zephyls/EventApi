using Microsoft.AspNetCore.Mvc;
using EventApi.DTOs;
using EventApi.Services;
using System.Threading.Tasks;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;
        public EventsController(IEventService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ev = await _service.CreateEventAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            var list = await _service.GetUpcomingEventsAsync();
            return Ok(list);
        }

        [HttpPost("{eventId}/speakers/{speakerId}")]
        public async Task<IActionResult> AssignSpeaker(int eventId, int speakerId)
        {
            try
            {
                await _service.AssignSpeakerAsync(eventId, speakerId);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> RegisterParticipant(int eventId, int participantId)
        {
            try
            {
                await _service.RegisterParticipantAsync(eventId, participantId);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> CancelRegistration(int eventId, int participantId)
        {
            try
            {
                await _service.CancelRegistrationAsync(eventId, participantId);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("participants/{participantId}/report")]
        public async Task<IActionResult> ParticipationReport(int participantId)
        {
            var report = await _service.GetParticipationReportAsync(participantId);
            return Ok(report);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // optional: implement if needed
            return NoContent();
        }
    }
}