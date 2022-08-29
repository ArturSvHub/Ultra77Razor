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
    public class ProductRepository : IProductRepository
    {
        readonly MssqlContext _context;

        public ProductRepository(MssqlContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            try
            {
                _context.Products!.Add(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products!.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void DeleteProduct(int id)
        {
            try
            {
                Product? product = _context.Products!.Find(id);
                if(product != null)
                {
                    _context?.Products!.Remove(product);
                    _context?.SaveChanges();
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

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                Product? product =await _context.Products!.FindAsync(id);
                if (product != null)
                {
                    _context?.Products!.Remove(product);
                    await _context!.SaveChangesAsync();
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

        public Product GetProductById(int id)
        {
            try
            {
                Product? product = _context.Products!.Find(id);
                if (product != null)
                {
                    return product;
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

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                Product? product = await _context.Products!.FindAsync(id);
                if (product != null)
                {
                    return product;
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

        public List<Product> GetProductDetails()
        {
            try
            {
                return _context.Products!.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Product>> GetProductDetailsAsync()
        {
            try
            {
                return await _context.Products!.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product GetProductWithCategoryById(int id)
        {
            try
            {
                Product? product = _context.Products!.Include(c=>c.Category)
                    .FirstOrDefault(p=>p.Id == id);
                if (product != null)
                {
                    return product;
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

        public async Task<Product> GetProductWithCategoryByIdAsync(int id)
        {
            try
            {
                Product? product = await _context.Products!
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    return product;
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

        public void UpdateProduct(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
