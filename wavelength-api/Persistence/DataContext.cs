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

    public DbSet<GameSessionMemberRoundRole> GameSessionMemberRoundRoles { get; set; }

    public DbSet<GameRoundGhostGuess> GameRoundGhostGuesses { get; set; }

    public DbSet<GameRoundOpposingTeamGuess> GameRoundOpposingTeamGuesses { get; set; }

    public DbSet<GameRoundOpposingTeamSelection> GameRoundOpposingTeamSelections { get; set; }

    public DbSet<GameRoundSelectorSelection> GameRoundSelectorSelections { get; set; }

    public DbSet<GameSessionResult> GameSessionResults { get; set; }

    public DbSet<SpectrumCard> SpectrumCards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SeedSpectrumCards();

        builder.Entity<User>()
            .HasIndex(u => u.UserId)
            .IsUnique();
            

        builder.Entity<GameSessionMember>()
            .HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(gsm => gsm.UserId)
            .HasPrincipalKey(u => u.UserId);

        base.OnModelCreating(builder);
    }
}