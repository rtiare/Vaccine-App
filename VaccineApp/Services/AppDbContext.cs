using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineApp.Models;

namespace VaccineApp.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Vaccine> Vaccines { get; set; }

        public DbSet<Patient> Patients { get; set; }
    }
}
