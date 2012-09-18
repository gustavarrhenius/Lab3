using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4; // We will change this later
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = repository.Products.Where(p => category == null || p.Category.Name == category)
                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category.Name == category).Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }

        public PartialViewResult ProductList(string category, int page = 1)
        {
            if (category == "Home")
            {
                category = null;
            }
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                
                Products = repository.Products.Where(p => category == null || p.Category.Name == category)
                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null  ?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category.Name == category).Count()
                },
                CurrentCategory = category
            };

            return PartialView(viewModel);
        }
        


        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}