
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Models
{
  
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceUrl { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int GroupId { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
