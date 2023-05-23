namespace fakestrore_Net.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //1-n
        public List<Product>? Products { get; set; }


    }
}
