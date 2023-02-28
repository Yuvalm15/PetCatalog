using PetShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace PetShopProject.DAL
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new { Id = 1, CategoryName = "Fish"},
                new { Id = 2, CategoryName = "Mamal" },
                new { Id = 3, CategoryName = "Birds" },
                new { Id = 4, CategoryName = "Reptiles" },
                new { Id = 5, CategoryName = "Amphibians" }
            );

            modelBuilder.Entity<Animal>().HasData(
                new { Id = 1, Name = "Steve", CategoryId = 1, Age = 9 , ImagePath = "Fish1.jpg", Description = "Steve is a Fish"},
                new { Id = 2, Name = "Adam", CategoryId = 2, Age = 8, ImagePath = "Mamal1.jpg", Description = "Adam is a Mamal" },
                new { Id = 3, Name = "George", CategoryId = 3, Age = 3, ImagePath = "Bird1.jpg", Description = "George is a bird" },
                new { Id = 4, Name = "Birb", CategoryId = 5, Age = 2, ImagePath = "Amph1.jpg", Description = "Birb is not a bird" },
                new { Id = 5, Name = "Donke", CategoryId = 3, Age = 8, ImagePath = "Bird2.jpg", Description = "Donke is surprisingly a bird" },
                new { Id = 6, Name = "Chad", CategoryId = 2, Age = 69, ImagePath = "Mamal2.jpg", Description = "Chad is a just a chad" },
                new { Id = 7, Name = "Lizzy", CategoryId = 4, Age = 3, ImagePath = "Reptile1.jpg", Description = "Lizzy wizzy makes you dizzy" },
                new { Id = 8, Name = "Nado", CategoryId = 1, Age = 3, ImagePath = "Shark1.jpg", Description = "SharkNado is coming, RUN!" }


            );
        }
    }
}
