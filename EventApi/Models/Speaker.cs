using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Models
{
    [Table("Speaker")]
    public class Speaker
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [MaxLength(2000)]
        public string? Bio { get; set; }

        public ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
    }
}