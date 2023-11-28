using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class AMT_DBContext : DbContext
    {
        public AMT_DBContext()
        {
        }

        public AMT_DBContext(DbContextOptions<AMT_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientCustomers> ClientCustomers { get; set; }
        public virtual DbSet<ClientProjects> ClientProjects { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<ProjectModules> ProjectModules { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SubscriptionPlans> SubscriptionPlans { get; set; }
        public virtual DbSet<UserProjectMapping> UserProjectMapping { get; set; }
        public virtual DbSet<UserTimesheet> UserTimesheet { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=mssql.algomonk.net;Database=PMTool;MultipleActiveResultSets=True;uid=justdb;pwd=AMT@269;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branches>(entity =>
            {
                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Town)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branches_Client");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.AccountNo).HasMaxLength(30);

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subdomain)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ThemeId).HasColumnName("Theme_id");

                entity.Property(e => e.Town)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClientCustomers>(entity =>
            {
                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Town)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientCustomers)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCustomers_Client");
            });

            modelBuilder.Entity<ClientProjects>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProjectEndDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ProjectStartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientProjects)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientProjects_Client");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ClientProjects)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientProjects_ClientCustomers");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentDesc).HasMaxLength(4000);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Client");
            });

            modelBuilder.Entity<ProjectModules>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectModules)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectModules_ClientProjects");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RoleTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles_Client");
            });

            modelBuilder.Entity<SubscriptionPlans>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyType).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(4000);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserProjectMapping>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.UserProjectMapping)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_UserProjectMapping_Client");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UserProjectMapping)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_UserProjectMapping_ClientCustomers");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.UserProjectMapping)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_UserProjectMapping_ClientProjects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProjectMapping)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserProjectMapping_Users");
            });

            modelBuilder.Entity<UserTimesheet>(entity =>
            {
                entity.Property(e => e.ApprovalStatus).HasMaxLength(400);

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.UserTimesheet)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTimesheet_Client");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UserTimesheet)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTimesheet_ClientCustomers");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.UserTimesheet)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTimesheet_ProjectModules");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.UserTimesheet)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTimesheet_ClientProjects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTimesheet)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTimesheet_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.AccessEndDate).HasColumnType("datetime");

                entity.Property(e => e.AccessStartDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyName).HasMaxLength(255);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomField1).HasMaxLength(4000);

                entity.Property(e => e.CustomField2).HasMaxLength(4000);

                entity.Property(e => e.CustomField3).HasMaxLength(4000);

                entity.Property(e => e.CustomField4).HasMaxLength(4000);

                entity.Property(e => e.CustomField5).HasMaxLength(4000);

                entity.Property(e => e.DepartmentName).HasMaxLength(500);

                entity.Property(e => e.Designation).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Forename).HasMaxLength(500);

                entity.Property(e => e.Middlename).HasMaxLength(500);

                entity.Property(e => e.Otp).HasMaxLength(6);

                entity.Property(e => e.OtpCreatedAt).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ProfilePic).HasMaxLength(500);

                entity.Property(e => e.Surname).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TmpPassword).HasMaxLength(400);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.WorkTelephone).HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Client");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Roles");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_SubscriptionPlans");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
