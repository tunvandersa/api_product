using BusinessObject.DTO;
using DataAccess.Interfaces;
using Repositories.Interfaces;

namespace Repositories.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IproductDao _productDao;

        public ProductBusiness(IproductDao productDao)
        {
            _productDao = productDao;
        }

        public async Task<bool> CreateAsync(ProductDto model)
        {
            return await _productDao.CreateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productDao.DeleteAsync(id);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _productDao.GetAllAsync();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productDao.GetByIdAsync(id);
            var result = product == null ? null : new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Category = new CategoryDto
                {
                    CategoryId = product.Category!.CategoryId,
                    CategoryName = product.Category!.CategoryName,
                }
            };

            return result;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _productDao.GetCategoiesAsync();
        }

        public async Task<bool> UpdateAsync(ProductDto model)
        {
            return await _productDao.UpdateAsync(model);
        }
    }
}