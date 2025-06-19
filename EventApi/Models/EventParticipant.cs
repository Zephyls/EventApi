using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventApi.Models
{
    [Table("EventParticipant")]
    public class EventParticipant
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; } = null!;

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}