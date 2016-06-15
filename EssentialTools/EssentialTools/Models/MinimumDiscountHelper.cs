using EssentialTools.Models.Substract;
using System;

namespace EssentialTools.Models
{
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            if (totalParam < 0)
            {
                throw new ArgumentNullException("Price cannot be negative");
            }
            else if(totalParam > 0 && totalParam < 10)
            {
                return totalParam;
            }
            else if(totalParam >= 10 && totalParam <= 100)
            {
                return totalParam - 5;
            }
            else
            {
                return totalParam * 0.9m;
            }
        }
    }
}