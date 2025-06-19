using System.ComponentModel.DataAnnotations;

namespace EventApi.DTOs
{
    public class CreateSpeakerDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [MaxLength(2000)]
        public string? Bio { get; set; }
    }
}