namespace SportsStore.Domain.Abstract
{
    using System.Collections.Generic;

    using Entites;

    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
