
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Models
{


    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }
        public string ApprovedBy { get; set; }
        public int RequestStatus { get; set; }
    }

    public class AgentAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AccountName { get; set; }
      
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class AgentManager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AgentManagerName { get; set; }
        public string AgentManagerCode { get; set; }
        public bool IsDeleted { get; set; }

    }

}
