using MonolithApp.Demo.Models;
using SQLite;

namespace MonolithApp.Demo.Services
{
    public class ProductsService
    {
        private readonly SQLiteConnection db =
            new SQLiteConnection("Products.db");

        public ProductsService()
        {
            db.CreateTable<Product>();
        }

        public void AddProduct(Product product)
        {
            db.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            db.Update(product);
        }

        public void DeleteProduct(int Id)
        {
            var product = GetProductById(Id);
            if (product != null)
            {
                db.Delete(product);
            }
        }

        public Product GetProductById(int id){
            TableQuery<Product> result = 
                db.Table<Product>().Where(q => q.Id.Equals(id));
            return result.FirstOrDefault();
        }
    }
}
