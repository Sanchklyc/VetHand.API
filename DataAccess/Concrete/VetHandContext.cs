using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class VetHandContext : DbContext
    {
        private IConfiguration _configuration;
        public VetHandContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL_Connection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<EmergencyRequest> EmergencyRequests { get; set; }
        public DbSet<EmergencyRequestVetMapping> EmergencyRequestVetMappings { get; set; }
    }
}
