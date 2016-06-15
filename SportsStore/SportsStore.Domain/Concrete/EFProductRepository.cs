namespace SportsStore.Domain.Concrete
{
    using Abstract;
    using System.Collections.Generic;
    using Entites;
    
    public class EFProductRepository:IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }
    }
}
