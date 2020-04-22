using Microsoft.AspNetCore.Mvc;
using CandyShop.Models;
using CandyShop.ViewModels;

namespace CandyShop.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICandyRepository _candyRepository;

        public HomeController(ICandyRepository candyRepository) 
        {
            _candyRepository = candyRepository;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                CandyOnSale = _candyRepository.GetCandyOnSale
            };
            return View(homeViewModel);
        }
    }
}
