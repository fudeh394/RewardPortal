using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Models
{
    public class MailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }       
        public string CC { get; set; }
        public string BC { get; set; }
        public string MailContent { get; set; }
        public byte[] Attachement { get; set; }
        public bool isBodyHtml { get; set; }
    }
}
