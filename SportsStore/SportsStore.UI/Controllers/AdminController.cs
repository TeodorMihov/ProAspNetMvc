using System.Web.Mvc;
using System.Linq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entites;
using System.Web;

namespace SportsStore.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository productRepo;

        public AdminController(IProductRepository productRepository)
        {
            this.productRepo = productRepository;
        }

        public ActionResult Index()
        {
            return View(this.productRepo.Products);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        
        public ViewResult Edit(int productId)
        {
            var product = productRepo.Products.FirstOrDefault(x => x.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                productRepo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var deletedProduct = productRepo.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"Product {deletedProduct.Name} was deleted";
            }

            return RedirectToAction("index");
        }
    }
}