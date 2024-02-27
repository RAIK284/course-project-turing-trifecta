using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<GameSession> GameSessions { get; set; }

    public DbSet<GameSessionMember> GameSessionMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GameSessionMember>(x => x.HasKey(aa => new { aa.UserID, aa.GameSessionID }));

        builder.Entity<GameSessionMember>()
            .HasOne(u => u.User)
            .WithMany(a => a.GameSessions)
            .HasForeignKey(aa => aa.UserID);

        builder.Entity<GameSessionMember>()
            .HasOne(u => u.GameSession)
            .WithMany(a => a.Members)
            .HasForeignKey(aa => aa.GameSessionID);
    }
}