using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UpakDataAccessLibrary.DataContext;
using UpakDataAccessLibrary.Repository.Interfases;

using UpakModelsLibrary.Models;

namespace UpakDataAccessLibrary.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MssqlContext? _context;

        public CategoryRepository(MssqlContext? context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            try
            {
                _context?.Categories?.Add(category);
                _context?.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                await _context!.Categories!.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                Category category = _context?.Categories?.FirstOrDefault(c => c.Id == id)!;
                if (category != null)
                {
                    _context?.Categories?.Remove(category);
                    _context?.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                Category category = _context?.Categories?.FirstOrDefault(c => c.Id == id)!;
                if (category != null)
                {
                    _context?.Categories?.Remove(category);
                    await _context?.SaveChangesAsync()!;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _context!.Categories!.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                return await _context!.Categories!.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                Category category = _context!.Categories!.FirstOrDefault(c => c.Id == id)!;
                if(category != null)
                {
                    return category;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                Category category =await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    return category;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Category GetCategoryWithProductsById(int id)
        {
            try
            {
                Category category = _context!.Categories!.Include(c => c.Products).FirstOrDefault(c => c.Id == id)!;
                if (category != null)
                {
                    return category;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> GetCategoryWithProductsByIdAsync(int id)
        {
            try
            {
                Category category = await _context.Categories.Include(c=>c.Products).FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    return category;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                _context!.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _context!.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
