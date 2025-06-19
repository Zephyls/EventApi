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
    public class EventService : IEventService
    {
        private readonly EventDbContext _db;
        public EventService(EventDbContext db) => _db = db;

        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            if(dto.Date < DateTime.UtcNow)
                throw new ArgumentException("Event date cannot be in the past.");
            var ev = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                MaxParticipants = dto.MaxParticipants
            };
            _db.Events.Add(ev);
            await _db.SaveChangesAsync();
            return new EventDto { Id = ev.Id, Title = ev.Title, Description = ev.Description, Date = ev.Date, MaxParticipants = ev.MaxParticipants };
        }

        public async Task AssignSpeakerAsync(int eventId, int speakerId)
        {
            // transaction
            using var tx = await _db.Database.BeginTransactionAsync();
            var ev = await _db.Events.FindAsync(eventId) ?? throw new ArgumentException("Event not found");
            var sp = await _db.Speakers.FindAsync(speakerId) ?? throw new ArgumentException("Speaker not found");
            // check time clash
            var sameTime = await _db.EventSpeakers
                .Include(es => es.Event)
                .AnyAsync(es => es.SpeakerId==speakerId && es.Event.Date==ev.Date);
            if(sameTime) throw new InvalidOperationException("Speaker already assigned to another event at the same time.");
            _db.EventSpeakers.Add(new EventSpeaker{ EventId=eventId, SpeakerId=speakerId });
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }

        public async Task RegisterParticipantAsync(int eventId, int participantId)
        {
            using var tx = await _db.Database.BeginTransactionAsync();
            var ev = await _db.Events.Include(e=>e.EventParticipants).FirstOrDefaultAsync(e=>e.Id==eventId) ?? throw new ArgumentException("Event not found");
            if(ev.EventParticipants.Count >= ev.MaxParticipants)
                throw new InvalidOperationException("Event is full.");
            if(ev.EventParticipants.Any(ep=>ep.ParticipantId==participantId))
                throw new InvalidOperationException("Participant already registered.");
            _db.EventParticipants.Add(new EventParticipant{ EventId=eventId, ParticipantId=participantId, RegistrationDate=DateTime.UtcNow });
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }

        public async Task CancelRegistrationAsync(int eventId, int participantId)
        {
            var ep = await _db.EventParticipants
                .Include(x=>x.Event)
                .FirstOrDefaultAsync(x=>x.EventId==eventId && x.ParticipantId==participantId)
                ?? throw new ArgumentException("Registration not found");
            if(ep.Event.Date.AddHours(-24) < DateTime.UtcNow)
                throw new InvalidOperationException("Cannot cancel within 24h of event.");
            _db.EventParticipants.Remove(ep);
            await _db.SaveChangesAsync();
        }

        public async Task<List<EventAvailabilityDto>> GetUpcomingEventsAsync()
        {
            var now = DateTime.UtcNow;
            var list = await _db.Events
                .Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker)
                .Include(e=>e.EventParticipants)
                .Where(e=>e.Date>now)
                .ToListAsync();
            return list.Select(e=>new EventAvailabilityDto{
                Id=e.Id, Title=e.Title, Date=e.Date,
                Speakers=e.EventSpeakers.Select(es=>new SpeakerDto{ Id=es.SpeakerId, FirstName=es.Speaker.FirstName, LastName=es.Speaker.LastName }).ToList(),
                RegisteredCount=e.EventParticipants.Count, AvailableSeats=e.MaxParticipants-e.EventParticipants.Count
            }).ToList();
        }

        public async Task<ParticipationReportDto> GetParticipationReportAsync(int participantId)
        {
            var p = await _db.Participants.FindAsync(participantId)
                ?? throw new ArgumentException("Participant not found");
            var report = new ParticipationReportDto{ ParticipantId=p.Id, ParticipantName=$"{p.FirstName} {p.LastName}"};
            var regs = await _db.EventParticipants
                .Include(ep=>ep.Event).ThenInclude(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker)
                .Where(ep=>ep.ParticipantId==participantId)
                .ToListAsync();
            report.Events = regs.Select(ep=>new ReportEventDto{
                EventId=ep.EventId, Title=ep.Event.Title, Date=ep.Event.Date,
                Speakers=ep.Event.EventSpeakers.Select(es=>new SpeakerDto{ FirstName=es.Speaker.FirstName, LastName=es.Speaker.LastName }).ToList()
            }).ToList();
            return report;
        }
    }
}