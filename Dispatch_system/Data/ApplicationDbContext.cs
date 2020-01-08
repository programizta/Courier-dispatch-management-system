using System;
using System.Collections.Generic;
using System.Text;
using Dispatch_system.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dispatch_system.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<ParcelStatus> ParcelStatuses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<ParcelHistory> ParcelHistories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
