using System.Collections.Generic;

namespace EssentialTools.Models.Substract
{
    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);

        decimal ValueProductsWithDiscount(IEnumerable<Product> products);
    }
}