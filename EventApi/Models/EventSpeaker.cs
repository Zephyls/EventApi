using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Models
{
    [Table("EventSpeaker")]
    public class EventSpeaker
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; } = null!;
    }
}