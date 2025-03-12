using Microsoft.EntityFrameworkCore;
using VotingSystem.API.Models;

namespace VotingSystem.API.Data
{
    public class VotingSystemDbContext : DbContext
    {
        public VotingSystemDbContext(DbContextOptions<VotingSystemDbContext> options) : base(options) { }

        public DbSet<State> States { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<StateResult> StateResults { get; set; }
        public DbSet<NationalResult> NationalResults { get; set; }
        public DbSet<ElectionConfig> ElectionConfig { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<State>().HasIndex(s => s.Name).IsUnique();

            modelBuilder.Entity<Party>().HasIndex(p => p.Name).IsUnique();

            modelBuilder.Entity<Party>().HasIndex(p => p.Symbol).IsUnique();

            modelBuilder.Entity<Candidate>().HasIndex(c => new { c.StateId, c.PartyId }).IsUnique();

            modelBuilder.Entity<ElectionConfig>().HasIndex(e => e.Id).IsUnique();

            modelBuilder.Entity<StateResult>()
                .HasOne(sr => sr.State)
                .WithMany()
                .HasForeignKey(sr => sr.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StateResult>()
                .HasOne(sr => sr.Candidate)
                .WithMany()
                .HasForeignKey(sr => sr.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Voter)
                .WithMany()
                .HasForeignKey(v => v.VoterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Candidate)
                .WithMany()
                .HasForeignKey(v => v.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
