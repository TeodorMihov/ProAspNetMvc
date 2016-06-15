namespace EssentialTools.Models
{
    using Substract;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper discountHelper;
        private static int Counter = 0;

        public LinqValueCalculator(IDiscountHelper discountHelper)
        {
            this.discountHelper = discountHelper;
            Debug.WriteLine(string.Format("Instanse {0} created", Counter++));
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return products.Sum(p => p.Price);
        }

        public decimal ValueProductsWithDiscount(IEnumerable<Product> products)
        {
            return discountHelper.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}