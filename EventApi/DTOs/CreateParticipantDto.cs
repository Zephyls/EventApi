using System.ComponentModel.DataAnnotations;

namespace EventApi.DTOs
{
    public class CreateParticipantDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = null!;
    }
}