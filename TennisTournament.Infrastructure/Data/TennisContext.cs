using Microsoft.EntityFrameworkCore;
using TennisTournament.Entities;
using TennisTournament.Seedwork;

namespace TennisTournament.Data
{
    public class TennisContext : DbContext
    {
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Court> Courts => Set<Court>();
        public DbSet<Match> Matchs => Set<Match>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Press> Presses => Set<Press>();
        public DbSet<Referee> Referees => Set<Referee>();
        public DbSet<Result> Results => Set<Result>();
        public DbSet<Tournament> Tournaments => Set<Tournament>();


        // Constructeur
        public TennisContext(DbContextOptions<TennisContext> options) : base(options)
        {
        }

        // Model Builder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Admin>()
                .Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .HasMaxLength(100)
                .IsRequired();



            modelBuilder.Entity<Referee>()
                .Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Referee>()
                .Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Referee>()
                .Property(e => e.Sexe)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Referee>()
                .Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsRequired();



            modelBuilder.Entity<Press>()
                .Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Press>()
                .Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Press>()
                .Property(e => e.Password)
                .HasMaxLength(100)
                .IsRequired();



            modelBuilder.Entity<Player>()
                .Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Player>()
                .Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Player>()
                .Property(e => e.Sexe)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Player>()
                .Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsRequired();



            modelBuilder.Entity<Tournament>()
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();



            modelBuilder.Entity<Court>()
               .Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired();
            modelBuilder.Entity<Court>()
                .Property(e => e.Number)
                .IsRequired();



            modelBuilder.Entity<Match>()
                .HasOne(e => e.FirstPlayer)
                .WithMany()
                .HasPrincipalKey(k => k.ID)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Match>()
                .HasOne(e => e.SecondPlayer)
                .WithMany()
                .HasPrincipalKey(k => k.ID)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Match>()
                .Property(e => e.StartingDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");



            modelBuilder.Entity<Result>()
                .Property(e => e.Scores)
                .IsRequired();
            modelBuilder.Entity<Result>()
                .Property(e => e.EndingDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");



            modelBuilder.Entity<Admin>()
                .HasData(
                new { ID = 1, LastName = "AdminL", FirstName = "AdminF", Password = "Password" });

            modelBuilder.Entity<Press>()
                .HasData(
                new { ID = 1, LastName = "PressL", FirstName = "PressF", Password = "Password" });

            modelBuilder.Entity<Referee>()
                .HasData(
                new { ID = 1, LastName = "ArbitreL", FirstName = "ArbitreF", Sexe = Sexe.M, Nationality = Nationality.Spain });

            modelBuilder.Entity<Player>()
                .HasData(
                new { ID = 1, LastName = "CLAVIER", FirstName = "Quentin", Sexe = Sexe.M, Nationality = Nationality.France },
                new { ID = 2, LastName = "LETOURNEUR", FirstName = "Hugo", Sexe = Sexe.M, Nationality = Nationality.France });

            modelBuilder.Entity<Tournament>()
                .HasData(
                new { ID = 1, Name = "Test" });

            modelBuilder.Entity<Court>()
                .HasData(
                 new { ID = 1, Name = "CourtTest", Number = 111 });

            modelBuilder.Entity<Match>()
                .HasData(
                new { ID = 1, FirstPlayerID = 1, SecondPlayerID = 2, RefereeID = 1, CourtID = 1, TournamentID = 1, AdminID = 1 });

            modelBuilder.Entity<Result>()
                .HasData(
                new { ID = 1, MatchID = 1, Scores = "6-2, 6-0, 6-1", PlayerID = 1 });
        }

    }
}
