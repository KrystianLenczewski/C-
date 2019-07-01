using System;
using System.IO;
using MemeGenerator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemeGenerator.Models
{
    public partial class memyContext : DbContext
    {
        public memyContext(DbContextOptions<memyContext> options)
              : base(options)
        {

        }
       
        public memyContext() { }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=den1.mssql7.gear.host;Initial Catalog=memedb;User ID=memedb;Password=Cz88?6P9h1-X");
            }
        }

        public DbSet<Marking> Marking { get; set; }
        public DbSet<Memy> Memy { get; set; }
        public DbSet<MemeReports> MemeReports { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}