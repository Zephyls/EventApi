using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Models
{
    [Table("Event")]
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }

        public ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
        public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}