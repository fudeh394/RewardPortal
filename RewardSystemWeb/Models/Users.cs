using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RewardSystemWeb.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string UserRole { get; set; }
        public string SolId { get; set; }
        public DateTime LastLoginDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public int RequestStatus { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string  CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
    }


}
