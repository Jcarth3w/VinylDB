using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DomainModel;

namespace DomainModel
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Record> Record {get; set;}

        public DbSet<Artist> Artist {get; set;}

        public string DbPath {get;}

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Ensures we only configure if DI hasn't already
            {
                string dbFileName = "collection.db";
                string folderPath = Directory.GetCurrentDirectory();
                string dbPath = Path.Combine(folderPath, dbFileName);

                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

    }
}