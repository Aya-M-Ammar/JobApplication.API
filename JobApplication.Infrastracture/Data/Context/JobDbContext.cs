using JobApplication.Domain;
using JobApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace JobApplication.Infrastracture.Data.Context
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
        }

      

        public DbSet<JobCandidateApplication> Applications { get; set; } = default!;
        public DbSet<Candidate> Candidates { get; set; } = default!;
        public DbSet<Job> Jobs { get; set; } = default!;
    }
}
