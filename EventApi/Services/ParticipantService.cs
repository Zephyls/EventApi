using EventApi.Data;
using EventApi.DTOs;
using EventApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApi.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly EventDbContext _db;
        public ParticipantService(EventDbContext db) => _db = db;

        public async Task<ParticipantDto> CreateParticipantAsync(CreateParticipantDto dto)
        {
            var participant = new Participant
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            _db.Participants.Add(participant);
            await _db.SaveChangesAsync();
            return new ParticipantDto
            {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                Email = participant.Email
            };
        }

        public async Task<List<ParticipantDto>> GetAllParticipantsAsync()
        {
            var list = await _db.Participants.ToListAsync();
            return list.Select(p => new ParticipantDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email
            }).ToList();
        }
    }
}