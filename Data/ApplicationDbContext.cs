using Microsoft.EntityFrameworkCore;
using PolicyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<PolicyMaster> policyMasters { get; set; }
        public DbSet<ConsumerPolicy> consumerPolicies { get; set; }
        
    }
}
