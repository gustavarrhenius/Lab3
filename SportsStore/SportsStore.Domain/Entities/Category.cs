using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}