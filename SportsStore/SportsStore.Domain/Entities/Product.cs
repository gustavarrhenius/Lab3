using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "You must enter a product name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must enter a product description.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price can not be a negative value")]
        public decimal Price { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}