using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CandyShop.Models;
using CandyShop.ViewModels;

namespace CandyShop.Controllers
{
    public class CandyController:Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CandyController(ICandyRepository candyRepository, ICategoryRepository categoryRepository)
        {
            //inject service(because it will enject it duringing lifecycle)
            _candyRepository = candyRepository;
            _categoryRepository = categoryRepository;
        }

        //public ViewResult List() : ViewResult is part of IActionResult
        public IActionResult List(string category)
        {
            //ViewBag.CurrentCategory = "BestSeller";
            IEnumerable<Candy> candys = null;
            if (string.IsNullOrEmpty(category))
            {
                candys = _candyRepository.GetAllCandy;
            }
            else 
            {
                candys = _candyRepository.GetAllCandy.Where(a => a.Category.Categoryname == category);
            }

            var candyListViewModel = new CandyListViewModel();
            candyListViewModel.Candies = candys;
            candyListViewModel.CurrentCatecory = "Candy CateGory:"+category;
            return View(candyListViewModel);
        }

        public IActionResult Details(int id) 
        {
            var candy = _candyRepository.GetCandyById(id);

            if (candy == null)
                return NotFound();

            return View(candy);
        }
    
    }
}
