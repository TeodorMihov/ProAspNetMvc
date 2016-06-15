namespace SportsStore.UI.Models
{
    using Domain.Entites;
    using System.Collections.Generic;
    
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}