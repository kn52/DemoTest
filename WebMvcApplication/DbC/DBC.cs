using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcApplication.Models.Mgmt;

namespace WebMvcApplication.DbC
{
    public class DBC : DbContext
    {
        public DBC(DbContextOptions<DBC> options) : base(options)
        {
        }

        //Customer
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<SDescription> SDescriptions { get; set; }
        public DbSet<PDescription> PDescriptions { get; set; }
    }
}
