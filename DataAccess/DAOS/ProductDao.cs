using BusinessObject.Models;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using BusinessObject.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOS
{
    public class ProductDao : IproductDao
    {
        private readonly Prn221DbContext _dbContext;
        private readonly ILogger<Product> _logger;

        public ProductDao(Prn221DbContext dbContext, ILogger<Product> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(ProductDto model)
        {
            try
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    UnitPrice = model.UnitPrice,
                    UnitsInStock = model.UnitsInStock,
                };
                await _dbContext.Products.AddAsync(product);
                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            try
            {
                _dbContext.Remove(product);
                var response = await _dbContext.SaveChangesAsync() > 0 ? true : false;
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _dbContext.Products.Include(x => x.Category).ToListAsync();
            var response = products.Select(e => new ProductDto
            {
                ProductId = e.ProductId,
                ProductName = e.ProductName,
                CategoryId = e.CategoryId,
                UnitPrice = e.UnitPrice,
                UnitsInStock = e.UnitsInStock,
                Category = new CategoryDto
                {
                    CategoryId = e.Category!.CategoryId,
                    CategoryName = e.Category!.CategoryName,
                }
            }).ToList();

            return response;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _dbContext.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            return product;
        }

        public async Task<List<CategoryDto>> GetCategoiesAsync()
        {
            var response = await _dbContext.Categories.ToListAsync();

            return response.Select(e =>
            new CategoryDto { CategoryId = e.CategoryId, CategoryName = e.CategoryName }).ToList();
        }

        public async Task<bool> UpdateAsync(ProductDto model)
        {
            var product = new Product
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                CategoryId = model.CategoryId,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
            };

            try
            {
                _dbContext.Update(product);
                var response = await _dbContext.SaveChangesAsync() > 0 ? true : false;
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

   

    }
}
