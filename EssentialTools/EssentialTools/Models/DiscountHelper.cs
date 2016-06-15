using System;
using EssentialTools.Models.Substract;

namespace EssentialTools.Models
{
    public class DiscountHelper : IDiscountHelper
    {
        public decimal DiscountSize { get; set; }
        
        public decimal ApplyDiscount(decimal totalPrice)
        {
            return totalPrice - (this.DiscountSize / 100 * totalPrice);
        }
    }
}