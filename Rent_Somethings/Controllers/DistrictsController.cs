using Rent_Somethings.Data;
using Rent_Somethings.Data.Services;
using Rent_Somethings.Data.Static;
using Rent_Somethings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Rent_Somethings.Controllers
{
    
    [Authorize(Roles = UserRoles.Admin)]
    public class DistrictsController : Controller
    {
        private readonly IDistrictsService _service;
        public DistrictsController(IDistrictsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data =await _service.GetAllAsync();
            return View(data);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DistrictName,Features")]District district)
        {
            if (!ModelState.IsValid)
            {
                return View(district);
            }
           await _service.AddAsync(district);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var districtDetails = await _service.GetByIdAsync(id);
            if (districtDetails == null) return View("NotFound");
            return View(districtDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var districtDetails = await _service.GetByIdAsync(id);
            if (districtDetails == null) return View("NotFound");
            return View(districtDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("DistrictName,Features")] District district)
        {
            district.Id = id;
            if (!ModelState.IsValid)
            {
                return View(district);
            }
            await _service.UpdateAsync(id, district);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var districtDetails = await _service.GetByIdAsync(id);
            if (districtDetails == null) return View("NotFound");
            return View(districtDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var districtDetails = await _service.GetByIdAsync(id);
            if (districtDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
