
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Models
{


    public class Incentive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rank { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }


        public int BandWith
        {
            get
            {

                return (this.Min.Equals(0) || this.Max.Equals(0)) ? (this.Max - this.Min) : ((this.Max - this.Min) + 1);
            }
        }
        public decimal Reward { get; set; }
    }

    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

    }
}
