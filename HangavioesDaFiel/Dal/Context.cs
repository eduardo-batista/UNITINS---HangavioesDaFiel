using HangavioesDaFiel.Models;
using Microsoft.EntityFrameworkCore;

namespace HangavioesDaFiel.Dal
{
    public class Context : DbContext
    {
        internal object users;

        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Airplane> Airplanes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<HangavioesDaFiel.Models.Garage>? Garage { get; set; }

        public DbSet<HangavioesDaFiel.Models.Employee>? Employee { get; set; }

        public DbSet<HangavioesDaFiel.Models.Pilot>? Pilot { get; set; }

        public DbSet<HangavioesDaFiel.Models.Administrator>? Administrator { get; set; }

        public DbSet<HangavioesDaFiel.Models.Team>? Team { get; set; }

        public DbSet<HangavioesDaFiel.Models.Travel>? Travel { get; set; }
    }
}