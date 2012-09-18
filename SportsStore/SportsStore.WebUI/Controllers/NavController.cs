using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;
        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Menu(string category = null)
        {
            MenuModel MenuModel = new MenuModel();
            MenuModel.selected = category;

            // ViewBag för testet!
            ViewBag.Categorie = category;
            // ViewBag för testet!

            MenuModel.categories = repository.Products.Select(x => x.Category.Name).Distinct().OrderBy(x => x);
            return View(MenuModel);
        }
    }
}
