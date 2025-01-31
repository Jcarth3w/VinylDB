using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DomainModel;

namespace DomainModel
{
    public interface IDataContext
    {
        public DbSet<Record> Record {get; set;}

        public DbSet<Artist> Artist {get; set;}
    }
}