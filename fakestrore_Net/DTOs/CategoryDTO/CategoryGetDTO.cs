﻿using fakestrore_Net.DTOs.ProductDTO;

namespace fakestrore_Net.DTOs.CategoryDTO
{
    public class CategoryGetDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public List<ProductGetDTO>? Products { get; set; }
    }
}

