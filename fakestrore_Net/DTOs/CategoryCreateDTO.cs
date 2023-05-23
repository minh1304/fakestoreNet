namespace fakestrore_Net.DTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<ProductCreateDTO>? Products { get; set; }
    }
}
