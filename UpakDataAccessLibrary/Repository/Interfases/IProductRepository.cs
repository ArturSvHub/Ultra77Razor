using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UpakModelsLibrary.Models;

namespace UpakDataAccessLibrary.Repository.Interfases
{
    public interface IProductRepository
    {
        public List<Product> GetProductDetails();
        public Task<List<Product>> GetProductDetailsAsync();
        public void AddProduct(Product Product);
        public Task AddProductAsync(Product Product);
        public void UpdateProduct(Product Product);
        public Task UpdateProductAsync(Product Product);
        public Product GetProductById(int id);
        public Task<Product> GetProductByIdAsync(int id);
        public Product GetProductWithCategoryById(int id);
        public Task<Product> GetProductWithCategoryByIdAsync(int id);
        public Product GetProductWithDetailsById(int id);
        public Task<Product> GetProductWithDetailsByIdAsync(int id);
        public void DeleteProduct(int id);
        public Task DeleteProductAsync(int id);
    }
}
