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

        public DataContext()
        {
            DbPath = "collection.db";
        }
    }
}