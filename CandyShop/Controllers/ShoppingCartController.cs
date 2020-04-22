using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CandyShop.Models;
using CandyShop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandyShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICandyRepository _candyRepository;

        private readonly ShoppingCart _shoppingCart;


        public ShoppingCartController(ICandyRepository candyRepository, ShoppingCart shoppingCart) 
        {
            _candyRepository = candyRepository;
            _shoppingCart = shoppingCart;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int candyId) 
        {
            Candy candy = _candyRepository.GetCandyById(candyId);

            if (candy != null) 
            {
                _shoppingCart.AddToCart(candy, 1);
            }

            return RedirectToAction("Index");
        }


        public RedirectToActionResult RemoveFromCart(int candyId)
        {
            Candy candy = _candyRepository.GetCandyById(candyId);

            if (candy != null)
            {
                _shoppingCart.RemoveFromCart(candy);
            }

            return RedirectToAction("Index");
        }



        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();

            return RedirectToAction("Index");
        }

    }
}
