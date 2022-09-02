using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UpakModelsLibrary.Models;

namespace UpakDataAccessLibrary.Repository.Interfases
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Task<List<Category>> GetCategoriesAsync();
        public void AddCategory(Category category);
        public Task AddCategoryAsync(Category category);
        public void UpdateCategory(Category category);
        public Task UpdateCategoryAsync(Category category);
        public Category GetCategoryById(int id);
        public Task<Category> GetCategoryByIdAsync(int id);
        public Category GetCategoryWithProductsById(int id);
        public Task<Category> GetCategoryWithProductsByIdAsync(int id);
        public void DeleteCategory(int id);
        public Task DeleteCategoryAsync(int id);
    }
}
