namespace SportsStore.UI.Controllers
{
    using System.Web.Mvc;

    using Domain.Abstract;
    using System.Linq;
    using Models;
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 3;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                        .Where(p => p.Category == null || p.Category == category)
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                        repository.Products.Count() : 
                        repository.Products.Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }
    }
}