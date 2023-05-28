using fakestrore_Net.DTOs.ProductDTO;

namespace fakestrore_Net.DTOs.CategoryDTO
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<ProductCreateDTO>? Products { get; set; }
    }
}
