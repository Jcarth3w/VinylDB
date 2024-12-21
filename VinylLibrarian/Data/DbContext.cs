using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DomainModel;

namespace DomainModel
{
    public class DataContext : DbContext
    {
        public DbSet<Record> Record {get; set;}

        public DbSet<Artist> Artist {get; set;}

        public string DbPath {get;}

        public DataContext()
        {
            DbPath = "collection.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}