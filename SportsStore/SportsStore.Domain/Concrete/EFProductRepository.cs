using System.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }
        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0) {
                context.Products.Add(product);
            } else {
                if (product.CategoryID != product.Category.CategoryID) {
                    product.Category.CategoryID = product.CategoryID;
                }

                context.Entry(product).State = System.Data.EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}