
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;

        public int? CategoryId { get; set; }
        
        [Required]
        [Range(1, 100)]
        public decimal? UnitPrice { get; set; }

        [Range(1, 100)]
        public short? UnitsInStock { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
