using Rent_Somethings.Data;
using Rent_Somethings.Data.Services;
using Rent_Somethings.Data.Static;
using Rent_Somethings.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent_Somethings.Models;

namespace Rent_Somethings.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        public ProductsController(IProductsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n=>n.City);
            return View(allProducts);

        }

        
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            return View(productDetail);
        }

        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "CityName");
            ViewBag.Districts = new SelectList(productDropdownsData.Districts, "Id", "DistrictName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid) 
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "CityName");
                ViewBag.Districts = new SelectList(productDropdownsData.Districts, "Id", "DistrictName");
                
                return View(product);
            }
            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //Edit
        public async Task<IActionResult> Edit(int id, 
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                StartDate = productDetails.StartDate,
                EndDate = productDetails.EndDate,
                ImageURL = productDetails.ImageURL,
                ProductCategory = productDetails.ProductCategory,
                CityId = productDetails.CityId,
                DistrictIds = productDetails.Districts_Products.Select(n => n.DistrictId).ToList()
            };
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "CityName");
            ViewBag.Districts = new SelectList(productDropdownsData.Districts, "Id", "DistrictName");
            return View(response);
        } 

        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");
            
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "CityName");
                ViewBag.Districts = new SelectList(productDropdownsData.Districts, "Id", "DistrictName");
                return View(product);
            }
            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
 
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync(n => n.City);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allProducts.Where(n =>string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase)|| string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index",filteredResultNew);
            }
            return View("Index",allProducts);
        }

        
    }
}
