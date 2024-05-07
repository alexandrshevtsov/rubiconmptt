using Microsoft.EntityFrameworkCore;
using RubiconMp.Domain;

namespace RubiconMp.Data
{
    public class ApplicationContext : DbContext
    {
        const string CreateIndexStmt = @"
            CREATE SPATIAL INDEX [IX_Rectangles_Area] ON [dbo].[Rectangles] ([Area])
            USING GEOMETRY_GRID
            WITH (BOUNDING_BOX = (0, 0, 1000, 1000), CELLS_PER_OBJECT = 16);";

        const string SeedTestDataStmt = @"
            INSERT INTO Rectangles (Name, Area) VALUES ('Test 1', 'POLYGON((1 1, 1 2, 2 2, 2 1, 1 1))')
            INSERT INTO Rectangles (Name, Area) VALUES ('Test 2', 'POLYGON((1 1, 1 4, 4 4, 4 1, 1 1))')
            INSERT INTO Rectangles (Name, Area) VALUES ('Test 3', 'POLYGON((2 8, 2 13, 7 13, 7 8, 2 8))')";

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            var databaseCreated = Database.EnsureCreated();
            if (databaseCreated)
            {
                Database.ExecuteSqlRaw(CreateIndexStmt);

                var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
                
                if (isDevelopment)
                {
                    Database.ExecuteSqlRaw(SeedTestDataStmt);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rectangle>()
                .Property(e => e.Area)
                .HasColumnType("geometry");
        }

        public DbSet<Rectangle> Rectangles { get; set; }

    }
}
