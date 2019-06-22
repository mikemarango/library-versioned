﻿using Library.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().ToTable("Author").HasKey("Id");
            modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.DateOfBirth).IsRequired();
            modelBuilder.Entity<Author>().Property(a => a.Genre).IsRequired().HasMaxLength(50);
            //modelBuilder.Entity<Author>().HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>().ToTable("Book").HasKey("Id");
            modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.Description).HasMaxLength(500);
            modelBuilder.Entity<Book>().HasOne(b => b.Author).WithMany(b => b.Books).HasForeignKey(b => b.AuthorId);
        }
    }
}
