namespace fakestrore_Net.DTOs
{
    public class CategoryGetDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<ProductGetDTO>? Products { get; set; }
    }
}

