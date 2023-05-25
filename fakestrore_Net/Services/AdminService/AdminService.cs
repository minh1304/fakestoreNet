﻿using fakestrore_Net.Data;
using fakestrore_Net.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;

        public AdminService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<Category>>> AddCategory(CategoryCreateDTO request)
        {

            var newCategory = new Category
            {
                Name = request.Name
            };
            var products = request.Products.Select(p => new Product
            {
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                Image = p.Image,
                Rating = new Rating
                {
                    Rate = p.Rating.Rate,
                    Count = p.Rating.Count
                },
                Category = newCategory
            }).ToList();
            newCategory.Products = products;
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();
        }
    }
}