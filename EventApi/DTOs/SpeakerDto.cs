using System;
namespace EventApi.DTOs
{
    public class SpeakerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Bio { get; set; }
    }
}