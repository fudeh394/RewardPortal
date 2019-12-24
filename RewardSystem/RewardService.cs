using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardSystem
{
    public class RewardService
    {
        List<RewardBand> rewardBands;

        public RewardService(List<RewardBand> _rewardBands)
        {
            rewardBands = _rewardBands;
        }

        public RewardService()
        {
            rewardBands = new List<RewardBand>
            {
                 new RewardBand
                 {
                      Rank =1, Max=30, Min = 1, Reward = 100
                 },
                 new RewardBand
                 {
                      Rank =2, Max=60, Min = 31, Reward = 125
                 },
                 new RewardBand
                 {
                      Rank =3, Max=90, Min = 61, Reward = 150
                 },
                 new RewardBand
                 {
                      Rank =4, Max=120, Min = 91, Reward = 175
                 },
                 new RewardBand
                 {
                      Rank =5, Max=100000000, Min = 121, Reward = 200
                 }
            };
        }


        private RewardBand GetRewardBand (int agentEnrolment,List<RewardBand> rewards)
        {
           
            var result = new RewardBand();

            for(int i=0; i < rewards.Count; i++)
            {
                var currentRewardBand = rewards.Where(x => x.Rank.Equals(i + 1)).FirstOrDefault();
                
                //if it is the last rank then return it.
                if (i.Equals(rewards.Count -1))
                {
                    result = currentRewardBand; break;
                }

                // MIND THE ORDER OF THIS CHECKS AS IT HAS ALOT TO DO WITH THE RESULT
                //if agentEnrolement is greater than current Band check next
                if (agentEnrolment > currentRewardBand.Max)
                    continue;

                var nextRank = new RewardBand();
                nextRank = rewards.Where(x => x.Rank.Equals(currentRewardBand.Rank + 1)).FirstOrDefault();


                if ((currentRewardBand.Max >= agentEnrolment) && (nextRank == null))
                {
                    //has no lowerBand so it is the band
                    result = currentRewardBand;break;
                }

                if ((agentEnrolment <= currentRewardBand.Max ) && ( agentEnrolment < nextRank.Min) )
                {
                    //the lower val of the higher band is more than the enrolment
                    result = currentRewardBand; break;
                }


            }
            return result;
        }

        public decimal GetReward(int agentEnrolment)
        {
            var result = 0M;
            if (agentEnrolment.Equals(0) || rewardBands.Count <=0)
                return result;
            var band = GetRewardBand(agentEnrolment, rewardBands);
            var affectedBands = rewardBands.Where(x => x.Rank < band.Rank).ToList();

            int rem = agentEnrolment;
            foreach(var itm in affectedBands)
            {
                result = result + (itm.BandWith * itm.Reward);
                rem = rem - itm.BandWith;
            }

            if(rem > 0)
            {
                result = result + (rem * band.Reward);
            }



            return result;


        }
    }
}
