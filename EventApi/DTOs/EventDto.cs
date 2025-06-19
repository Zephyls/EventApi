using System;
using System.Collections.Generic;

namespace EventApi.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int MaxParticipants { get; set; }
        public List<SpeakerDto> Speakers { get; set; } = new();
        public int RegisteredCount { get; set; }
        public int AvailableSeats => MaxParticipants - RegisteredCount;
    }
}