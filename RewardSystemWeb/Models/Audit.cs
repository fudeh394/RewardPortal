
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RewardSystemWeb.Models
{

    public  class Audit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long? EventId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuditDescription { get; set; }
        public long? BvnrequestId { get; set; }
        public string Comment { get; set; }
    }
}
