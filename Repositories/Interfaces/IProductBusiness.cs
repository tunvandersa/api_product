using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTO;
using BusinessObject.Models;

namespace Repositories.Interfaces
{
    public interface IProductBusiness
    {
        Task<bool> CreateAsync(ProductDto model);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<bool> UpdateAsync(ProductDto model);
    }
}
