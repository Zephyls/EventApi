using System;
using System.Collections.Generic;

namespace EventApi.DTOs
{
    public class EventAvailabilityDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<SpeakerDto> Speakers { get; set; } = new();
        public int RegisteredCount { get; set; }
        public int AvailableSeats { get; set; }
    }
}