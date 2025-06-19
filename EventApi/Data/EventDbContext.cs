using Microsoft.EntityFrameworkCore;
using EventApi.Models;

namespace EventApi.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Speaker> Speakers { get; set; } = null!;
        public DbSet<Participant> Participants { get; set; } = null!;
        public DbSet<EventSpeaker> EventSpeakers { get; set; } = null!;
        public DbSet<EventParticipant> EventParticipants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>().ToTable("Event");
            builder.Entity<Speaker>().ToTable("Speaker");
            builder.Entity<Participant>().ToTable("Participant");
            builder.Entity<EventSpeaker>().ToTable("EventSpeaker");
            builder.Entity<EventParticipant>().ToTable("EventParticipant");

            // Composite keys
            builder.Entity<EventSpeaker>().HasKey(es => new { es.EventId, es.SpeakerId });
            builder.Entity<EventParticipant>().HasKey(ep => new { ep.EventId, ep.ParticipantId });

            // Relations
            builder.Entity<EventSpeaker>()
                .HasOne(es => es.Event)
                .WithMany(e => e.EventSpeakers)
                .HasForeignKey(es => es.EventId);

            builder.Entity<EventSpeaker>()
                .HasOne(es => es.Speaker)
                .WithMany(s => s.EventSpeakers)
                .HasForeignKey(es => es.SpeakerId);

            builder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventParticipants)
                .HasForeignKey(ep => ep.EventId);

            builder.Entity<EventParticipant>()
                .HasOne(ep => ep.Participant)
                .WithMany(p => p.EventParticipants)
                .HasForeignKey(ep => ep.ParticipantId);

            // Prevent speaker in two events same time via unique index
            builder.Entity<EventSpeaker>()
                .HasIndex(es => new { es.SpeakerId, es.EventId })
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}