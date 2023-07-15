using Rent_Somethings.Data.Base;
using Rent_Somethings.Data.ViewModels;
using Rent_Somethings.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Rent_Somethings.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CityId = data.CityId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                ProductCategory = data.ProductCategory
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            //Add Product Districts
            foreach (var districtId in data.DistrictIds)
            {
                var newDistrictProduct = new District_Product()
                {
                    ProductId = newProduct.Id,
                    DistrictId = districtId
                };
                await _context.Districts_Products.AddAsync(newDistrictProduct);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(c => c.City)
                .Include(am => am.Districts_Products).ThenInclude(a => a.District)
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;

        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Districts = await _context.Districts.OrderBy(n => n.DistrictName).ToListAsync(),
                Cities = await _context.Cities.OrderBy(n => n.CityName).ToListAsync()
            };
            return response;

        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbProduct != null)
            {

                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.CityId = data.CityId;
                dbProduct.StartDate = data.StartDate;
                dbProduct.EndDate = data.EndDate;
                dbProduct.ProductCategory = data.ProductCategory;

                await _context.SaveChangesAsync();
            }

            //Remove existing District
            var existingDistrictsDb = _context.Districts_Products.Where(n => n.ProductId == data.Id).ToList();
            _context.Districts_Products.RemoveRange(existingDistrictsDb);
            await _context.SaveChangesAsync();

            foreach (var districtId in data.DistrictIds)
            {
                var newDistrictProduct = new District_Product()
                {
                    ProductId = data.Id,
                    DistrictId = districtId
                };
                await _context.Districts_Products.AddAsync(newDistrictProduct);
            }
            await _context.SaveChangesAsync();
        }
    }
}
