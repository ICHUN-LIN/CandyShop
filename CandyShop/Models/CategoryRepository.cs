﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="appDbContext"></param>
        public CategoryRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Category> GetAllCategories => _appDbContext.Categories;
    }
}
