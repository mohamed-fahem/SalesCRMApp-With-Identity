
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCRM.Models;
using System.Collections.Generic;

namespace SCRM.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<SalesLeadEntity> salesLead { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
    }
}
