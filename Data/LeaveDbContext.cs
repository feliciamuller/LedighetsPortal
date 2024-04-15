using LedighetsPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedighetsPortal.Data
{
    public class LeaveDbContext : DbContext 
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRegistration> LeaveRegistrations { get; set; }
    }
}
