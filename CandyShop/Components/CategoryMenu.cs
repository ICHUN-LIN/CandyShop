using CandyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        //Component vs Partial_view
        //Component: has logic and Partial_view: no logical in side
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetAllCategories.OrderBy(c => c.Categoryname);

            return View(categories);
        }
    }
}
