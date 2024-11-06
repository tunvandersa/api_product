using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTO;
using BusinessObject.Models;


namespace DataAccess.Interfaces
{
    public interface IproductDao    
    {
        Task<bool> CreateAsync(ProductDto model);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<List<CategoryDto>> GetCategoiesAsync();
        Task<bool> UpdateAsync (ProductDto model);
    }
}
