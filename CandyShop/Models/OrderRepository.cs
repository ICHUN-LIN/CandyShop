using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;
        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart) 
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            foreach (var item in shoppingCartItems) 
            {
                OrderDetail tempdetail = new OrderDetail
                {
                    Amount = item.Amount,
                    Candy = item.Candy,
                    //assigm ID , not object self, maybe we can try
                    CandyId = item.Candy.CandyId,
                    OrderId = order.OrderId,
                    Price = item.Candy.Price
                    //OrderDetailId =
                    //Order = order,

                };
                _appDbContext.OrderDetails.Add(tempdetail);
            }

            _appDbContext.SaveChanges();
        }
    }
}
