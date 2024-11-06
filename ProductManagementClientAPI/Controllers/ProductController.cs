using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace ProducManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetListAsync()
        {
            var products = await _productBusiness.GetAllAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _productBusiness.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDto model)
        {
            var response = await _productBusiness.CreateAsync(model);

            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _productBusiness.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var response = await _productBusiness.DeleteAsync(id);

            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDto model)
        {
            var product = await _productBusiness.GetByIdAsync(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }
            var response = await _productBusiness.UpdateAsync(model);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productBusiness.GetByIdAsync(id);
            return Ok(product);
        }
    }
}