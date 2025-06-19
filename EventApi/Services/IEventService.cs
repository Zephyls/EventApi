using System.Collections.Generic;
using System.Threading.Tasks;
using EventApi.DTOs;

namespace EventApi.Services
{
    public interface IEventService
    {
        Task<EventDto> CreateEventAsync(CreateEventDto dto);
        Task AssignSpeakerAsync(int eventId, int speakerId);
        Task RegisterParticipantAsync(int eventId, int participantId);
        Task CancelRegistrationAsync(int eventId, int participantId);
        Task<List<EventAvailabilityDto>> GetUpcomingEventsAsync();
        Task<ParticipationReportDto> GetParticipationReportAsync(int participantId);
    }
}