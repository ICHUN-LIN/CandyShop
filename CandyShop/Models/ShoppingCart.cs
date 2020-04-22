using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    /// <summary>
    /// Cart only exist in Session
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// Db contection interface(like ODBC link)
        /// </summary>
        private readonly AppDbContext _appDbContext;
        /// <summary>
        /// ShoppCart unique ID
        /// </summary>
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            //one session: one Shapping Card ID
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Use service to get service that has registered in service;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            //every request, only Shopping cartId is old, Cart is new, ShoppingCartItems is also new();

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        /// <summary>
        /// Add candy to shoppingcart
        /// </summary>
        /// <param name="candy">candy needed</param>
        /// <param name="amount">number of candy needed</param>
        public void AddToCart(Candy candy, int amount)
        {
            //find item from db
            var shoppingCartItem = _appDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);

            //If first 
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Candy = candy,
                    Amount = amount
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Candy candy)
        {
            //This shows that while item is obtained from DB, it still link to DB. when we change the value of propery of object, we can save difference by DbContext
            var shoppingCartItem = _appDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Candy)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext.ShoppingCartItems.Where(a => a.ShoppingCartId == ShoppingCartId);
            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
            /*
            var cartItems = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
            */
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Candy.Price * c.Amount).Sum();

            return total;
        }
    }
}
