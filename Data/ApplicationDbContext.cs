using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Models;

namespace SmartAdmin.WebUI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public DbSet<EmployeeMasterArchieve> EmployeeMasterArchieves { get; set; }
        public DbSet<Timing> Timings { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<ContentManager> CMS { get; set; }
        public DbSet<ImageGallary> ImageGallary { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<HraAssessment> HraAssessments { get; set; }
        public DbSet<BroadCast> BroadCasts { get; set; }
        public DbSet<AppoinmentCategory> AppoinmentCategories { get; set; }
        public DbSet<AppointmentCategoryMaster> AppointmentCategoryMasters{ get; set; }
        public DbSet<ContactU> ContactUs { get; set; }
        public DbSet<OherBooking> OherBookings { get; set; }
        public DbSet<AppointmentDocument> AppointmentDocuments { get; set; }
        public virtual DbSet<ClientDomains> ClientDomains { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
