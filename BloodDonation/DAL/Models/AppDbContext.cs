using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Blood> Blood { get; set; }
        public DbSet<BloodBank> BloodBanks { get; set; }
        public DbSet<BloodTestResult> BloodTestResults { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
    }
}
