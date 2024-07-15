using Entities.Configuration;
using Entities.Model;
using Entities.Model.DepartamentsModel;
using Entities.Model.FileModel;
using Entities.Model.StatusModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {      

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());


            modelBuilder.Entity<Files>().HasIndex(f => f.title).IsUnique();
            modelBuilder.Entity<User>().HasIndex(f => f.login).IsUnique();

            base.OnModelCreating(modelBuilder);

        }
       




        public DbSet<Files> files_2024parent { get; set; }
        public DbSet<Status> statuses_2024parent { get; set; }
        public DbSet<Departament> departament_2024parent { get; set; }
        public DbSet<User> users_2024parent { get; set; }
        public DbSet<UserType> user_types_2024parent { get; set; }


    }



}
