
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Models
{
    
    
    public class RoleResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int RoleId { get; set; }
        public int ResourceId { get; set; }       
        public bool IsDeleted { get; set; }

    }


  
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GroupName { get; set; }
    }

}
