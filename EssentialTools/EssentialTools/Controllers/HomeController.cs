using EssentialTools.Models;
using EssentialTools.Models.Substract;
using Ninject;
using System.Web.Mvc;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator calc;

        public HomeController(IValueCalculator calc, IValueCalculator calculator2)
        {
            this.calc = calc;
        }

        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            decimal totalValueWithDiscount = cart.CalculateProductWithDiscount();
            ViewBag.DiscountsProducts = totalValueWithDiscount;

            return View(totalValue);
        }


        //database in this example
        private Product[] products = {
        new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
        new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
        new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
        new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };
    }
}