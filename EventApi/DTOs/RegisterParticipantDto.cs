using System.ComponentModel.DataAnnotations;

namespace EventApi.DTOs
{
    public class RegisterParticipantDto
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int ParticipantId { get; set; }
    }
}