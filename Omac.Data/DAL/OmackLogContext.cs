using Microsoft.EntityFrameworkCore;
using Omack.Data.LogModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.DAL
{
    public class OmackLogContext : DbContext
    {
        public OmackLogContext(DbContextOptions<OmackLogContext> options) : base(options)
        {          
        }
        public DbSet<ApiLog> ApiLog { get; set; }
        public DbSet<WebLog> WebLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
