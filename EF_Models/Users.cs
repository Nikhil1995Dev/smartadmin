using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class Users
    {
        public Users()
        {
            UserProjectMapping = new HashSet<UserProjectMapping>();
            UserTimesheet = new HashSet<UserTimesheet>();
        }

        public long Id { get; set; }
        public long ClientId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }
        public int AccessLevel { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string WorkTelephone { get; set; }
        public string Designation { get; set; }
        public bool Enabled { get; set; }
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public string Notes { get; set; }
        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long? ManagerId { get; set; }
        public long? SupervisorId { get; set; }
        public long? BranchId { get; set; }
        public string ProfilePic { get; set; }
        public long? RoleId { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
        public long? CustomInt1 { get; set; }
        public long? CustomInt2 { get; set; }
        public long? CustomInt3 { get; set; }
        public long? CustomInt4 { get; set; }
        public long? CustomInt5 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? AccessStartDate { get; set; }
        public DateTime? AccessEndDate { get; set; }
        public long SubscriptionId { get; set; }
        public double? HourlyRate { get; set; }
        public string TmpPassword { get; set; }
        public string Otp { get; set; }
        public DateTime? OtpCreatedAt { get; set; }

        public virtual Client Client { get; set; }
        public virtual Roles Role { get; set; }
        public virtual SubscriptionPlans Subscription { get; set; }
        public virtual ICollection<UserProjectMapping> UserProjectMapping { get; set; }
        public virtual ICollection<UserTimesheet> UserTimesheet { get; set; }
    }
}
