using System;
using System.Collections.Generic;

namespace EventApi.DTOs
{
    public class ParticipationReportDto
    {
        public int ParticipantId { get; set; }
        public string ParticipantName { get; set; } = null!;
        public List<ReportEventDto> Events { get; set; } = new();
    }

    public class ReportEventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<SpeakerDto> Speakers { get; set; } = new();
    }
}