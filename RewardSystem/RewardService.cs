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

        public IList<AgntRewardDetail> GetComputationDetail(int agentEnrolment)
        {
            var result = new List<AgntRewardDetail>();
            var sum = 0m;
            var firstElement = new AgntRewardDetail() {  Count = agentEnrolment,DateRange= string.Format("{0}-{1}", DateTime.Now.AddDays(-7).ToString("dd/MM/yy"), DateTime.Now.ToString("dd/MM/yy")) };
            if (agentEnrolment.Equals(0) || rewardBands.Count <= 0)
                return result;
            var band = GetRewardBand(agentEnrolment, rewardBands);

            var affectedBands = rewardBands.Where(x => x.Rank < band.Rank).ToList();

            int rem = agentEnrolment;
            var currentBand = new RewardBand();
            foreach (var itm in affectedBands)
            {
                currentBand = itm;
                if (itm.Rank.Equals(1))
                {
                    firstElement.Band = string.Format("{0}-{1}", itm.Min, itm.Max);
                    sum = sum + (itm.BandWith * itm.Reward);
                    firstElement.Amount = sum;
                    firstElement.Price = itm.Reward;
                    firstElement.Count = itm.BandWith;
                    firstElement.TotalCount = agentEnrolment.ToString();
                    rem = rem - itm.BandWith;
                    result.Add(firstElement);
                }
                else
                {
                    var newResultItem = new AgntRewardDetail
                    {
                        Price = itm.Reward,
                        TotalCount = string.Empty, 
                        Amount = (itm.BandWith * itm.Reward),
                        Band = string.Format("{0}-{1}", itm.Min, itm.Max),
                        Count = itm.BandWith
                    };
                    sum = sum + (itm.BandWith * itm.Reward);
                    rem = rem - itm.BandWith;
                    result.Add(newResultItem);

                }
               
            }

            if (rem > 0)
            {
                 currentBand = rewardBands.Where(x => x.Rank.Equals(currentBand.Rank+1)).FirstOrDefault<RewardBand>();

                //set the  current Band
                var newResultItem = new AgntRewardDetail
                {
                    Price = currentBand.Reward,
                    Amount = (rem * currentBand.Reward),
                    Band = string.Format("{0}-{1}", currentBand.Min, currentBand.Max),
                    Count = rem,
                    TotalCount = string.Empty
                };

                if(currentBand.Rank.Equals(1))
                {
                    newResultItem.DateRange = string.Format("{0}-{1}", DateTime.Now.AddDays(-7).ToString("dd/MM/yy"), DateTime.Now.ToString("dd/MM/yy"));
                    newResultItem.TotalCount = agentEnrolment.ToString();
                }

                sum = sum + (rem * currentBand.Reward);
                
                result.Add(newResultItem);

                //set the total Band
                
                result.Add(new AgntRewardDetail { Amount = sum , Count =agentEnrolment });
            }
            else
            {

                //set the total Band
                result.Add(new AgntRewardDetail { Amount = sum , Count = agentEnrolment });
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
