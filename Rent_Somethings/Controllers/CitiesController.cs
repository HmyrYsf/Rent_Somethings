using Rent_Somethings.Data;
using Rent_Somethings.Data.Services;
using Rent_Somethings.Data.Static;
using Rent_Somethings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Rent_Somethings.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CitiesController : Controller
    {
        private readonly ICitiesService _service;
        public CitiesController(ICitiesService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCities = await _service.GetAllAsync();
            return View(allCities);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CityName")]City city)
        {
                if (!ModelState.IsValid) return View(city);
                await _service.AddAsync(city);
                return RedirectToAction(nameof(Index));     
            
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,CityName")] City city)
        {
            if (!ModelState.IsValid) return View(city);
            if (id==city.Id)
            {
                await _service.UpdateAsync(id, city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
