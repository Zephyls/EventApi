using System.Collections.Generic;
using System.Threading.Tasks;
using EventApi.DTOs;

namespace EventApi.Services
{
    public interface ISpeakerService
    {
        Task<SpeakerDto> CreateSpeakerAsync(CreateSpeakerDto dto);
        Task<List<SpeakerDto>> GetAllSpeakersAsync();
    }
}