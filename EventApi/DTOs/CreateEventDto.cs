using System;
using System.ComponentModel.DataAnnotations;

namespace EventApi.DTOs
{
    public class CreateEventDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }
    }
}