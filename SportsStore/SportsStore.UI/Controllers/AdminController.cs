﻿using System.Web.Mvc;
using System.Linq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entites;

namespace SportsStore.UI.Controllers
{
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

        public ViewResult Edit(int productId)
        {
            var product = productRepo.Products.FirstOrDefault(x => x.ProductID == productId);

            return View(product);
        }
    }
}