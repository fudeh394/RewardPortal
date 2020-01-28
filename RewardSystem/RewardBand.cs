using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardSystem
{

    public class AgntRewardDetail
    {
        
        public string DateRange { get; set; }

      
        public string TotalCount { get; set; }
       
        public string Band { get; set; }

       
        public decimal Amount { get; set; }

        public decimal Price { get; set; }


        public int Count { get; set; }

    }
    public class RewardBand
    {
        public int Rank { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        
        public int BandWith {
            get {
                   
                    return (this.Min.Equals(0) || this.Max.Equals(0)) ? (this.Max - this.Min) :((this.Max - this.Min)+1);
                }
        }
        public decimal Reward { get; set; }
    }
}
