using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.DAL
{
    public class OmackContext: IdentityDbContext<User, Role, int>
    {
        //use dbcontextoption configured in startup.cs class
        public OmackContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<Group_User> Group_User { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //give custom names for identity tables, <int>: change default string type to int for primary key
            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken");

        }
    }
}
