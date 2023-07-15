using Rent_Somethings.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Somethings.Data.ViewComponents
{
   
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shopping)
        {
            _shoppingCart = shopping;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
