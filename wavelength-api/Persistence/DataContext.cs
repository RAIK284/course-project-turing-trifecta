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

    public DbSet<GameRound> GameRounds { get; set; }
    
    public DbSet<GameSessionMemberRoundRole> GameSessionMemberRoundRoles {get; set; }

    public DbSet<GameRoundGhostGuess> GameRoundGhostGuesses { get; set; }

    public DbSet<GameRoundOpposingTeamGuess> GameRoundOpposingTeamGuesses { get; set; }

    public DbSet<GameRoundOpposingTeamSelection> GameRoundOpposingTeamSelections { get; set; }

    public DbSet<GameRoundSelectorSelection> GameRoundSelectorSelections { get; set; }

    public DbSet<GameSessionResult> GameSessionResults { get; set; }

    public DbSet<SpectrumCard> SpectrumCards { get; set; }

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