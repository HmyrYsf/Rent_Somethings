using Rent_Somethings.Data.Cart;
using Rent_Somethings.Data.Services;
using Rent_Somethings.Data.Static;
using Rent_Somethings.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rent_Somethings.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IRentalsService _rentalsService;

        public RentalsController(IProductsService productsService, ShoppingCart shoppingCart, IRentalsService rentalsService)
        {
            _productsService = productsService;
            _shoppingCart = shoppingCart;
            _rentalsService = rentalsService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var rentals = await _rentalsService.GetRentalsByUserIdAndRoleAsync(userId, userRole);
            return View(rentals);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteRental()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            string userPhoneNumber = User.FindFirstValue(ClaimTypes.MobilePhone);
           

            await _rentalsService.StoreRentalAsync(items, userId, userEmailAddress, userPhoneNumber);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("RentalCompleted");
        }
    }
}