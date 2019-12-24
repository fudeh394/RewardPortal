using System.Data.Entity;
using RewardSystemWeb.Models;

namespace RewardSystemWeb.Entities
{
    public class Database : DbContext
    {
       


        public Database() : base("name=DbConnectionString")
        {
        }

     
        public Database(string connection)
           : base(string.Format("{0}", connection))
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        //public DbSet<PortalSetting> PortalSettings { set; get; }
        //public DbSet<Request> Requests { set; get; }
        //public DbSet<HoRequest> HoRequests { set; get; }
        //public DbSet<VerifiedRecord> VerifiedRecords { set; get; }
        //public DbSet<UploadModel> UploadModels { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<AgentAccount> AgentAccounts { set; get; }
        public DbSet<AgentManager> AgentManagers { set; get; }
        public DbSet<Resource> Resources { set; get; }
        public DbSet<RoleResource> RoleResources { set; get; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Audit> Audits { set; get; }

        public DbSet<Incentive> RewardBands { set; get; }
        




    }
}