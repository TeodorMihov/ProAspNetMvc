﻿namespace SportsStore.Domain.Abstract
{
    using System.Collections.Generic;

    using Entites;

    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
