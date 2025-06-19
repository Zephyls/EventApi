using EventApi.Data;
using EventApi.DTOs;
using EventApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApi.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly EventDbContext _db;
        public SpeakerService(EventDbContext db) => _db = db;

        public async Task<SpeakerDto> CreateSpeakerAsync(CreateSpeakerDto dto)
        {
            var speaker = new Speaker
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Bio = dto.Bio
            };
            _db.Speakers.Add(speaker);
            await _db.SaveChangesAsync();
            return new SpeakerDto
            {
                Id = speaker.Id,
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                Bio = speaker.Bio
            };
        }

        public async Task<List<SpeakerDto>> GetAllSpeakersAsync()
        {
            var list = await _db.Speakers.ToListAsync();
            return list.Select(s => new SpeakerDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Bio = s.Bio
            }).ToList();
        }
    }
}