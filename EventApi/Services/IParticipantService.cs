using System.Collections.Generic;
using System.Threading.Tasks;
using EventApi.DTOs;

namespace EventApi.Services
{
    public interface IParticipantService
    {
        Task<ParticipantDto> CreateParticipantAsync(CreateParticipantDto dto);
        Task<List<ParticipantDto>> GetAllParticipantsAsync();
    }
}