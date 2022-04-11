using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public class JobAppsDbContext : DbContext
    {
        public JobAppsDbContext(DbContextOptions<JobAppsDbContext> options)
            : base(options)
        {
        }

        public DbSet<FormInput> FormInputs { get; set; }
        public DbSet<JobApp> JobApps { get; set; }
        public DbSet<JobCriteria> JobCriterias { get; set; }
    }
}
