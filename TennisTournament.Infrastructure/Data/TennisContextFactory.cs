using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TennisTournament.Data
{
    public class SchoolContextFactory : IDesignTimeDbContextFactory<TennisContext>
    {
        public TennisContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TennisContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-LLKN7L7;Database=TennisTournament;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new TennisContext(optionsBuilder.Options);
        }
    }
}
