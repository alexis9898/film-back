using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class FilmAppContext: IdentityDbContext<AppUser>
    {
        public FilmAppContext(DbContextOptions <FilmAppContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=FilmsApp;Integrated Security=True");
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<FilmCategory> FilmCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var Films= modelBuilder.Entity<Film>();

            //var filmCategory = modelBuilder.Entity<FilmCategory>();
            var film = modelBuilder.Entity<Film>();

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasKey(c => new { c.FilmId, c.CategoryId });
            });


            film
                .HasMany(f => f.Categories)
                .WithMany(c => c.Films)
                .UsingEntity<FilmCategory>(
                    x => x.HasOne(f => f.Category)
                    .WithMany(x=>x.FilmCategories).HasForeignKey(c => c.CategoryId),
                    x => x.HasOne(f => f.Film)
                    .WithMany(x=>x.FilmCategories).HasForeignKey(c => c.FilmId));
                
                //.UsingEntity(fc => fc.ToTable("CategoryFilm"));

            //filmCategory
            //    .HasKey(fc => new { fc.CategoryId, fc.FilmId });

            //filmCategory
            //    .HasOne(fc => fc.Category)
            //    .WithMany(c => c.FilmCategories)
            //    .HasForeignKey(fc => fc.CategoryId);

            //filmCategory
            //    .HasOne(fc => fc.Film)
            //    .WithMany(f => f.FilmCategories)
            //    .HasForeignKey(fc => fc.FilmId);


            var Images = modelBuilder.Entity<Image>();
            Images
                .HasOne(f => f.Films)
                .WithMany(i => i.Images)
                .HasForeignKey(f => f.FilmId);
                //.OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
